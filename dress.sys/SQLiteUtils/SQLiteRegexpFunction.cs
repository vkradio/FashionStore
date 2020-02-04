using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace sys.SQLiteUtils
{
    [SQLiteFunction(FuncType=FunctionType.Scalar, Name="regexp", Arguments=2)]
    public class SQLiteRegexpFunction: SQLiteFunction
    {
        public override object Invoke(object[] args)
        {
            return Regex.IsMatch((string)args[1], (string)args[0]);
        }
    };

    /*
     * На случай, если это не заработает, в оригинале было так:
     * 
[SQLiteFunction(Name = "REGEXP", Arguments = 2, FuncType = FunctionType.Scalar)]
class MyRegEx : SQLiteFunction
{
  public override object Invoke(object[] args)
  {
    return System.Text.RegularExpressions.Regex.IsMatch(Convert.ToString(args[1]), Convert.ToString(args[0]));
  }
}

And an example SQL statement:  SELECT * FROM Foo WHERE Foo.Name REGEXP '$bar'
     */
}
