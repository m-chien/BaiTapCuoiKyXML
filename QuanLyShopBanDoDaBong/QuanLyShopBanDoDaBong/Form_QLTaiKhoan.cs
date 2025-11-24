using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Xsl;
using System.Text;

namespace WFA_Quanlyquancafe
{
    public partial class Form_QLTaiKhoan : Form
    {
        // Chuỗi kết nối tới cơ sở dữ liệu SQL Server
        private string connectionString = "Data Source=DESKTOP-ER8LV7D; Initial Catalog=FootballShop; Integrated Security=True";

        public Form_QLTaiKhoan()
        {
            InitializeComponent();
        }

        private void dgvtaikhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào một hàng hợp lệ
            if (e.RowIndex >= 0)
            {
                // Lấy dữ liệu từ dòng được chọn trong DataGridView
                DataGridViewRow row = dgvtaikhoan.Rows[e.RowIndex];

                // 1. TextBox: Email (thay vì TenDangNhap)
                txtendangnhap.Text = row.Cells["Email"].Value?.ToString();

                // 2. TextBox: Mật khẩu
                txtmatkhau.Text = row.Cells["password"].Value?.ToString();

                // 3. ComboBox: Vai trò (Admin hoặc User)
                if (row.Cells["VaiTro"].Value != null)
                {
                    string vaiTro = row.Cells["VaiTro"].Value.ToString();

                    // Kiểm tra và chọn Vai Trò trong ComboBox
                    if (cmbvaitro.Items.Contains(vaiTro))
                    {
                        cmbvaitro.SelectedItem = vaiTro;
                    }
                    else
                    {
                        cmbvaitro.SelectedIndex = -1;
                    }
                }
            }
        }

