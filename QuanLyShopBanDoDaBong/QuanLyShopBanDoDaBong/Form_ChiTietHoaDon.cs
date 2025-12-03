using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_ChiTietHoaDon : Form
    {
        // 1. Biến để lưu ID được truyền sang
        private int idHoaDonCanXem;
        string connectionString = @"Data Source=.;Initial Catalog=FootballShop;Integrated Security=True";

        // 2. Sửa lại Constructor để nhận tham số ID
        public Form_ChiTietHoaDon(int id)
        {
            InitializeComponent();
            this.idHoaDonCanXem = id; // Lưu ID vào biến
        }

        private void Form_ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            // Khi form load, dùng cái ID đó để tải dữ liệu chi tiết
            LoadChiTiet();
        }

        private void LoadChiTiet()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Query lấy chi tiết sản phẩm trong hóa đơn đó
                    string query = @"
                                SELECT 
                                    ct.IdHoaDon AS [Mã HĐ],   -- Thêm dòng này vào
                                    s.Hang + ' - ' + s.mausac AS [Tên Sản Phẩm],
                                    ct.SoLuong AS [Số Lượng],
                                    ct.DonGia AS [Đơn Giá],
                                    (ct.SoLuong * ct.DonGia) AS [Thành Tiền]
                                FROM ChiTietHoaDon ct
                                JOIN SanPham s ON ct.IdSanPham = s.IDSanPham
                                WHERE ct.IdHoaDon = @IdHoaDon";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdHoaDon", this.idHoaDonCanXem); // Dùng biến ID ở đây

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Giả sử bạn có datagridview tên dgvChiTiet trong form này
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
    }
}