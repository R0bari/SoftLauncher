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
        private readonly AppEntity _appEntity = new AppEntity();

        public CreateAppDialog()
        {
            InitializeComponent();
            DeleteFormBorders();
        }
        private void DeleteFormBorders()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            Text = "";
        }

        private void ChooseDeactivatedIcon(object sender, EventArgs e)
        {
            if (openIcon.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            _appEntity.IconPath = openIcon.FileName;
            deactivatedPath.Text = openIcon.FileName;
            MessageBox.Show("Файл открыт");
        }
        private void ClearDeactivatedIcon(object sender, EventArgs e)
        {
            _appEntity.IconPath = "";
            deactivatedPath.Text = "";
        }
    }
}
