using System;
using System.Collections.Generic;

namespace DatabaseAndLogLibrary.Logging
{
    public class MultipleLogger : IResumeLogger
    {
        public List<IResumeLogger> loggers;

        public MultipleLogger()
        {
            loggers = new List<IResumeLogger>();
        }

        public MultipleLogger Add(IResumeLogger logger)
        {
            if (logger != null)
            {
                loggers.Add(logger);
            }

            return this;
        }

        public void Error(string message, Exception ex, int indent = 0)
        {
            foreach(var t in loggers)
            {
                t.Error(message, ex, indent);
            }
        }

        public void Fail(string message, int indent = 0)
        {
            foreach (var t in loggers)
            {
                t.Fail(message, indent);
            }
        }

        public void Result(ActionResult result, string message, int indent = 0)
        {
            foreach (var t in loggers)
            {
                t.Result(result, message, indent);
            }
        }

        public void Result(bool isSuccess, string message, int indent = 0)
        {
            foreach (var t in loggers)
            {
                t.Result(isSuccess, message, indent);
            }
        }

        public void Success(string message, int indent = 0)
        {
            foreach (var t in loggers)
            {
                t.Success(message, indent);
            }
        }

        public void Trace(string message, int indent = 0)
        {
            foreach (var t in loggers)
            {
                t.Trace(message, indent);
            }
        }
    }
}
