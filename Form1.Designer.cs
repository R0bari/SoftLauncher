namespace SoftLauncher
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.launchButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.hideButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // launchButton
            // 
            this.launchButton.BackColor = System.Drawing.Color.PaleGreen;
            this.launchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.launchButton.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.launchButton.Location = new System.Drawing.Point(29, 127);
            this.launchButton.Margin = new System.Windows.Forms.Padding(20);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(422, 75);
            this.launchButton.TabIndex = 4;
            this.launchButton.Text = "Launch";
            this.launchButton.UseVisualStyleBackColor = false;
            this.launchButton.Click += new System.EventHandler(this.LaunchApps);
            // 
            // quitButton
            // 
            this.quitButton.BackColor = System.Drawing.Color.Tomato;
            this.quitButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.quitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.quitButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.quitButton.Location = new System.Drawing.Point(421, 29);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(30, 30);
            this.quitButton.TabIndex = 5;
            this.quitButton.Text = "X";
            this.quitButton.UseVisualStyleBackColor = false;
            this.quitButton.Click += new System.EventHandler(this.ExitApp);
            // 
            // hideButton
            // 
            this.hideButton.BackColor = System.Drawing.SystemColors.Control;
            this.hideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hideButton.Location = new System.Drawing.Point(421, 74);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(30, 30);
            this.hideButton.TabIndex = 6;
            this.hideButton.Text = "-";
            this.hideButton.UseVisualStyleBackColor = false;
            this.hideButton.Click += new System.EventHandler(this.HideApp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(480, 230);
            this.Controls.Add(this.hideButton);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.launchButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.LoadForm);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KeepForm);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UnkeepForm);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Button hideButton;
    }
}

