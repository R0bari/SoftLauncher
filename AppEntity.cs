using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class AppEntity
    {
        public string AppName { get; set; }
        public string DeactivatedIconPath { get; set; }
        public string ActivatedIconPath { get; set; }
        public string ExecutePath { get; set; }
        public PictureBox PictureBox { get; set; }

        public AppEntity(string appName, string deactivatedIconPath, string activatedIconPath, string executePath, PictureBox image = null)
        {
            AppName = appName;
            DeactivatedIconPath = deactivatedIconPath;
            ActivatedIconPath = activatedIconPath;
            ExecutePath = executePath;
            PictureBox = image;
        }

        public void Activate()
        {
            PictureBox.ImageLocation = ActivatedIconPath;
        }
        public void Deactivate()
        {
            PictureBox.ImageLocation = DeactivatedIconPath;
        }
        public bool IsActivated()
        {
            return PictureBox.ImageLocation == ActivatedIconPath;
        }
    }
}
