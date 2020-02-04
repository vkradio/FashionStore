using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionStoreWinForms.Sys
{
    public struct BackupParams
    {
        public DateTime Date;
        public int Number;

        public static bool TryParse(string in_fileName, out BackupParams out_params)
        {
            out_params = new BackupParams();

            string[] fileNameParts = in_fileName.Split(new char[] { '.' });
            string fileName = fileNameParts[0];

            if (fileName.Length != 10)
                return false;

            int year, month, day, number;
            if (!int.TryParse(fileName.Substring(0, 4), out year))
                return false;
            if (!int.TryParse(fileName.Substring(4, 2), out month) || month < 1 || month > 12)
                return false;
            if (!int.TryParse(fileName.Substring(6, 2), out day) || day < 1 || day > 31)
                return false;
            if (!int.TryParse(fileName.Substring(8, 2), out number) || number < 1)
                return false;

            out_params.Date = new DateTime(year, month, day);
            out_params.Number = number;

            return true;
        }
    };

    static class BackupUtil
    {
        
    };
}
