using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using FashionStoreWinForms.Forms;
using FashionStoreWinForms.Properties;

namespace FashionStoreWinForms
{
    static class Program
    {
        static Mutex _mutex;

#if !DEBUG
        public const string Configuration = "Release";
#else
        public const string Configuration = "Debug";
#endif
        public static readonly Version CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;
        public static readonly string AppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dress.su");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");

            _mutex = new Mutex(true, Assembly.GetExecutingAssembly().FullName, out bool singleInstance);
            if (!singleInstance)
            {
                MessageBox.Show(string.Format(Resources.PROGRAM_ALREADY_LAUCHED, Resources.APP_BRAND), Resources.ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            Application.Run(new FRM_Main());

            _mutex.Close();
        }

        //public static string GetVersionString() { return string.Format(Resources.PROGRAM_AND_VERSION, Resources.APP_BRAND, CurrentVersion.ToString()); }
        public static string GetVersionString() => Resources.APP_BRAND;
    }
}
