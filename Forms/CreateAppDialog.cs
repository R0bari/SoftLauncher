using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftLauncher.Forms
{
    public partial class CreateAppDialog : Form
    {
        public readonly AppEntity appEntity = new AppEntity();
        private readonly TextBoxWithPlaceholder _appName = new TextBoxWithPlaceholder();
        private Icon icon;

        private bool _dragFormStatus;
        private int _deltaX, _deltaY;

        public CreateAppDialog()
        {
            InitializeComponent();
            DeleteFormBorders();

            InitializeAppNameTextBox();
            BoundAddButtonChanger();
        }
        private void InitializeAppNameTextBox()
        {
            _appName.Location = new Point(chooseApp.Location.X, 20);
            _appName.Size = new Size(clearAppPath.Location.X + clearAppPath.Width - chooseApp.Location.X, appPath.Height);
            _appName.Placeholder = "Enter app name";
            _appName.Font = new Font("San Serif", 12, FontStyle.Regular);
            Controls.Add(_appName);
        }
        private void BoundAddButtonChanger()
        {
            _appName.TextChanged += ChangeAddButtonStatus;
            appPath.TextChanged += ChangeAddButtonStatus;
        }
        private void ChangeAddButtonStatus(object sender, EventArgs e)
        {
            addButton.Enabled = (_appName.Text.Trim() != "" && appPath.Text != "")
                ? true
                : false;
        }

        private void DeleteFormBorders()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            Text = "";
        }

        private void ChooseAppPath(object sender, EventArgs e)
        {
            if (openAppPath.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            appPath.Text = openAppPath.FileName;
            _appName.Text = GetFilenameFromPath(openAppPath.FileName);
        }
        private void ClearAppPath(object sender, EventArgs e)
        {
            appPath.Text = "";
        }

        private void ClickForm(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        private void AddApp(object sender, EventArgs e)
        {
            icon = Icon.ExtractAssociatedIcon(openAppPath.FileName);
            appEntity.AppName = _appName.Text;
            appEntity.ExecutePath = appPath.Text;
            appEntity.PictureBox = new TransparentPictureBox();
            appEntity.PictureBox.Image = icon.ToBitmap();
        }

        private void KeepForm(object sender, MouseEventArgs e)
        {
            _dragFormStatus = true;
            _deltaX = Cursor.Position.X - Location.X;
            _deltaY = Cursor.Position.Y - Location.Y;
        }
        private void UnkeepForm(object sender, MouseEventArgs e) => _dragFormStatus = false;
        private void DragForm(object sender, MouseEventArgs e)
        {
            if (_dragFormStatus)
            {
                Location = new Point(Cursor.Position.X - _deltaX, Cursor.Position.Y - _deltaY);
            }
        }

        private string GetFilenameFromPath(string path)
        {
            Regex regex = new Regex(@"(\w*).exe");
            var match = regex.Match(path);
            return char.ToUpper(match.Groups[1].Value[0]) + match.Groups[1].Value.Substring(1);
        }
    }
}
