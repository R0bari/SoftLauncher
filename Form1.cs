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

        private bool _dragFormStatus;
        private int _deltaX, _deltaY;
        private readonly int _defaultMargin = 20;
        private readonly int _iconSize = 75;
        private readonly int _controlButtonSize = 30;
        private readonly int _rowCapacity = 4;

        public Form1()
        {
            InitializeComponent();
            DeleteFormBorders();
            InitializeAppImages();
            ActivateAllApps();
            SetFormSize();
            SetLaunchButton();
            SetControlButtons();
            BoundClickButtonHandlers();
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
                    Location = new Point(_defaultMargin + i % _rowCapacity * (_iconSize + _defaultMargin), _defaultMargin + i / _rowCapacity * (_iconSize + _defaultMargin)),
                    Width = _iconSize,
                    Height = _iconSize,
                    SizeMode = PictureBoxSizeMode.Normal
                };
                Controls.Add(_apps[i].PictureBox);
            }
        }
        private void SetFormSize()
        {
            Width = _defaultMargin + ((_iconSize + _defaultMargin) * _rowCapacity);
            Height = (_defaultMargin * 2) + launchButton.Height + ((_iconSize + _defaultMargin) * (_apps.Count / _rowCapacity + 1));
        }
        private void SetLaunchButton()
        {
            launchButton.Location = new Point(_defaultMargin, Height - _defaultMargin - launchButton.Height);
            launchButton.Size = new Size(Width - 2 * _defaultMargin - _controlButtonSize - _defaultMargin, _iconSize);
            launchButton.Text = "Launch (" + CountActivatedAppIcons() + ")";
        }
        private void SetControlButtons()
        {
            quitButton.Size = new Size(_controlButtonSize, _controlButtonSize);
            quitButton.Location = new Point(Width - _defaultMargin - quitButton.Width, Height - _defaultMargin - launchButton.Height + 1);
            hideButton.Size = new Size(_controlButtonSize, _controlButtonSize);
            hideButton.Location = new Point(Width - _defaultMargin - hideButton.Width, Height - _defaultMargin - hideButton.Height - 1);
        }
        private void BoundClickButtonHandlers()
        {
            foreach (var app in _apps)
            {
                app.PictureBox.Click += SwitchApp;
                app.PictureBox.Click += SetLaunchButtonStatus;
            }

            launchButton.Click += SetLaunchButtonStatus;
        }

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
