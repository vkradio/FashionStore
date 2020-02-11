using FashionStoreWinForms.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace FashionStoreWinForms.Sys
{
    internal static class CrashHandler
	{
        readonly static string msgFull = Resources.CRITICAL_ERROR_MSG_COMMON1 + " " + Resources.CRITICAL_ERROR_MSG_SEND_FILE + " " + Resources.CRITICAL_ERROR_MSG_COMMON2;
        readonly static string msgReduced = Resources.CRITICAL_ERROR_MSG_COMMON1 + " " + Resources.CRITICAL_ERROR_MSG_DESCRIBE_ACTIONS + " " + Resources.CRITICAL_ERROR_MSG_COMMON2;

        static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if (e.IsTerminating)
			{
				string crashDir = Path.Combine(Program.AppDataDirectory, "crash");
				Directory.CreateDirectory(crashDir);

				Exception ex = (Exception)e.ExceptionObject;
                bool logSaved = true;
                try
                {
				    CreateCrashLog(crashDir, ex);
                }
                catch
                {
                    logSaved = false;
                }

                MessageBox.Show(logSaved ? msgFull : msgReduced, Resources.CRITICAL_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                //try { Logger.LogException(ex); } catch {}

				System.Diagnostics.Process.Start(crashDir);
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}

		static void CreateCrashLog(string directory, Exception exception)
		{
			StreamWriter writer = null;

			try
			{
				string filePath = Path.Combine(directory, "crash.log");
				writer = new StreamWriter(filePath);

				writer.WriteLine("Version: {0} {1}", Program.GetVersionString(), Program.Configuration);
				writer.WriteLine("OS: {0}", Environment.OSVersion.VersionString);

				writer.WriteLine();
				//writer.WriteLine(ExceptionUtil.FormatException(exception));
                writer.WriteLine(exception.ToString());
                //Exception innerException = exception.InnerException;
                //while (innerException != null)
                //{
                //    writer.WriteLine(ExceptionUtil.FormatException(innerException));
                //    innerException = innerException.InnerException;
                //}

                //writer.WriteLine();
                //writer.WriteLine(exception.StackTrace);
			}
			catch {}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

		public static void CreateGlobalErrorHandler()
		{
#if !DEBUG
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomain_UnhandledException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
#endif
		}
	};
}
