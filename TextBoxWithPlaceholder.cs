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
            GotFocus += RemoveText;
            LostFocus += AddText;
        }
        public TextBoxWithPlaceholder(Point location, Size size, string placeholder, Font font) : base()
        {
            Multiline = true;
            Location = location;
            Size = size;
            Placeholder = placeholder;
            Text = Placeholder;
            Font = font;

            GotFocus += RemoveText;
            LostFocus += AddText;
        }

        private void AddText(object sender, EventArgs e)
        {
            if (Text.Trim().Length == 0)
            {
                Text = Placeholder;
            }
        }
        private void RemoveText(object sender, EventArgs e)
        {
            if (Text.Trim().Equals(Placeholder))
            {
                Text = "";
            }
        }
    }
}
