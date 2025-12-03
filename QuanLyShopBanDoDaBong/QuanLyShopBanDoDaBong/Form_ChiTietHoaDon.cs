using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_ChiTietHoaDon : Form
    {
        private int idHoaDonCanXem = 0;

        string connectionString = ConfigurationManager.ConnectionStrings["MyConnect"].ConnectionString;

        public Form_ChiTietHoaDon()
        {
            InitializeComponent();
            this.idHoaDonCanXem = 0;
        }

        public Form_ChiTietHoaDon(int id)
        {
            InitializeComponent();
            this.idHoaDonCanXem = id;
        }

        private void Form_ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            if (idHoaDonCanXem > 0)
            {
                LoadChiTiet();
            }
            else
            {
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

        private void LoadTatCaChiTiet()
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
                                    ORDER BY ct.IdHoaDon DESC";

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
            this.Close(); 
        }
    }
}