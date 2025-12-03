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
            // 1. Setup ComboBox Loại báo cáo
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Doanh thu năm", "Top sản phẩm bán chạy", "Tồn kho sản phẩm" });
            comboBox1.SelectedIndex = 0;

            // 2. Setup ComboBox Năm (cbNam)
            // Tự động thêm từ năm 2020 đến năm hiện tại
            cbNam.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i >= 2020; i--)
            {
                cbNam.Items.Add(i.ToString());
            }
            cbNam.SelectedIndex = 0; // Mặc định chọn năm nay
        }

        // --- HÀM XEM BÁO CÁO (HIỂN THỊ LÊN GRID) ---
        private void xembaocao_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn năm chưa
            if (cbNam.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn năm cần xem!");
                return;
            }

            // Lấy năm từ ComboBox
            int nam = int.Parse(cbNam.SelectedItem.ToString());

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

                    if (comboBox1.SelectedItem == null) { MessageBox.Show("Vui lòng chọn loại báo cáo!"); return; }

                    string reportType = comboBox1.SelectedItem.ToString();

                    // --- GIỮ NGUYÊN LOGIC FIX XML (Dùng gạch dưới _) ---
                    if (reportType == "Doanh thu năm")
                    {
                        query = @"SELECT 'Tháng ' + CAST(MONTH(NgayDat) AS VARCHAR) AS [Thời_Gian], 
                                         SUM(TongTien) AS [Doanh_Thu] 
                                  FROM HoaDon 
                                  WHERE YEAR(NgayDat) = @Nam 
                                  GROUP BY MONTH(NgayDat) 
                                  ORDER BY MONTH(NgayDat)";
                    }
                    else if (reportType == "Top sản phẩm bán chạy")
                    {
                        query = @"SELECT TOP 10 s.Hang + ' - ' + s.mausac AS [Sản_Phẩm], 
                                         SUM(ct.SoLuong) AS [Số_Lượng_Bán] 
                                  FROM ChiTietHoaDon ct 
                                  JOIN SanPham s ON ct.IdSanPham = s.IDSanPham 
                                  JOIN HoaDon h ON ct.IdHoaDon = h.IDHoaDon 
                                  WHERE YEAR(h.NgayDat) = @Nam 
                                  GROUP BY s.Hang, s.mausac 
                                  ORDER BY [Số_Lượng_Bán] DESC";
                    }
                    else if (reportType == "Tồn kho sản phẩm")
                    {
                        query = @"SELECT Hang + ' - ' + mausac AS [Sản_Phẩm], SoLuongTonKho AS [Tồn_Kho] FROM SanPham";
                    }
                    else { MessageBox.Show("Loại báo cáo không hợp lệ!"); return; }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (reportType != "Tồn kho sản phẩm") cmd.Parameters.AddWithValue("@Nam", nam);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Table");
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dt.Rows.Count == 0) MessageBox.Show("Không tìm thấy dữ liệu trong năm " + nam);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        // --- HÀM XUẤT BÁO CÁO (GIỮ NGUYÊN) ---
        private void xuatbaocao_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) { MessageBox.Show("Chưa có dữ liệu!"); return; }

            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                dt.TableName = "Table";
                string xmlPath = Path.Combine(Application.StartupPath, "DataThongKe.xml");
                dt.WriteXml(xmlPath, XmlWriteMode.WriteSchema);

                string xsltPath = Path.Combine(Application.StartupPath, "ThongKe.xslt");

                // Luôn tạo lại file XSLT mới nhất
                if (File.Exists(xsltPath)) File.Delete(xsltPath);
                TaoFileXSLT(xsltPath);

                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(xsltPath);

                string htmlPath = Path.Combine(Application.StartupPath, "BaoCao_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
                transform.Transform(xmlPath, htmlPath);

                Process.Start(htmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất báo cáo: " + ex.Message);
            }
        }

        private void TaoFileXSLT(string path)
        {
            // GIỮ NGUYÊN LOGIC TRANSLATE ĐỂ SỬA LỖI FONT/SPACE
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
            <tr><xsl:for-each select='NewDataSet/Table[1]/*'><th><xsl:value-of select=""translate(name(), '_', ' ')""/></th></xsl:for-each></tr>
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

        private void cbNam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}