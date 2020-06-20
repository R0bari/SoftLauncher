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
            this.button1 = new System.Windows.Forms.Button();
            this.openIcon = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.deactivatedPath = new System.Windows.Forms.TextBox();
            this.clearDeactivatedIconButton = new System.Windows.Forms.Button();
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
            this.cancelButton.Location = new System.Drawing.Point(215, 206);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 3, 20, 20);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(164, 56);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PaleGreen;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(29, 206);
            this.button1.Margin = new System.Windows.Forms.Padding(20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 56);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // openIcon
            // 
            this.openIcon.Filter = "Image files(*.jpg)|*.jpg|Image files(*.png)|*.png|All files(*.*)|*.*";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(29, 29);
            this.button2.Margin = new System.Windows.Forms.Padding(20, 20, 3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 28);
            this.button2.TabIndex = 2;
            this.button2.Text = "Choose Icon";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ChooseDeactivatedIcon);
            // 
            // deactivatedPath
            // 
            this.deactivatedPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deactivatedPath.Location = new System.Drawing.Point(180, 29);
            this.deactivatedPath.Margin = new System.Windows.Forms.Padding(20, 20, 3, 3);
            this.deactivatedPath.Multiline = true;
            this.deactivatedPath.Name = "deactivatedPath";
            this.deactivatedPath.ReadOnly = true;
            this.deactivatedPath.Size = new System.Drawing.Size(148, 28);
            this.deactivatedPath.TabIndex = 3;
            // 
            // clearDeactivatedIconButton
            // 
            this.clearDeactivatedIconButton.BackColor = System.Drawing.Color.IndianRed;
            this.clearDeactivatedIconButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearDeactivatedIconButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.clearDeactivatedIconButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearDeactivatedIconButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.clearDeactivatedIconButton.Location = new System.Drawing.Point(351, 29);
            this.clearDeactivatedIconButton.Margin = new System.Windows.Forms.Padding(20);
            this.clearDeactivatedIconButton.Name = "clearDeactivatedIconButton";
            this.clearDeactivatedIconButton.Size = new System.Drawing.Size(28, 28);
            this.clearDeactivatedIconButton.TabIndex = 4;
            this.clearDeactivatedIconButton.Text = "X";
            this.clearDeactivatedIconButton.UseVisualStyleBackColor = false;
            this.clearDeactivatedIconButton.Click += new System.EventHandler(this.ClearDeactivatedIcon);
            // 
            // CreateAppDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(408, 291);
            this.Controls.Add(this.clearDeactivatedIconButton);
            this.Controls.Add(this.deactivatedPath);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cancelButton);
            this.Name = "CreateAppDialog";
            this.Text = "CreateAppDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openIcon;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox deactivatedPath;
        private System.Windows.Forms.Button clearDeactivatedIconButton;
    }
}