namespace QuanLyShopBanDoDaBong
{
    partial class Form_BinhLuan
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
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.chkLocNgay = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbTinhTrang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.grpAction = new System.Windows.Forms.GroupBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvBinhLuan = new System.Windows.Forms.DataGridView();
            this.btnXuatXML = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBinhLuan)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(380, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "QUẢN LÝ BÌNH LUẬN";
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.chkLocNgay);
            this.grpSearch.Controls.Add(this.label3);
            this.grpSearch.Controls.Add(this.cbbTinhTrang);
            this.grpSearch.Controls.Add(this.label2);
            this.grpSearch.Controls.Add(this.dtpNgay);
            this.grpSearch.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpSearch.Location = new System.Drawing.Point(50, 90);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(650, 150);
            this.grpSearch.TabIndex = 1;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Tiêu chí tìm kiếm";
            // 
            // chkLocNgay
            // 
            this.chkLocNgay.AutoSize = true;
            this.chkLocNgay.Location = new System.Drawing.Point(610, 42);
            this.chkLocNgay.Name = "chkLocNgay";
            this.chkLocNgay.Size = new System.Drawing.Size(22, 21);
            this.chkLocNgay.TabIndex = 6;
            this.chkLocNgay.UseVisualStyleBackColor = true;
            this.chkLocNgay.CheckedChanged += new System.EventHandler(this.chkLocNgay_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(30, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "Trạng thái :";
            // 
            // cbbTinhTrang
            // 
            this.cbbTinhTrang.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbbTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTinhTrang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbTinhTrang.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbTinhTrang.FormattingEnabled = true;
            this.cbbTinhTrang.Location = new System.Drawing.Point(150, 82);
            this.cbbTinhTrang.Name = "cbbTinhTrang";
            this.cbbTinhTrang.Size = new System.Drawing.Size(450, 36);
            this.cbbTinhTrang.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(30, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ngày bình :";
            // 
            // dtpNgay
            // 
            this.dtpNgay.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.dtpNgay.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Location = new System.Drawing.Point(150, 38);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(450, 35);
            this.dtpNgay.TabIndex = 0;
            // 
            // grpAction
            // 
            this.grpAction.Controls.Add(this.btnXuatXML);
            this.grpAction.Controls.Add(this.btnLamMoi);
            this.grpAction.Controls.Add(this.btnTimKiem);
            this.grpAction.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpAction.Location = new System.Drawing.Point(730, 90);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(320, 171);
            this.grpAction.TabIndex = 2;
            this.grpAction.TabStop = false;
            this.grpAction.Text = "Chức năng";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(171)))), ((int)(((byte)(227)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(170, 50);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 45);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(171)))), ((int)(((byte)(227)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(30, 50);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(120, 45);
            this.btnTimKiem.TabIndex = 0;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click_1);
            // 
            // dgvBinhLuan
            // 
            this.dgvBinhLuan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBinhLuan.BackgroundColor = System.Drawing.Color.White;
            this.dgvBinhLuan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBinhLuan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBinhLuan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBinhLuan.ColumnHeadersHeight = 35;
            this.dgvBinhLuan.EnableHeadersVisualStyles = false;
            this.dgvBinhLuan.Location = new System.Drawing.Point(50, 298);
            this.dgvBinhLuan.Name = "dgvBinhLuan";
            this.dgvBinhLuan.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBinhLuan.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBinhLuan.RowTemplate.Height = 24;
            this.dgvBinhLuan.Size = new System.Drawing.Size(1000, 300);
            this.dgvBinhLuan.TabIndex = 3;
            // 
            // btnXuatXML
            // 
            this.btnXuatXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(171)))), ((int)(((byte)(227)))));
            this.btnXuatXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatXML.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatXML.ForeColor = System.Drawing.Color.White;
            this.btnXuatXML.Location = new System.Drawing.Point(30, 105);
            this.btnXuatXML.Name = "btnXuatXML";
            this.btnXuatXML.Size = new System.Drawing.Size(260, 45);
            this.btnXuatXML.TabIndex = 5;
            this.btnXuatXML.Text = "Xuất XML";
            this.btnXuatXML.UseVisualStyleBackColor = false;
            this.btnXuatXML.Click += new System.EventHandler(this.btnXuatXML_Click);
            // 
            // Form_BinhLuan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 652);
            this.Controls.Add(this.dgvBinhLuan);
            this.Controls.Add(this.grpAction);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.pnlHeader);
            this.Name = "Form_BinhLuan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý bình luận";
            this.Load += new System.EventHandler(this.Form_BinhLuan_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBinhLuan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.GroupBox grpAction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbTinhTrang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvBinhLuan;
        private System.Windows.Forms.CheckBox chkLocNgay;
        private System.Windows.Forms.Button btnXuatXML;
    }
}