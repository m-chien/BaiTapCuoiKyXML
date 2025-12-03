using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_ThongKe : Form
    {
        string connectionString = @"Data Source=.;Initial Catalog=FootballShop;Integrated Security=True";

        public Form_ThongKe()
        {
            InitializeComponent();
        }

        private void Form_ThongKe_Load_1(object sender, EventArgs e)
        {
            // Cập nhật lại tên các mục cho phù hợp
            comboBox1.Items.AddRange(new string[] { "Doanh thu theo ngày", "Top sản phẩm bán chạy", "Tồn kho sản phẩm" });
            comboBox1.SelectedIndex = 0;

            // Mặc định điền năm hiện tại vào TextBox
            txtNam.Text = DateTime.Now.Year.ToString();
        }

        // --- HÀM XEM BÁO CÁO (HIỂN THỊ LÊN GRID) ---
        private void xembaocao_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập liệu năm
            if (string.IsNullOrEmpty(txtNam.Text) || !int.TryParse(txtNam.Text, out int nam))
            {
                MessageBox.Show("Vui lòng nhập năm hợp lệ (VD: 2023, 2024)!");
                txtNam.Focus();
                return;
            }

            LoadDataToGrid(nam);
        }

        private void LoadDataToGrid(int nam)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "";
                    string reportType = comboBox1.SelectedItem.ToString();

                    if (reportType == "Doanh thu năm")
                    {
                        // Sửa Query: Lấy doanh thu từng tháng trong năm đó
                        query = @"SELECT 'Tháng ' + CAST(MONTH(NgayDat) AS VARCHAR) AS [Thời Gian], 
                                         SUM(TongTien) AS [Doanh Thu] 
                                  FROM HoaDon 
                                  WHERE YEAR(NgayDat) = @Nam 
                                  GROUP BY MONTH(NgayDat) 
                                  ORDER BY MONTH(NgayDat)";
                    }
                    else if (reportType == "Top sản phẩm bán chạy")
                    {
                        // Sửa Query: Lọc top sản phẩm trong năm đó
                        query = @"SELECT TOP 10 s.Hang + ' - ' + s.mausac AS [Sản Phẩm], 
                                         SUM(ct.SoLuong) AS [Số Lượng Bán] 
                                  FROM ChiTietHoaDon ct 
                                  JOIN SanPham s ON ct.IdSanPham = s.IDSanPham 
                                  JOIN HoaDon h ON ct.IdHoaDon = h.IDHoaDon 
                                  WHERE YEAR(h.NgayDat) = @Nam 
                                  GROUP BY s.Hang, s.mausac 
                                  ORDER BY [Số Lượng Bán] DESC";
                    }
                    else if (reportType == "Tồn kho sản phẩm")
                    {
                        // Tồn kho là hiện tại, không phụ thuộc vào năm nhập vào
                        query = @"SELECT Hang + ' - ' + mausac AS [Sản Phẩm], SoLuongTonKho AS [Tồn Kho] FROM SanPham";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Chỉ thêm tham số @Nam nếu không phải xem tồn kho
                    if (reportType != "Tồn kho sản phẩm")
                    {
                        cmd.Parameters.AddWithValue("@Nam", nam);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Table");
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu trong năm " + nam);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        // --- HÀM XUẤT BÁO CÁO (GIỮ NGUYÊN KHÔNG ĐỔI) ---
        private void xuatbaocao_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) { MessageBox.Show("Chưa có dữ liệu!"); return; }

            try
            {
                // 1. Lưu dữ liệu ra file XML tạm
                DataTable dt = (DataTable)dataGridView1.DataSource;
                dt.TableName = "Table";
                string xmlPath = Path.Combine(Application.StartupPath, "DataThongKe.xml");
                dt.WriteXml(xmlPath, XmlWriteMode.WriteSchema);

                // 2. Kiểm tra file XSLT mẫu
                string xsltPath = Path.Combine(Application.StartupPath, "ThongKe.xslt");
                if (!File.Exists(xsltPath))
                {
                    TaoFileXSLT(xsltPath);
                }

                // 3. Biến đổi XML -> HTML bằng XSLT
                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(xsltPath);

                string htmlPath = Path.Combine(Application.StartupPath, "BaoCao_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
                transform.Transform(xmlPath, htmlPath);

                // 4. Mở file HTML lên xem
                Process.Start(htmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất báo cáo: " + ex.Message);
            }
        }

        // --- GIỮ NGUYÊN FILE XSLT VÌ NÓ TỰ ĐỘNG NHẬN CỘT DỮ LIỆU ---
        private void TaoFileXSLT(string path)
        {
            string content = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
  <xsl:template match='/'>
    <html>
      <head>
        <title>Báo Cáo Thống Kê</title>
        <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>
        <style>
          body { font-family: Segoe UI, sans-serif; padding: 20px; background: #f0f2f5; }
          .container { max-width: 1000px; margin: 0 auto; background: white; padding: 40px; border-radius: 10px; box-shadow: 0 4px 6px rgba(0,0,0,0.1); }
          h1 { text-align: center; color: #1a73e8; }
          table { width: 100%; border-collapse: collapse; margin-top: 30px; }
          th { background: #1a73e8; color: white; padding: 12px; }
          td { border-bottom: 1px solid #ddd; padding: 10px; }
          .chart-box { margin: 40px 0; height: 400px; }
        </style>
      </head>
      <body>
        <div class='container'>
          <h1>BIỂU ĐỒ THỐNG KÊ</h1>
          <div class='chart-box'><canvas id='myChart'></canvas></div>
          <h3>Dữ liệu chi tiết</h3>
          <table>
            <tr><xsl:for-each select='NewDataSet/Table[1]/*'><th><xsl:value-of select='name()'/></th></xsl:for-each></tr>
            <xsl:for-each select='NewDataSet/Table'>
              <tr><xsl:for-each select='*'><td><xsl:value-of select='.'/></td></xsl:for-each></tr>
            </xsl:for-each>
          </table>
        </div>
        <script>
          const ctx = document.getElementById('myChart');
          new Chart(ctx, {
            type: 'bar',
            data: {
              labels: [<xsl:for-each select='NewDataSet/Table'>'<xsl:value-of select='*[1]'/>',</xsl:for-each>],
              datasets: [{
                label: 'Giá trị',
                data: [<xsl:for-each select='NewDataSet/Table'><xsl:value-of select='*[last()]'/>,</xsl:for-each>],
                backgroundColor: '#36a2eb'
              }]
            }
          });
        </script>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>";
            File.WriteAllText(path, content);
        }
    }
}