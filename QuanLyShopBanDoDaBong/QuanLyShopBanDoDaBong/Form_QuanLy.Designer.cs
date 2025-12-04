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
            this.quanLyTaiKhoanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanLyDanhMucToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanLySanPhamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanLyHoaDonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanLyBinhLuanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baoCaoThongKeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.menuquanlycaphe.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuquanlycaphe
            // 
            this.menuquanlycaphe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(171)))), ((int)(((byte)(227)))));
            this.menuquanlycaphe.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuquanlycaphe.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuquanlycaphe.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuquanlycaphe.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quanLyTaiKhoanToolStripMenuItem,
            this.quanLyDanhMucToolStripMenuItem,
            this.quanLySanPhamToolStripMenuItem,
            this.quanLyHoaDonToolStripMenuItem,
            this.quanLyBinhLuanToolStripMenuItem,
            this.baoCaoThongKeToolStripMenuItem});
            this.menuquanlycaphe.Location = new System.Drawing.Point(0, 0);
            this.menuquanlycaphe.Name = "menuquanlycaphe";
            this.menuquanlycaphe.Padding = new System.Windows.Forms.Padding(5);
            this.menuquanlycaphe.Size = new System.Drawing.Size(1106, 54);
            this.menuquanlycaphe.TabIndex = 0;
            this.menuquanlycaphe.Text = "menuStrip1";
            // 
            // quanLyTaiKhoanToolStripMenuItem
            // 
            this.quanLyTaiKhoanToolStripMenuItem.AutoSize = false;
            this.quanLyTaiKhoanToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quanLyTaiKhoanToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("quanLyTaiKhoanToolStripMenuItem.Image")));
            this.quanLyTaiKhoanToolStripMenuItem.Name = "quanLyTaiKhoanToolStripMenuItem";
            this.quanLyTaiKhoanToolStripMenuItem.Size = new System.Drawing.Size(170, 40);
            this.quanLyTaiKhoanToolStripMenuItem.Text = " Quản lý tài khoản";
            this.quanLyTaiKhoanToolStripMenuItem.Click += new System.EventHandler(this.quanLyTaiKhoanToolStripMenuItem_Click);
            // 
            // quanLyDanhMucToolStripMenuItem
            // 
            this.quanLyDanhMucToolStripMenuItem.AutoSize = false;
            this.quanLyDanhMucToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quanLyDanhMucToolStripMenuItem.Name = "quanLyDanhMucToolStripMenuItem";
            this.quanLyDanhMucToolStripMenuItem.Size = new System.Drawing.Size(170, 40);
            this.quanLyDanhMucToolStripMenuItem.Text = "Quản lý danh mục";
            this.quanLyDanhMucToolStripMenuItem.Click += new System.EventHandler(this.quanLyDanhMucToolStripMenuItem_Click);
            // 
            // quanLySanPhamToolStripMenuItem
            // 
            this.quanLySanPhamToolStripMenuItem.AutoSize = false;
            this.quanLySanPhamToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quanLySanPhamToolStripMenuItem.Name = "quanLySanPhamToolStripMenuItem";
            this.quanLySanPhamToolStripMenuItem.Size = new System.Drawing.Size(170, 40);
            this.quanLySanPhamToolStripMenuItem.Text = "Quản lý sản phẩm";
            this.quanLySanPhamToolStripMenuItem.Click += new System.EventHandler(this.quanLySanPhamToolStripMenuItem_Click);
            // 
            // quanLyHoaDonToolStripMenuItem
            // 
            this.quanLyHoaDonToolStripMenuItem.AutoSize = false;
            this.quanLyHoaDonToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quanLyHoaDonToolStripMenuItem.Name = "quanLyHoaDonToolStripMenuItem";
            this.quanLyHoaDonToolStripMenuItem.Size = new System.Drawing.Size(150, 40);
            this.quanLyHoaDonToolStripMenuItem.Text = "Quản lý hóa đơn";
            this.quanLyHoaDonToolStripMenuItem.Click += new System.EventHandler(this.quanLyHoaDonToolStripMenuItem_Click);
            // 
            // quanLyBinhLuanToolStripMenuItem
            // 
            this.quanLyBinhLuanToolStripMenuItem.AutoSize = false;
            this.quanLyBinhLuanToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quanLyBinhLuanToolStripMenuItem.Name = "quanLyBinhLuanToolStripMenuItem";
            this.quanLyBinhLuanToolStripMenuItem.Size = new System.Drawing.Size(160, 40);
            this.quanLyBinhLuanToolStripMenuItem.Text = "Quản lý bình luận";
            this.quanLyBinhLuanToolStripMenuItem.Click += new System.EventHandler(this.quanLyBinhLuanToolStripMenuItem_Click);
            // 
            // baoCaoThongKeToolStripMenuItem
            // 
            this.baoCaoThongKeToolStripMenuItem.AutoSize = false;
            this.baoCaoThongKeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.baoCaoThongKeToolStripMenuItem.Name = "baoCaoThongKeToolStripMenuItem";
            this.baoCaoThongKeToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.baoCaoThongKeToolStripMenuItem.Text = "Báo cáo và Thống kê";
            this.baoCaoThongKeToolStripMenuItem.Click += new System.EventHandler(this.baoCaoThongKeToolStripMenuItem_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 54);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1106, 582);
            this.pnlContent.TabIndex = 1;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // Form_QuanLy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1106, 636);
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
        private System.Windows.Forms.ToolStripMenuItem quanLyTaiKhoanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanLyDanhMucToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanLySanPhamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanLyHoaDonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanLyBinhLuanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baoCaoThongKeToolStripMenuItem;
        private System.Windows.Forms.Panel pnlContent;
    }
}