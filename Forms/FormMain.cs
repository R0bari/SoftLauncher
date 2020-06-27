using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftLauncher.Exceptions;
using SoftLauncher.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SoftLauncher
{
    public partial class FormMain : Form
    {
        private readonly Config config = new Config(
            jsonFilePath: "apps.json",
            margin: 20,
            iconSize: 64,
            controlButtonSize: 32,
            rowCapacity: 3);
        private readonly Logger logger = new Logger("log.txt");
        private readonly Sounds sounds = new Sounds();
        private readonly List<AppEntity> apps = new List<AppEntity>();
        private AppEntity _currentApp = new AppEntity();
        private readonly ControlButton quitButton = new ControlButton(index: 0);
        private readonly ControlButton hideButton = new ControlButton(index: 1);
        private readonly ControlButton addButton = new ControlButton(index: 2);
        private readonly ControlButton switchButton = new ControlButton(index: 3);
        private readonly Button launchButton = new Button();

        private bool _dragFormStatus;
        private int _deltaX, _deltaY;

        public FormMain()
        {
            InitializeComponent();
            DeleteFormBorders(this);

            apps = ReadFromFile(config.FilePath);
            InitAppImages(
                apps: apps,
                switchApp: ClickAppIcon,
                updateLaunchButtonStatus: UpdateLaunchButtonStatus);
            UpdateFormSize(this);
            InitControlButtons(
                quitButton: quitButton,
                addButton: addButton,
                hideButton: hideButton,
                switchButton: switchButton);
            InitLaunchButton(launchButton, LaunchSelectedApps, apps);

            UpdateSwitchButtonStatus(switchButton, apps);
            UpdateLaunchButtonText(launchButton);
        }

        private List<AppEntity> ReadFromFile(string filePath)
        {

            using (var stream = new StreamReader(filePath))
            {
                var jsonString = stream.ReadToEnd();
                var jsonApps = new List<AppEntityJson>();
                try
                {
                    jsonApps = JsonConvert.DeserializeObject<List<AppEntityJson>>(jsonString);
                }
                catch (JsonReadException ex)
                {
                    MessageBox.Show(ex.Message, ex.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    logger.Log(LogType.Error, ex.Message);
                }

                return AppEntityJson.Convert(jsonApps);
            }
        }
        private void WriteToFile(string filePath, List<AppEntity> apps)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            var jsonApps = AppEntityJson.Convert(apps);

            using (var stream = new StreamWriter(filePath))
            {
                try
                {
                    stream.Write(JsonConvert.SerializeObject(jsonApps));
                }
                catch (JsonWriteException ex)
                {
                    MessageBox.Show(ex.Message, ex.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    logger.Log(LogType.Error, ex.Message);
                }
            }
        }

        private void DeleteFormBorders(FormMain form)
        {
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.ControlBox = false;
        }

        private void InitAppImages(List<AppEntity> apps, MouseEventHandler switchApp, MouseEventHandler updateLaunchButtonStatus)
        {
            foreach (var app in this.apps)
            {
                app.PictureBox.Name = app.AppName;
                app.PictureBox.Image = Icon.ExtractAssociatedIcon(app.ExecutePath).ToBitmap();
                app.SetSize(new Size(config.IconSize, config.IconSize));
                app.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                app.PictureBox.MouseClick += switchApp;
                app.PictureBox.MouseClick += updateLaunchButtonStatus;
                Controls.Add(app.PictureBox);
            }
            UpdateAppImagesLocation(apps);
        }
        private void UpdateAppImagesLocation(List<AppEntity> apps)
        {
            for (int i = 0; i < apps.Count; ++i)
            {
                apps[i].PictureBox.Location = new Point(
                    config.Margin + i % config.RowCapacity * (config.IconSize + config.Margin),
                    config.Margin * 2 + config.ControlButtonSize + i / config.RowCapacity * (config.IconSize + config.Margin));
            }
        }
        private void UpdateFormSize(FormMain form)
        {
            form.Width = config.Margin + (config.Margin + config.IconSize) * config.RowCapacity;
            form.Height = config.Margin * 2 + config.ControlButtonSize + ((config.IconSize + config.Margin) * ((apps.Count - 1) / config.RowCapacity + 1)) +
                config.IconSize + config.Margin;
        }
        private void InitLaunchButton(Button launchButton, EventHandler launchSelectedApps, List<AppEntity> apps)
        {
            InitLaunchButtonProps(launchButton);
            launchButton.Enabled = apps.Where(a => a.IsSelected).Count() > 0;
            launchButton.Click += launchSelectedApps;
            Controls.Add(launchButton);
        }
        private void InitLaunchButtonProps(Button launchButton)
        {
            launchButton.Size = new Size(Width - 2 * config.Margin, config.IconSize);
            launchButton.BackColor = Color.PaleGreen;
            launchButton.ForeColor = Color.Black;
            launchButton.FlatStyle = FlatStyle.Flat;
            launchButton.Font = new Font("San Serif", config.ControlFontSize, FontStyle.Regular);
            UpdateLaunchButtonLocation(launchButton);
            UpdateLaunchButtonText(launchButton);
        }
        private void UpdateLaunchButtonLocation(Button launchButton)
        {
            launchButton.Location = new Point(config.Margin, Height - config.Margin - config.IconSize);
        }
        private void UpdateLaunchButtonText(Button launchButton) => launchButton.Text = launchButton.Enabled
                ? "Launch (" + CountActivatedAppIcons(apps) + ")"
                : "Launch";
        private void UpdateLaunchButtonStatus(object sender, EventArgs e)
        {
            launchButton.Enabled = false;
            foreach (var app in apps)
            {
                if (app.IsSelected)
                {
                    launchButton.Enabled = true;
                    break;
                }
            }
            UpdateLaunchButtonText(launchButton);
        }
        private void UpdateSwitchButtonStatus(Button switchAllButton, List<AppEntity> apps)
        {
            switchAllButton.BackColor = (apps.Any(app => !app.IsSelected)) ? Color.IndianRed : Color.PaleGreen;
            switchButton.Tip.SetToolTip(switchButton, apps.Any(app => !app.IsSelected) ? "Select All" : "Unselect All");
        }

        private void InitControlButtons(ControlButton quitButton, ControlButton hideButton, ControlButton addButton, ControlButton switchButton)
        {
            quitButton.Init("X", Color.IndianRed, config, this, ExitApp);
            hideButton.Init("-", Color.LightGray, config, this, HideApp);
            addButton.Init("+", Color.PaleGreen, config, this, AddApp);
            switchButton.Init("", Color.PaleGreen, config, this, SwitchAll, @"img\check.png");
        }

        private int CountActivatedAppIcons(List<AppEntity> apps) => apps.Where(app => app.IsSelected).Count();
        private void SwitchAll(object sender, EventArgs e)
        {
            Player.PlaySound(sounds.Click);
            if (apps.Any(app => !app.IsSelected))
            {
                SelectAllApps(apps, sender, e);
                switchButton.Tip.SetToolTip(switchButton, "Unselect All");
            }
            else
            {
                UnselectAllApps(apps, sender, e);
                switchButton.Tip.SetToolTip(switchButton, "Select All");
            }
            UpdateLaunchButtonStatus(sender, e);
        }
        private void SelectAllApps(List<AppEntity> apps, object sender, EventArgs e)
        {
            apps.ForEach(app => app.Select());
            WriteToFile(config.FilePath, apps);
            UpdateSwitchButtonStatus(switchButton, apps);
            UpdateLaunchButtonStatus(sender, e);
        }
        private void UnselectAllApps(List<AppEntity> apps, object sender, EventArgs e)
        {
            apps.ForEach(app => app.Unselect());
            WriteToFile(config.FilePath, apps);
            UpdateSwitchButtonStatus(switchButton, apps);
            UpdateLaunchButtonStatus(sender, e);
        }

        private void ClickAppIcon(object sender, MouseEventArgs e)
        {
            Player.PlaySound(sounds.Click);
            if (e.Button == MouseButtons.Left)
            {
                SwitchApp(sender as PictureBox);
            }
            else if (e.Button == MouseButtons.Right)
            {
                MakeCurrent(sender as PictureBox);
                appContextMenu.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }
        private void SwitchApp(PictureBox picture)
        {
            var app = apps.Find(a => a.PictureBox == picture);
            app.Switch();
            WriteToFile(config.FilePath, apps);
            UpdateSwitchButtonStatus(switchButton, apps);
        }
        private void MakeCurrent(PictureBox picture)
        {
            _currentApp = apps.Find(app => app.PictureBox == picture);
        }
        private void LaunchSelectedApps(object sender, EventArgs e)
        {
            Player.PlaySound(sounds.Click);
            foreach (var app in apps)
            {
                if (app.IsSelected)
                {
                    app.Launch();
                }
            }
            UnselectAllApps(apps, sender, e);
        }
        private void LoadFormMain(object sender, EventArgs e)
        {
            if (!HasAdminRight())
            {
                RunWithAdminRight();
            }
            Player.PlaySound(sounds.Start);
        }
        private bool HasAdminRight()
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return pricipal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        private void RunWithAdminRight()
        {
            var programWithAdminRight = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = Application.ExecutablePath
            };
            try
            {
                Process.Start(programWithAdminRight);
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message, "Недостаточно прав", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                logger.Log(LogType.Error, ex.Message);
            }
            Application.Exit();
        }

        private void HoldForm(object sender, MouseEventArgs e)
        {
            _dragFormStatus = true;
            _deltaX = Cursor.Position.X - Location.X;
            _deltaY = Cursor.Position.Y - Location.Y;
        }
        private void UnholdForm(object sender, MouseEventArgs e) => _dragFormStatus = false;
        private void DragForm(object sender, MouseEventArgs e)
        {
            if (_dragFormStatus)
            {
                Location = new Point(Cursor.Position.X - _deltaX, Cursor.Position.Y - _deltaY);
            }
        }

        private void ExitApp(object sender, EventArgs e)
        {
            Player.PlaySound(sounds.Click);
            Player.PlaySound(sounds.Exit);
            Application.Exit();
        }
        private void HideApp(object sender, EventArgs e)
        {
            Player.PlaySound(sounds.Click);
            WindowState = FormWindowState.Minimized;
        }
        private void AddApp(object sender, EventArgs e)
        {
            Player.PlaySound(sounds.Click);
            var createAppDialog = new CreateAppDialog();
            if (createAppDialog.ShowDialog() == DialogResult.OK)
            {
                var newApp = (createAppDialog.appEntity.Clone() as AppEntity);
                apps.Add(newApp);
                Controls.Add(newApp.PictureBox);
                InitIconConfig(newApp);
                UpdateFormSize(this);
                InitLaunchButtonProps(launchButton);
                UpdateSwitchButtonStatus(switchButton, apps);
                WriteToFile(config.FilePath, apps);
                Player.PlaySound(sounds.PositiveAction);
                logger.Log(LogType.Add, newApp.AppName);
            } 
            else
            {
                Player.PlaySound(sounds.NegativeAction);
            }
        }
        private void EditApp(object sender, EventArgs e)
        {
            Player.PlaySound(sounds.Click);
            var index = apps.FindIndex(a => a == _currentApp);
            if (apps[index] == null) return;

            var createAppDialog = new CreateAppDialog(apps[index]);
            if (createAppDialog.ShowDialog() == DialogResult.OK)
            {
                Controls.Remove(apps[index].PictureBox);
                apps[index] = createAppDialog.appEntity;
                Controls.Add(apps[index].PictureBox);
                InitIconConfig(apps[index]);
                UpdateLaunchButtonText(launchButton);
                UpdateSwitchButtonStatus(switchButton, apps);
                WriteToFile(config.FilePath, apps);
                Player.PlaySound(sounds.PositiveAction);
                logger.Log(LogType.Edit, apps[index].AppName);
            }
            else
            {
                Player.PlaySound(sounds.NegativeAction);
            }
        }
        private void DeleteApp(object sender, EventArgs e)
        {
            var app = apps.Find(a => a == _currentApp);
            if (app == null) return;

            apps.Remove(app);
            Controls.Remove(app.PictureBox);
            UpdateFormSize(this);
            UpdateAppImagesLocation(apps);
            UpdateLaunchButtonLocation(launchButton);
            UpdateLaunchButtonText(launchButton);
            UpdateSwitchButtonStatus(switchButton, apps);
            WriteToFile(config.FilePath, apps);
            Player.PlaySound(sounds.NegativeAction);
            logger.Log(LogType.Delete, app.AppName);
        }
        private void LaunchApp(object sender, MouseEventArgs e)
        {
            Player.PlaySound(sounds.Click);
            var app = apps.Find(a => a == _currentApp);
            if (app == null) return;

            app.Launch();
        }

        private void InitIconConfig(AppEntity app)
        {
            var index = apps.FindIndex(a => a == app);
            apps[index].PictureBox.Location = new Point(
                    config.Margin + index % config.RowCapacity * (config.IconSize + config.Margin),
                    config.Margin * 2 + config.ControlButtonSize + index / config.RowCapacity * (config.IconSize + config.Margin));
            apps[index].SetSize(new Size(config.IconSize, config.IconSize));
            apps[index].PictureBox.MouseClick += ClickAppIcon;
            apps[index].PictureBox.MouseClick += UpdateLaunchButtonStatus;
        }
    }
}
