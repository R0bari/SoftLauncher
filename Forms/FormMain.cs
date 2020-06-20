using SoftLauncher.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SoftLauncher
{
    public partial class FormMain : Form
    {
        private readonly Config _formConfig = new Config(
               margin: 20,
               iconSize: 50,
               controlButtonSize: 25,
               rowCapacity: 3);
        private readonly List<AppEntity> _apps = new List<AppEntity>()
        {
            new AppEntity(
                appName: "Skype",
                executePath: @"C:\Program Files (x86)\Microsoft\Skype for Desktop\Skype.exe"),
            new AppEntity(
                appName: "Discord",
                executePath: @"C:\Users\Eugene\AppData\Local\Discord\app-0.0.305\Discord.exe"),
            new AppEntity(
                appName: "Visual Studio",
                executePath: @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe"),
            new AppEntity(
                appName: "Visual Studio Code",
                executePath: @"C:\Users\Eugene\AppData\Local\Programs\Microsoft VS Code\Code.exe"),
            new AppEntity(
                appName: "Telegram",
                executePath: @"E:\Telegram Desktop\Telegram.exe")
        };
        private readonly ControlButton _quitButton = new ControlButton(index: 0);
        private readonly ControlButton _hideButton = new ControlButton(index: 1);
        private readonly ControlButton _addButton = new ControlButton(index: 2);
        private readonly Button _launchButton = new Button();

        private bool _dragFormStatus;
        private int _deltaX, _deltaY;

        public FormMain()
        {
            InitializeComponent();
            DeleteFormBorders(this);

            InitAppImages(_apps);
            InitFormSize(this);
            InitLaunchButton(_launchButton);
            InitControlButtons(
                quitButton: _quitButton, 
                addButton: _addButton, 
                hideButton: _hideButton);

            BoundClickHandlers(switchApp: SwitchApp, updateLaunchButtonStatus: UpdateLaunchButtonStatus);
            ActivateAllApps(_apps);
            UpdateLaunchButtonText(_launchButton);
        }

        private void DeleteFormBorders(FormMain form)
        {
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.ControlBox = false;
            form.Text = "";
        }

        private void InitAppImages(List<AppEntity> apps)
        {
            for (int i = 0; i < apps.Count; ++i)
            {
                apps[i].PictureBox.Name = _apps[i].AppName;
                apps[i].PictureBox.Location = new Point(
                    _formConfig.Margin + i % _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin),
                    _formConfig.Margin * 2 + _formConfig.ControlButtonSize + i / _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin));
                apps[i].PictureBox.Image = Icon.ExtractAssociatedIcon(_apps[i].ExecutePath).ToBitmap();
                apps[i].SetSize(new Size(_formConfig.IconSize, _formConfig.IconSize));
                apps[i].PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Controls.Add(_apps[i].PictureBox);
            }
        }

        private void InitFormSize(FormMain form)
        {
            form.Width = _formConfig.Margin + (_formConfig.Margin + _formConfig.IconSize) * _formConfig.RowCapacity;
            form.Height = _formConfig.Margin * 2 + _formConfig.ControlButtonSize + ((_formConfig.IconSize + _formConfig.Margin) * ((_apps.Count - 1) / _formConfig.RowCapacity + 1)) +
                _formConfig.IconSize + _formConfig.Margin;
        }

        private void InitLaunchButton(Button launchButton)
        {
            InitLaunchButtonProps(launchButton);
            launchButton.Click += LaunchApps;
            Controls.Add(launchButton);
        }
        private void InitLaunchButtonProps(Button launchButton)
        {
            launchButton.Size = new Size(Width - 2 * _formConfig.Margin, _formConfig.IconSize);
            launchButton.Location = new Point(_formConfig.Margin, Height - _formConfig.Margin - _formConfig.IconSize);
            launchButton.BackColor = Color.PaleGreen;
            launchButton.ForeColor = Color.Black;
            launchButton.FlatStyle = FlatStyle.Flat;
            launchButton.Font = new Font("San Serif", _formConfig.ControlFontSize, FontStyle.Regular);
            UpdateLaunchButtonText(launchButton);
        }
        private void UpdateLaunchButtonStatus(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _launchButton.Enabled = false;
                foreach (var app in _apps)
                {
                    if (app.IsActivated)
                    {
                        _launchButton.Enabled = true;
                        break;
                    }
                }
                UpdateLaunchButtonText(_launchButton);
            }
        }
        private void UpdateLaunchButtonText(Button launchButton) => launchButton.Text = launchButton.Enabled
                ? "Launch (" + CountActivatedAppIcons() + ")"
                : "Launch";

        private void InitControlButtons(ControlButton quitButton, ControlButton hideButton, ControlButton addButton)
        {
            quitButton.Init("X", Color.IndianRed, _formConfig, this, ExitApp);
            hideButton.Init("-", Color.LightGray, _formConfig, this, HideApp);
            addButton.Init("+", Color.PaleGreen, _formConfig, this, AddApp);
        }

        private void BoundClickHandlers(MouseEventHandler switchApp, MouseEventHandler updateLaunchButtonStatus)
        {
            foreach (var app in _apps)
            {
                app.PictureBox.MouseClick += switchApp;
                app.PictureBox.MouseClick += updateLaunchButtonStatus;
            }
        }

        private int CountActivatedAppIcons() => _apps.Where(app => app.IsActivated).Count();
        private void ActivateAllApps(List<AppEntity> apps) => apps.ForEach(app => app.Activate());
        private void DeactivateAllApps(List<AppEntity> apps) => apps.ForEach(app => app.Deactivate());

        private void SwitchApp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var picture = (sender as PictureBox);
                var currentApp = _apps.Find(app => app.PictureBox == picture);
                if (currentApp.IsActivated)
                {
                    currentApp.Deactivate();
                }
                else
                {
                    currentApp.Activate();
                }
            }
        }


        private void LaunchApps(object sender, EventArgs e)
        {
            foreach (var app in _apps)
            {
                if (app.IsActivated)
                {
                    Process.Start(app.ExecutePath);
                }
            }
            DeactivateAllApps(_apps);
        }

        private void LoadForm(object sender, EventArgs e) => RunWithAdminRight();
        private void RunWithAdminRight()
        {
            if (HasAdminRight())
            {
                return;
            }

            var programWithAdminRight = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = Application.ExecutablePath
            };
            try
            {
                Process.Start(programWithAdminRight);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Приложению необходимы права администратора!");
            }
            Application.Exit();
        }
        private bool HasAdminRight()
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return pricipal.IsInRole(WindowsBuiltInRole.Administrator);
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

        private void ExitApp(object sender, EventArgs e) => Application.Exit();
        private void HideApp(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void AddApp(object sender, EventArgs e)
        {
            CreateAppDialog createAppDialog = new CreateAppDialog();
            if (createAppDialog.ShowDialog() == DialogResult.OK)
            {
                var newApp = createAppDialog.appEntity.Clone();
                _apps.Add(newApp as AppEntity);
                Controls.Add((newApp as AppEntity).PictureBox);
                InitIconConfig(newApp as AppEntity);
                InitFormSize(this);
                InitLaunchButtonProps(_launchButton);
            }
        }
        private void InitIconConfig(AppEntity app)
        {
            var index = _apps.FindIndex(a => a == app);
            _apps[index].PictureBox.Location = new Point(
                    _formConfig.Margin + index % _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin),
                    _formConfig.Margin * 2 + _formConfig.ControlButtonSize + index / _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin));
            _apps[index].PictureBox.Width = _formConfig.IconSize;
            _apps[index].PictureBox.Height = _formConfig.IconSize;
            _apps[index].SetSize(new Size(_formConfig.IconSize, _formConfig.IconSize));
            _apps[index].PictureBox.MouseClick += SwitchApp;
            _apps[index].PictureBox.MouseClick += UpdateLaunchButtonStatus;
        }
    }
}
