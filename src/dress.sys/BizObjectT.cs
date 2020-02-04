using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

using MainLibrary.misc;

namespace MainLibrary.database.orm
{
    public class CustomSqlCommand
    {
        protected string                _sql;
        protected DataTable             _resultDataTable    = new DataTable();
        protected List<SQLiteParameter> _parameters         = new List<SQLiteParameter>();

        public CustomSqlCommand(string in_sql) { _sql = in_sql; }

        public virtual DataRow Execute()
        {
            using (SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = _sql;
                    cmd.CommandType = CommandType.Text;

                    foreach (SQLiteParameter param in _parameters)
                        cmd.Parameters.AddRange(_parameters.ToArray());

                    using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                        a.Fill(_resultDataTable);

                    return _resultDataTable.Rows.Count != 0 ? _resultDataTable.Rows[0] : null;
                }
            }
        }
        public DataTable ResultDataTable { get { return _resultDataTable; } }

        public void AddParameter(string in_paramName, long? in_value)
        {
            SQLiteParameter par = in_value.HasValue ?
                new SQLiteParameter(in_paramName, in_value) :
                new SQLiteParameter(in_paramName, DBNull.Value);
            _parameters.Add(par);
        }
        public void AddParameter(string in_paramName, string in_value)
        {
            SQLiteParameter par = in_value != null ?
                new SQLiteParameter(in_paramName, in_value) :
                new SQLiteParameter(in_paramName, DBNull.Value);
            _parameters.Add(par);
        }
    };

    public abstract class BizObject<T> : BizObject2, IComparable<T> where T : BizObject<T>, new()
    {
        protected class GetObjectSql: CustomSqlCommand
        {
            public GetObjectSql(int in_id) : base(ProcNameRead)
            {
                AddParameter("@in_id", in_id);
            }
            public GetObjectSql(string in_name) : base(ProcNameRead)
            {
                _sql = _sql.Replace("@in_id", "@in_name").Replace("where id = ", "where name = ");
                AddParameter("@in_name", in_name);
            }
        };
        protected class AddObjectSql: CustomSqlCommand
        {
            public int NewId { get; private set; }

            public AddObjectSql() : base(ProcNameAdd)
            {
                //AddParameter("@in_id", in_object.Id);
            }

            public override DataRow Execute()
            {
                using (SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = _sql;
                        cmd.CommandType = CommandType.Text;

                        foreach (SQLiteParameter param in _parameters)
                            cmd.Parameters.AddRange(_parameters.ToArray());

                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "select last_insert_rowid()";
                        cmd.Parameters.Clear();
                        NewId = (int)(long)cmd.ExecuteScalar();

                        return null;
                    }
                }
            }
        };
        protected class UpdateObjectSql: CustomSqlCommand
        {
            public UpdateObjectSql(T in_object) : base(ProcNameUpdate)
            {
                AddParameter("@in_id", in_object.Id);
            }

            public override DataRow Execute()
            {
                using (SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = _sql;
                        cmd.CommandType = CommandType.Text;

                        foreach (SQLiteParameter param in _parameters)
                            cmd.Parameters.AddRange(_parameters.ToArray());

                        cmd.ExecuteNonQuery();
                        return null;
                    }
                }
            }
        };
        protected class DeleteObjectSql: CustomSqlCommand
        {
            public DeleteObjectSql(T in_object) : base(ProcNameDelete)
            {
                AddParameter("@in_id", in_object.Id);
            }

            public override DataRow Execute()
            {
                using (SQLiteConnection conn = ConnectionRegistry.Instance.OpenNewConnection())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = _sql;
                        cmd.CommandType = CommandType.Text;

                        foreach (SQLiteParameter param in _parameters)
                            cmd.Parameters.AddRange(_parameters.ToArray());

                        cmd.ExecuteNonQuery();
                        return null;
                    }
                }
            }
        };

        protected bool _isNew;

        protected static List<T> _cache = new List<T>();

        protected static string ProcNameRead
        {
            get
            {
                SPNamesAttribute a = Attribute.GetCustomAttribute(typeof(T), typeof(SPNamesAttribute)) as SPNamesAttribute;
                if (a == null)
                    throw new NotImplementedException(string.Format("Класс {0} не содержит обязательного атрибута SPNamesAttribute.", typeof(T).Name));
                if ( string.IsNullOrEmpty( a.Read ) )
                    throw new NotImplementedException(string.Format("Класс {0} не содержит обязательного значения атрибута SPNames.Read.", typeof(T).Name));
                return a.Read;
            }
        }
        protected static string ProcNameDelete
        {
            get
            {
                SPNamesAttribute a = Attribute.GetCustomAttribute(typeof(T), typeof(SPNamesAttribute)) as SPNamesAttribute;
                if (a == null)
                    throw new NotImplementedException(string.Format("Класс {0} не содержит обязательного атрибута SPNamesAttribute.", typeof(T).Name));
                if ( string.IsNullOrEmpty( a.Delete ) )
                    throw new NotImplementedException(string.Format("Класс {0} не содержит обязательного значения атрибута SPNames.Delete.", typeof(T).Name));
                return a.Delete;
            }
        }
        protected static string ProcNameAdd
        {
            get
            {
                SPNamesAttribute a = Attribute.GetCustomAttribute(typeof(T), typeof(SPNamesAttribute)) as SPNamesAttribute;
                if (a == null)
                    throw new NotImplementedException(string.Format("Класс {0} не содержит обязательного атрибута SPNamesAttribute.", typeof(T).Name));
                if (string.IsNullOrEmpty(a.Add))
                    throw new NotImplementedException(string.Format("Класс {0} не содержит обязательного значения атрибута SPNames.Add.", typeof(T).Name));
                return a.Add;
            }
        }
        protected static string ProcNameUpdate
        {
            get
            {
                SPNamesAttribute a = Attribute.GetCustomAttribute(typeof(T), typeof(SPNamesAttribute)) as SPNamesAttribute;
                if (a == null)
                    throw new NotImplementedException(string.Format("Класс {0} не содержит обязательного атрибута SPNamesAttribute.", typeof(T).Name));
                if (string.IsNullOrEmpty(a.Update))
                    throw new NotImplementedException(string.Format("Класс {0} не содержит обязательного значения атрибута SPNames.Update.", typeof(T).Name));
                return a.Update;
            }
        }
        protected static string DeleteIdParamName { get { return ((SPNamesAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SPNamesAttribute))).DeleteParamName; } }

        public static T Restore(int? in_id)
        {
            if (!in_id.HasValue)
                return null;
            foreach (T o in _cache)
                if (o.Id == in_id.Value)
                    return o;
            DataRow r = new GetObjectSql(in_id.Value).Execute();
            if (r == null)
                return null;
            T res = new T();
            _cache.Add(res);
            res.FillProps(r);
            res.SetStorageConsistency();
            return res;
        }
        public static T Restore(string in_name)
        {
            if (string.IsNullOrEmpty(in_name))
                return null;
            string name = in_name.ToUpper();
            foreach (T o in _cache)
                if (o.Name.ToUpper() == name)
                    return o;
            DataRow r = new GetObjectSql(in_name).Execute();
            if (r == null)
                return null;
            T res = new T();
            _cache.Add(res);
            res.FillProps(r);
            res.SetStorageConsistency();
            return res;
        }
        public virtual void Flush()
        {
            Debug.Assert(Validate() == null, "Debug.Assert(Validate() == null)");

            _isNew = Id == 0;

            CustomSqlCommand sp = _isNew ?
                (CustomSqlCommand)new AddObjectSql() :
                (CustomSqlCommand)new UpdateObjectSql((T)this);

            FillUpdateParams(sp);

            sp.Execute();

            if (_isNew)
                ((T)this).Id = ((AddObjectSql)sp).NewId;

            SetStorageConsistency();

            if (_isNew)
                _cache.Add((T)this);
            _isNew = false;
        }

        /*public virtual string Delete(DataRow in_row)
        {
            if (in_row == null)
                return string.Empty;
            string err = Delete(Restore((int?)in_row["Id"]));
            if (err != null)
                return err;
            in_row.Delete();
            in_row.AcceptChanges();
            return null;
        }*/
        public static string Delete(T in_object)
        {
            if (in_object == null)
                return null;
            DeleteObjectSql sp = new DeleteObjectSql(in_object);
            sp.Execute();
            //DataTable t = sp.ResultDataTable;
            //if (t == null)
            //{
                _cache.Remove(in_object);
                return null;
            //}
            //return t.Rows[0].Field<string>(0);
            //return null;
        }

        //public virtual ResultState DeleteRecord(DataRow in_newRow)
        //{
        //    try
        //    {
        //        string err = Delete(in_newRow);
        //        return err == null ? ResultState.ResultSuccess : new ResultState(false, err);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogException(ex);
        //        return new ResultState(false, string.Format("Ошибка в программе: [{0}] {1}", ex.GetType().Name, ex.Message));
        //    }
        //}
        //public virtual object PolymorphHole(object in_params) { return null; }

        protected virtual void FillUpdateParams(CustomSqlCommand in_sp)
        {
            if (!NoUseOfName)
                in_sp.AddParameter("@in_name", Name);
        }

        public static void ResetCache() { _cache = new List<T>(); }

        public int CompareTo(T in_other) { return Name.CompareTo(in_other.Name); }

        public bool IsNew { get { return Id == 0 || _isNew; } }
    };
}
