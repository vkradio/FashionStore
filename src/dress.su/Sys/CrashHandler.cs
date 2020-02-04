using System;
using System.IO;
using System.Windows.Forms;

namespace dress.su.Sys
{
    internal static class CrashHandler
	{
        const string c_caption      = "Критическая ошибка";
        const string c_msgCommon1   = "Произошла критическая ошибка. Приложение сейчас будет закрыто. ";
        const string c_msgCommon2   = "Это поможет найти и исправить дефект.";
        const string c_msgFull      = c_msgCommon1 +
                                      "Пожалуйста, отправьте файл, который сейчас увидите, с описанием " +
                                      "ваших действий, после которых это произошло, разработчику программы. " +
                                      c_msgCommon2;
        const string c_msgReduced   = c_msgCommon1 +
                                      "Пожалуйста, опишите ваши действия, после которых это произошло, разработчику программы. " +
                                      c_msgCommon2;

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

                //// Если соединение было разорвано сервером или логин был убит, выводим сообщение об этом.
                //if (ExceptionUtil.IsDisconnectedOrKilledLogin(ex))
                //    MessageBox.Show("Сервер принудительно разорвал соединение, из-за чего продолжать работу невозможно. " +
                //                    "Это может быть связано с регламентными работами на сервере, аварией сети или сервера " +
                //                    "или удалением вашей учетной записи.", "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //// В противном случае выводим общее сообщение о критической ошибке.
                //else
                MessageBox.Show(logSaved ? c_msgFull : c_msgReduced, c_caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
