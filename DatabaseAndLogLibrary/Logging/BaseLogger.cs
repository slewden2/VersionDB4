using System;
using System.Runtime.CompilerServices;

namespace DatabaseAndLogLibrary.Logging
{
    public abstract class BaseLogger : IResumeLogger
    {
        /// <inheritdoc />
        public void Error(string message, Exception ex, int indent = 0)
        {
            if (string.IsNullOrWhiteSpace(message) && ex == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                message = "Error detected";
            }

            if (ex != null)
            {
                Fail($"{message} : {ex.Message}", indent);
            }
            else
            {
                Fail(message, indent);
            }
        }

        /// <inheritdoc />
        public void Fail(string message, int indent = 0)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            Log(ELogLevel.Error, indent, message);
        }

        /// <inheritdoc />
        public void Success(string message, int indent = 0)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            Log(ELogLevel.Sucess, indent, message);
        }

        /// <inheritdoc />
        public void Trace(string message, int indent = 0)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            Log(ELogLevel.Trace, indent, message);
        }

        /// <inheritdoc />
        public void Result(ActionResult result, string message, int indent = 0)
        {
            if (result.IsSuccess)
            {
                Success(message + " with success.", indent);
            }
            else
            {
                Fail($"{message} with error: {result.ErrorMessage}", indent);
            }
        }

        /// <inheritdoc />
        public void Result(bool isSuccess, string message, int indent = 0)
        {
            var ar = isSuccess ? ActionResult.Success() : ActionResult.Error("See previous log");
            Result(ar, message, indent);
        }

        /// <summary>
        /// Do the log 
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="indent">indentation of message</param>
        /// <param name="message">Message to log</param>
        protected abstract void Log(ELogLevel level, int indent, string message);

        /// <summary>
        /// List of log level
        /// </summary>
        protected enum ELogLevel
        {
            Error,
            Sucess,
            Trace
        }
    }
}
