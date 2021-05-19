using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VersionDB4
{
    static class Program
    {
        public static VersionDBSettings Settings { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings = new VersionDBSettings();
            Settings.Load();

            try
            {
                using var frm = new FVersionDB();
                Settings.PositionLoad(frm);
                //using var frm = new FTest();
                Application.Run(frm);
                Settings.PositionSave(frm);
            }
            finally
            {
                Settings.Save();
            }
        }
    }
}
