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
using System.IO;

namespace WFA_Quanlyquancafe
{
    public partial class Form_ThongKe : Form
    {
        private string connectionString = "Data Source=DESKTOP-ER8LV7D; Initial Catalog=FootballShop; Integrated Security=True";

        public Form_ThongKe()
        {
            InitializeComponent();
        }

        private void Form_ThongKe_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Doanh thu theo hóa đơn");
            comboBox1.Items.Add("Sản phẩm bán chạy");
            comboBox1.Items.Add("Thống kê người dùng");
            comboBox1.Items.Add("Bình luận sản phẩm");
            comboBox1.SelectedIndex = 0; // Mặc định chọn mục đầu tiên
        }

        private void xembaocao_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string loaiBaoCao = comboBox1.SelectedItem.ToString();
            string fromDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string toDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            // Kiểm tra ngày hợp lệ
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (loaiBaoCao)
            {
                case "Doanh thu theo hóa đơn":
                    LoadDoanhThu(fromDate, toDate);
                    break;

                case "Sản phẩm bán chạy":
                    LoadSanPhamBanChay(fromDate, toDate);
                    break;

                case "Thống kê người dùng":
                    LoadThongKeNguoiDung(fromDate, toDate);
                    break;

                case "Bình luận sản phẩm":
                    LoadBinhLuanSanPham(fromDate, toDate);
                    break;

                default:
                    MessageBox.Show("Vui lòng chọn loại báo cáo hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        // Hàm nạp dữ liệu Doanh thu theo hóa đơn
        private void LoadDoanhThu(string fromDate, string toDate)
        {
            string query = @"SELECT 
                h.IDHoaDon AS [Mã Hóa Đơn],
                h.NgayDat AS [Ngày Đặt],
                n.Email AS [Email Khách Hàng],
                h.TongTien AS [Tổng Tiền],
                h.TrangThai AS [Trạng Thái],
                h.DiaChiGiaoHang AS [Địa Chỉ Giao Hàng]
            FROM HoaDon h
            JOIN NguoiDung n ON h.IdUser = n.IDNguoiDung
            WHERE h.NgayDat BETWEEN @fromDate AND @toDate
            ORDER BY h.NgayDat DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@fromDate", SqlDbType.Date) { Value = DateTime.Parse(fromDate) },
                new SqlParameter("@toDate", SqlDbType.Date) { Value = DateTime.Parse(toDate) }
            };

            LoadDataToGrid(query, parameters);
        }

        // Hàm nạp dữ liệu Sản phẩm bán chạy
        private void LoadSanPhamBanChay(string fromDate, string toDate)
        {
            string query = @"SELECT 
                sp.IDSanPham AS [Mã Sản Phẩm],
                sp.Hang AS [Hãng],
                sp.mausac AS [Màu Sắc],
                sp.KichThuoc AS [Kích Thước],
                SUM(ct.SoLuong) AS [Số Lượng Bán],
                SUM(ct.DonGia * ct.SoLuong) AS [Tổng Doanh Thu],
                sp.SoLuongTonKho AS [Tồn Kho]
            FROM ChiTietHoaDon ct
            JOIN SanPham sp ON ct.IdSanPham = sp.IDSanPham
            JOIN HoaDon h ON ct.IdHoaDon = h.IDHoaDon
            WHERE h.NgayDat BETWEEN @fromDate AND @toDate
                AND h.TrangThai = N'Đã thanh toán'
            GROUP BY sp.IDSanPham, sp.Hang, sp.mausac, sp.KichThuoc, sp.SoLuongTonKho
            ORDER BY [Số Lượng Bán] DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@fromDate", SqlDbType.Date) { Value = DateTime.Parse(fromDate) },
                new SqlParameter("@toDate", SqlDbType.Date) { Value = DateTime.Parse(toDate) }
            };

            LoadDataToGrid(query, parameters);
        }

        // Hàm nạp dữ liệu Thống kê người dùng
        private void LoadThongKeNguoiDung(string fromDate, string toDate)
        {
            string query = @"SELECT 
                n.IDNguoiDung AS [Mã Người Dùng],
                n.Email AS [Email],
                n.VaiTro AS [Vai Trò],
                n.gioitinh AS [Giới Tính],
                COUNT(DISTINCT h.IDHoaDon) AS [Số Đơn Hàng],
                ISNULL(SUM(h.TongTien), 0) AS [Tổng Chi Tiêu],
                COUNT(DISTINCT bl.IDBinhLuan) AS [Số Bình Luận]
            FROM NguoiDung n
            LEFT JOIN HoaDon h ON n.IDNguoiDung = h.IdUser 
                AND h.NgayDat BETWEEN @fromDate AND @toDate
            LEFT JOIN BinhLuan bl ON n.IDNguoiDung = bl.IDNguoiDung 
                AND bl.NgayBinhLuan BETWEEN @fromDate AND @toDate
            GROUP BY n.IDNguoiDung, n.Email, n.VaiTro, n.gioitinh
            ORDER BY [Tổng Chi Tiêu] DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@fromDate", SqlDbType.Date) { Value = DateTime.Parse(fromDate) },
                new SqlParameter("@toDate", SqlDbType.Date) { Value = DateTime.Parse(toDate) }
            };

