
namespace WFA_Quanlyquancafe
{
    partial class Form_QuanLy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_QuanLy));
            this.menuquanlycaphe = new System.Windows.Forms.MenuStrip();
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemThốngKêVàBáoCáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tìmKiếmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.menuquanlycaphe.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuquanlycaphe
            // 
            this.menuquanlycaphe.BackColor = System.Drawing.Color.LemonChiffon;
            this.menuquanlycaphe.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuquanlycaphe.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuquanlycaphe.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuquanlycaphe.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem,
            this.xemThốngKêVàBáoCáoToolStripMenuItem,
            this.tìmKiếmToolStripMenuItem});
            this.menuquanlycaphe.Location = new System.Drawing.Point(0, 0);
            this.menuquanlycaphe.Name = "menuquanlycaphe";
            this.menuquanlycaphe.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuquanlycaphe.Size = new System.Drawing.Size(1261, 64);
            this.menuquanlycaphe.TabIndex = 0;
            this.menuquanlycaphe.Text = "menuStrip1";
            // 
            // quảnLýDanhMụcSảnPhẩmToolStripMenuItem
            // 
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.AutoSize = false;
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Image")));
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Name = "quảnLýDanhMụcSảnPhẩmToolStripMenuItem";
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Size = new System.Drawing.Size(152, 60);
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Text = "Quản lý tài khoản";
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Click += new System.EventHandler(this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem_Click);
            // 
            // xemThốngKêVàBáoCáoToolStripMenuItem
            // 
            this.xemThốngKêVàBáoCáoToolStripMenuItem.Name = "xemThốngKêVàBáoCáoToolStripMenuItem";
            this.xemThốngKêVàBáoCáoToolStripMenuItem.Size = new System.Drawing.Size(275, 60);
            this.xemThốngKêVàBáoCáoToolStripMenuItem.Text = "Xem thống kê và báo cáo";
            this.xemThốngKêVàBáoCáoToolStripMenuItem.Click += new System.EventHandler(this.xemThốngKêVàBáoCáoToolStripMenuItem_Click);
            // 
            // tìmKiếmToolStripMenuItem
            // 
            this.tìmKiếmToolStripMenuItem.Name = "tìmKiếmToolStripMenuItem";
            this.tìmKiếmToolStripMenuItem.Size = new System.Drawing.Size(201, 60);
            this.tìmKiếmToolStripMenuItem.Text = "Quản lý sản phẩm";
            this.tìmKiếmToolStripMenuItem.Click += new System.EventHandler(this.tìmKiếmToolStripMenuItem_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.Silver;
            this.pnlContent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlContent.BackgroundImage")));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlContent.Location = new System.Drawing.Point(0, 64);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1261, 705);
            this.pnlContent.TabIndex = 1;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // Form_QuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1261, 785);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.menuquanlycaphe);
            this.MainMenuStrip = this.menuquanlycaphe;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_QuanLy";
            this.Text = "Form_QuanLy";
            this.Load += new System.EventHandler(this.Form_QuanLy_Load);
            this.menuquanlycaphe.ResumeLayout(false);
            this.menuquanlycaphe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuquanlycaphe;
        private System.Windows.Forms.ToolStripMenuItem quảnLýDanhMụcSảnPhẩmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemThốngKêVàBáoCáoToolStripMenuItem;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ToolStripMenuItem tìmKiếmToolStripMenuItem;
    }
}