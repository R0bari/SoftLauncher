using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftLauncher
{
    public class Logger
    {
        public string Path { get; private set; }
        public Logger(string logFilePath)
        {
            Path = logFilePath;
        }
        public void Log(LogType logType, string logText)
        {
            
            using (var stream = new FileStream(Path, FileMode.Append, FileAccess.Write))
            {
                switch (logType)
                {
                    case LogType.Add: logText = $"[{DateTime.Now}] ADD: {logText}\n"; break;
                    case LogType.Edit: logText = $"[{DateTime.Now}] EDIT: {logText}\n"; break;
                    case LogType.Delete: logText = $"[{DateTime.Now}] DELETE: {logText}\n"; break;
                    case LogType.Error: logText = $"[{DateTime.Now}] ERROR: {logText}\n"; break;
                    case LogType.Launch: logText = $"[{DateTime.Now}] LAUNCH: {logText}\n"; break;
                }
                var buff = Encoding.Default.GetBytes(logText);
                stream.Write(buff, 0, buff.Length);
            }
        }
    }

    public enum LogType
    {
        Add,
        Edit,
        Delete,
        Error,
        Launch
    }
}
