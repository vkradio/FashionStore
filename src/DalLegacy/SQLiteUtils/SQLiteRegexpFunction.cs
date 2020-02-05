using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace DalLegacy.SQLiteUtils
{
    [SQLiteFunction(FuncType=FunctionType.Scalar, Name="regexp", Arguments=2)]
    public class SQLiteRegexpFunction: SQLiteFunction
    {
        public override object Invoke(object[] args)
        {
            return Regex.IsMatch((string)args[1], (string)args[0]);
        }
    }
}
