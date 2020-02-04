using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Runtime.Serialization;
using System.Security.Permissions;

using MainLibrary.misc;

namespace MainLibrary.database
{
    /// <summary>
    /// Класс представляет собой унифицированный метод работы с хранимой процедурой.
    /// Для каждой хранимой процедуры нужно создавать новый класс, который наследуется
    /// от данного класса.
    /// </summary>
    public abstract class AbstractStoredProcedure
    {
        private const String ParameterPrefix = "@";
        private readonly String _procedureName;
        protected readonly List<SqlParameter> _parameters = new List<SqlParameter>();
        protected readonly List<SqlParameter> _outputParameters = new List<SqlParameter>();
        protected DataSet _resultSet;
        protected readonly Boolean _needResult;
        protected StoredProcedureState _state = StoredProcedureState.New;
        protected int _commandTimeOut = 30;

        /// <summary>
        /// Создание нового экземпляра вызова хранимой процедуры.
        /// Вызвать процедуру через созданный объект можно только один раз.
        /// Используйте этот конструктор, если хранимая процедура вам нужен результат
        /// исполнения процедуры.
        /// </summary>
        /// <param name="procedureName">имя хранимой процедуры</param>
        protected AbstractStoredProcedure(string procedureName)
            : this(procedureName, true)
        {
        }

        /// <summary>
        /// Создание нового экземпляра вызова хранимой процедуры.
        /// Вызвать процедуру через созданный объект можно только один раз.
        /// </summary>
        /// <param name="procedureName">имя хранимой процедуры</param>
        /// <param name="needResult">нужен ли результат?</param>
        protected AbstractStoredProcedure(string procedureName, bool needResult)
        {
            _needResult = needResult;
            _procedureName = procedureName;
        }

        /// <summary>
        /// Версия конструктора для ХП, которые заведомо имеют 1 параметр.
        /// </summary>
        /// <param name="in_procedureName">Имя ХП</param>
        /// <param name="in_soleParam">Имя параметра</param>
        /// <param name="in_value">Значение параметра</param>
        protected AbstractStoredProcedure( string in_procedureName, string in_soleParam, object in_value ) : this( in_procedureName )
        {
            AddParameter( in_soleParam, in_value );
        }

        /// <summary>
        /// Создает соединение, которое находится в конфигурационном файле, устанавливает время ожидания и выполняет процедуру
        /// </summary>
        /// <param name="timeOut"></param>
        public void Execute(int timeOut)
        {
            _commandTimeOut = timeOut;
            Execute();
        }

        /// <summary>
        /// Версия Execute для случая, когда заведомо ожидается 1 строка результата (или null).
        /// </summary>
        /// <param name="in_stub">Заглушка для перегрузки</param>
        /// <returns>Строка результата, либо null</returns>
        public DataRow Execute(SPExecuteMode in_stub)
        {
            Execute();
            if ( ResultDataTable == null || ResultDataTable.Rows.Count == 0 )
                return null;
            return ResultDataTable.Rows[ 0 ];
        }

        /// <summary>
        /// Создает соединение, которое находится в конфиге и выполняет процедуру
        /// </summary>
        public void Execute()
        
