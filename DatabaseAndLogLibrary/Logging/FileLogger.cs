using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace DatabaseAndLogLibrary.Logging
{
    /// <summary>
    /// Object pour logguer dans un fichier
    /// </summary>
    public class FileLogger : BaseLogger
    {
        private readonly Encoding encoding;
        private readonly string applicationName;

        private string fullfileName;


        /// <summary>
        /// Initialise une nouvelleinstance de la classe <see cref="FileLogger"/>
        /// </summary>
        public FileLogger()
        {
            encoding = Encoding.Default;
            applicationName = Assembly.GetEntryAssembly().GetName().Name;
            fullfileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).FullName, $"Log {applicationName}.log");
        }

        /// <summary>
        /// Obtient le nom de fichier de log
        /// </summary>
        public string FileName
        {
            get => fullfileName;
            set => fullfileName = value;
        }

        protected override void Log(ELogLevel level, int indent, string message)
        {
            lock (this)
            {
                var sepi = new string(' ', indent * 3);
                string txt = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fffff} [{Thread.CurrentThread.ManagedThreadId:00}] {Convert(level)} : {sepi}{message}";

                using var file = new StreamWriter(FileName, true, encoding);
                file.WriteLine(txt, 0, txt.Length);
                file.Close();
            }
        }
                
        private string Convert(ELogLevel level)
            => level switch
            {
                ELogLevel.Error => "Error ",
                ELogLevel.Sucess => "Succes",
                ELogLevel.Trace => "Trace ",
                _ => "Info  "
            };
    }
}
