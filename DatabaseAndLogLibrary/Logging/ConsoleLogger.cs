using System;
using System.Threading;


namespace DatabaseAndLogLibrary.Logging
{
    /// <summary>
    /// Objet pour loguer du texte coloré sur la console
    /// </summary>
    public class ConsoleLogger : BaseLogger
    {
        protected override void Log(ELogLevel level, int indent, string message)
        {
            lock (this)
            {
                var sepi = new string(' ', indent * 3);
                var thread = $"[{Thread.CurrentThread.ManagedThreadId:00}]";

                ConsoleColor clr = Console.ForegroundColor;
                Console.ForegroundColor = Convert(level);
                Console.WriteLine(thread + sepi + message);
                Console.ForegroundColor = clr;
            }
        }

        private ConsoleColor Convert(ELogLevel level)
            => level switch
            { 
                ELogLevel.Error => ConsoleColor.Red,
                ELogLevel.Sucess => ConsoleColor.Green,
                ELogLevel.Trace => ConsoleColor.DarkGray,
                _ => ConsoleColor.White
            };
    }
}
