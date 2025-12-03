using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // Thêm các tùy chọn vào ComboBox
            cbbTinhTrang.Items.Add("Tất cả");
            cbbTinhTrang.Items.Add("Chờ duyệt");
            cbbTinhTrang.Items.Add("Đã duyệt");
            cbbTinhTrang.SelectedIndex = 0; // Mặc định chọn "Tất cả"

            // Cấu hình DateTimePicker
            dtpNgay.Format = DateTimePickerFormat.Short;
            dtpNgay.Value = DateTime.Now; // Mặc định là ngày hôm nay

            // Load dữ liệu ban đầu (Load tất cả)
            LoadBinhLuan();
        }

        // Hàm load dữ liệu - SỬA LẠI HOÀN TOÀN
        private void LoadBinhLuan(string tinhTrang = "Tất cả", DateTime? ngay = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // XÂY DỰNG CÂU TRUY VẤN ĐỘNG
                    StringBuilder query = new StringBuilder();
                    query.Append(@"
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

                    // ĐIỀU KIỆN LỌC THEO TÌNH TRẠNG
                    if (tinhTrang != "Tất cả")
                    {
                        query.Append(" AND b.TinhTrang = @TinhTrang");
                        cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                    }
                    // Nếu chọn "Tất cả" thì không cần thêm điều kiện gì
                    // SQL sẽ tự động lấy tất cả: "Đã duyệt" và "Chờ duyệt"

                    // ĐIỀU KIỆN LỌC THEO NGÀY (chỉ khi có giá trị ngày)
                    if (ngay.HasValue)
                    {
                        query.Append(" AND CAST(b.NgayBinhLuan AS DATE) = CAST(@Ngay AS DATE)");
                        cmd.Parameters.AddWithValue("@Ngay", ngay.Value);
                    }

                    // Sắp xếp theo ngày mới nhất
                    query.Append(" ORDER BY b.NgayBinhLuan DESC");

                    cmd.CommandText = query.ToString();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvBinhLuan.DataSource = dt;

                    // Tùy chỉnh giao diện Grid
                    dgvBinhLuan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Hiển thị số lượng kết quả
                    this.Text = $"Quản lý bình luận - Tìm thấy {dt.Rows.Count} kết quả";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Nút làm mới - Load lại toàn bộ dữ liệu


        private void dgvBinhLuan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Có thể thêm xử lý khi click vào cell nếu cần
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            if (cbbTinhTrang.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tình trạng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy giá trị từ ComboBox và DateTimePicker
            string trangThaiDuocChon = cbbTinhTrang.SelectedItem.ToString();
            DateTime ngayDuocChon = dtpNgay.Value;

            // Gọi hàm load với tham số lọc
            LoadBinhLuan(trangThaiDuocChon, ngayDuocChon);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbbTinhTrang.SelectedIndex = 0; // Chọn "Tất cả"
            dtpNgay.Value = DateTime.Now;
            LoadBinhLuan();
        }
    }
}