using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        // 1. Chuỗi kết nối (Nếu bạn dùng App.config thì thay bằng ConfigurationManager)
        private string connectionString = "Data Source=localhost; Initial Catalog=FootballShop; Integrated Security=True";

        // 2. Biến quan trọng: Lưu ID của dòng đang chọn để Sửa hoặc Xóa
        private int idHienTai = -1;

        public Form_QLTaiKhoan()
        {
            InitializeComponent();
        }

        // --- SỰ KIỆN KHI FORM LOAD ---
        private void Form_QLTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadVaiTroComboBox(); // Nạp combobox Vai trò
            LoadNguoiDung();      // Nạp dữ liệu lên lưới
        }

        // --- CÁC HÀM HỖ TRỢ LOAD DỮ LIỆU ---
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
                    DataTable dt = new DataTable("NguoiDung"); // Đặt tên bảng để dùng cho XML sau này
                    adapter.Fill(dt);
                    dgvtaikhoan.DataSource = dt;

                    // Đổi tên cột hiển thị cho đẹp
                    if (dgvtaikhoan.Columns["IDNguoiDung"] != null) dgvtaikhoan.Columns["IDNguoiDung"].HeaderText = "ID";
                    if (dgvtaikhoan.Columns["Email"] != null) dgvtaikhoan.Columns["Email"].HeaderText = "Email (Tài khoản)";
                    if (dgvtaikhoan.Columns["password"] != null) dgvtaikhoan.Columns["password"].HeaderText = "Mật khẩu";
                    if (dgvtaikhoan.Columns["VaiTro"] != null) dgvtaikhoan.Columns["VaiTro"].HeaderText = "Vai trò";
                }
                ResetForm(); // Xóa trắng các ô nhập sau khi load
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- SỰ KIỆN CLICK VÀO BẢNG (QUAN TRỌNG NHẤT) ---
        private void dgvtaikhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvtaikhoan.Rows[e.RowIndex];

                // 1. Lưu ID dòng đang chọn vào biến toàn cục
                if (row.Cells["IDNguoiDung"].Value != DBNull.Value)
                {
                    idHienTai = Convert.ToInt32(row.Cells["IDNguoiDung"].Value);
                }

                // 2. Đẩy dữ liệu lên các ô nhập
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

        // --- 1. CHỨC NĂNG THÊM ---
        private void btnthem_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhapLieu()) return;

            // Kiểm tra trùng Email
            if (KiemTraEmailTonTai(txtendangnhap.Text.Trim(), -1)) // -1 là thêm mới
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

        // --- 2. CHỨC NĂNG SỬA ---
        private void btnsua_Click(object sender, EventArgs e)
        {
            if (idHienTai == -1)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!");
                return;
            }
            if (!KiemTraNhapLieu()) return;

            // Kiểm tra trùng Email (Trừ chính nó ra)
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

        // --- 3. CHỨC NĂNG XÓA ---
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
                        }
                    }
                    MessageBox.Show("Xóa thành công!");
                    LoadNguoiDung();
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 547) // Lỗi khóa ngoại (FK)
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

        // --- 4. CHỨC NĂNG XUẤT XML ---
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

                // Đặt tên bảng để file XML đẹp hơn
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

        // --- CÁC HÀM KIỂM TRA (VALIDATION) ---

        private void ResetForm()
        {
            txtendangnhap.Clear();
            txtmatkhau.Clear();
            cmbvaitro.SelectedIndex = -1;
            idHienTai = -1; // Reset lại ID
        }

        private bool KiemTraNhapLieu()
        {
            if (string.IsNullOrWhiteSpace(txtendangnhap.Text)) { MessageBox.Show("Chưa nhập Email!"); return false; }
            if (string.IsNullOrWhiteSpace(txtmatkhau.Text)) { MessageBox.Show("Chưa nhập Mật khẩu!"); return false; }
            if (cmbvaitro.SelectedIndex == -1) { MessageBox.Show("Chưa chọn Vai trò!"); return false; }
            return true;
        }

        // Hàm kiểm tra trùng email thông minh (dùng cho cả thêm và sửa)
        private bool KiemTraEmailTonTai(string email, int idLoaiTru)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Nếu là thêm mới (idLoaiTru = -1) => Tìm xem có email nào trùng không
                    // Nếu là sửa (idLoaiTru = 5) => Tìm xem có email nào trùng NHƯNG ID phải KHÁC 5
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
    }
}