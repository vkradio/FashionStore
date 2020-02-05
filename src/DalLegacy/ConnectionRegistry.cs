using System;
using System.Data.SQLite;

using DalLegacy.SQLiteUtils;

namespace DalLegacy
{
    public class ConnectionRegistry
    {
        static ConnectionRegistry s_instance;

        string connectionString;

        ConnectionRegistry()
        {
            SQLiteFunction.RegisterFunction(typeof(SQLiteCaseInsensitiveCollation));
            SQLiteFunction.RegisterFunction(typeof(SQLiteLikeCI));
        }

        public string ConnectionString
        {
            get
            {
                if (connectionString == null)
                    throw new InvalidOperationException("Get ConnectionRegistry.ConnectionString, while it is null.");
                return connectionString;
            }

            set => connectionString = value;
        }

        public SQLiteConnection OpenNewConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "PRAGMA foreign_keys = ON";
                cmd.ExecuteNonQuery();
            }
            return connection;
        }

        public static ConnectionRegistry Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new ConnectionRegistry();
                return s_instance;
            }
        }

        public static void Init(string connectionString) => Instance.ConnectionString = connectionString;
    };
}
