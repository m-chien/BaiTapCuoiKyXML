using QuanLyShopBanDoDaBong.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_BinhLuan : Form
    {
        BinhLuan objBL = new BinhLuan();

        public Form_BinhLuan()
        {
            InitializeComponent();
        }

        private void Form_BinhLuan_Load(object sender, EventArgs e)
        {
            cbbTinhTrang.Items.Clear();
            cbbTinhTrang.Items.AddRange(new string[] { "Tất cả", "Chờ duyệt", "Đã duyệt" });
            cbbTinhTrang.SelectedIndex = 0;

            dtpNgay.Format = DateTimePickerFormat.Custom;
            dtpNgay.CustomFormat = "dd/MM/yyyy";

            if (chkLocNgay != null)
            {
                chkLocNgay.Checked = false;
                dtpNgay.Enabled = false;
            }

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvBinhLuan.DataSource = objBL.LayDanhSach();

                if (dgvBinhLuan.Columns.Count > 0)
                {
                    if (dgvBinhLuan.Columns.Contains("IDBinhLuan"))
                        dgvBinhLuan.Columns["IDBinhLuan"].HeaderText = "Mã BL";
                    if (dgvBinhLuan.Columns.Contains("IDNguoiDung"))
                        dgvBinhLuan.Columns["IDNguoiDung"].HeaderText = "Mã người dùng";
                    if (dgvBinhLuan.Columns.Contains("IdSanPham"))
                        dgvBinhLuan.Columns["IdSanPham"].HeaderText = "Mã sản phẩm";
                    if (dgvBinhLuan.Columns.Contains("NoiDung"))
                        dgvBinhLuan.Columns["NoiDung"].HeaderText = "Nội dung";
                    if (dgvBinhLuan.Columns.Contains("NgayBinhLuan"))
                        dgvBinhLuan.Columns["NgayBinhLuan"].HeaderText = "Ngày bình luận";
                    if (dgvBinhLuan.Columns.Contains("TinhTrang"))
                        dgvBinhLuan.Columns["TinhTrang"].HeaderText = "Tình trạng";

                    dgvBinhLuan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }

                this.Text = $"Quản lý bình luận - Tìm thấy {dgvBinhLuan.Rows.Count} kết quả";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            try
            {
                string tinhTrang = cbbTinhTrang.SelectedItem?.ToString() ?? "Tất cả";

                DateTime? ngayBinhLuan = null;
                if (chkLocNgay != null && chkLocNgay.Checked)
                    ngayBinhLuan = dtpNgay.Value.Date;

                DataTable ketQua = objBL.TimKiem(
                    tinhTrang,
                    ngayBinhLuan
                );

                dgvBinhLuan.DataSource = ketQua;

                this.Text = $"Quản lý bình luận - Tìm thấy {ketQua.Rows.Count} kết quả";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbbTinhTrang.SelectedIndex = 0;
            if (chkLocNgay != null)
            {
                chkLocNgay.Checked = false;
                dtpNgay.Enabled = false;
            }
            LoadData();
        }

        private void btnXuatXML_Click(object sender, EventArgs e)
        {
            try
            {
                objBL.KhoiTaoXML();
                MessageBox.Show("Đã đồng bộ lại từ SQL!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo XML: " + ex.Message);
            }
        }

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (dtpNgay != null)
            {
                dtpNgay.Enabled = chkLocNgay.Checked;
            }
        }

        private void dgvBinhLuan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}