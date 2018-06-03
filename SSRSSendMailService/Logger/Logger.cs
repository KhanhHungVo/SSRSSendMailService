using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSSendMailService.Logger
{
    public class Logger
    {
        public static void Log(Exception ex)
        {
            new LogUtils(SystemType.SystemWin).Log(ex);
        }
        public static void LogExceptionToFile(Exception ex)
        {
            new LogUtils(SystemType.SystemWin).LogExceptionToFile(ex);
        }
        public static void LogToFile(LogFileType logType, string logMessage)
        {
            new LogUtils(SystemType.SystemWin).LogToFile(logType, logMessage);
        }

    }
}
