using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ProtonList.Common
{
    public static class Logger
    {
        /// <summary>
        /// Initializes static members of the <see cref="Logger" /> class
        /// </summary>
        static Logger()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logger));
        }

        /// <summary>
        /// Gets or sets Log property of ILog interface
        /// </summary>
        private static log4net.ILog Log { get; set; }
        //   public static object ConfigurationManager { get; private set; }

        /// <summary>
        /// Logs errors into log file
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="exception">Exception class object</param>
        public static void LogFailErrorMessage(string message, Exception exception)
        {
            StringBuilder exceptionDetails = new StringBuilder();
            exceptionDetails.Append(GetMessageStack(message) + " || ");
            exceptionDetails.Append(GetExceptionDetails(exception));
            //// Log Error Csv File
            Log.ErrorFormat(",{0},{1}", exceptionDetails, GetMessageStack(Convert.ToString(exception, CultureInfo.CurrentCulture)));
        }

        /// <summary>
        /// Method to log Information
        /// </summary>
        /// <param name="message">Information message</param>
        public static void InfoTrace(string message)
        {


            bool debugModeValue = Convert.ToBoolean(Convert.ToString(ConfigurationManager.AppSettings["TraceInfoLogs"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
            if (debugModeValue)
            {
                Log.InfoFormat(",{0}", GetMessageStack(message));
            }
        }

        /// <summary>
        /// Method to get the exception stack.
        /// </summary>
        /// <param name="exceptionMessage">Exception message</param>
        /// <returns>Exception stack</returns>
        public static string GetMessageStack(string exceptionMessage)
        {
            if (!string.IsNullOrEmpty(exceptionMessage))
            {
                exceptionMessage = exceptionMessage.Replace(Environment.NewLine, " ");
                exceptionMessage = exceptionMessage.Replace("\n", " ");
                exceptionMessage = exceptionMessage.Replace(",", ";");
            }

            return exceptionMessage;
        }

        /// <summary>
        /// Method to get the complete exception details 
        /// </summary>
        /// <param name="exception">Object of exception class</param>
        /// <returns>Returns details of exception</returns>
        private static string GetExceptionDetails(Exception exception)
        {
            StringBuilder exceptionDetails = new StringBuilder();
            if (exception != null)
            {
                //// Condition to check exception stack trace
                if (exception.StackTrace != null)
                {
                    exceptionDetails.Append(GetMessageStack(exception.StackTrace) + "||");
                }

                //// Condition to check help link value contains following string
                if (exception.HelpLink != null && exception.HelpLink.Length > 0)
                {
                    exceptionDetails.Append(GetMessageStack(exception.HelpLink) + "||");
                }
                else
                {
                    exceptionDetails.Append(GetMessageStack(exception.Message) + "||");
                }

                exceptionDetails.Append(GetMessageStack(exception.Source) + "||");

                //// Condition to check TargetSite is not null
                if (exception.TargetSite != null)
                {
                    exceptionDetails.Append(GetMessageStack(Convert.ToString(exception.TargetSite, CultureInfo.CurrentCulture)) + "||");
                }

                //// Condition to check InnerException is not null
                while (exception.InnerException != null)
                {
                    exceptionDetails.Append(GetMessageStack(exception.InnerException.Message) + "||" + GetMessageStack(exception.InnerException.StackTrace) + "||");
                    exception = exception.InnerException;
                }
            }

            return exceptionDetails.ToString();
        }
    }
}
