using System;
using System.Data.SQLite;

namespace sys.SQLiteUtils
{
    [SQLiteFunction(FuncType=FunctionType.Scalar, Name="LIKECI", Arguments=2)]
    public class SQLiteLikeCI: SQLiteFunction
    {
        public override object Invoke(object[] args)
        {
            return ((string)args[0]).StartsWith((string)args[1], StringComparison.CurrentCultureIgnoreCase);
        }
    };
}
