/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 15:48:03
** desc：    Logging类
** Ver.:     V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Infrastructure.Common
{
    public class Logging
    {
        private const string FILE_EXTENSION = ".log";

        private static string LogDirectory;

        private static long LogMaxFileSize;

        private static bool IsLogInfo;

        private static bool IsLogWarn;

        private static bool IsLogError;

        private static bool IsLogSystem;

        private static bool IsLogTrace;

        private static bool IsLogApp;

        private static bool IsLogRelease;

        static Logging()
        {
            LogDirectory = string.Empty;
            LogMaxFileSize = 5242880L;
            IsLogInfo = SetLogging("IsLogInfo", true);
            IsLogError = SetLogging("IsLogError", true);
            IsLogWarn = SetLogging("IsLogWarn", true);
            IsLogSystem = SetLogging("IsLogSystem", true);
            IsLogApp = SetLogging("IsLogApp", false);
            IsLogTrace = SetLogging("IsLogTrace", false);
            IsLogRelease = SetLogging("IsLogRelease", true);
            LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!string.IsNullOrEmpty(AppSettingsHelper.GetString("LogDirectory", "")))
            {
                LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppSettingsHelper.GetString("LogDirectory", ""));
            }
            int num = AppSettingsHelper.GetIntValue("LogMaxFileSize");
            if (num > 0)
            {
                LogMaxFileSize = (long)(num * 1024 * 1024);
            }
        }

        private static bool SetLogging(string setting, bool defaultValue)
        {
            if (!string.IsNullOrEmpty(AppSettingsHelper.GetString(setting, "")))
            {
                return AppSettingsHelper.GetBoolValue(setting);
            }
            return defaultValue;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void LogToFile(object log)
        {
            if (log is LoginfoEntity loginfoEntity)
            {
                Log(GetLogInfo(loginfoEntity), LogDirectory, FILE_EXTENSION, loginfoEntity.Type, loginfoEntity.ChildLogDirectory);
            } 
        }

        private static bool IsEnableLogging(LogType type)
        {
            if (!IsLogRelease)
            {
                return false;
            }
            switch (type)
            {
                case LogType.App:
                    return IsLogApp;
                case LogType.Trace:
                    return IsLogTrace;
                case LogType.Info:
                    return IsLogInfo;
                case LogType.Warn:
                    return IsLogWarn;
                case LogType.Error:
                    return IsLogError;
                case LogType.SystemLog:
                    return IsLogSystem;
                default:
                    return false;
            }
        }

        private static void Log(string message, string fileDirectory, string fileExtension, LogType type, string childLogDirectory)
        {
            string text;
            if (type == LogType.SystemLog)
            {
                text = Path.Combine(fileDirectory, type.ToString());
            }
            else
            {
                text = Path.Combine(
                        fileDirectory, 
                        DateTime.Now.ToString("yyyyMM"), 
                        DateTime.Now.Day.ToString(), 
                        string.IsNullOrEmpty(childLogDirectory) ? "" : childLogDirectory,
                        type.ToString()
                        );
            }
            var filePath = text + fileExtension;
            var path = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.AppendAllText(filePath, message + "\r\n", Encoding.UTF8);
            try
            {
                FileInfo fileInfo = new FileInfo(text + fileExtension);
                if (fileInfo.Length >= LogMaxFileSize)
                {
                    string str = string.Empty;
                    int num = 1;
                    do
                    {
                        num++;
                    }
                    while (File.Exists(text + "_" + num.ToString() + fileExtension));
                    str = text + "_" + num.ToString();
                    fileInfo.MoveTo(str + fileExtension);
                }
            }
            catch
            {
            }
        }

        private static string GetLogInfo(LoginfoEntity entity)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"));
            stringBuilder.Append("  ");
            stringBuilder.Append(entity.Message);
            if (entity.Parameters != null && entity.Parameters.Keys.Count > 0)
            {
                foreach (string current in entity.Parameters.Keys)
                {
                    stringBuilder.AppendLine($"{current}:{entity.Parameters[current]}");
                }
            }
            return stringBuilder.ToString();
        }

        public static void LogInfo(string message, LogType type, Dictionary<string, string> parameters = null, string childLogDirectory = "")
        {
            if (!IsEnableLogging(type))
            {
                return;
            }
            LoginfoEntity loginfoEntity = new LoginfoEntity();
            loginfoEntity.Message = message;
            loginfoEntity.Type = type;
            loginfoEntity.ChildLogDirectory = childLogDirectory;
            loginfoEntity.Parameters = parameters;
            ThreadPool.QueueUserWorkItem(new WaitCallback(LogToFile), loginfoEntity);
        }

        public static void LogError(string message, Exception ex, Dictionary<string, string> parameters = null)
        {
            LogError(message, ex, parameters, true, "");
        }

        public static void LogError(string message, Exception ex, Dictionary<string, string> parameters, bool isErrorLog, string childLogDirectory = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            LoginfoEntity loginfoEntity = new LoginfoEntity();
            loginfoEntity.Type = ((isErrorLog && ex != null) ? LogType.Error : LogType.Warn);
            loginfoEntity.ChildLogDirectory = childLogDirectory;
            loginfoEntity.Parameters = parameters;
            stringBuilder.AppendLine(message);
            if (!IsEnableLogging(loginfoEntity.Type))
            {
                return;
            }
            if (ex != null)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    stringBuilder.AppendLine($"错误描述:{ex.Message}");
                }
                if (ex.InnerException != null)
                {
                    stringBuilder.AppendLine($"内部异常:{ex.InnerException}");
                }
                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    stringBuilder.AppendLine($"异常跟踪:{ex.StackTrace}");
                }
            }
            loginfoEntity.Message = stringBuilder.ToString();
            ThreadPool.QueueUserWorkItem(new WaitCallback(LogToFile), loginfoEntity);
        }
    }
}
