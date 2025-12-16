using System;
using System.Data;
using System.Windows.Forms;
using QuanLyShopBanDoDaBong.Class;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_QLKhachHang : Form
    {
        KhachHang objTK = new KhachHang();
        string idHienTai = "";

        public Form_QLKhachHang()
        {
            InitializeComponent();
        }

        private void Form_QLTaiKhoan_Load(object sender, EventArgs e)
        {
            cmbvaitro.Items.Add("Admin");
            cmbvaitro.Items.Add("User");
            cmbGioiTinh.Items.Add("Nam");
            cmbGioiTinh.Items.Add("Nữ");
            cmbGioiTinh.SelectedIndex = 0;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvtaikhoan.DataSource = objTK.LayDanhSach();

                if (dgvtaikhoan.Columns.Count > 0)
                {
                    if (dgvtaikhoan.Columns.Contains("IDNguoiDung")) dgvtaikhoan.Columns["IDNguoiDung"].HeaderText = "ID";
                    if (dgvtaikhoan.Columns.Contains("Email")) dgvtaikhoan.Columns["Email"].HeaderText = "Email (Tài khoản)";
                    if (dgvtaikhoan.Columns.Contains("sdt")) dgvtaikhoan.Columns["sdt"].HeaderText = "SĐT";
                    if (dgvtaikhoan.Columns.Contains("DiaChi")) dgvtaikhoan.Columns["DiaChi"].HeaderText = "Địa chỉ";
                    if (dgvtaikhoan.Columns.Contains("gioitinh")) dgvtaikhoan.Columns["gioitinh"].HeaderText = "Giới tính";
                    if (dgvtaikhoan.Columns.Contains("AvatarURL")) dgvtaikhoan.Columns["AvatarURL"].HeaderText = "Avatar";

                    if (dgvtaikhoan.Columns.Contains("password")) dgvtaikhoan.Columns["password"].Visible = false;
                    if (dgvtaikhoan.Columns.Contains("VaiTro")) dgvtaikhoan.Columns["VaiTro"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
            ResetForm();
        }

        private void dgvtaikhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvtaikhoan.Rows[e.RowIndex];

                if (row.Cells["IDNguoiDung"].Value != null)
                    idHienTai = row.Cells["IDNguoiDung"].Value.ToString();

                if (row.Cells["Email"].Value != null)
                    txtendangnhap.Text = row.Cells["Email"].Value.ToString();

                txtSDT.Text = row.Cells["sdt"].Value != null ? row.Cells["sdt"].Value.ToString() : "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value != null ? row.Cells["DiaChi"].Value.ToString() : "";
                txtAvatar.Text = row.Cells["AvatarURL"].Value != null ? row.Cells["AvatarURL"].Value.ToString() : "";
                cmbGioiTinh.Text = row.Cells["gioitinh"].Value != null ? row.Cells["gioitinh"].Value.ToString() : "";
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtendangnhap.Text))
            {
                MessageBox.Show("Vui lòng nhập Email!");
                return;
            }

            if (objTK.KiemTraEmail(txtendangnhap.Text))
            {
                MessageBox.Show("Email đã tồn tại!");
                return;
            }

            objTK.ThemTK(
                txtendangnhap.Text,
                txtSDT.Text,
                txtDiaChi.Text,
                txtAvatar.Text,
                cmbGioiTinh.Text
            );

            MessageBox.Show("Thêm thành công! (Mật khẩu mặc định: 123456)");
            LoadData();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idHienTai))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!");
                return;
            }

            objTK.SuaTK(
                idHienTai,
                txtendangnhap.Text,
                txtSDT.Text,
                txtDiaChi.Text,
                txtAvatar.Text,
                cmbGioiTinh.Text
            );

            MessageBox.Show("Cập nhật thông tin thành công!");
            LoadData();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idHienTai))
            {
                MessageBox.Show("Chọn tài khoản cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                objTK.XoaTK(idHienTai);
                MessageBox.Show("Xóa thành công!");
                LoadData();
            }
        }

        private void btnxml_Click(object sender, EventArgs e)
        {
            objTK.KhoiTaoXML();
            MessageBox.Show("Đã đồng bộ dữ liệu từ SQL sang XML thành công!");
            LoadData();
        }

        private void ResetForm()
        {
            idHienTai = "";
            txtendangnhap.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtAvatar.Clear();
            cmbGioiTinh.SelectedIndex = -1;

            txtendangnhap.Focus();
        }
        private void btnsua_Click_1(object sender, EventArgs e) { btnsua_Click(sender, e); }
        private void dgvtaikhoan_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e) { }
    }
}