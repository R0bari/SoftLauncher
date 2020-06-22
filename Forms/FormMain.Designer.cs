namespace SoftLauncher
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.appContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editApp = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteApp = new System.Windows.Forms.ToolStripMenuItem();
            this.appContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // appContextMenu
            // 
            this.appContextMenu.BackColor = System.Drawing.Color.AliceBlue;
            this.appContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchToolStripMenuItem,
            this.editApp,
            this.deleteApp});
            this.appContextMenu.Name = "appContextMenu";
            this.appContextMenu.Size = new System.Drawing.Size(181, 92);
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.BackColor = System.Drawing.Color.PaleGreen;
            this.launchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("launchToolStripMenuItem.Image")));
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            this.launchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.launchToolStripMenuItem.Text = "Launch";
            this.launchToolStripMenuItem.Click += new System.EventHandler(this.LaunchApp);
            // 
            // editApp
            // 
            this.editApp.BackColor = System.Drawing.Color.Azure;
            this.editApp.Image = ((System.Drawing.Image)(resources.GetObject("editApp.Image")));
            this.editApp.Name = "editApp";
            this.editApp.Size = new System.Drawing.Size(180, 22);
            this.editApp.Text = "Info/Edit";
            this.editApp.Click += new System.EventHandler(this.EditApp);
            // 
            // deleteApp
            // 
            this.deleteApp.BackColor = System.Drawing.Color.IndianRed;
            this.deleteApp.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.deleteApp.Image = ((System.Drawing.Image)(resources.GetObject("deleteApp.Image")));
            this.deleteApp.Name = "deleteApp";
            this.deleteApp.Size = new System.Drawing.Size(180, 22);
            this.deleteApp.Text = "Delete";
            this.deleteApp.Click += new System.EventHandler(this.DeleteApp);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(480, 230);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMain";
            this.Opacity = 0.98D;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.LoadFormMain);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HoldForm);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UnholdForm);
            this.appContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip appContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editApp;
        private System.Windows.Forms.ToolStripMenuItem deleteApp;
        private System.Windows.Forms.ToolStripMenuItem launchToolStripMenuItem;
    }
}

