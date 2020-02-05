using System.Data.SQLite;
using System.Globalization;

namespace DalLegacy.SQLiteUtils
{
    [SQLiteFunction(FuncType = FunctionType.Collation, Name = "UTF8CI")]
    public class SQLiteCaseInsensitiveCollation : SQLiteFunction
    {
        /// 
        /// CultureInfo for comparing strings in case insensitive manner
        /// 
        private static readonly CultureInfo _cultureInfo = CultureInfo.CreateSpecificCulture("ru-RU");

        /// 
        /// Does case-insensitive comparison using _cultureInfo
        /// 
        /// Left string
        /// Right string
        /// The result of a comparison
        public override int Compare(string x, string y)
        {
            return string.Compare(x, y, _cultureInfo, CompareOptions.IgnoreCase);
        }
    };
}
