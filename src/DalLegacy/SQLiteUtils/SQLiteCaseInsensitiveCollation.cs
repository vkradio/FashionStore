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
        static readonly CompareInfo s_compareInfo = CultureInfo.CurrentUICulture.CompareInfo;

        /// 
        /// Does case-insensitive comparison using _cultureInfo
        /// 
        /// Left string
        /// Right string
        /// The result of a comparison
        public override int Compare(string x, string y) => s_compareInfo.Compare(x, y, CompareOptions.IgnoreCase);
    };
}
