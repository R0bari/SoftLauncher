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
            DeleteFormBorders(this);

            InitAppNameTextBox(_appName);
            BoundAddButtonChanger(ChangeAddButtonStatus);
        }
        private void InitAppNameTextBox(TextBoxWithPlaceholder appName)
        {
            appName.Location = new Point(chooseApp.Location.X, 20);
            appName.Size = new Size(clearAppPath.Location.X + clearAppPath.Width - chooseApp.Location.X, appPath.Height);
            appName.Placeholder = "Enter app name";
            appName.Font = new Font("San Serif", 12, FontStyle.Regular);
            Controls.Add(_appName);
        }
        private void BoundAddButtonChanger(EventHandler changeAddButtonStatus)
        {
            _appName.TextChanged += changeAddButtonStatus;
            appPath.TextChanged += changeAddButtonStatus;
        }
        private void ChangeAddButtonStatus(object sender, EventArgs e)
        {
            addButton.Enabled = (_appName.Text.Trim() != _appName.Placeholder && appPath.Text != "")
                ? true
                : false;
        }

        private void DeleteFormBorders(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.ControlBox = false;
            form.Text = "";
        }

        private void ChooseAppPath(object sender, EventArgs e)
        {
            if (openAppPath.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                _appName.Text = GetFilenameFromPath(openAppPath.FileName);
                appPath.Text = openAppPath.FileName;
            }
            catch (WrongFileFormatException ex)
            {
                MessageBox.Show(ex.Message, ex.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Clear(object sender, EventArgs e)
        {
            appPath.Clear();
            _appName.Clear();
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
            appEntity.PictureBox = new TransparentPictureBox
            {
                Image = icon.ToBitmap()
            };
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
            if (!IsCorrectFormat(path))
            {
                throw new WrongFileFormatException();
            }
            Regex regex = new Regex(@"\\((\w|\s)*).exe");
            var match = regex.Match(path);
            return char.ToUpper(match.Groups[1].Value[0]) + match.Groups[1].Value.Substring(1);
        }
        private bool IsCorrectFormat(string path)
        {
            Regex regex = new Regex(@"(\w*).exe");
            var match = regex.Match(path);
            return match != null && match.Groups[1].Value.Trim() != "";
        }
    }
}
