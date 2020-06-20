using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftLauncher.Forms
{
    public partial class CreateAppDialog : Form
    {
        public readonly AppEntity appEntity = new AppEntity();
        private readonly TextBoxWithPlaceholder _appName = new TextBoxWithPlaceholder();

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
            _appName.Location = new Point(chooseIconButton.Location.X, 20);
            _appName.Size = new Size(clearIconButton.Location.X + clearIconButton.Width - chooseIconButton.Location.X, iconPath.Height);
            _appName.Placeholder = "Enter app name";
            _appName.Font = new Font("San Serif", 12, FontStyle.Regular);
            Controls.Add(_appName);
        }
        private void BoundAddButtonChanger()
        {
            _appName.TextChanged += ChangeAddButtonStatus;
            iconPath.TextChanged += ChangeAddButtonStatus;
            appPath.TextChanged += ChangeAddButtonStatus;
        }
        private void ChangeAddButtonStatus(object sender, EventArgs e)
        {
            addButton.Enabled = (_appName.Text.Trim() != "" && iconPath.Text != "" && appPath.Text != "")
                ? true
                : false;
        }

        private void DeleteFormBorders()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            Text = "";
        }

        private void ChooseIcon(object sender, EventArgs e)
        {
            if (openIcon.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            iconPath.Text = openIcon.FileName;
        }
        private void ClearIcon(object sender, EventArgs e)
        {
            iconPath.Text = "";
        }

        private void ChooseAppPath(object sender, EventArgs e)
        {
            if (openIcon.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            appPath.Text = openIcon.FileName;
        }
        private void ClearAppPath(object sender, EventArgs e)
        {
            appPath.Text = "";
        }

        private void ClickForm(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void KeepForm(object sender, MouseEventArgs e)
        {
            _dragFormStatus = true;
            _deltaX = Cursor.Position.X - Location.X;
            _deltaY = Cursor.Position.Y - Location.Y;
        }
        private void UnkeepForm(object sender, MouseEventArgs e) => _dragFormStatus = false;

        private void AddApp(object sender, EventArgs e)
        {
            appEntity.AppName = _appName.Text;
            appEntity.IconPath = iconPath.Text;
            appEntity.ExecutePath = appPath.Text;
        }

        private void DragForm(object sender, MouseEventArgs e)
        {
            if (_dragFormStatus)
            {
                Location = new Point(Cursor.Position.X - _deltaX, Cursor.Position.Y - _deltaY);
            }
        }
    }
}
