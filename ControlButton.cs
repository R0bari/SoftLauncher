using System.Drawing;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class ControlButton : Button
    {
        public ControlButton() : base() { }
        public ControlButton(string text = "Control", int width = 25, int height = 25) : base()
        {
            Text = text;
            Width = width;
            Height = height;
            Font = new Font("San Serif", Width / 2 - 2, FontStyle.Regular);
        }
    }
}
