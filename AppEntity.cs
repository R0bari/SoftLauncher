using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class AppEntity
    {
        public string AppName { get; set; }
        public string IconPath { get; set; }
        public string ExecutePath { get; set; }
        public TransparentPictureBox PictureBox { get; set; }
        public bool IsActivated { get; private set; }
        public AppEntity() { }
        public AppEntity(string appName, string iconPath, string executePath)
        {
            AppName = appName;
            IconPath = iconPath;
            ExecutePath = executePath;
            PictureBox = new TransparentPictureBox()
            {
                Name = AppName,
                ImageLocation = IconPath,
                Image = Image.FromFile(IconPath)
            };
            var bmp = new Bitmap(PictureBox.Image);
            bmp.MakeTransparent();
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
    }
}
