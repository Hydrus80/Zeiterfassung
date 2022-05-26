using System.Diagnostics;

namespace TimeCore.ErrorHandler
{
    public static class ErrorHandlerLog
    {
        static ErrorHandlerLog()
        {
            // Create the source, if it does not already exist.
            if (!EventLog.SourceExists("TimeCore"))
                EventLog.CreateEventSource("TimeCore", "ErrorHandler");
        }

        public static void WriteError(string selErrorMsg)
        {
            EventLog myLog = new EventLog();
            myLog.Source = "TimeCore";
            myLog.WriteEntry(selErrorMsg);
        }
    }
}
