using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_HoaDon : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnect"].ConnectionString;

        public Form_HoaDon()
        {
            InitializeComponent();
        }

        private void Form_HoaDon_Load(object sender, EventArgs e)
        {
            cbbTrangThai.Items.Clear();
            cbbTrangThai.Items.AddRange(new string[] { "Tất cả", "Đã thanh toán", "Chờ thanh toán", "Hủy thanh toán" });
            cbbTrangThai.SelectedIndex = 0;

            dtpNgay.Format = DateTimePickerFormat.Custom;
            dtpNgay.CustomFormat = "dd/MM/yyyy";

            if (chkLocNgay != null)
            {
                chkLocNgay.Checked = false;
                dtpNgay.Enabled = false;
            }
            LoadHoaDon();
        }

        private void LoadHoaDon(string trangThai = "Tất cả", DateTime? ngay = null, decimal? tongTien = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    StringBuilder query = new StringBuilder(@"
                        SELECT 
                            h.IDHoaDon AS [Mã HĐ],
                            n.Email AS [Khách hàng],
                            h.TongTien AS [Tổng tiền],
                            h.DiaChiGiaoHang AS [Địa chỉ],
                            h.NgayDat AS [Ngày đặt],
                            h.TrangThai AS [Trạng thái]
                        FROM HoaDon h
                        JOIN NguoiDung n ON h.IdUser = n.IDNguoiDung
                        WHERE 1=1");

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    if (trangThai != "Tất cả")
                    {
                        query.Append(" AND h.TrangThai = @TrangThai");
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    }

                    if (ngay != null)
                    {
                        query.Append(" AND CAST(h.NgayDat AS DATE) = CAST(@Ngay AS DATE)");
                        cmd.Parameters.AddWithValue("@Ngay", ngay.Value);
                    }

                    if (tongTien != null)
                    {
                        query.Append(" AND h.TongTien >= @TongTien");
                        cmd.Parameters.AddWithValue("@TongTien", tongTien.Value);
                    }

                    query.Append(" ORDER BY h.NgayDat DESC");
                    cmd.CommandText = query.ToString();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvHoaDon.DataSource = dt;

                    if (dgvHoaDon.Columns["Tổng tiền"] != null)
                        dgvHoaDon.Columns["Tổng tiền"].DefaultCellStyle.Format = "#,### VNĐ";

                    dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    this.Text = $"Quản lý hóa đơn - Tìm thấy {dt.Rows.Count} kết quả";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            string trangThai = cbbTrangThai.SelectedItem?.ToString() ?? "Tất cả";

            DateTime? ngayChon = null;
            if (chkLocNgay.Checked)
            {
                ngayChon = dtpNgay.Value;
            }

            decimal? tienChon = null;
            if (!string.IsNullOrWhiteSpace(txtTongTien.Text))
            {
                if (decimal.TryParse(txtTongTien.Text, out decimal temp))
                    tienChon = temp;
                else
                {
                    MessageBox.Show("Số tiền không hợp lệ!");
                    return;
                }
            }

            LoadHoaDon(trangThai, ngayChon, tienChon);
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            cbbTrangThai.SelectedIndex = 0;
            txtTongTien.Clear();
            chkLocNgay.Checked = false;
            dtpNgay.Enabled = false; 
            LoadHoaDon();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow != null && dgvHoaDon.CurrentRow.Index >= 0)
            {
                var cellValue = dgvHoaDon.CurrentRow.Cells["Mã HĐ"].Value;

                if (cellValue != DBNull.Value)
                {
                    int id = Convert.ToInt32(cellValue);
                    Form_ChiTietHoaDon f = new Form_ChiTietHoaDon(id);
                    f.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xem!");
            }
        }

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgay.Enabled = chkLocNgay.Checked;
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dgvHoaDon.DataSource;

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu hóa đơn để xuất!");
                    return;
                }

                dt.TableName = "HoaDon";

                string fileName = "DanhSachHoaDon_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml";
                string path = Path.Combine(Application.StartupPath, fileName);

                dt.WriteXml(path, XmlWriteMode.WriteSchema);

                MessageBox.Show("Xuất XML thành công!\nĐường dẫn: " + path);

                System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất XML: " + ex.Message);
            }
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}