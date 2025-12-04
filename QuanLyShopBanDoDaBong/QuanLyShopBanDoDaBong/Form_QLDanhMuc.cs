using QuanLyShopBanDoDaBong.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_QLDanhMuc : Form
    {
        DanhMuc objDM = new DanhMuc();
        string idHienTai = "";

        public Form_QLDanhMuc()
        {
            InitializeComponent();
        }

        private void Form_QLDanhMuc_Load(object sender, EventArgs e)
        {
            txtMaDM.Enabled = false; // Mã tự tăng, không cho sửa
            dtpNgayTao.Format = DateTimePickerFormat.Short; // Hiển thị ngày ngắn gọn
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvDanhMuc.DataSource = objDM.LayDanhSach();

                // Đặt tên cột tiếng Việt
                if (dgvDanhMuc.Columns.Count > 0)
                {
                    if (dgvDanhMuc.Columns.Contains("IDDanhMuc")) dgvDanhMuc.Columns["IDDanhMuc"].HeaderText = "Mã DM";
                    if (dgvDanhMuc.Columns.Contains("TenDanhmuc")) dgvDanhMuc.Columns["TenDanhmuc"].HeaderText = "Tên Danh Mục";
                    if (dgvDanhMuc.Columns.Contains("MoTa")) dgvDanhMuc.Columns["MoTa"].HeaderText = "Mô Tả";
                    if (dgvDanhMuc.Columns.Contains("NgayTao")) dgvDanhMuc.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
            ResetForm();
        }

        private void ResetForm()
        {
            txtMaDM.Clear();
            txtTenDM.Clear();
            txtMoTa.Clear();
            dtpNgayTao.Value = DateTime.Now; // Reset về ngày hiện tại
            txtTimKiem.Clear();
            idHienTai = "";
            dgvDanhMuc.ClearSelection();
        }

        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDanhMuc.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvDanhMuc.Rows[e.RowIndex];
                    if (row.Cells["IDDanhMuc"].Value != null)
                    {
                        idHienTai = row.Cells["IDDanhMuc"].Value.ToString();
                        txtMaDM.Text = idHienTai;
                        txtTenDM.Text = row.Cells["TenDanhmuc"].Value?.ToString() ?? "";
                        txtMoTa.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

                        // Xử lý Ngày tháng (Tránh lỗi nếu dữ liệu rỗng hoặc sai format)
                        if (DateTime.TryParse(row.Cells["NgayTao"].Value?.ToString(), out DateTime date))
                        {
                            dtpNgayTao.Value = date;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi chọn dòng: " + ex.Message); }
            }
        }

        // --- THÊM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDM.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!");
                return;
            }

            try
            {
                // Lấy ngày tháng format chuẩn SQL (yyyy-MM-dd)
                string ngay = dtpNgayTao.Value.ToString("yyyy-MM-dd");

                objDM.ThemDM(txtTenDM.Text, txtMoTa.Text, ngay);
                MessageBox.Show("Thêm thành công!");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // --- SỬA ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idHienTai))
            {
                MessageBox.Show("Chọn danh mục cần sửa!");
                return;
            }

            try
            {
                string ngay = dtpNgayTao.Value.ToString("yyyy-MM-dd");

                objDM.SuaDM(idHienTai, txtTenDM.Text, txtMoTa.Text, ngay);
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // --- XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idHienTai))
            {
                MessageBox.Show("Chọn danh mục cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    objDM.XoaDM(idHienTai);
                    MessageBox.Show("Đã xóa!");
                    LoadData();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        // --- TÌM KIẾM ---
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string kw = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(kw)) LoadData();
            else dgvDanhMuc.DataSource = objDM.TimKiem(kw);
        }

        // --- ĐỒNG BỘ XML ---
        private void btnXML_Click(object sender, EventArgs e)
        {
            objDM.KhoiTaoXML();
            MessageBox.Show("Đã đồng bộ lại từ SQL Server!");
            LoadData();
        }
    }
}