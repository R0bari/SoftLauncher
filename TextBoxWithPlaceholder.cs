using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class TextBoxWithPlaceholder : TextBox
    {
        private string _placeholder;
        public string Placeholder {
            get => _placeholder;
            set => _placeholder = Text = value;
        }
        public TextBoxWithPlaceholder() : base() {
            GotFocus += RemovePlaceholder;
            LostFocus += AddPlaceholder;
        }
        public TextBoxWithPlaceholder(Point location, Size size, string placeholder, Font font) : base()
        {
            Multiline = true;
            Location = location;
            Size = size;
            Placeholder = placeholder;
            Text = Placeholder;
            Font = font;

            GotFocus += RemovePlaceholder;
            LostFocus += AddPlaceholder;
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            if (Text.Trim().Length == 0)
            {
                Text = Placeholder;
            }
        }
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (Text.Trim().Equals(Placeholder))
            {
                Text = "";
            }
        }
        public new void Clear() => Text = Placeholder;
    }
}