            LoadDataToGrid(query, parameters);
        }

        // Hàm nạp dữ liệu Bình luận sản phẩm
        private void LoadBinhLuanSanPham(string fromDate, string toDate)
        {
            string query = @"SELECT 
                bl.IDBinhLuan AS [Mã Bình Luận],
                sp.Hang AS [Hãng Sản Phẩm],
                sp.mausac AS [Màu Sắc],
                n.Email AS [Email Người Dùng],
                bl.NoiDung AS [Nội Dung],
                bl.NgayBinhLuan AS [Ngày Bình Luận],
                bl.TinhTrang AS [Tình Trạng]
            FROM BinhLuan bl
            JOIN SanPham sp ON bl.IdSanPham = sp.IDSanPham
            JOIN NguoiDung n ON bl.IDNguoiDung = n.IDNguoiDung
            WHERE bl.NgayBinhLuan BETWEEN @fromDate AND @toDate
            ORDER BY bl.NgayBinhLuan DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@fromDate", SqlDbType.Date) { Value = DateTime.Parse(fromDate) },
                new SqlParameter("@toDate", SqlDbType.Date) { Value = DateTime.Parse(toDate) }
            };

            LoadDataToGrid(query, parameters);
        }

        // Hàm chung để nạp dữ liệu vào DataGridView
        private void LoadDataToGrid(string query, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Nếu có tham số thì thêm vào câu lệnh SQL
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;

                        // Tự động điều chỉnh độ rộng cột
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        MessageBox.Show($"Đã tải {dt.Rows.Count} bản ghi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        MessageBox.Show("Không có dữ liệu trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void xuatbaocao_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML files (*.xml)|*.xml|HTML files (*.html)|*.html|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.Title = "Lưu báo cáo";
                saveFileDialog.FileName = $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();

                    if (extension == ".xml")
                    {
                        // Xuất ra XML
                        dt.WriteXml(saveFileDialog.FileName, XmlWriteMode.WriteSchema);
                        MessageBox.Show($"Dữ liệu đã được xuất ra file XML:\n{saveFileDialog.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (extension == ".html")
                    {
                        // Xuất ra HTML
                        XuatHTML(dt, saveFileDialog.FileName);
                        MessageBox.Show($"Dữ liệu đã được xuất ra file HTML:\n{saveFileDialog.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Mở file HTML
                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Định dạng file không được hỗ trợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuatHTML(DataTable dt, string filePath)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<!DOCTYPE html>");
            html.Append("<html><head>");
            html.Append("<meta charset='UTF-8'>");
            html.Append($"<title>Báo Cáo - {DateTime.Now:dd/MM/yyyy HH:mm}</title>");
            html.Append("<style>");
            html.Append("body { font-family: Arial, sans-serif; margin: 20px; background-color: #f5f5f5; }");
            html.Append(".header { text-align: center; margin-bottom: 30px; }");
            html.Append(".header h1 { color: #333; margin-bottom: 10px; }");
            html.Append(".header p { color: #666; }");
            html.Append("table { width: 100%; border-collapse: collapse; background-color: white; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }");
            html.Append("th { background-color: #4CAF50; color: white; padding: 12px; text-align: left; font-weight: bold; }");
            html.Append("td { padding: 10px; border: 1px solid #ddd; }");
            html.Append("tr:nth-child(even) { background-color: #f9f9f9; }");
            html.Append("tr:hover { background-color: #f1f1f1; }");
            html.Append(".footer { text-align: center; margin-top: 30px; color: #666; font-size: 12px; }");
            html.Append("</style>");
            html.Append("</head><body>");

            // Header
            html.Append("<div class='header'>");
            html.Append("<h1>BÁO CÁO THỐNG KÊ</h1>");
            html.Append($"<p>Loại báo cáo: <strong>{comboBox1.SelectedItem}</strong></p>");
            html.Append($"<p>Từ ngày: <strong>{dateTimePicker1.Value:dd/MM/yyyy}</strong> - Đến ngày: <strong>{dateTimePicker2.Value:dd/MM/yyyy}</strong></p>");
            html.Append($"<p>Ngày xuất: <strong>{DateTime.Now:dd/MM/yyyy HH:mm:ss}</strong></p>");
            html.Append("</div>");

            // Table
            html.Append("<table>");

            // Header row
            html.Append("<tr>");
            foreach (DataColumn col in dt.Columns)
            {
                html.Append($"<th>{col.ColumnName}</th>");
            }
            html.Append("</tr>");

            // Data rows
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (var item in row.ItemArray)
                {
                    html.Append($"<td>{item}</td>");
                }
                html.Append("</tr>");
            }

            html.Append("</table>");

            // Footer
            html.Append("<div class='footer'>");
            html.Append($"<p>Tổng số bản ghi: {dt.Rows.Count}</p>");
            html.Append("<p>© Football Shop - Hệ thống quản lý bán hàng</p>");
            html.Append("</div>");

            html.Append("</body></html>");

            File.WriteAllText(filePath, html.ToString(), Encoding.UTF8);
        }

        private void Form_ThongKe_Load_1(object sender, EventArgs e)
        {
            // Có thể để trống hoặc xóa method này nếu không dùng
        }
    }
}