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
                    case LogType.Add: logText = "ADD: " + logText + Environment.NewLine; break;
                    case LogType.Edit: logText = "EDIT: " + logText + Environment.NewLine; break;
                    case LogType.Delete: logText = "DELETE: " + logText + Environment.NewLine; break;
                    case LogType.Error: logText = "ERROR: " + logText + Environment.NewLine; break;
                    case LogType.Launch: logText = "LAUNCH: " + logText + Environment.NewLine; break;
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
