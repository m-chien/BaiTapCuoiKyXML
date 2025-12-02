using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_QLSanpham : Form
    {
        private string connectionString = "Data Source=localhost; Initial Catalog=FootballShop; Integrated Security=True";

        public Form_QLSanpham()
        {
            InitializeComponent();
        }

        private void Form_QLSanpham_Load(object sender, EventArgs e)
        {

        }

        // Button 1: Xuất dữ liệu SanPham ra XML
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Cập nhật query theo cấu trúc bảng mới
                    string query = @"SELECT 
                        IDSanPham, 
                        IDdanhMuc, 
                        Hang, 
                        KichThuoc, 
                        mausac, 
                        mota, 
                        hinhanh, 
                        SoLuongTonKho, 
                        DonViTinh 
                    FROM SanPham";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable table = new DataTable("SanPham");
                    adapter.Fill(table);

                    string filePath = Path.Combine(Application.StartupPath, "SanPham.xml");
                    table.WriteXml(filePath, XmlWriteMode.WriteSchema);

                    MessageBox.Show("Xuất XML thành công!\n" + filePath, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Button 2: Chuyển đổi XML sang HTML và hiển thị
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string xmlPath = Path.Combine(Application.StartupPath, "SanPham.xml");
                if (!File.Exists(xmlPath))
                {
                    MessageBox.Show("Chưa có file SanPham.xml. Vui lòng xuất XML trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataSet ds = new DataSet();
                ds.ReadXml(xmlPath);

                string htmlPath = Path.Combine(Application.StartupPath, "SanPham.html");
                StringBuilder html = new StringBuilder();

                html.Append("<!DOCTYPE html>");
                html.Append("<html><head>");
                html.Append("<meta charset='UTF-8'>");
                html.Append("<style>");
                html.Append("body { font-family: Arial, sans-serif; margin: 20px; }");
                html.Append("h2 { color: #333; text-align: center; }");
                html.Append("table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
                html.Append("th { background-color: #4CAF50; color: white; padding: 12px; text-align: left; }");
                html.Append("td { padding: 10px; border: 1px solid #ddd; }");
                html.Append("tr:nth-child(even) { background-color: #f2f2f2; }");
                html.Append("tr:hover { background-color: #ddd; }");
                html.Append("</style>");
                html.Append("</head><body>");

                html.Append("<h2>DANH SÁCH SẢN PHẨM FOOTBALL SHOP</h2>");
                html.Append("<table>");

                // Header
                html.Append("<tr>");
                foreach (DataColumn col in ds.Tables[0].Columns)
                    html.Append("<th>" + col.ColumnName + "</th>");
                html.Append("</tr>");

                // Rows
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    html.Append("<tr>");
                    foreach (var item in row.ItemArray)
                        html.Append("<td>" + item + "</td>");
                    html.Append("</tr>");
                }

                html.Append("</table></body></html>");

                File.WriteAllText(htmlPath, html.ToString(), Encoding.UTF8);
                System.Diagnostics.Process.Start(htmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Button 3: Xuất dữ liệu HoaDon ra XML
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Cập nhật query theo cấu trúc bảng mới
                    string query = @"SELECT 
                        IDHoaDon, 
                        IdUser, 
                        TongTien, 
                        DiaChiGiaoHang, 
                        NgayDat, 
                        TrangThai 
                    FROM HoaDon";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable table = new DataTable("HoaDon");
                    adapter.Fill(table);

                    string filePath = Path.Combine(Application.StartupPath, "HoaDon.xml");
                    table.WriteXml(filePath, XmlWriteMode.WriteSchema);

                    MessageBox.Show($"Tệp XML đã được lưu tại: {filePath}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Button 4: Chuyển đổi HoaDon XML sang HTML bằng XSLT
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo tệp XML từ cơ sở dữ liệu
                TaoXML taoXML = new TaoXML();
                string sql = @"SELECT 
                    IDHoaDon, 
                    IdUser, 
                    TongTien, 
                    DiaChiGiaoHang, 
                    NgayDat, 
                    TrangThai 
                FROM HoaDon";
                string fileXML = "HoaDon.xml";
                taoXML.taoXML(sql, "HoaDon", fileXML);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo XML: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // Đường dẫn đến các tệp XML, XSLT và HTML
                string xmlPath = Path.Combine(Application.StartupPath, "HoaDon.xml");
                string xsltPath = Path.Combine(Application.StartupPath, "HoaDon.xslt");
                string htmlPath = Path.Combine(Application.StartupPath, "HoaDon.html");

                // Kiểm tra xem các tệp XML và XSLT có tồn tại không
                if (!File.Exists(xmlPath))
                {
                    MessageBox.Show($"Tệp XML không tồn tại tại: {xmlPath}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!File.Exists(xsltPath))
                {
                    MessageBox.Show($"Tệp XSLT không tồn tại tại: {xsltPath}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Khởi tạo đối tượng XSLT để thực hiện chuyển đổi
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                // Thực hiện chuyển đổi XML sang HTML
                using (StreamWriter writer = new StreamWriter(htmlPath, false, Encoding.UTF8))
                {
                    xslt.Transform(xmlPath, null, writer);
                }

                // Mở tệp HTML trong trình duyệt mặc định
                System.Diagnostics.Process.Start(htmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chuyển đổi: {ex.Message}\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Button 5: Tìm kiếm hóa đơn theo IdUser
        private void button5_Click(object sender, EventArgs e)
        {
            string idUser = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(idUser))
            {
                MessageBox.Show("Vui lòng nhập ID User để tìm hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimKiemHoaDon(idUser);
        }

        private void TimKiemHoaDon(string idUser)
        {
            try
            {
                string fileXML = Path.Combine(Application.StartupPath, "HoaDon.xml");

                if (!File.Exists(fileXML))
                {
                    MessageBox.Show("Chưa có file HoaDon.xml. Vui lòng xuất dữ liệu trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileXML);

                // Tìm hóa đơn theo IdUser
                XmlNodeList nodes = xmlDoc.SelectNodes($"/NewDataSet/HoaDon[IdUser='{idUser}']");

                if (nodes.Count > 0)
                {
                    StringBuilder htmlContent = new StringBuilder();

                    htmlContent.Append("<!DOCTYPE html>");
                    htmlContent.Append("<html><head>");
                    htmlContent.Append("<meta charset='UTF-8'>");
                    htmlContent.Append("<style>");
                    htmlContent.Append("body { font-family: Arial, sans-serif; margin: 20px; }");
                    htmlContent.Append("h1 { color: #333; text-align: center; }");
                    htmlContent.Append("table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
                    htmlContent.Append("th { background-color: #4CAF50; color: white; padding: 12px; text-align: left; }");
                    htmlContent.Append("td { padding: 10px; border: 1px solid #ddd; }");
                    htmlContent.Append("tr:nth-child(even) { background-color: #f2f2f2; }");
                    htmlContent.Append("tr:hover { background-color: #ddd; }");
                    htmlContent.Append("</style>");
                    htmlContent.Append("</head><body>");

                    htmlContent.Append("<h1>HÓA ĐƠN CỦA USER ID: " + idUser + "</h1>");
                    htmlContent.Append("<table><tr>" +
                        "<th>ID Hóa Đơn</th>" +
                        "<th>ID User</th>" +
                        "<th>Tổng Tiền</th>" +
                        "<th>Địa Chỉ Giao Hàng</th>" +
                        "<th>Ngày Đặt</th>" +
                        "<th>Trạng Thái</th>" +
                        "</tr>");

                    foreach (XmlNode node in nodes)
                    {
                        htmlContent.Append("<tr>");
                        htmlContent.Append("<td>" + node["IDHoaDon"]?.InnerText + "</td>");
                        htmlContent.Append("<td>" + node["IdUser"]?.InnerText + "</td>");
                        htmlContent.Append("<td>" + node["TongTien"]?.InnerText + "</td>");
                        htmlContent.Append("<td>" + node["DiaChiGiaoHang"]?.InnerText + "</td>");
                        htmlContent.Append("<td>" + node["NgayDat"]?.InnerText + "</td>");
                        htmlContent.Append("<td>" + node["TrangThai"]?.InnerText + "</td>");
                        htmlContent.Append("</tr>");
                    }

                    htmlContent.Append("</table></body></html>");

                    string tempHtmlFile = Path.Combine(Application.StartupPath, "KetQuaTimHoaDon.html");
                    File.WriteAllText(tempHtmlFile, htmlContent.ToString(), Encoding.UTF8);

                    System.Diagnostics.Process.Start(tempHtmlFile);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn cho ID User này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}