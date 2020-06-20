using System.Drawing;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class ControlButton : Button
    {
        public int Index { get; }
        public ControlButton(int index) : base() 
        {
            Index = index;
        }
        public void Init(string text, Color backColor, FormConfig config, Form form)
        {
            Text = text;
            Size = new Size(config.ControlButtonSize, config.ControlButtonSize);
            Font = new Font("San Serif", config.ControlFontSize, FontStyle.Bold);
            Location = new Point(form.Width - (Index + 1) * (config.Margin + config.ControlButtonSize), config.Margin);
            BackColor = backColor;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.Black;

            form.Controls.Add(this);
        }
    }
}
