namespace SoftLauncher.Forms
{
    partial class CreateAppDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.openIcon = new System.Windows.Forms.OpenFileDialog();
            this.chooseApp = new System.Windows.Forms.Button();
            this.appPath = new System.Windows.Forms.TextBox();
            this.clearAppPath = new System.Windows.Forms.Button();
            this.openAppPath = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.IndianRed;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cancelButton.Location = new System.Drawing.Point(215, 123);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 3, 20, 20);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(164, 56);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.PaleGreen;
            this.addButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.addButton.Enabled = false;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addButton.Location = new System.Drawing.Point(29, 122);
            this.addButton.Margin = new System.Windows.Forms.Padding(20);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(163, 56);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.AddApp);
            // 
            // openIcon
            // 
            this.openIcon.Filter = "Image files(*.jpg)|*.jpg|Image files(*.png)|*.png|All files(*.*)|*.*";
            // 
            // chooseApp
            // 
            this.chooseApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chooseApp.Location = new System.Drawing.Point(29, 71);
            this.chooseApp.Margin = new System.Windows.Forms.Padding(20, 20, 3, 3);
            this.chooseApp.Name = "chooseApp";
            this.chooseApp.Size = new System.Drawing.Size(128, 28);
            this.chooseApp.TabIndex = 5;
            this.chooseApp.Text = "Choose App";
            this.chooseApp.UseVisualStyleBackColor = true;
            this.chooseApp.Click += new System.EventHandler(this.ChooseAppPath);
            // 
            // appPath
            // 
            this.appPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.appPath.Location = new System.Drawing.Point(180, 72);
            this.appPath.Margin = new System.Windows.Forms.Padding(20, 20, 3, 3);
            this.appPath.Multiline = true;
            this.appPath.Name = "appPath";
            this.appPath.ReadOnly = true;
            this.appPath.Size = new System.Drawing.Size(148, 28);
            this.appPath.TabIndex = 6;
            // 
            // clearAppPath
            // 
            this.clearAppPath.BackColor = System.Drawing.Color.IndianRed;
            this.clearAppPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearAppPath.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.clearAppPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearAppPath.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.clearAppPath.Location = new System.Drawing.Point(351, 72);
            this.clearAppPath.Margin = new System.Windows.Forms.Padding(20);
            this.clearAppPath.Name = "clearAppPath";
            this.clearAppPath.Size = new System.Drawing.Size(28, 28);
            this.clearAppPath.TabIndex = 7;
            this.clearAppPath.Text = "X";
            this.clearAppPath.UseVisualStyleBackColor = false;
            this.clearAppPath.Click += new System.EventHandler(this.Clear);
            // 
            // openAppPath
            // 
            this.openAppPath.Filter = "Exe files(*.exe)|*.exe|All files(*.*)|*.*";
            // 
            // CreateAppDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(408, 208);
            this.Controls.Add(this.clearAppPath);
            this.Controls.Add(this.appPath);
            this.Controls.Add(this.chooseApp);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "CreateAppDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateAppDialog";
            this.Click += new System.EventHandler(this.ClickForm);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KeepForm);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UnkeepForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.OpenFileDialog openIcon;
        private System.Windows.Forms.Button chooseApp;
        private System.Windows.Forms.TextBox appPath;
        private System.Windows.Forms.Button clearAppPath;
        private System.Windows.Forms.OpenFileDialog openAppPath;
    }
}