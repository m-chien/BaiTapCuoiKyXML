using QuanLyShopBanDoDaBong.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_ChiTietHoaDon : Form
    {
        ChiTietHoaDon objCTHD = new ChiTietHoaDon();
        SanPham objSP = new SanPham();
        private int idHoaDonCanXem = 0;

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
            try
            {
                DataTable dtChiTiet = objCTHD.LayChiTietTheoHoaDon(idHoaDonCanXem.ToString());
                DataTable dtSanPham = objSP.LayDanhSach();

                // Kiểm tra nếu không có dữ liệu
                if (dtChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy chi tiết hóa đơn này!");
                    dgvChiTiet.DataSource = null;
                    return;
                }

                // Xác định tên cột (IdHoaDon hoặc IDHoaDon)
                string colIdHoaDon = GetColumnName(dtChiTiet, new[] { "IdHoaDon", "IDHoaDon" });
                string colIdSanPham = GetColumnName(dtChiTiet, new[] { "IdSanPham", "IDSanPham" });

                DataTable dtHienThi = new DataTable();
                dtHienThi.Columns.Add("Mã HĐ", typeof(string));
                dtHienThi.Columns.Add("Tên Sản Phẩm", typeof(string));
                dtHienThi.Columns.Add("Số Lượng", typeof(int));
                dtHienThi.Columns.Add("Đơn Giá", typeof(decimal));
                dtHienThi.Columns.Add("Thành Tiền", typeof(decimal));

                foreach (DataRow rowCT in dtChiTiet.Rows)
                {
                    string idSP = rowCT[colIdSanPham]?.ToString() ?? "";

                    DataRow[] rowsSP = dtSanPham.Select($"IDSanPham = '{idSP}'");
                    string tenSP = "Không rõ";

                    if (rowsSP.Length > 0)
                    {
                        string hang = rowsSP[0]["Hang"] != DBNull.Value ? rowsSP[0]["Hang"].ToString() : "";
                        string mauSac = rowsSP[0]["mausac"] != DBNull.Value ? rowsSP[0]["mausac"].ToString() : "";
                        tenSP = string.IsNullOrEmpty(hang) && string.IsNullOrEmpty(mauSac)
                            ? "Không rõ"
                            : $"{hang} - {mauSac}";
                    }

                    int soLuong = Convert.ToInt32(rowCT["SoLuong"]);
                    decimal donGia = Convert.ToDecimal(rowCT["DonGia"]);
                    decimal thanhTien = soLuong * donGia;

                    dtHienThi.Rows.Add(
                        rowCT[colIdHoaDon]?.ToString() ?? "",
                        tenSP,
                        soLuong,
                        donGia,
                        thanhTien
                    );
                }

                dgvChiTiet.DataSource = dtHienThi;
                FormatDataGridView();
                this.Text = $"Chi tiết hóa đơn #{idHoaDonCanXem}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chi tiết: " + ex.Message + "\n\nChi tiết: " + ex.StackTrace);
            }
        }

        private void LoadTatCaChiTiet()
        {
            try
            {
                DataTable dtChiTiet = objCTHD.LayDanhSach();
                DataTable dtSanPham = objSP.LayDanhSach();

                // Kiểm tra nếu không có dữ liệu
                if (dtChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có chi tiết hóa đơn nào!");
                    dgvChiTiet.DataSource = null;
                    return;
                }

                // Xác định tên cột
                string colIdHoaDon = GetColumnName(dtChiTiet, new[] { "IdHoaDon", "IDHoaDon" });
                string colIdSanPham = GetColumnName(dtChiTiet, new[] { "IdSanPham", "IDSanPham" });

                DataTable dtHienThi = new DataTable();
                dtHienThi.Columns.Add("Mã HĐ", typeof(string));
                dtHienThi.Columns.Add("Tên Sản Phẩm", typeof(string));
                dtHienThi.Columns.Add("Số Lượng", typeof(int));
                dtHienThi.Columns.Add("Đơn Giá", typeof(decimal));
                dtHienThi.Columns.Add("Thành Tiền", typeof(decimal));

                foreach (DataRow rowCT in dtChiTiet.Rows)
                {
                    string idSP = rowCT[colIdSanPham]?.ToString() ?? "";

                    DataRow[] rowsSP = dtSanPham.Select($"IDSanPham = '{idSP}'");
                    string tenSP = "Không rõ";

                    if (rowsSP.Length > 0)
                    {
                        string hang = rowsSP[0]["Hang"] != DBNull.Value ? rowsSP[0]["Hang"].ToString() : "";
                        string mauSac = rowsSP[0]["mausac"] != DBNull.Value ? rowsSP[0]["mausac"].ToString() : "";
                        tenSP = string.IsNullOrEmpty(hang) && string.IsNullOrEmpty(mauSac)
                            ? "Không rõ"
                            : $"{hang} - {mauSac}";
                    }

                    int soLuong = Convert.ToInt32(rowCT["SoLuong"]);
                    decimal donGia = Convert.ToDecimal(rowCT["DonGia"]);
                    decimal thanhTien = soLuong * donGia;

                    dtHienThi.Rows.Add(
                        rowCT[colIdHoaDon]?.ToString() ?? "",
                        tenSP,
                        soLuong,
                        donGia,
                        thanhTien
                    );
                }

                DataView dv = new DataView(dtHienThi);
                dv.Sort = "Mã HĐ DESC";
                dgvChiTiet.DataSource = dv.ToTable();
                FormatDataGridView();
                this.Text = "Chi tiết tất cả hóa đơn";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load tất cả chi tiết: " + ex.Message + "\n\nChi tiết: " + ex.StackTrace);
            }
        }

        // Hàm hỗ trợ: Tìm tên cột chính xác trong DataTable
        private string GetColumnName(DataTable dt, string[] possibleNames)
        {
            foreach (string name in possibleNames)
            {
                if (dt.Columns.Contains(name))
                    return name;
            }
            // Trả về tên đầu tiên nếu không tìm thấy (sẽ gây lỗi để debug)
            return possibleNames[0];
        }

        // Hàm format DataGridView
        private void FormatDataGridView()
        {
            if (dgvChiTiet.Columns["Đơn Giá"] != null)
                dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Format = "#,### VNĐ";
            if (dgvChiTiet.Columns["Thành Tiền"] != null)
                dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.Format = "#,### VNĐ";

            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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