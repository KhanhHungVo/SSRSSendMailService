using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSRSSendMailService.Logger
{
    public class LogUtils
    {
        public static string LogPath = string.Empty;
        public LogUtils(SystemType type)
        {
            if (type == SystemType.SystemWeb)
            {
                LogPath = HttpContext.Current.Server.MapPath("~/");
            }
            else if (type == SystemType.SystemWin)
            {
                LogPath = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            }
        }
        public void Log(Exception ex)
        {
            EventLog log = new EventLog
            {
                Source = "TOPICA/BizService"
            };
            log.WriteEntry(string.Concat(new object[] { ex.Message, Environment.NewLine,
                                                        ex.Source, Environment.NewLine,
                                                        ex.StackTrace,
                                                        ex.TargetSite,
                                                        ex.InnerException }), EventLogEntryType.Error, 100);
            log.Close();
        }

        public void LogExceptionToFile(Exception ex)
        {
            string logDirectory = LogPath + @"\" + DateTime.Today.ToString("yyyyMMdd");
            string filePath = string.Empty;
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                filePath = logDirectory + @"\EXCEPTION.0.log";
            }
            else
            {
                string[] filePaths = Directory.GetFiles(logDirectory, "*.log");
                if (filePaths.Length == 0)
                {
                    filePath = logDirectory + @"\EXCEPTION.0.log";
                }
                else
                {
                    List<string> fileList = new List<string>();
                    foreach (string fPath in filePaths)
                    {
                        FileInfo file = new FileInfo(fPath);
                        string[] parts = file.Name.Split('.');
                        if (parts[0].ToUpper() == LogFileType.EXCEPTION.ToString())
                        {
                            fileList.Add(fPath);
                        }
                    }

                    int lastestIndex = int.MinValue;
                    string lastestFilePath = "";
                    if (fileList.Count <= 0)
                    {
                        filePath = logDirectory + @"\EXCEPTION.0.log";
                    }
                    else
                    {
                        foreach (string fPath in fileList)
                        {
                            FileInfo file = new FileInfo(fPath);
                            string[] parts = file.Name.Split('.'); //fPath.Split('.');
                            if (Convert.ToInt32(parts[1]) >= lastestIndex)
                            {
                                lastestIndex = Convert.ToInt32(parts[1]);
                                lastestFilePath = fPath;
                            }
                        }
                        filePath = lastestFilePath;
                    }

                    if (File.Exists(filePath))
                    {
                        FileInfo lastestFile = new FileInfo(filePath);
                        // check if file size be larger than 5MB then create new one
                        if (((lastestFile.Length / 1024f) / 1024f) > 5)
                        {
                            lastestIndex++;
                            filePath = logDirectory + @"\" + LogFileType.EXCEPTION + "." + lastestIndex + ".log";
                        }
                    }
                }
            }

            string logMessage = string.Concat(new object[] { ex.Message, Environment.NewLine,
                                                        ex.Source, Environment.NewLine,
                                                        ex.StackTrace,
                                                        ex.TargetSite,
                                                        ex.InnerException });
            logMessage = DateTime.Now.ToString("HH:mm:ss") + " " + logMessage;
            TextWriterTraceListener listener = new TextWriterTraceListener(filePath);
            listener.WriteLine(logMessage);
            listener.Flush();
            listener.Close();
        }

        public void LogToFile(LogFileType logType, string logMessage)
        {
            string logDirectory = LogPath + @"\" + DateTime.Today.ToString("yyyyMMdd");
            string filePath = string.Empty;
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                switch (logType)
                {
                    case LogFileType.TRACE:
                        filePath = logDirectory + @"\TRACE.0.log";
                        break;
                    case LogFileType.MESSAGE:
                        filePath = logDirectory + @"\MESSAGE.0.log";
                        break;
                    case LogFileType.EXCEPTION:
                        filePath = logDirectory + @"\EXCEPTION.0.log";
                        break;
                    default:
                        filePath = logDirectory + @"\TRACE.0.log";
                        break;
                }
            }
            else
            {
                string[] filePaths = Directory.GetFiles(logDirectory, "*.log");
                if (filePaths.Length == 0)
                {
                    switch (logType)
                    {
                        case LogFileType.TRACE:
                            filePath = logDirectory + @"\TRACE.0.log";
                            break;
                        case LogFileType.MESSAGE:
                            filePath = logDirectory + @"\MESSAGE.0.log";
                            break;
                        case LogFileType.EXCEPTION:
                            filePath = logDirectory + @"\EXCEPTION.0.log";
                            break;
                        default:
                            filePath = logDirectory + @"\TRACE.0.log";
                            break;
                    }
                }
                else
                {
                    List<string> fileList = new List<string>();
                    foreach (string fPath in filePaths)
                    {
                        FileInfo file = new FileInfo(fPath);
                        string[] parts = file.Name.Split('.');
                        if (parts[0].ToUpper() == logType.ToString().ToUpper())
                        {
                            fileList.Add(fPath);
                        }
                    }

                    int lastestIndex = int.MinValue;
                    string lastestFilePath = "";
                    if (fileList.Count <= 0)
                    {
                        filePath = logDirectory + @"\" + logType.ToString().ToUpper() + ".0.log";
                    }
                    else
                    {
                        foreach (string fPath in fileList)
                        {
                            FileInfo file = new FileInfo(fPath);
                            string[] parts = file.Name.Split('.'); //fPath.Split('.');
                            if (Convert.ToInt32(parts[1]) >= lastestIndex)
                            {
                                lastestIndex = Convert.ToInt32(parts[1]);
                                lastestFilePath = fPath;
                            }
                        }

                        filePath = lastestFilePath;
                    }

                    if (File.Exists(filePath))
                    {
                        FileInfo lastestFile = new FileInfo(filePath);
                        // check if file size be larger than 5MB then create new one
                        if (((lastestFile.Length / 1024f) / 1024f) > 5)
                        {
                            lastestIndex++;
                            filePath = logDirectory + @"\" + logType.ToString().ToUpper() + "." + lastestIndex + ".log";
                        }
                    }
                }
            }

            logMessage = DateTime.Now.ToString("HH:mm:ss") + " " + logMessage;
            TextWriterTraceListener listener = new TextWriterTraceListener(filePath);
            listener.WriteLine(logMessage);
            listener.Flush();
            listener.Close();
        }
    }
}
