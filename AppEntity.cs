using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class AppEntity : ICloneable
    {
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
        public AppEntity(AppEntityJson appJson)
        {
            AppName = appJson.AppName;
            ExecutePath = appJson.ExecutePath;
            IsSelected = appJson.IsSelected;
            PictureBox = new TransparentPictureBox()
            {
                Name = AppName,
                Image = Icon.ExtractAssociatedIcon(ExecutePath).ToBitmap(),
            };
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SetSize(PictureBox.Image.Size);
        }

        public void SetSize(Size size)
        {
            PictureBox.Size = size;
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
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