        private void LoadVaiTroComboBox()
        {
            try
            {
                // Thêm 2 vai trò theo database
                cmbvaitro.Items.Clear();
                cmbvaitro.Items.Add("Admin");
                cmbvaitro.Items.Add("User");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu vào ComboBox: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_QLTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadVaiTroComboBox();
            LoadNguoiDung();

            // Đăng ký sự kiện CellClick
            dgvtaikhoan.CellClick += dgvtaikhoan_CellClick;
        }

        private void LoadNguoiDung()
        {
            try
            {
                string fileXML = Path.Combine(Application.StartupPath, "NguoiDung.xml");
                TaoXML taoXML = new TaoXML();

                // Tạo file XML từ database
                string sql = @"SELECT 
                    IDNguoiDung, 
                    Email, 
                    password, 
                    sdt, 
                    DiaChi, 
                    AvatarURL, 
                    VaiTro, 
                    gioitinh 
                FROM NguoiDung";

                taoXML.taoXML(sql, "NguoiDung", fileXML);

                // Load vào DataGridView
                DataTable dt = taoXML.loadDataGridView(fileXML);
                dgvtaikhoan.DataSource = dt;

                // Tùy chỉnh hiển thị các cột
                if (dgvtaikhoan.Columns.Count > 0)
                {
                    dgvtaikhoan.Columns["IDNguoiDung"].HeaderText = "ID";
                    dgvtaikhoan.Columns["Email"].HeaderText = "Email";
                    dgvtaikhoan.Columns["password"].HeaderText = "Mật khẩu";
                    dgvtaikhoan.Columns["sdt"].HeaderText = "Số điện thoại";
                    dgvtaikhoan.Columns["DiaChi"].HeaderText = "Địa chỉ";
                    dgvtaikhoan.Columns["VaiTro"].HeaderText = "Vai trò";
                    dgvtaikhoan.Columns["gioitinh"].HeaderText = "Giới tính";
                    dgvtaikhoan.Columns["AvatarURL"].HeaderText = "Avatar";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                string email = txtendangnhap.Text.Trim();
                string matKhau = txtmatkhau.Text.Trim();

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Vui lòng nhập Email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(matKhau))
                {
                    MessageBox.Show("Vui lòng nhập Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbvaitro.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn Vai trò!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string vaiTro = cmbvaitro.SelectedItem.ToString();

                // Kiểm tra email đã tồn tại chưa
                if (!KiemTraEmailTonTai(email))
                {
                    // Thêm người dùng mới vào database
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string sql = @"INSERT INTO NguoiDung (Email, password, VaiTro) 
                                     VALUES (@Email, @Password, @VaiTro)";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", matKhau);
                        cmd.Parameters.AddWithValue("@VaiTro", vaiTro);

                        cmd.ExecuteNonQuery();
                    }

                    // Load lại dữ liệu
                    LoadNguoiDung();

                    // Xóa các trường nhập liệu
                    txtendangnhap.Clear();
                    txtmatkhau.Clear();
                    cmbvaitro.SelectedIndex = -1;

                    MessageBox.Show("Thêm người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Email đã tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool KiemTraEmailTonTai(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM NguoiDung WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Phương thức chuyendoi() để xuất danh sách người dùng ra HTML
        private void chuyendoi()
        {
            try
            {
                string pathXML = Path.Combine(Application.StartupPath, "NguoiDung.xml");
                string pathXSLT = Path.Combine(Application.StartupPath, "NguoiDung.xslt");
                string pathHTML = Path.Combine(Application.StartupPath, "NguoiDung.html");

                // Kiểm tra file XML tồn tại
                if (!File.Exists(pathXML))
                {
                    MessageBox.Show("File XML không tồn tại. Vui lòng tải lại dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra file XSLT
                if (!File.Exists(pathXSLT))
                {
                    // Tạo file XSLT mặc định nếu chưa có
                    TaoFileXSLTMacDinh(pathXSLT);
                }

                // Tạo đối tượng XslCompiledTransform
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(pathXSLT);

                // Thực hiện chuyển đổi
                using (XmlReader xmlReader = XmlReader.Create(pathXML))
                using (XmlWriter writer = XmlWriter.Create(pathHTML, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
                {
                    xslt.Transform(xmlReader, writer);
                }

                // Mở file HTML
                Process.Start(pathHTML);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaoFileXSLTMacDinh(string path)
        {
            string xsltContent = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
    <xsl:template match='/'>
        <html>
            <head>
                <meta charset='UTF-8'/>
                <title>Danh sách người dùng</title>
                <style>
                    body { font-family: Arial, sans-serif; margin: 20px; }
                    h2 { color: #333; text-align: center; }
                    table { width: 100%; border-collapse: collapse; margin-top: 20px; }
                    th { background-color: #4CAF50; color: white; padding: 12px; text-align: left; }
                    td { padding: 10px; border: 1px solid #ddd; }
                    tr:nth-child(even) { background-color: #f2f2f2; }
                    tr:hover { background-color: #ddd; }
                </style>
            </head>
            <body>
                <h2>DANH SÁCH NGƯỜI DÙNG</h2>
                <table>
                    <tr>
                        <th>ID</th>
                        <th>Email</th>
                        <th>Mật khẩu</th>
                        <th>SĐT</th>
                        <th>Địa chỉ</th>
                        <th>Vai trò</th>
                        <th>Giới tính</th>
                    </tr>
                    <xsl:for-each select='NewDataSet/NguoiDung'>
                        <tr>
                            <td><xsl:value-of select='IDNguoiDung'/></td>
                            <td><xsl:value-of select='Email'/></td>
                            <td><xsl:value-of select='password'/></td>
                            <td><xsl:value-of select='sdt'/></td>
                            <td><xsl:value-of select='DiaChi'/></td>
                            <td><xsl:value-of select='VaiTro'/></td>
                            <td><xsl:value-of select='gioitinh'/></td>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>";

            File.WriteAllText(path, xsltContent, Encoding.UTF8);
        }

        private void btnxml_Click(object sender, EventArgs e)
        {
            chuyendoi();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có hàng nào được chọn không
                if (dgvtaikhoan.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy ID người dùng từ hàng được chọn
                int idNguoiDung = Convert.ToInt32(dgvtaikhoan.SelectedRows[0].Cells["IDNguoiDung"].Value);
                string email = dgvtaikhoan.SelectedRows[0].Cells["Email"].Value.ToString();

                // Xác nhận trước khi xóa
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa người dùng '{email}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Xóa người dùng
                        string sql = "DELETE FROM NguoiDung WHERE IDNguoiDung = @IDNguoiDung";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@IDNguoiDung", idNguoiDung);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Load lại dữ liệu
                            LoadNguoiDung();

                            // Xóa các trường nhập liệu
                            txtendangnhap.Clear();
                            txtmatkhau.Clear();
                            cmbvaitro.SelectedIndex = -1;
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Xử lý lỗi ràng buộc khóa ngoại
                if (sqlEx.Number == 547)
                {
                    MessageBox.Show("Không thể xóa người dùng này vì đã có dữ liệu liên quan (hóa đơn, bình luận,...)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}