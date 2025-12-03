using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO; // Thêm thư viện này để dùng Path và File

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_BinhLuan : Form
    {
        // Chuỗi kết nối
        string connectionString = @"Data Source=.;Initial Catalog=FootballShop;Integrated Security=True";

        public Form_BinhLuan()
        {
            InitializeComponent();
        }

        private void Form_BinhLuan_Load(object sender, EventArgs e)
        {
            // Cấu hình ComboBox
            cbbTinhTrang.Items.Clear();
            cbbTinhTrang.Items.AddRange(new string[] { "Tất cả", "Chờ duyệt", "Đã duyệt" });
            cbbTinhTrang.SelectedIndex = 0;

            // Cấu hình DateTimePicker
            dtpNgay.Format = DateTimePickerFormat.Custom;
            dtpNgay.CustomFormat = "dd/MM/yyyy";

            // Mặc định: Bỏ lọc ngày
            if (chkLocNgay != null)
            {
                chkLocNgay.Checked = false;
                dtpNgay.Enabled = false;
            }

            // Load dữ liệu ban đầu
            LoadBinhLuan();
        }

        // --- HÀM TẢI DỮ LIỆU ---
        private void LoadBinhLuan(string tinhTrang = "Tất cả", DateTime? ngay = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    StringBuilder query = new StringBuilder(@"
                        SELECT 
                            b.IDBinhLuan AS [Mã BL],
                            n.Email AS [Người dùng],
                            s.Hang + ' - ' + s.mausac AS [Sản phẩm],
                            b.NoiDung AS [Nội dung],
                            b.NgayBinhLuan AS [Ngày bình luận],
                            b.TinhTrang AS [Trạng thái]
                        FROM BinhLuan b
                        JOIN NguoiDung n ON b.IDNguoiDung = n.IDNguoiDung
                        JOIN SanPham s ON b.IdSanPham = s.IDSanPham
                        WHERE 1=1");

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    // 1. Lọc theo Trạng thái
                    if (tinhTrang != "Tất cả")
                    {
                        query.Append(" AND b.TinhTrang = @TinhTrang");
                        cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                    }

                    // 2. Lọc theo Ngày (Chỉ khi có giá trị ngày)
                    if (ngay != null)
                    {
                        query.Append(" AND CAST(b.NgayBinhLuan AS DATE) = CAST(@Ngay AS DATE)");
                        cmd.Parameters.AddWithValue("@Ngay", ngay.Value);
                    }

                    query.Append(" ORDER BY b.NgayBinhLuan DESC");
                    cmd.CommandText = query.ToString();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvBinhLuan.DataSource = dt;
                    dgvBinhLuan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    this.Text = $"Quản lý bình luận - Tìm thấy {dt.Rows.Count} kết quả";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                }
            }
        }

        // --- SỰ KIỆN TÌM KIẾM ---
        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            string trangThai = cbbTinhTrang.SelectedItem?.ToString() ?? "Tất cả";

            // Chỉ lấy ngày khi checkbox được tick
            DateTime? ngayChon = null;
            if (chkLocNgay.Checked)
            {
                ngayChon = dtpNgay.Value;
            }

            LoadBinhLuan(trangThai, ngayChon);
        }

        // --- SỰ KIỆN LÀM MỚI ---
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbbTinhTrang.SelectedIndex = 0;
            chkLocNgay.Checked = false;
            dtpNgay.Enabled = false;
            LoadBinhLuan();
        }

        // --- SỰ KIỆN CHECKBOX ---
        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgay.Enabled = chkLocNgay.Checked;
        }

        // --- SỰ KIỆN XUẤT XML ---
        private void btnXuatXML_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy DataTable từ DataGridView
                DataTable dt = (DataTable)dgvBinhLuan.DataSource;

                // Kiểm tra dữ liệu
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu bình luận để xuất!");
                    return;
                }

                // Đặt tên bảng cho XML
                dt.TableName = "BinhLuan";

                // Tạo tên file
                string fileName = "DanhSachBinhLuan_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml";
                string path = Path.Combine(Application.StartupPath, fileName);

                // Ghi file XML
                dt.WriteXml(path, XmlWriteMode.WriteSchema);

                // Thông báo
                MessageBox.Show("Xuất XML thành công!\nĐường dẫn: " + path);

                // Mở thư mục chứa file vừa xuất
                System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất XML: " + ex.Message);
            }
        }
    }
}