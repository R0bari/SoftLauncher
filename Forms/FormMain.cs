using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftLauncher.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SoftLauncher
{
    public partial class FormMain : Form
    {
        private readonly Config _formConfig = new Config(
            filePath: "apps.json",
            margin: 20,
            iconSize: 64,
            controlButtonSize: 32,
            rowCapacity: 3);
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

            apps = ReadFromFile(_formConfig.FilePath);
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
            InitLaunchButton(launchButton, LaunchSelectedApps);

            UpdateSwitchButtonStatus(switchButton);
            UpdateLaunchButtonText(launchButton);
        }

        private List<AppEntity> ReadFromFile(string filePath)
        {

            using (StreamReader stream = new StreamReader(filePath))
            {
                var jsonString = stream.ReadToEnd();
                var jsonApps = new List<AppEntityJson>();
                try
                {
                    jsonApps = JsonConvert.DeserializeObject<List<AppEntityJson>>(jsonString);
                }
                catch
                {
                    MessageBox.Show($"Can't read apps list from file \"{Environment.CurrentDirectory}\\{filePath}\"", "Read error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                var apps = new List<AppEntity>();
                (jsonApps as List<AppEntityJson>).ForEach(jsonApp => apps.Add(new AppEntity(jsonApp)));

                return apps;
            }
        }
        private void WriteToFile(string filePath, List<AppEntity> apps)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            var jsonApps = new List<AppEntityJson>();
            apps.ForEach(app => jsonApps.Add(new AppEntityJson(app)));

            using (StreamWriter stream = new StreamWriter(filePath))
            {
                try
                {
                    stream.Write(JsonConvert.SerializeObject(jsonApps));
                }
                catch
                {
                    MessageBox.Show("Can't write apps list to file \"{Environment.CurrentDirectory}\\{filePath}\"", "Write error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
        private void DeleteFormBorders(FormMain form)
        {
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.ControlBox = false;
            form.Text = "";
        }

        private void InitAppImages(List<AppEntity> apps, MouseEventHandler switchApp, MouseEventHandler updateLaunchButtonStatus)
        {
            foreach (var app in this.apps)
            {
                app.PictureBox.Name = app.AppName;
                app.PictureBox.Image = Icon.ExtractAssociatedIcon(app.ExecutePath).ToBitmap();
                app.SetSize(new Size(_formConfig.IconSize, _formConfig.IconSize));
                app.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                app.PictureBox.MouseDoubleClick += LaunchApp;
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
                    _formConfig.Margin + i % _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin),
                    _formConfig.Margin * 2 + _formConfig.ControlButtonSize + i / _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin));
            }
        }
        private void UpdateFormSize(FormMain form)
        {
            form.Width = _formConfig.Margin + (_formConfig.Margin + _formConfig.IconSize) * _formConfig.RowCapacity;
            form.Height = _formConfig.Margin * 2 + _formConfig.ControlButtonSize + ((_formConfig.IconSize + _formConfig.Margin) * ((apps.Count - 1) / _formConfig.RowCapacity + 1)) +
                _formConfig.IconSize + _formConfig.Margin;
        }
        private void InitLaunchButton(Button launchButton, EventHandler launchSelectedApps)
        {
            InitLaunchButtonProps(launchButton);
            launchButton.Click += launchSelectedApps;
            Controls.Add(launchButton);
        }
        private void InitLaunchButtonProps(Button launchButton)
        {
            launchButton.Size = new Size(Width - 2 * _formConfig.Margin, _formConfig.IconSize);
            launchButton.BackColor = Color.PaleGreen;
            launchButton.ForeColor = Color.Black;
            launchButton.FlatStyle = FlatStyle.Flat;
            launchButton.Font = new Font("San Serif", _formConfig.ControlFontSize, FontStyle.Regular);
            UpdateLaunchButtonLocation(launchButton);
            UpdateLaunchButtonText(launchButton);
        }
        private void UpdateLaunchButtonLocation(Button launchButton)
        {
            launchButton.Location = new Point(_formConfig.Margin, Height - _formConfig.Margin - _formConfig.IconSize);
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
        private void UpdateSwitchButtonStatus(Button switchAllButton)
        {
            ToolTip t = new ToolTip();
            if (apps.Any(app => !app.IsSelected))
            {
                switchAllButton.BackColor = Color.PaleGreen;
                //switchAllButton.Text = "Select";
                t.SetToolTip(switchAllButton, "Select all");
            }
            else
            {
                switchAllButton.BackColor = Color.IndianRed;
                //switchAllButton.Text = "Unselect";
                t.SetToolTip(switchAllButton, "Unselect all");
            }
        }

        private void InitControlButtons(ControlButton quitButton, ControlButton hideButton, ControlButton addButton, ControlButton switchButton)
        {
            quitButton.Init("X", Color.IndianRed, _formConfig, this, ExitApp);
            hideButton.Init("-", Color.LightGray, _formConfig, this, HideApp);
            addButton.Init("+", Color.PaleGreen, _formConfig, this, AddApp);
            switchButton.Init("", Color.PaleGreen, _formConfig, this, SwitchAll, @"E:\Planfact\Projects\SoftLauncher\SoftLauncher\img\check.png");
        }

        private int CountActivatedAppIcons(List<AppEntity> apps) => apps.Where(app => app.IsSelected).Count();
        private void SwitchAll(object sender, EventArgs e)
        {
            if (apps.Any(app => !app.IsSelected))
            {
                SelectAllApps(apps);
            }
            else
            {
                UnselectAllApps(apps);
            }
            UpdateLaunchButtonStatus(sender, (MouseEventArgs)e);
        }
        private void SelectAllApps(List<AppEntity> apps)
        {
            apps.ForEach(app => app.Select());
            WriteToFile(_formConfig.FilePath, apps);
            UpdateSwitchButtonStatus(switchButton);
        }
        private void UnselectAllApps(List<AppEntity> apps)
        {
            apps.ForEach(app => app.Unselect());
            WriteToFile(_formConfig.FilePath, apps);
            UpdateSwitchButtonStatus(switchButton);
        }

        private void ClickAppIcon(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SwitchApp(sender as PictureBox);
            }
            else if (e.Button == MouseButtons.Right)
            {
                MakeChosen(sender as PictureBox);
                appContextMenu.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }
        private void SwitchApp(PictureBox picture)
        {
            var app = apps.Find(a => a.PictureBox == picture);
            app.Switch();
            WriteToFile(_formConfig.FilePath, apps);
            UpdateSwitchButtonStatus(switchButton);
        }
        private void MakeChosen(PictureBox picture)
        {
            _currentApp = apps.Find(app => app.PictureBox == picture);
        }
        private void LaunchSelectedApps(object sender, EventArgs e)
        {
            foreach (var app in apps)
            {
                if (app.IsSelected)
                {
                    Process.Start(app.ExecutePath);
                }
            }
            UnselectAllApps(apps);
        }

        private void LoadFormMain(object sender, EventArgs e)
        {
            if (!HasAdminRight())
            {
                RunWithAdminRight();
            }
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
            catch (Win32Exception)
            {
                MessageBox.Show("Приложению необходимы права администратора!");
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

        private void ExitApp(object sender, EventArgs e) => Application.Exit();
        private void HideApp(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void AddApp(object sender, EventArgs e)
        {
            CreateAppDialog createAppDialog = new CreateAppDialog();
            if (createAppDialog.ShowDialog() == DialogResult.OK)
            {
                var newApp = createAppDialog.appEntity.Clone();
                apps.Add(newApp as AppEntity);
                Controls.Add((newApp as AppEntity).PictureBox);
                InitIconConfig(newApp as AppEntity);
                UpdateFormSize(this);
                InitLaunchButtonProps(launchButton);
                UpdateSwitchButtonStatus(switchButton);
                WriteToFile(_formConfig.FilePath, apps);
            }
        }
        private void EditApp(object sender, EventArgs e)
        {
            var index = apps.FindIndex(a => a == _currentApp);
            if (apps[index] == null) return;

            CreateAppDialog createAppDialog = new CreateAppDialog(apps[index]);
            if (createAppDialog.ShowDialog() == DialogResult.OK)
            {
                Controls.Remove(apps[index].PictureBox);
                apps[index] = createAppDialog.appEntity;
                Controls.Add(apps[index].PictureBox);
                InitIconConfig(apps[index]);
                UpdateLaunchButtonText(launchButton);
                UpdateSwitchButtonStatus(switchButton);
                WriteToFile(_formConfig.FilePath, apps);
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
            UpdateSwitchButtonStatus(switchButton);
            WriteToFile(_formConfig.FilePath, apps);
        }
        private void LaunchApp(object sender, MouseEventArgs e)
        {
            var app = apps.Find(a => a == _currentApp);
            if (app == null) return;

            if (app.IsSelected)
            {
                Process.Start(app.ExecutePath);
            }
        }

        private void InitIconConfig(AppEntity app)
        {
            var index = apps.FindIndex(a => a == app);
            apps[index].PictureBox.Location = new Point(
                    _formConfig.Margin + index % _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin),
                    _formConfig.Margin * 2 + _formConfig.ControlButtonSize + index / _formConfig.RowCapacity * (_formConfig.IconSize + _formConfig.Margin));
            apps[index].SetSize(new Size(_formConfig.IconSize, _formConfig.IconSize));
            apps[index].PictureBox.MouseClick += ClickAppIcon;
            apps[index].PictureBox.MouseClick += UpdateLaunchButtonStatus;
        }
    }
}
