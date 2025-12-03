using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient; // Thư viện kết nối SQL
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml; // Thư viện XML

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_QLTaiKhoan : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnect"].ConnectionString;

        private int idHienTai = -1;

        public Form_QLTaiKhoan()
        {
            InitializeComponent();
        }

        private void Form_QLTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadVaiTroComboBox(); 
            LoadNguoiDung();    
        }

        private void LoadVaiTroComboBox()
        {
            cmbvaitro.Items.Clear();
            cmbvaitro.Items.Add("Admin");
            cmbvaitro.Items.Add("User");
        }

        private void LoadNguoiDung()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM NguoiDung";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable("NguoiDung"); 
                    adapter.Fill(dt);
                    dgvtaikhoan.DataSource = dt;

                    if (dgvtaikhoan.Columns["IDNguoiDung"] != null) dgvtaikhoan.Columns["IDNguoiDung"].HeaderText = "ID";
                    if (dgvtaikhoan.Columns["Email"] != null) dgvtaikhoan.Columns["Email"].HeaderText = "Tài khoản";
                    if (dgvtaikhoan.Columns["password"] != null) dgvtaikhoan.Columns["password"].HeaderText = "Mật khẩu";
                    if (dgvtaikhoan.Columns["VaiTro"] != null) dgvtaikhoan.Columns["VaiTro"].HeaderText = "Vai trò";
                }
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void dgvtaikhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvtaikhoan.Rows[e.RowIndex];

                if (row.Cells["IDNguoiDung"].Value != DBNull.Value)
                {
                    idHienTai = Convert.ToInt32(row.Cells["IDNguoiDung"].Value);
                }

                txtendangnhap.Text = row.Cells["Email"].Value?.ToString();
                txtmatkhau.Text = row.Cells["password"].Value?.ToString();

                string vaiTro = row.Cells["VaiTro"].Value?.ToString();
                if (!string.IsNullOrEmpty(vaiTro) && cmbvaitro.Items.Contains(vaiTro))
                {
                    cmbvaitro.SelectedItem = vaiTro;
                }
                else
                {
                    cmbvaitro.SelectedIndex = -1;
                }
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhapLieu()) return;

            if (KiemTraEmailTonTai(txtendangnhap.Text.Trim(), -1))
            {
                MessageBox.Show("Email này đã tồn tại!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO NguoiDung (Email, password, VaiTro) VALUES (@Email, @Pass, @Role)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", txtendangnhap.Text.Trim());
                        cmd.Parameters.AddWithValue("@Pass", txtmatkhau.Text.Trim());
                        cmd.Parameters.AddWithValue("@Role", cmbvaitro.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm thành công!");
                LoadNguoiDung();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (idHienTai == -1)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string sql = "DELETE FROM NguoiDung WHERE IDNguoiDung = @ID";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", idHienTai);
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "DELETE FROM HoaDon WHERE IdUser = @ID";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "DELETE FROM BinhLuan WHERE IDNguoiDung = @ID";
                            cmd.ExecuteNonQuery();

                        }
                    }
                    MessageBox.Show("Xóa thành công!");
                    LoadNguoiDung();
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        MessageBox.Show("Không thể xóa tài khoản này vì đã có dữ liệu liên quan (Hóa đơn, v.v)!");
                    else
                        MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khác: " + ex.Message);
                }
            }
        }

        private void btnxml_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dgvtaikhoan.DataSource;
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!");
                    return;
                }

                dt.TableName = "NguoiDung";
                
                string path = Path.Combine(Application.StartupPath, "DanhSachTaiKhoan.xml");
                dt.WriteXml(path, XmlWriteMode.WriteSchema);

                MessageBox.Show("Xuất XML thành công tại:\n" + path);
                System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất XML: " + ex.Message);
            }
        }

        private void ResetForm()
        {
            txtendangnhap.Clear();
            txtmatkhau.Clear();
            cmbvaitro.SelectedIndex = -1;
            idHienTai = -1;
        }

        private bool KiemTraNhapLieu()
        {
            if (string.IsNullOrWhiteSpace(txtendangnhap.Text)) { MessageBox.Show("Chưa nhập Email!"); return false; }
            if (string.IsNullOrWhiteSpace(txtmatkhau.Text)) { MessageBox.Show("Chưa nhập Mật khẩu!"); return false; }
            if (cmbvaitro.SelectedIndex == -1) { MessageBox.Show("Chưa chọn Vai trò!"); return false; }
            return true;
        }
        private bool KiemTraEmailTonTai(string email, int idLoaiTru)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM NguoiDung WHERE Email = @Email AND IDNguoiDung != @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@ID", idLoaiTru); 

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch { return false; }
        }

        private void btnsua_Click_1(object sender, EventArgs e)
        {
            if (idHienTai == -1)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!");
                return;
            }
            if (!KiemTraNhapLieu()) return;

            if (KiemTraEmailTonTai(txtendangnhap.Text.Trim(), idHienTai))
            {
                MessageBox.Show("Email này đang được sử dụng bởi người khác!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE NguoiDung SET Email = @Email, password = @Pass, VaiTro = @Role WHERE IDNguoiDung = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", idHienTai);
                        cmd.Parameters.AddWithValue("@Email", txtendangnhap.Text.Trim());
                        cmd.Parameters.AddWithValue("@Pass", txtmatkhau.Text.Trim());
                        cmd.Parameters.AddWithValue("@Role", cmbvaitro.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cập nhật thành công!");
                LoadNguoiDung();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        private void dgvtaikhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}