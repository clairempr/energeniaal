using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Energeniaal
{
    static class Program
    {
        public static Boolean TestMode { get; set; }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern short GetUserDefaultUILanguage();

        const short EnglishUS = 1033;
        private static short defaultUIlanguage;

        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException +=
              new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // Determine whether or not application should run in test mode
            SetTestMode();

            defaultUIlanguage = GetUserDefaultUILanguage();
            SetApplicationLanguage();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void CurrentDomain_UnhandledException
          (object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMessage = ErrorHelper.GetFormattedErrorMessage(ex, "An unhandled error has occurred.");             
                MessageBox.Show(errorMessage, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Application.Exit();
            }
        }

        private static void SetTestMode()
        {
            // If debugging in Visual Studio, then enable test mode functionality
            if (System.Diagnostics.Debugger.IsAttached)
            {
                TestMode = true;
            }
            else
            {
                TestMode = false;
            }
        }
        
        // change language on all open forms
        static internal void ReLocalizeAll() 
        {
            SetApplicationLanguage();
            (new ReLocalizeWaitDialog()).ShowDialog(); 
        }

        static void SetApplicationLanguage()
        {
            switch (Energeniaal.Properties.Settings.Default.Language)
            {
                case "en":
                    if (defaultUIlanguage == EnglishUS)
                    {
                        SetCulture("en-US");
                    }
                    else
                    {
                        SetCulture("en-GB");
                    }
                    break;
                case "??":
                    // if language not yet selected, let it choose automatically based on Windows settings
                    break;
                default:
                   SetCulture(Energeniaal.Properties.Settings.Default.Language);
                   break;
            }
        }

        static void SetCulture(string language)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(language);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            }
            catch
            {
                MessageBox.Show("Invalid language/culture: " + language);
                SetCulture("en-GB");
            }
        }
    }

    public interface IReLocalizable {
        void ReLocalize(); 
    }
}