        {
            var connectionString = ConnectionRegistry.Instance.ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var newTransaction = connection.BeginTransaction();
            try
            {
                Execute(connection, newTransaction);
                newTransaction.Commit();
            }
            catch
            {
                newTransaction.Rollback();
                throw;
            }
            finally
            {
                newTransaction.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// Выполнить процедуру в новой транзакции (все другие транзакции в
        /// данном соединении должны быть завершены).
        /// </summary>
        /// <param name="connection">соединение с БД</param>
        public void Execute(SqlConnection connection)
        {
            var newTransaction = connection.BeginTransaction();
            try
            {
                Execute(connection, newTransaction);
                newTransaction.Commit();
            }
            catch
            {
                newTransaction.Rollback();
                throw;
            }
            finally
            {
                newTransaction.Dispose();
            }
        }

        /// <summary>
        /// Выполнить хранимую процедуру с заданными параметрами (в существующей транзакции)
        /// </summary>
        /// <param name="connection">открытое соединение с БД</param>
        /// <param name="transaction">текущая транзакция</param>
        public void Execute(SqlConnection connection, SqlTransaction transaction)
        {
            ValidateState(StoredProcedureState.New);
            _state = StoredProcedureState.Invoked;
            try
            {
                var command = GetPreparedCommand(connection, transaction);
                if (_needResult)
                {
                    var adapter = new SqlDataAdapter(command);
                    _resultSet = new DataSet();
                    adapter.Fill(_resultSet);
                }
                else
                {
                    _resultSet = null;
                    command.ExecuteNonQuery();
                }
                foreach (SqlParameter outputParam in command.Parameters)
                {
                    if (outputParam.Direction == ParameterDirection.Output
                        || outputParam.Direction == ParameterDirection.InputOutput
                        || outputParam.Direction == ParameterDirection.ReturnValue)
                    {
                        _outputParameters.Add(outputParam);
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0 && (ex.Errors[0].Number == 229 || ex.Errors[0].Number == 15151))
                    throw new PermissionDeniedSPException(ex);
                else
                    throw new StoredProcedureException("Ошибка вызова " + _procedureName, ex);
            }
            catch (Exception ex)
            {
                throw new StoredProcedureException("Ошибка вызова " + _procedureName, ex);
            }
        }

        /// <summary>
        /// Выполнить хранимую процедуру с соответствующим параметрами (в существующей транзакции) с возвратом DataReader
        /// </summary>
        /// <param name="connection">открытое соединение с БД</param>
        /// <param name="transaction">текущая транзакция</param>
        /// <returns>Открытый DataReader для чтения данных</returns>
        public SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction)
        {
            ValidateState(StoredProcedureState.New);
            _state = StoredProcedureState.Invoked;
            try
            {
                var command = GetPreparedCommand(connection, transaction);
                SqlDataReader reader;
                if (_needResult)
                {
                    reader = command.ExecuteReader();
                }
                else
                {
                    throw new NotSupportedException(
                        "Процедура вызвана с needResult=false, поэтому создание DataReader не поддерживатся.");
                }
                foreach (SqlParameter outputParam in command.Parameters)
                {
                    if (outputParam.Direction == ParameterDirection.Output
                        || outputParam.Direction == ParameterDirection.InputOutput
                        || outputParam.Direction == ParameterDirection.ReturnValue)
                    {
                        _outputParameters.Add(outputParam);
                    }
                }
                return reader;
            }
            catch (Exception ex)
            {
                throw new StoredProcedureException("Ошибка вывова " + _procedureName, ex);
            }
        }
        
        /// <summary>
        /// Выполнить процедуру в новой транзакции с возвратом DataReader (все другие транзакции в
        /// данном соединении должны быть завершены).
        /// </summary>
        /// <param name="connection">открытое соединение с БД</param>
        /// <returns>Открытый DataReader для чтения данных</returns>
        public SqlDataReader ExecuteReader(SqlConnection connection)
        {
            var newTransaction = connection.BeginTransaction();
            try
            {
                var reader = ExecuteReader(connection, newTransaction);
                newTransaction.Commit();
                return reader;
            }
            catch
            {
                newTransaction.Rollback();
                throw;
            }
            finally
            {
                newTransaction.Dispose();
            }
        }

        /// <summary>
        /// Создает соединение, которое находится в конфиге и выполняет процедуру с возвратом DataReader 
        /// </summary>
        /// <returns>Открытый DataReader для чтения данных</returns>
        public SqlDataReader ExecuteReader()
        {
            var connectionString = ConnectionRegistry.Instance.ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var newTransaction = connection.BeginTransaction();
            try
            {
                var reader = ExecuteReader(connection, newTransaction);
                newTransaction.Commit();
                return reader;
            }
            catch
            {
                newTransaction.Rollback();
                throw;
            }
            finally
            {
                newTransaction.Dispose();
            }
        }

        /// <summary>
        /// Получить комманду для исполнения с заполненными параметрами
        /// </summary>
        /// <param name="connection">соединение с БД</param>
        /// <param name="transaction">транзакция</param>
        /// <returns></returns>
        private SqlCommand GetPreparedCommand(SqlConnection connection, SqlTransaction transaction)
        {
            var command = new SqlCommand(_procedureName, connection, transaction);
            command.Parameters.AddRange(_parameters.ToArray());
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = _commandTimeOut;
            return command;
        }

        /// <summary>
        /// Показвает, получили ли мы результат
        /// </summary>
        public Boolean HasResult
        {
            get
            {
                ValidateState(StoredProcedureState.Invoked);
                return _resultSet != null;
            }
        }

        /// <summary>
        /// Позволяет получить DataSet, возвращённый хранимой процедурой
        /// </summary>
        public DataSet ResultDataSet
        {
            get
            {
                ValidateState(StoredProcedureState.Invoked);
                return _resultSet;
            }
        }

        /// <summary>
        /// Позволяет получить первую DataTable (если имеется) в возвращённом DataSet
        /// </summary>
        public DataTable ResultDataTable
        {
            get
            {
                ValidateState(StoredProcedureState.Invoked);
                return _resultSet != null && _resultSet.Tables.Count != 0 ? _resultSet.Tables[0] : null;
            }
        }

        /// <summary>
        /// Время ожидания перед завершением попытки выполнить команду и генерацией ошибки. По умолчанию 30 секунд
        /// </summary>
        public int CommandTimeOut
        {
            get
            {
                return _commandTimeOut;
            }
            set
            {
                _commandTimeOut = value;
            }
        }


        /// <summary>
        /// Проверка состояния объекта на предмет требуемого состояния.
        /// Если состояние не соответствует требуемому, то возникает
        /// иключительная ситуация
        /// </summary>
        /// <param name="needState">требуемое состояние</param>
        private void ValidateState(StoredProcedureState needState)
        {
            if (_state != needState)
            {
                if (needState == StoredProcedureState.New)
                {
                    throw new InvalidOperationException("Процедура уже была вызвана");
                }
                if (needState == StoredProcedureState.Invoked)
                {
                    throw new InvalidOperationException("Процедура ещё не была вызвана");
                }
            }
        }

        /// <summary>
        /// Добавляем входной параметр
        /// </summary>
        /// <param name="name">имя</param>
        /// <param name="value">значение</param>
        public void AddParameter(String name, Object value)
        {
            AddParameter(name, value, ParameterDirection.Input);
        }

        /// <summary>
        /// Добавляем параметр с указанием направления и типа данных
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="value">Значение</param>
        /// <param name="direction">Направление</param>
        /// <param name="dataType">Тип данных</param>
        public void AddParameter(String name, Object value, ParameterDirection direction, SqlDbType dataType)
        {
            ValidateState(StoredProcedureState.New);
            String key = name;
            if (!key.StartsWith(ParameterPrefix))
            {
                key = ParameterPrefix + key;
            }

            SqlParameter newParameter;
            if (value == null || value.ToString().Equals("NULL") || value == DBNull.Value)
            {
                newParameter = new SqlParameter(key, DBNull.Value);
            }
            else if (value is DateTime)
            {
                if ((DateTime)value < SqlDateTime.MinValue)
                    value = SqlDateTime.MinValue;
                else if ((DateTime)value > SqlDateTime.MaxValue)
                    value = SqlDateTime.MaxValue;
                newParameter = new SqlParameter(key, SqlDbType.DateTime) { Value = new SqlDateTime((DateTime)value) };
            }
            else
            {
                newParameter = new SqlParameter(key, dataType)
                                   {
                                       Value = value,
                                       Direction = direction
                                   };
            }
            _parameters.Add(newParameter);
        }

        /// <summary>
        /// Добавляем параметр с указанием направления
        /// </summary>
        /// <param name="name">имя</param>
        /// <param name="value">значение</param>
        /// <param name="direction">направление</param>
        public void AddParameter(String name, Object value, ParameterDirection direction)
        {
            ValidateState(StoredProcedureState.New);
            String key = name;
            if (!key.StartsWith(ParameterPrefix))
            {
                key = ParameterPrefix + key;
            }

            SqlParameter newParameter;
            if (value == null || value.ToString().Equals("NULL") || value == DBNull.Value)
            {
                newParameter = new SqlParameter(key, DBNull.Value);
            }
            else if (value is DateTime)
            {
                if ((DateTime)value < SqlDateTime.MinValue)
                    value = SqlDateTime.MinValue;
                else if ((DateTime)value > SqlDateTime.MaxValue)
                    value = SqlDateTime.MaxValue;
                newParameter = new SqlParameter(key, SqlDbType.DateTime) { Value = new SqlDateTime((DateTime)value) };
            }
            else if (value is int)
            {
                newParameter = new SqlParameter(key, SqlDbType.Int) { Value = value };
            }
            else if (value is short)
            {
                newParameter = new SqlParameter(key, SqlDbType.SmallInt) { Value = value };
            }
            else if (value is bool)
            {
                newParameter = new SqlParameter(key, SqlDbType.Bit) { Value = value };

            }
            else if (value is Guid)
            {
                newParameter = new SqlParameter(key, SqlDbType.UniqueIdentifier)
                {
                    Value = (Guid)value
                };
            }
            else if (value is decimal?)
            {
                newParameter = new SqlParameter(key, SqlDbType.Real)
                {
                    Value = (decimal)value
                };                
            }
            else if (value is short?)
            {
                newParameter = new SqlParameter(key, SqlDbType.SmallInt)
                {
                    Value = (short)value
                };
            }
            else if (value is DataTable)
            {
                newParameter = new SqlParameter(key, value);
            }
            else
            {
                newParameter = new SqlParameter(key, SqlDbType.VarChar);
                if (value.ToString().Length > 0) newParameter.Size = value.ToString().Length;
                else newParameter.Size = 4096;
                newParameter.Value = value.ToString();
            }
            newParameter.Direction = direction;
            _parameters.Add(newParameter);
        }

        /// <summary>
        /// Получаем выходной параметр по имени
        /// </summary>
        /// <param name="name">имя</param>
        /// <returns>значение или null</returns>
        public Object GetOutputParameter(String name)
        {
            String key = name;
            if (!key.StartsWith(ParameterPrefix))
            {
                key = ParameterPrefix + key;
            };
            ValidateState(StoredProcedureState.Invoked);
            foreach (var param in _outputParameters)
            {
                if (param.ParameterName.Equals(key))
                {
                    return param.Value;
                }
            }
            return null;
        }

        public bool OutputParameterExists(String name)
        {
            String key = name;
            if (!key.StartsWith(ParameterPrefix))
            {
                key = ParameterPrefix + key;
            };
            ValidateState(StoredProcedureState.Invoked);
            foreach (var param in _outputParameters)
            {
                if (param.ParameterName.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        public int GetOutputParameterCount()
        {
            return _outputParameters.Count;
        }

        /// <summary>
        /// Возможные состояния хранимой процедуры
        /// </summary>
        protected enum StoredProcedureState
        {
            /// <summary>
            /// Процедура не была вызвана
            /// </summary>
            New,
            /// <summary>
            /// Процедура была вызвана
            /// </summary>
            Invoked
        }
    }

    /// <summary>
    /// Класс представляет собой иключительную ситуацию, которая появляется
    /// в результате ошибки вызова хранимой процедуры
    /// </summary>
    [Serializable]
    public class StoredProcedureException : Exception, ISerializable
    {
        #region Инициализация

        /// <summary>
        /// Создание нового экземпляра исплючительной ситуации
        /// </summary>
        /// <param name="message">текст ошибки</param>
        /// <param name="innerException">дочерняя ошибка</param>
        public StoredProcedureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion

        #region Поддержка сериализации

        protected StoredProcedureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        #endregion
    }

    /// <summary>
    /// Класс представляет собой иключительную ситуацию, которая появляется
    /// в результате попытки вызова хранимой процедуры без наличия у пользователя прав на выполнение
    /// </summary>
    [Serializable]
    public class PermissionDeniedSPException : Exception
    {

        #region Инициализация

        public PermissionDeniedSPException(Exception innerException)
            : base("Процедура не может быть выполнена, так нет прав на ее выполнение", innerException)
        {
        }

        #endregion

        #region Поддержка сериализации

        protected PermissionDeniedSPException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        #endregion
    }

    public enum SPExecuteMode { stub };
}