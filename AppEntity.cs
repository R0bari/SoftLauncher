using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class AppEntity : ICloneable
    {
        private readonly Logger logger = new Logger("log.txt");
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
                logger.Log(LogType.Launch, AppName);
            }
            catch
            {
                var message = $"Failed to start application {AppName}";
                MessageBox.Show(message, "Launch app error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                logger.Log(LogType.Error, message);
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

        public object Clone() => new AppEntity(AppName, ExecutePath);
    }
}
