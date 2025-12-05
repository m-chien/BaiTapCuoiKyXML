namespace QuanLyShopBanDoDaBong
{
    partial class Form_QLSanPham
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.cboDanhMuc = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDVT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHang = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpAction = new System.Windows.Forms.GroupBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnXML = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvSanpham = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.grpAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanpham)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1100, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(450, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "QUẢN LÝ SẢN PHẨM";
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.cboDanhMuc);
            this.grpInfo.Controls.Add(this.label7);
            this.grpInfo.Controls.Add(this.txtSoLuong);
            this.grpInfo.Controls.Add(this.label5);
            this.grpInfo.Controls.Add(this.txtDVT);
            this.grpInfo.Controls.Add(this.label4);
            this.grpInfo.Controls.Add(this.txtHang);
            this.grpInfo.Controls.Add(this.label3);
            this.grpInfo.Controls.Add(this.txtTenSP);
            this.grpInfo.Controls.Add(this.label2);
            this.grpInfo.Controls.Add(this.txtMaSP);
            this.grpInfo.Controls.Add(this.label6);
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpInfo.Location = new System.Drawing.Point(50, 90);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(520, 250);
            this.grpInfo.TabIndex = 1;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Thông tin chi tiết";
            // 
            // cboDanhMuc
            // 
            this.cboDanhMuc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDanhMuc.FormattingEnabled = true;
            this.cboDanhMuc.Location = new System.Drawing.Point(150, 80);
            this.cboDanhMuc.Name = "cboDanhMuc";
            this.cboDanhMuc.Size = new System.Drawing.Size(330, 36);
            this.cboDanhMuc.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(20, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 30);
            this.label7.TabIndex = 10;
            this.label7.Text = "Danh mục :";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSoLuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoLuong.Location = new System.Drawing.Point(150, 215);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(330, 35);
            this.txtSoLuong.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(20, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 30);
            this.label5.TabIndex = 8;
            this.label5.Text = "Số lượng :";
            // 
            // txtDVT
            // 
            this.txtDVT.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDVT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDVT.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDVT.Location = new System.Drawing.Point(150, 180);
            this.txtDVT.Name = "txtDVT";
            this.txtDVT.Size = new System.Drawing.Size(330, 35);
            this.txtDVT.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(20, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 30);
            this.label4.TabIndex = 6;
            this.label4.Text = "Đơn vị tính :";
            // 
            // txtHang
            // 
            this.txtHang.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHang.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHang.Location = new System.Drawing.Point(150, 145);
            this.txtHang.Name = "txtHang";
            this.txtHang.Size = new System.Drawing.Size(330, 35);
            this.txtHang.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(20, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hãng :";
            // 
            // txtTenSP
            // 
            this.txtTenSP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTenSP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenSP.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenSP.Location = new System.Drawing.Point(150, 115);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(330, 35);
            this.txtTenSP.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(20, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên sản phẩm :";
            // 
            // txtMaSP
            // 
            this.txtMaSP.BackColor = System.Drawing.Color.White;
            this.txtMaSP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaSP.Enabled = false;
            this.txtMaSP.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaSP.Location = new System.Drawing.Point(150, 40);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.ReadOnly = true;
            this.txtMaSP.Size = new System.Drawing.Size(330, 35);
            this.txtMaSP.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(20, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "Mã SP :";
            // 
            // grpAction
            // 
            this.grpAction.Controls.Add(this.btnTimKiem);
            this.grpAction.Controls.Add(this.txtTimKiem);
            this.grpAction.Controls.Add(this.btnXML);
            this.grpAction.Controls.Add(this.btnXoa);
            this.grpAction.Controls.Add(this.btnSua);
            this.grpAction.Controls.Add(this.btnThem);
            this.grpAction.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpAction.Location = new System.Drawing.Point(590, 90);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(460, 250);
            this.grpAction.TabIndex = 2;
            this.grpAction.TabStop = false;
            this.grpAction.Text = "Chức năng";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.Silver;
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.Black;
            this.btnTimKiem.Location = new System.Drawing.Point(320, 39);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 31);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BackColor = System.Drawing.Color.White;
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(40, 40);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(260, 35);
            this.txtTimKiem.TabIndex = 4;
            // 
            // btnXML
            // 
            this.btnXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(171)))), ((int)(((byte)(227)))));
            this.btnXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXML.ForeColor = System.Drawing.Color.White;
            this.btnXML.Location = new System.Drawing.Point(260, 160);
            this.btnXML.Name = "btnXML";
            this.btnXML.Size = new System.Drawing.Size(160, 45);
            this.btnXML.TabIndex = 3;
            this.btnXML.Text = "Tạo XML";
            this.btnXML.UseVisualStyleBackColor = false;
            this.btnXML.Click += new System.EventHandler(this.btnXML_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(171)))), ((int)(((byte)(227)))));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(40, 160);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(160, 45);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(171)))), ((int)(((byte)(227)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(260, 90);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(160, 45);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(171)))), ((int)(((byte)(227)))));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(40, 90);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(160, 45);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvSanpham
            // 
            this.dgvSanpham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanpham.BackgroundColor = System.Drawing.Color.White;
            this.dgvSanpham.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSanpham.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSanpham.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSanpham.ColumnHeadersHeight = 35;
            this.dgvSanpham.EnableHeadersVisualStyles = false;
            this.dgvSanpham.Location = new System.Drawing.Point(50, 360);
            this.dgvSanpham.Name = "dgvSanpham";
            this.dgvSanpham.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSanpham.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSanpham.RowTemplate.Height = 24;
            this.dgvSanpham.Size = new System.Drawing.Size(1000, 200);
            this.dgvSanpham.TabIndex = 3;
            this.dgvSanpham.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanpham_CellClick);
            this.dgvSanpham.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanpham_CellContentClick);
            // 
            // Form_QLSanPham
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.dgvSanpham);
            this.Controls.Add(this.grpAction);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.pnlHeader);
            this.Name = "Form_QLSanPham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý sản phẩm";
            this.Load += new System.EventHandler(this.Form_QLSanpham_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.grpAction.ResumeLayout(false);
            this.grpAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanpham)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.GroupBox grpAction;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDVT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXML;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvSanpham;
        private System.Windows.Forms.ComboBox cboDanhMuc;
        private System.Windows.Forms.Label label7;
    }
}