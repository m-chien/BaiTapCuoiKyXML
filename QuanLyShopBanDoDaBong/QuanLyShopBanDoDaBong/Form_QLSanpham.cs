using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Configuration;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_QLSanPham : Form
    {
        string strCon = ConfigurationManager.ConnectionStrings["MyConnect"].ConnectionString;

        public Form_QLSanPham()
        {
            InitializeComponent();
        }

        private void Form_QLSanpham_Load(object sender, EventArgs e)
        {
            LoadData();
            dgvSanpham.CellClick += dgvSanpham_CellClick;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    conn.Open();
                    // Lấy dữ liệu đúng theo tên cột trong DB của bạn
                    string sql = @"SELECT IDSanPham, mota, Hang, DonViTinh, SoLuongTonKho FROM SanPham";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvSanpham.DataSource = dt;
                    dgvSanpham.Columns["IDSanPham"].HeaderText = "Mã SP";
                    dgvSanpham.Columns["mota"].HeaderText = "Tên Sản Phẩm";
                    dgvSanpham.Columns["Hang"].HeaderText = "Hãng";
                    dgvSanpham.Columns["DonViTinh"].HeaderText = "Đơn Vị";
                    dgvSanpham.Columns["SoLuongTonKho"].HeaderText = "Số Lượng";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void ResetForm()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtHang.Clear();
            txtDVT.Clear();
            txtSoLuong.Clear();
            txtTenSP.Focus();
        }

        private void dgvSanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanpham.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["IDSanPham"].Value.ToString();
                txtTenSP.Text = row.Cells["mota"].Value.ToString();
                txtHang.Text = row.Cells["Hang"].Value.ToString();
                txtDVT.Text = row.Cells["DonViTinh"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuongTonKho"].Value.ToString();
            }
        }

        // --- NÚT THÊM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenSP.Text)) { MessageBox.Show("Nhập tên sản phẩm!"); return; }

            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    conn.Open();
                    string sql = @"INSERT INTO SanPham (IDdanhMuc, mota, Hang, DonViTinh, SoLuongTonKho) 
                                   VALUES (1, @mota, @hang, @dvt, @soluong)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@mota", txtTenSP.Text);
                        cmd.Parameters.AddWithValue("@hang", txtHang.Text);
                        cmd.Parameters.AddWithValue("@dvt", txtDVT.Text);
                        int sl = 0; int.TryParse(txtSoLuong.Text, out sl);
                        cmd.Parameters.AddWithValue("@soluong", sl);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm thành công!");
                LoadData();
                ResetForm();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // --- NÚT SỬA ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text)) { MessageBox.Show("Chọn sản phẩm cần sửa!"); return; }

            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    conn.Open();
                    string sql = @"UPDATE SanPham 
                                   SET mota = @mota, Hang = @hang, DonViTinh = @dvt, SoLuongTonKho = @soluong 
                                   WHERE IDSanPham = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", txtMaSP.Text);
                        cmd.Parameters.AddWithValue("@mota", txtTenSP.Text);
                        cmd.Parameters.AddWithValue("@hang", txtHang.Text);
                        cmd.Parameters.AddWithValue("@dvt", txtDVT.Text);
                        int sl = 0; int.TryParse(txtSoLuong.Text, out sl);
                        cmd.Parameters.AddWithValue("@soluong", sl);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
                ResetForm();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // --- NÚT XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text)) { MessageBox.Show("Chọn sản phẩm cần xóa!"); return; }

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(strCon))
                    {
                        conn.Open();
                        string sql = "DELETE FROM SanPham WHERE IDSanPham = @id";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", txtMaSP.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    ResetForm();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa (có thể do ràng buộc dữ liệu): " + ex.Message); }
            }
        }

        // --- TÌM KIẾM ---
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string kw = txtTimKiem.Text.Trim();
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    conn.Open();
                    string sql = @"SELECT IDSanPham, mota, Hang, DonViTinh, SoLuongTonKho 
                                   FROM SanPham WHERE mota LIKE @kw OR Hang LIKE @kw";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@kw", "%" + kw + "%");
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSanpham.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message); }
        }

        // --- XUẤT XML ---
        private void btnXML_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM SanPham", conn);
                    DataTable dt = new DataTable("SanPham");
                    adapter.Fill(dt);
                    string path = Path.Combine(Application.StartupPath, "DanhSachSanPham.xml");
                    dt.WriteXml(path, XmlWriteMode.WriteSchema);
                    MessageBox.Show("Xuất XML thành công tại:\n" + path);
                    System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi xuất XML: " + ex.Message); }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}