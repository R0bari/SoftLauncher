using SoftLauncher.Exceptions;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class AppEntity : ICloneable
    {
        public Logger Logger { get; private set; }
        public bool IsLoggingActive { get; private set; } = false;
        public string AppName { get; set; }
        public string ExecutePath { get; set; }
        public TransparentPictureBox PictureBox { get; set; }
        public bool IsSelected { get; private set; }
        public AppEntity() { }
        public AppEntity(string appName, string executePath)
        {
            AppName = appName;
            ExecutePath = executePath;
            PictureBox = new TransparentPictureBox()
            {
                Name = AppName,
                Image = Icon.ExtractAssociatedIcon(executePath).ToBitmap(),
            };
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SetSize(PictureBox.Image.Size);
        }
        public AppEntity(AppEntityJson appJson)
        {
            AppName = appJson.AppName;
            ExecutePath = appJson.ExecutePath;
            IsLoggingActive = appJson.IsLoggingActive;
            Logger = new Logger(appJson.LoggerPath);
            PictureBox = new TransparentPictureBox()
            {
                Name = AppName,
                Image = Icon.ExtractAssociatedIcon(ExecutePath).ToBitmap(),
            };
            if (appJson.IsSelected)
            {
                Select();
            }
            else
            {
                Unselect();
            }
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SetSize(PictureBox.Image.Size);
        }

        public void SetSize(Size size)
        {
            PictureBox.Size = size;
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void Launch()
        {
            try
            {
                Process.Start(ExecutePath);
                if (IsLoggingActive)
                {
                    Logger.Log(LogType.Launch, AppName);
                }
            }
            catch (LaunchAppException ex)
            {
                MessageBox.Show(ex.Message, ex.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (IsLoggingActive)
                {
                    Logger.Log(LogType.Error, ex.Message);
                }
            }
        }
        public void Switch()
        {
            if (IsSelected)
            {
                Unselect();
            } else
            {
                Select();
            }
        }
        public void Select()
        {
            PictureBox.BackColor = Color.PaleGreen;
            PictureBox.BorderStyle = BorderStyle.FixedSingle;
            IsSelected = true;
        }
        public void Unselect()
        {
            PictureBox.BackColor = Color.Transparent;
            PictureBox.BorderStyle = BorderStyle.None;
            IsSelected = false;
        }
        
        public void AddLogging(Logger logger)
        {
            Logger = logger;
            IsLoggingActive = true;
        }

        public object Clone() => new AppEntity(AppName, ExecutePath);
    }
}
