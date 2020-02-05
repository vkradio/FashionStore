using System;
using System.Data.SQLite;

using DalLegacy.SQLiteUtils;

namespace DalLegacy
{
    public class ConnectionRegistry
    {
        static ConnectionRegistry _instance;

        string _connectionString;

        static ConnectionRegistry GetInstance()
        {
            if (_instance == null)
                _instance = new ConnectionRegistry();
            return _instance;
        }

        ConnectionRegistry()
        {
            SQLiteFunction.RegisterFunction(typeof(SQLiteCaseInsensitiveCollation));
            //SQLiteFunction.RegisterFunction(typeof(SQLiteRegexpFunction));
            SQLiteFunction.RegisterFunction(typeof(SQLiteLikeCI));
        }

        public string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                    throw new InvalidOperationException("Get ConnectionRegistry.ConnectionString, while it is null.");
                    //_connectionString = ConfigService.GlobalInstance().ConnectionString;
                //if (_connectionString == null)
                //{
                //    SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
                //    builder.DataSource = "default.db3";
                //    //builder.InitialCatalog = "anbkdb";
                //    //builder.IntegratedSecurity = true;
                //    _connectionString = builder.ConnectionString;
                //}
                return _connectionString;
            }
            set { _connectionString = value; } }
        public SQLiteConnection OpenNewConnection()
        {
            //SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            //SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection();
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            //connection.ConnectionString = ConnectionString;
            connection.Open();
            using (SQLiteCommand cmd = connection.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "PRAGMA foreign_keys = ON";
                cmd.ExecuteNonQuery();
            }
            return connection;
        }

        public static ConnectionRegistry Instance { get { return GetInstance(); } }
        public static void Init(string in_connectionString)
        {
            Instance.ConnectionString = in_connectionString;
        }

        //public static string DatabaseName { get { return new SqlConnectionStringBuilder(Instance.ConnectionString).InitialCatalog; } }
        //public static Smo.Database SmoDatabase { get { return SmoWrapper.Server.Databases[DatabaseName]; } }
        //public static string UserName
        //{
        //    get
        //    {
        //        SqlConnectionStringBuilder sqlConnStrBuilder = new SqlConnectionStringBuilder(Instance.ConnectionString);
        //        return sqlConnStrBuilder.IntegratedSecurity ?
        //            (string.IsNullOrEmpty(Environment.UserDomainName) ? string.Empty : Environment.UserDomainName + "\\") + Environment.UserName :
        //            sqlConnStrBuilder.UserID;
        //    }
        //}
    };
}
