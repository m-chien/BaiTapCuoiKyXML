using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Xsl;
using QuanLyShopBanDoDaBong.Class;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_ThongKe : Form
    {
        HoaDon objHD = new HoaDon();
        ChiTietHoaDon objCTHD = new ChiTietHoaDon();
        SanPham objSP = new SanPham();

        public Form_ThongKe()
        {
            InitializeComponent();
        }

        private void Form_ThongKe_Load_1(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Doanh thu năm", "Top sản phẩm bán chạy", "Tồn kho sản phẩm" });
            comboBox1.SelectedIndex = 0;

            cbNam.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i >= 2020; i--)
            {
                cbNam.Items.Add(i.ToString());
            }
            cbNam.SelectedIndex = 0;
        }

        private void xembaocao_Click(object sender, EventArgs e)
        {
            if (cbNam.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn năm cần xem!");
                return;
            }
            int nam = int.Parse(cbNam.SelectedItem.ToString());
            LoadDataToGrid(nam);
        }

        private void LoadDataToGrid(int nam)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn loại báo cáo!");
                    return;
                }

                string reportType = comboBox1.SelectedItem.ToString();
                DataTable dtKetQua = new DataTable("Table");

                if (reportType == "Doanh thu năm")
                {
                    dtKetQua = ThongKeDoanhThuTheoNam(nam);
                }
                else if (reportType == "Top sản phẩm bán chạy")
                {
                    dtKetQua = ThongKeTopSanPham(nam);
                }
                else if (reportType == "Tồn kho sản phẩm")
                {
                    dtKetQua = ThongKeTonKho();
                }

                dataGridView1.DataSource = dtKetQua;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dtKetQua.Rows.Count == 0)
                    MessageBox.Show("Không tìm thấy dữ liệu trong năm " + nam);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private DataTable ThongKeDoanhThuTheoNam(int nam)
        {
            DataTable dtHoaDon = objHD.LayDanhSach();

            var hoaDonNam = dtHoaDon.AsEnumerable()
                .Where(row => row["NgayDat"] != DBNull.Value &&
                             Convert.ToDateTime(row["NgayDat"]).Year == nam);

            var thongKe = hoaDonNam
                .GroupBy(row => Convert.ToDateTime(row["NgayDat"]).Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    DoanhThu = g.Sum(row => Convert.ToDecimal(row["TongTien"]))
                })
                .OrderBy(x => x.Thang);

            DataTable dtResult = new DataTable("Table");
            dtResult.Columns.Add("Thời_Gian", typeof(string));
            dtResult.Columns.Add("Doanh_Thu", typeof(decimal));

            foreach (var item in thongKe)
            {
                dtResult.Rows.Add($"Tháng {item.Thang}", item.DoanhThu);
            }

            return dtResult;
        }

        private DataTable ThongKeTopSanPham(int nam)
        {
            DataTable dtHoaDon = objHD.LayDanhSach();
            DataTable dtChiTiet = objCTHD.LayDanhSach();
            DataTable dtSanPham = objSP.LayDanhSach();

            string colIdHoaDon = dtChiTiet.Columns.Contains("IdHoaDon") ? "IdHoaDon" : "IDHoaDon";
            string colIdSanPham = dtChiTiet.Columns.Contains("IdSanPham") ? "IdSanPham" : "IDSanPham";

            var hoaDonNam = dtHoaDon.AsEnumerable()
                .Where(row => row["NgayDat"] != DBNull.Value &&
                             Convert.ToDateTime(row["NgayDat"]).Year == nam)
                .Select(row => row["IDHoaDon"].ToString())
                .ToHashSet();

            var chiTietNam = dtChiTiet.AsEnumerable()
                .Where(row => hoaDonNam.Contains(row[colIdHoaDon].ToString()));

            var thongKe = chiTietNam
                .GroupBy(row => row[colIdSanPham].ToString())
                .Select(g => new
                {
                    IdSanPham = g.Key,
                    SoLuongBan = g.Sum(row => Convert.ToInt32(row["SoLuong"]))
                })
                .OrderByDescending(x => x.SoLuongBan)
                .Take(10);

            DataTable dtResult = new DataTable("Table");
            dtResult.Columns.Add("Sản_Phẩm", typeof(string));
            dtResult.Columns.Add("Số_Lượng_Bán", typeof(int));

            foreach (var item in thongKe)
            {
                DataRow[] rowsSP = dtSanPham.Select($"IDSanPham = '{item.IdSanPham}'");
                string tenSP = "Không rõ";

                if (rowsSP.Length > 0)
                {
                    string hang = rowsSP[0]["Hang"] != DBNull.Value ? rowsSP[0]["Hang"].ToString() : "";
                    string mauSac = rowsSP[0]["mausac"] != DBNull.Value ? rowsSP[0]["mausac"].ToString() : "";
                    tenSP = $"{hang} - {mauSac}";
                }

                dtResult.Rows.Add(tenSP, item.SoLuongBan);
            }

            return dtResult;
        }

        private DataTable ThongKeTonKho()
        {
            DataTable dtSanPham = objSP.LayDanhSach();

            DataTable dtResult = new DataTable("Table");
            dtResult.Columns.Add("Sản_Phẩm", typeof(string));
            dtResult.Columns.Add("Tồn_Kho", typeof(int));

            foreach (DataRow row in dtSanPham.Rows)
            {
                string hang = row["Hang"] != DBNull.Value ? row["Hang"].ToString() : "";
                string mauSac = row["mausac"] != DBNull.Value ? row["mausac"].ToString() : "";
                string tenSP = $"{hang} - {mauSac}";
                int tonKho = Convert.ToInt32(row["SoLuongTonKho"]);

                dtResult.Rows.Add(tenSP, tonKho);
            }

            return dtResult;
        }

        private void xuatbaocao_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để xuất!");
                return;
            }

            try
            {
                // 1. Lấy dữ liệu từ Grid
                DataTable dtGrid = (DataTable)dataGridView1.DataSource;

                // 2. Tạo một DataSet mới để chứa Table (Để XML có thẻ root <NewDataSet>)
                DataSet ds = new DataSet("NewDataSet");

                // Copy dữ liệu sang bảng mới để tránh lỗi "Table already belongs to another DataSet"
                DataTable dtExport = dtGrid.Copy();
                dtExport.TableName = "Table"; // Đặt tên cố định để XSLT nhận diện
                ds.Tables.Add(dtExport);

                // 3. Ghi file XML (Dùng WriteSchema để đảm bảo format số liệu)
                string xmlPath = Path.Combine(Application.StartupPath, "DataThongKe.xml");
                ds.WriteXml(xmlPath, XmlWriteMode.WriteSchema);

                // 4. Tạo và chạy XSLT
                string xsltPath = Path.Combine(Application.StartupPath, "ThongKe.xslt");
                // Luôn tạo lại file XSLT để đảm bảo code mới nhất
                TaoFileXSLT(xsltPath);

                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(xsltPath);

                string htmlPath = Path.Combine(Application.StartupPath, "BaoCao_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
                transform.Transform(xmlPath, htmlPath);

                // Mở file HTML
                Process.Start(new ProcessStartInfo(htmlPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất báo cáo: " + ex.Message);
            }
        }

        private void TaoFileXSLT(string path)
        {
            // Dùng @ ở trước để viết chuỗi nhiều dòng
            // Lưu ý: Trong chuỗi @, muốn viết dấu " thì phải viết thành ""
            string content = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
  <xsl:template match='/'>
    <html>
      <head>
        <title>Báo Cáo Thống Kê</title>
        <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>
        <style>
          body { font-family: 'Segoe UI', Arial, sans-serif; padding: 20px; background: #f4f7f6; }
          .container { max-width: 1000px; margin: 0 auto; background: white; padding: 40px; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
          h1 { text-align: center; color: #2c3e50; margin-bottom: 30px; }
          
          /* Bảng */
          table { width: 100%; border-collapse: collapse; margin-top: 30px; }
          th { background: #3498db; color: white; padding: 12px; text-align: center; }
          td { border-bottom: 1px solid #ddd; padding: 10px; text-align: center; color: #333; }
          tr:nth-child(even) { background-color: #f9f9f9; }

          /* Biểu đồ */
          .chart-container { position: relative; height: 400px; width: 100%; margin-top: 20px; }
        </style>
      </head>
      <body>
        <div class='container'>
          <h1>BÁO CÁO THỐNG KÊ CỬA HÀNG</h1>
          
          <div class='chart-container'>
            <canvas id='myChart'></canvas>
          </div>

          <h3 style='margin-top:40px; color:#34495e;'>Chi tiết số liệu:</h3>
          <table>
            <thead>
                <tr>
                    <xsl:for-each select='NewDataSet/Table[1]/*'>
                        <th><xsl:value-of select=""translate(name(), '_', ' ')""/></th>
                    </xsl:for-each>
                </tr>
            </thead>
            <tbody>
                <xsl:for-each select='NewDataSet/Table'>
                  <tr>
                    <xsl:for-each select='*'>
                        <td><xsl:value-of select='.'/></td>
                    </xsl:for-each>
                  </tr>
                </xsl:for-each>
            </tbody>
          </table>
        </div>

        <script>
          // Lấy dữ liệu từ XML vào mảng JS
          // Cột đầu tiên là Nhãn (Labels), Cột cuối cùng là Dữ liệu (Data)
          const labels = [
            <xsl:for-each select='NewDataSet/Table'>
                ""<xsl:value-of select='*[1]'/>"", 
            </xsl:for-each>
          ];

          const dataValues = [
            <xsl:for-each select='NewDataSet/Table'>
                <xsl:value-of select='*[last()]'/>, 
            </xsl:for-each>
          ];

          // Vẽ biểu đồ
          const ctx = document.getElementById('myChart');
          new Chart(ctx, {
            type: 'bar', // Có thể đổi thành 'line', 'pie'
            data: {
              labels: labels,
              datasets: [{
                label: 'Thống Kê',
                data: dataValues,
                backgroundColor: 'rgba(52, 152, 219, 0.6)',
                borderColor: 'rgba(52, 152, 219, 1)',
                borderWidth: 1
              }]
            },
            options: {
              responsive: true,
              maintainAspectRatio: false,
              plugins: {
                legend: { position: 'top' },
                title: { display: true, text: 'Biểu đồ trực quan số liệu' }
              },
              scales: {
                y: { beginAtZero: true }
              }
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