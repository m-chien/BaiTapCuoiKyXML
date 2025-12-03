using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration; // Nhớ thêm thư viện này nếu dùng App.config

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_ChiTietHoaDon : Form
    {
        // 1. Biến để lưu ID được truyền sang (mặc định là 0)
        private int idHoaDonCanXem = 0;

        // Chuỗi kết nối
        string connectionString = "Data Source=localhost; Initial Catalog=FootballShop; Integrated Security=True";

        // --- CÁCH SỬA: THÊM 2 CONSTRUCTOR ---

        // Constructor 1: Không tham số (Dùng khi mở từ Menu)
        public Form_ChiTietHoaDon()
        {
            InitializeComponent();
            this.idHoaDonCanXem = 0; // Không có ID cụ thể
        }

        // Constructor 2: Có tham số (Dùng khi mở từ form Hóa Đơn để xem chi tiết)
        public Form_ChiTietHoaDon(int id)
        {
            InitializeComponent();
            this.idHoaDonCanXem = id; // Lưu ID được truyền vào
        }

        private void Form_ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            // Nếu có ID (id > 0) thì tải chi tiết của hóa đơn đó
            if (idHoaDonCanXem > 0)
            {
                LoadChiTiet();
            }
            else
            {
                // Nếu mở từ Menu (id = 0), có thể tải tất cả hoặc để trống tùy bạn
                // Ở đây mình ví dụ tải tất cả chi tiết (hoặc bạn có thể để trống)
                LoadTatCaChiTiet();
            }
        }

        private void LoadChiTiet()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                                    SELECT 
                                        ct.IdHoaDon AS [Mã HĐ], 
                                        s.Hang + ' - ' + s.mausac AS [Tên Sản Phẩm],
                                        ct.SoLuong AS [Số Lượng],
                                        ct.DonGia AS [Đơn Giá],
                                        (ct.SoLuong * ct.DonGia) AS [Thành Tiền]
                                    FROM ChiTietHoaDon ct
                                    JOIN SanPham s ON ct.IdSanPham = s.IDSanPham
                                    WHERE ct.IdHoaDon = @IdHoaDon";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdHoaDon", this.idHoaDonCanXem);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvChiTiet.DataSource = dt;
                    dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        // Hàm phụ: Tải tất cả chi tiết (nếu mở từ menu)
        private void LoadTatCaChiTiet()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Lấy tất cả chi tiết của tất cả hóa đơn
                    string query = @"
                                    SELECT 
                                        ct.IdHoaDon AS [Mã HĐ], 
                                        s.Hang + ' - ' + s.mausac AS [Tên Sản Phẩm],
                                        ct.SoLuong AS [Số Lượng],
                                        ct.DonGia AS [Đơn Giá],
                                        (ct.SoLuong * ct.DonGia) AS [Thành Tiền]
                                    FROM ChiTietHoaDon ct
                                    JOIN SanPham s ON ct.IdSanPham = s.IDSanPham
                                    ORDER BY ct.IdHoaDon DESC"; // Sắp xếp mới nhất lên đầu

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvChiTiet.DataSource = dt;
                    dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void dgvChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Lệnh đóng Form hiện tại
        }
    }
}