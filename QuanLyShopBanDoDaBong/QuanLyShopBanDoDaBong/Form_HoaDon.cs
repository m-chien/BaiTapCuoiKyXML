using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration; // Nhớ thêm thư viện này

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_HoaDon : Form
    {
        // Chuỗi kết nối
        string connectionString = @"Data Source=.;Initial Catalog=FootballShop;Integrated Security=True";

        public Form_HoaDon()
        {
            InitializeComponent();
        }

        private void Form_HoaDon_Load(object sender, EventArgs e)
        {
            // Cấu hình ComboBox
            cbbTrangThai.Items.Clear();
            cbbTrangThai.Items.AddRange(new string[] { "Tất cả", "Đã thanh toán", "Chờ thanh toán", "Hủy thanh toán" });
            cbbTrangThai.SelectedIndex = 0;

            // Cấu hình DateTimePicker
            dtpNgay.Format = DateTimePickerFormat.Custom;
            dtpNgay.CustomFormat = "dd/MM/yyyy";

            // Mặc định bỏ chọn lọc ngày
            if (chkLocNgay != null) // Kiểm tra null để tránh lỗi nếu chưa có checkbox
            {
                chkLocNgay.Checked = false;
                dtpNgay.Enabled = false;
            }

            // Load dữ liệu ban đầu
            LoadHoaDon();
        }

        // --- HÀM TẢI DỮ LIỆU ---
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

                    // 1. Lọc theo Trạng thái
                    if (trangThai != "Tất cả")
                    {
                        query.Append(" AND h.TrangThai = @TrangThai");
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    }

                    // 2. Lọc theo Ngày (Chỉ lọc khi tham số ngay KHÁC NULL)
                    if (ngay != null)
                    {
                        query.Append(" AND CAST(h.NgayDat AS DATE) = CAST(@Ngay AS DATE)");
                        cmd.Parameters.AddWithValue("@Ngay", ngay.Value);
                    }

                    // 3. Lọc theo Tiền (Lớn hơn hoặc bằng)
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

                    // Format cột tiền
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

        // --- SỰ KIỆN TÌM KIẾM ---
        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            // 1. Lấy trạng thái
            string trangThai = cbbTrangThai.SelectedItem?.ToString() ?? "Tất cả";

            // 2. Lấy ngày (QUAN TRỌNG: Chỉ lấy khi CheckBox được tích)
            DateTime? ngayChon = null;
            if (chkLocNgay.Checked)
            {
                ngayChon = dtpNgay.Value;
            }

            // 3. Lấy tiền
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

            // Gọi hàm load với các tham số đã chuẩn bị
            LoadHoaDon(trangThai, ngayChon, tienChon);
        }

        // --- SỰ KIỆN LÀM MỚI ---
        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            cbbTrangThai.SelectedIndex = 0;
            txtTongTien.Clear();
            chkLocNgay.Checked = false; // Bỏ tick chọn ngày
            dtpNgay.Enabled = false;    // Khóa ô ngày lại
            LoadHoaDon(); // Load lại tất cả
        }

        // --- XEM CHI TIẾT ---
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow != null && dgvHoaDon.CurrentRow.Index >= 0)
            {
                // Lấy ID hóa đơn (Cột đầu tiên hoặc theo tên cột "Mã HĐ")
                // Kiểm tra kỹ tên cột trong Database/Query của bạn
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

        // --- SỰ KIỆN CHECKBOX (TÙY CHỌN CHO ĐẸP) ---
        // Bạn cần gán sự kiện CheckedChanged cho checkbox này trong Design
        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu tick -> Cho phép chọn ngày. Không tick -> Mờ đi
            dtpNgay.Enabled = chkLocNgay.Checked;
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
    }
}