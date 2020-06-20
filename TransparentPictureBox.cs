using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class TransparentPictureBox : PictureBox
    {
        public TransparentPictureBox() : base() { }
        protected override void OnPaint(PaintEventArgs pe)
        {
            Bitmap bmp = new Bitmap(Image);
            Color backColor = bmp.GetPixel(1, 1);

            bmp.MakeTransparent(backColor);
            base.OnPaint(pe);
        }
    }
}
