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
    public partial class Form1 : Form
    {
        private readonly List<AppEntity> _apps = new List<AppEntity>()
        {
            new AppEntity(
                appName: "Skype",
                deactivatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\skypeIcon.png",
                activatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\skypeIconActivated.png",
                executePath: @"C:\Program Files (x86)\Microsoft\Skype for Desktop\Skype.exe"),
            new AppEntity(
                appName: "Discord",
                deactivatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\discordIcon.png",
                activatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\discordIconActivated.png",
                executePath: @"C:\Users\Eugene\AppData\Local\Discord\app-0.0.305\Discord.exe"),
            new AppEntity(
                appName: "Visual Studio",
                deactivatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\vsIcon.png",
                activatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\vsIconActivated.png",
                executePath: @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe"),
            new AppEntity(
                appName: "Visual Studio Code",
                deactivatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\vsCodeIcon.png",
                activatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\vsCodeIconActivated.png",
                executePath: @"C:\Users\Eugene\AppData\Local\Programs\Microsoft VS Code\Code.exe"),
            new AppEntity(
                appName: "Telegram",
                deactivatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\telegramIcon.png",
                activatedIconPath: @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\telegramIconActivated.png",
                executePath: @"E:\Telegram Desktop\Telegram.exe")
        };
        private readonly FormConfig _formConfig = new FormConfig(
            margin: 20,
            appIconSize: 75,
            controlButtonSize: 25,
            rowCapacity: 4);

        private bool _dragFormStatus;
        private int _deltaX, _deltaY;

        public Form1()
        {
            InitializeComponent();
            DeleteFormBorders();

            InitializeAppImages();
            InitializeFormSize();
            InitializeLaunchButton();
            InitializeControlButtons();

            BoundClickHandlers();
            ActivateAllApps();
            UpdateLaunchButtonText();
        }

        private void DeleteFormBorders()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            Text = "";
        }
        private void InitializeAppImages()
        {
            for (int i = 0; i < _apps.Count; ++i)
            {
                _apps[i].PictureBox = new PictureBox
                {
                    Name = _apps[i].AppName,
                    Location = new Point(
                        _formConfig.Margin + i % _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin), 
                        _formConfig.Margin + i / _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin)),
                    Width = _formConfig.IconSize,
                    Height = _formConfig.IconSize,
                    SizeMode = PictureBoxSizeMode.Normal
                };
                Controls.Add(_apps[i].PictureBox);
            }
        }
        private void InitializeFormSize()
        {
            Width = _formConfig.Margin + ((_formConfig.IconSize + _formConfig.Margin) * _formConfig.RowCapacity);
            Height = (_formConfig.Margin * 2) + _formConfig.IconSize + (_formConfig.IconSize + _formConfig.Margin) * (_apps.Count / _formConfig.RowCapacity + 1);
        }
        private void InitializeLaunchButton()
        {
            launchButton.Size = new Size(Width - 2 * _formConfig.Margin - _formConfig.ControlButtonSize - _formConfig.Margin, _formConfig.IconSize);
            launchButton.Location = new Point(_formConfig.Margin, Height - _formConfig.Margin - launchButton.Height);
            UpdateLaunchButtonText();
        }
        private void InitializeControlButtons()
        {
            quitButton.Size = new Size(_formConfig.ControlButtonSize, _formConfig.ControlButtonSize);
            quitButton.Location = new Point(Width - _formConfig.Margin - quitButton.Width, Height - _formConfig.Margin - launchButton.Height + 1);
            quitButton.Font = new Font("San Serif", _formConfig.ControlButtonFontSize, FontStyle.Regular);
            hideButton.Size = new Size(_formConfig.ControlButtonSize, _formConfig.ControlButtonSize);
            hideButton.Location = new Point(Width - _formConfig.Margin - hideButton.Width, Height - _formConfig.Margin - hideButton.Height - 1);
            hideButton.Font = new Font("San Serif", _formConfig.ControlButtonFontSize, FontStyle.Regular);
        }
        private void BoundClickHandlers()
        {
            foreach (var app in _apps)
            {
                app.PictureBox.Click += SwitchApp;
                app.PictureBox.Click += SetLaunchButtonStatus;
            }
            launchButton.Click += SetLaunchButtonStatus;
        }
        private void UpdateLaunchButtonText() => launchButton.Text = "Launch (" + CountActivatedAppIcons() + ")";
        private void SwitchApp(object sender, EventArgs e)
        {
            var picture = (sender as PictureBox);
            var currentApp = _apps.Find(app => app.PictureBox == picture);
            currentApp.PictureBox.ImageLocation = currentApp.IsActivated()
                ? currentApp.DeactivatedIconPath
                : currentApp.ActivatedIconPath;
        }
        private void SetLaunchButtonStatus(object sender, EventArgs e)
        {
            launchButton.Enabled = false;
            foreach (var app in _apps)
            {
                if (app.IsActivated())
                {
                    launchButton.Enabled = true;
                    break;
                }
            }
            launchButton.Text = launchButton.Enabled
                ? "Launch (" + CountActivatedAppIcons() + ")"
                : "Launch";
        }
        private int CountActivatedAppIcons() => _apps.Where(app => app.IsActivated()).Count();

        private void ActivateAllApps() => _apps.ForEach(app => app.Activate());
        private void DeactivateAllApps() => _apps.ForEach(app => app.Deactivate());

        private void LaunchApps(object sender, EventArgs e)
        {
            foreach (var app in _apps)
            {
                if (app.IsActivated())
                {
                    Process.Start(app.ExecutePath);
                }
            }
            DeactivateAllApps();
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
    }
}
