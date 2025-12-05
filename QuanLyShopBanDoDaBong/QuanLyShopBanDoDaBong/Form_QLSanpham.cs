using QuanLyShopBanDoDaBong.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_QLSanPham : Form
    {
        SanPham objSP = new SanPham();
        string idHienTai = "";

        public Form_QLSanPham()
        {
            InitializeComponent();
        }

        private void Form_QLSanpham_Load(object sender, EventArgs e)
        {
            txtMaSP.Enabled = false;
            LoadComboBox(); // <--- MỚI: Load danh mục vào ComboBox
            LoadData();
        }

        // --- MỚI: Hàm load ComboBox ---
        private void LoadComboBox()
        {
            try
            {
                DataTable dtDM = objSP.LayDanhSachDanhMuc();
                // Kiểm tra xem file DanhMuc.xml có dữ liệu không
                if (dtDM.Rows.Count > 0)
                {
                    cboDanhMuc.DataSource = dtDM;
                    cboDanhMuc.DisplayMember = "TenDanhmuc"; // Tên hiển thị (Người dùng thấy)
                    cboDanhMuc.ValueMember = "IDDanhMuc";    // Giá trị thực (ID lưu vào DB)
                    cboDanhMuc.SelectedIndex = -1; // Mặc định không chọn gì
                }
            }
            catch { /* Bỏ qua lỗi nếu chưa có file danh mục */ }
        }

        private void LoadData()
        {
            try
            {
                dgvSanpham.DataSource = objSP.LayDanhSach();

                if (dgvSanpham.Columns.Count > 0)
                {
                    if (dgvSanpham.Columns.Contains("IDSanPham")) dgvSanpham.Columns["IDSanPham"].HeaderText = "Mã SP";
                    if (dgvSanpham.Columns.Contains("mota")) dgvSanpham.Columns["mota"].HeaderText = "Tên Sản Phẩm";
                    if (dgvSanpham.Columns.Contains("Hang")) dgvSanpham.Columns["Hang"].HeaderText = "Hãng";
                    if (dgvSanpham.Columns.Contains("DonViTinh")) dgvSanpham.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
                    if (dgvSanpham.Columns.Contains("SoLuongTonKho")) dgvSanpham.Columns["SoLuongTonKho"].HeaderText = "Số lượng";

                    // Ẩn cột không cần thiết
                    if (dgvSanpham.Columns.Contains("IDdanhMuc")) dgvSanpham.Columns["IDdanhMuc"].Visible = false;
                    if (dgvSanpham.Columns.Contains("hinhanh")) dgvSanpham.Columns["hinhanh"].Visible = false;
                    if (dgvSanpham.Columns.Contains("KichThuoc")) dgvSanpham.Columns["KichThuoc"].Visible = false;
                    if (dgvSanpham.Columns.Contains("mausac")) dgvSanpham.Columns["mausac"].Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load dữ liệu: " + ex.Message); }
            ResetForm();
        }

        private void ResetForm()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtHang.Clear();
            txtDVT.Clear();
            txtSoLuong.Clear();
            cboDanhMuc.SelectedIndex = -1; // Reset ComboBox
            idHienTai = "";
            dgvSanpham.ClearSelection();
        }

        private void dgvSanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSanpham.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvSanpham.Rows[e.RowIndex];
                    if (row.Cells["IDSanPham"].Value != null)
                    {
                        idHienTai = row.Cells["IDSanPham"].Value.ToString();
                        txtMaSP.Text = idHienTai;
                        txtTenSP.Text = row.Cells["mota"].Value?.ToString() ?? "";
                        txtHang.Text = row.Cells["Hang"].Value?.ToString() ?? "";
                        txtDVT.Text = row.Cells["DonViTinh"].Value?.ToString() ?? "";
                        txtSoLuong.Text = row.Cells["SoLuongTonKho"].Value?.ToString() ?? "0";

                        // --- MỚI: Chọn lại Danh mục trên ComboBox ---
                        if (row.Cells["IDdanhMuc"].Value != null)
                        {
                            cboDanhMuc.SelectedValue = row.Cells["IDdanhMuc"].Value.ToString();
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi chọn dòng: " + ex.Message); }
            }
        }

        // --- THÊM (Cập nhật lấy ID từ ComboBox) ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text)) { MessageBox.Show("Chưa nhập tên SP!"); return; }
            if (cboDanhMuc.SelectedValue == null) { MessageBox.Show("Chưa chọn danh mục!"); return; }

            try
            {
                string idDM = cboDanhMuc.SelectedValue.ToString(); // Lấy ID danh mục
                objSP.ThemSP(txtTenSP.Text, idDM, txtHang.Text, txtDVT.Text, txtSoLuong.Text);

                MessageBox.Show("Thêm thành công!");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm: " + ex.Message); }
        }

        // --- SỬA (Cập nhật lấy ID từ ComboBox) ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idHienTai)) { MessageBox.Show("Chọn SP cần sửa!"); return; }
            if (cboDanhMuc.SelectedValue == null) { MessageBox.Show("Chưa chọn danh mục!"); return; }

            try
            {
                string idDM = cboDanhMuc.SelectedValue.ToString(); // Lấy ID danh mục
                objSP.SuaSP(idHienTai, txtTenSP.Text, idDM, txtHang.Text, txtDVT.Text, txtSoLuong.Text);

                MessageBox.Show("Sửa thành công!");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi sửa: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idHienTai)) { MessageBox.Show("Chọn SP cần xóa!"); return; }
            if (MessageBox.Show("Xóa SP này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                objSP.XoaSP(idHienTai);
                MessageBox.Show("Đã xóa!");
                LoadData();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string kw = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(kw)) LoadData();
            else dgvSanpham.DataSource = objSP.TimKiem(kw);
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            objSP.KhoiTaoXML();
            MessageBox.Show("Đã đồng bộ lại từ SQL!");
            LoadData();
        }

        private void dgvSanpham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}