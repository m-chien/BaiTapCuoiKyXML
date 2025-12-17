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

            var hoaDonNam = dtHoaDon.AsEnumerable()
                .Where(row => row["NgayDat"] != DBNull.Value &&
                             Convert.ToDateTime(row["NgayDat"]).Year == nam)
                .Select(row => row["IDHoaDon"].ToString())
                .ToHashSet();

            var chiTietNam = dtChiTiet.AsEnumerable()
                .Where(row => hoaDonNam.Contains(row["IdHoaDon"].ToString()));

            var thongKe = chiTietNam
                .GroupBy(row => row["IdSanPham"].ToString())
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
            var sorted = dtResult.AsEnumerable()
                        .OrderByDescending(row => row.Field<int>("Tồn_Kho"));

            return sorted.CopyToDataTable();
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
                DataTable dtGrid = (DataTable)dataGridView1.DataSource;
                DataSet ds = new DataSet("NewDataSet");
                DataTable dtExport = dtGrid.Copy();
                dtExport.TableName = "Table";
                ds.Tables.Add(dtExport);
                string xmlPath = Path.Combine(Application.StartupPath, "DataThongKe.xml");
                ds.WriteXml(xmlPath, XmlWriteMode.WriteSchema);
                string xsltPath = Path.Combine(Application.StartupPath, "ThongKe.xslt");
                if (!File.Exists(xsltPath))
                {
                    MessageBox.Show("Không tìm thấy file ThongKe.xslt!");
                    return;
                }

                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(xsltPath);

                string htmlPath = Path.Combine(Application.StartupPath, "BaoCao_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
                transform.Transform(xmlPath, htmlPath);
                Process.Start(new ProcessStartInfo(htmlPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất báo cáo: " + ex.Message);
            }
        }

        private void cbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}