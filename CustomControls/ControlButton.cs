using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class ControlButton : Button
    {
        public int Index { get; }
        public ToolTip Tip { get; }
        public ControlButton(int index) : base() 
        {
            Index = index;
            Tip = new ToolTip();
        }
        public void Init(string text, Color backColor, Config config, Form form, EventHandler action = null, string imagePath = null)
        {
            if (imagePath == null)
            {
                Text = text;
            } else
            {
                Image = Image.FromFile(imagePath);
            }

            Size = new Size(config.ControlButtonSize, config.ControlButtonSize);
            Font = new Font("San Serif", config.ControlFontSize, FontStyle.Bold);
            Location = new Point(form.Width - (Index + 1) * (config.Margin + config.ControlButtonSize), config.Margin);
            BackColor = backColor;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.Black;
            Click += action;

            form.Controls.Add(this);
        }
    }
}
