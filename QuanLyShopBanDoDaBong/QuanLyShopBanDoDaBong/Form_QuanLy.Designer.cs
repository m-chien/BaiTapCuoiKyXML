namespace QuanLyShopBanDoDaBong
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
            this.tìmKiếmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemThốngKêVàBáoCáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.menuquanlycaphe.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuquanlycaphe
            // 
            this.menuquanlycaphe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(171)))), ((int)(((byte)(227)))));
            this.menuquanlycaphe.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuquanlycaphe.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuquanlycaphe.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem,
            this.tìmKiếmToolStripMenuItem,
            this.xemThốngKêVàBáoCáoToolStripMenuItem});
            this.menuquanlycaphe.Location = new System.Drawing.Point(0, 0);
            this.menuquanlycaphe.Name = "menuquanlycaphe";
            this.menuquanlycaphe.Padding = new System.Windows.Forms.Padding(10);
            this.menuquanlycaphe.Size = new System.Drawing.Size(1001, 60);
            this.menuquanlycaphe.TabIndex = 0;
            this.menuquanlycaphe.Text = "menuStrip1";
            this.menuquanlycaphe.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuquanlycaphe_ItemClicked);
            // 
            // quảnLýDanhMụcSảnPhẩmToolStripMenuItem
            // 
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.AutoSize = false;
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Image")));
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Name = "quảnLýDanhMụcSảnPhẩmToolStripMenuItem";
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Text = "  Quản lý tài khoản";
            this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem.Click += new System.EventHandler(this.quảnLýDanhMụcSảnPhẩmToolStripMenuItem_Click);
            // 
            // tìmKiếmToolStripMenuItem
            // 
            this.tìmKiếmToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.tìmKiếmToolStripMenuItem.Name = "tìmKiếmToolStripMenuItem";
            this.tìmKiếmToolStripMenuItem.Size = new System.Drawing.Size(146, 40);
            this.tìmKiếmToolStripMenuItem.Text = "Quản lý sản phẩm";
            this.tìmKiếmToolStripMenuItem.Click += new System.EventHandler(this.tìmKiếmToolStripMenuItem_Click);
            // 
            // xemThốngKêVàBáoCáoToolStripMenuItem
            // 
            this.xemThốngKêVàBáoCáoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.xemThốngKêVàBáoCáoToolStripMenuItem.Name = "xemThốngKêVàBáoCáoToolStripMenuItem";
            this.xemThốngKêVàBáoCáoToolStripMenuItem.Size = new System.Drawing.Size(197, 40);
            this.xemThốngKêVàBáoCáoToolStripMenuItem.Text = "Xem thống kê và báo cáo";
            this.xemThốngKêVàBáoCáoToolStripMenuItem.Click += new System.EventHandler(this.xemThốngKêVàBáoCáoToolStripMenuItem_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1001, 540);
            this.pnlContent.TabIndex = 1;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // Form_QuanLy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1001, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.menuquanlycaphe);
            this.MainMenuStrip = this.menuquanlycaphe;
            this.Name = "Form_QuanLy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần mềm quản lý Shop Bóng Đá";
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