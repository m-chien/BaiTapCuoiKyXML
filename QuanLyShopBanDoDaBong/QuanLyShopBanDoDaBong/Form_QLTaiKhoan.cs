using System;
using System.Data;
using System.Windows.Forms;
using QuanLyShopBanDoDaBong.Class;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_QLTaiKhoan : Form
    {
        TaiKhoan objTK = new TaiKhoan();
        string idHienTai = "";

        public Form_QLTaiKhoan()
        {
            InitializeComponent();
        }

        private void Form_QLTaiKhoan_Load(object sender, EventArgs e)
        {
            cmbvaitro.Items.Add("Admin");
            cmbvaitro.Items.Add("User");

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
                    if (dgvtaikhoan.Columns.Contains("Email")) dgvtaikhoan.Columns["Email"].HeaderText = "Tài khoản";
                    if (dgvtaikhoan.Columns.Contains("password")) dgvtaikhoan.Columns["password"].HeaderText = "Mật khẩu";
                    if (dgvtaikhoan.Columns.Contains("VaiTro")) dgvtaikhoan.Columns["VaiTro"].HeaderText = "Vai trò";

                    if (dgvtaikhoan.Columns.Contains("sdt")) dgvtaikhoan.Columns["sdt"].Visible = false;
                    if (dgvtaikhoan.Columns.Contains("DiaChi")) dgvtaikhoan.Columns["DiaChi"].Visible = false;
                    if (dgvtaikhoan.Columns.Contains("AvatarURL")) dgvtaikhoan.Columns["AvatarURL"].Visible = false;
                    if (dgvtaikhoan.Columns.Contains("gioitinh")) dgvtaikhoan.Columns["gioitinh"].Visible = false;
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
                if (row.Cells["IDNguoiDung"].Value != null) idHienTai = row.Cells["IDNguoiDung"].Value.ToString();
                if (row.Cells["Email"].Value != null) txtendangnhap.Text = row.Cells["Email"].Value.ToString();
                if (row.Cells["password"].Value != null) txtmatkhau.Text = row.Cells["password"].Value.ToString();
                if (row.Cells["VaiTro"].Value != null) cmbvaitro.Text = row.Cells["VaiTro"].Value.ToString();
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtendangnhap.Text)) return;

            if (objTK.KiemTraEmail(txtendangnhap.Text))
            {
                MessageBox.Show("Email đã tồn tại!");
                return;
            }

            objTK.ThemTK(txtendangnhap.Text, txtmatkhau.Text, cmbvaitro.Text);
            MessageBox.Show("Thêm thành công (XML & SQL)!");
            LoadData();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idHienTai))
            {
                MessageBox.Show("Chọn tài khoản cần sửa!");
                return;
            }

            objTK.SuaTK(idHienTai, txtendangnhap.Text, txtmatkhau.Text, cmbvaitro.Text);
            MessageBox.Show("Cập nhật thành công (XML & SQL)!");
            LoadData();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idHienTai))
            {
                MessageBox.Show("Chọn tài khoản cần xóa!");
                return;
            }

            if (MessageBox.Show("Xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                objTK.XoaTK(idHienTai);
                MessageBox.Show("Xóa thành công (XML & SQL)!");
                LoadData();
            }
        }

        private void btnxml_Click(object sender, EventArgs e)
        {
            objTK.KhoiTaoXML();
            MessageBox.Show("Đã đồng bộ dữ liệu từ SQL sang XML!");
            LoadData();
        }

        private void ResetForm()
        {
            txtendangnhap.Clear();
            txtmatkhau.Clear();
            cmbvaitro.SelectedIndex = -1;
            idHienTai = "";
        }

        private void btnsua_Click_1(object sender, EventArgs e) { btnsua_Click(sender, e); }
        private void dgvtaikhoan_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}