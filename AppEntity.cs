using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class AppEntity : ICloneable
    {
        public string AppName { get; set; }
        public string ExecutePath { get; set; }
        public TransparentPictureBox PictureBox { get; set; }
        public bool IsActivated { get; private set; }
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
        public AppEntity(string appName, string executePath, Size size)
        {
            AppName = appName;
            ExecutePath = executePath;
            PictureBox = new TransparentPictureBox()
            {
                Name = AppName,
                Image = Icon.ExtractAssociatedIcon(executePath).ToBitmap(),
            };
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SetSize(size);
        }

        public void SetSize(Size size)
        {
            PictureBox.Size = size;
        }
        public void Activate()
        {
            PictureBox.BackColor = Color.PaleGreen;
            PictureBox.BorderStyle = BorderStyle.FixedSingle;
            IsActivated = true;
        }
        public void Deactivate()
        {
            PictureBox.BackColor = Color.Transparent;
            PictureBox.BorderStyle = BorderStyle.None;
            IsActivated = false;
        }

        public object Clone()
        {
            var newApp = new AppEntity(AppName, ExecutePath);
            return newApp;
        }
    }
}
