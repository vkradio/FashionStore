using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using dress.su.Forms;

namespace dress.su
{
    static class Program
    {
        static Mutex _mutex;

#if !DEBUG
        public const string             Configuration       = "Release";
#else
        public const string             Configuration       = "Debug";
#endif
        public static readonly Version  CurrentVersion      = Assembly.GetExecutingAssembly().GetName().Version;
		public static readonly string   AppDataDirectory    = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dress.su");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool singleInstance;
            _mutex = new Mutex(true, Assembly.GetExecutingAssembly().FullName, out singleInstance);
            if (!singleInstance)
            {
                MessageBox.Show("Программа \"" + C_APP_BRAND + "\" уже запущена. Вы нажали, возможно, случайно, запуск дважды.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            Application.Run(new FRM_Main());

            _mutex.Close();
        }

        public const string C_APP_BRAND = "РМ Одежда Розница";

        public static string GetVersionString() { return string.Format("{0} версия {1}", C_APP_BRAND, CurrentVersion.ToString()); }
    }
}
