using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Thư viện kết nối SQL
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_HoaDon : Form
    {
        // 1. Chuỗi kết nối
        string connectionString = @"Data Source=.;Initial Catalog=FootballShop;Integrated Security=True";

        public Form_HoaDon()
        {
            InitializeComponent();
        }

        private void Form_HoaDon_Load(object sender, EventArgs e)
        {
            // Cấu hình ComboBox
            cbbTrangThai.Items.Clear();
            cbbTrangThai.Items.Add("Tất cả");
            cbbTrangThai.Items.Add("Đã thanh toán");
            cbbTrangThai.Items.Add("Chờ thanh toán");
            cbbTrangThai.Items.Add("Hủy thanh toán");
            cbbTrangThai.SelectedIndex = 0; // Mặc định chọn "Tất cả"

            // Cấu hình DateTimePicker
            dtpNgay.Format = DateTimePickerFormat.Custom;
            dtpNgay.CustomFormat = "dd/MM/yyyy";

            // Mẹo: Set về năm 2024 để khi chọn ngày tìm kiếm sẽ thấy dữ liệu mẫu ngay
            dtpNgay.Value = new DateTime(2024, 02, 01);

            // Load dữ liệu ban đầu (Load tất cả - không truyền tham số)
            LoadHoaDon();
        }

        // Hàm load dữ liệu chung (Dynamic Query giống Form_BinhLuan)
        private void LoadHoaDon(string trangThai = "Tất cả", DateTime? ngay = null, decimal? tongTien = null)
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
                            h.IDHoaDon AS [Mã HĐ],
                            n.Email AS [Khách hàng],
                            h.TongTien AS [Tổng tiền],
                            h.DiaChiGiaoHang AS [Địa chỉ],
                            h.NgayDat AS [Ngày đặt],
                            h.TrangThai AS [Trạng thái]
                        FROM HoaDon h
                        JOIN NguoiDung n ON h.IdUser = n.IDNguoiDung
                        WHERE 1=1"); // Kỹ thuật 1=1 để dễ nối chuỗi AND

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    // 1. ĐIỀU KIỆN LỌC THEO TRẠNG THÁI
                    if (trangThai != "Tất cả")
                    {
                        query.Append(" AND h.TrangThai = @TrangThai");
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    }

                    // 2. ĐIỀU KIỆN LỌC THEO NGÀY (Chỉ khi có giá trị ngày được truyền vào)
                    if (ngay.HasValue)
                    {
                        query.Append(" AND CAST(h.NgayDat AS DATE) = CAST(@Ngay AS DATE)");
                        cmd.Parameters.AddWithValue("@Ngay", ngay.Value);
                    }

                    // 3. ĐIỀU KIỆN LỌC THEO TIỀN (Chỉ khi có giá trị tiền được truyền vào)
                    if (tongTien.HasValue)
                    {
                        query.Append(" AND h.TongTien >= @TongTien");
                        cmd.Parameters.AddWithValue("@TongTien", tongTien.Value);
                    }

                    // Sắp xếp giảm dần theo ngày
                    query.Append(" ORDER BY h.NgayDat DESC");

                    cmd.CommandText = query.ToString();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvHoaDon.DataSource = dt;

                    // Tùy chỉnh giao diện Grid
                    if (dgvHoaDon.Columns["Tổng tiền"] != null)
                        dgvHoaDon.Columns["Tổng tiền"].DefaultCellStyle.Format = "#,### VNĐ";

                    dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Hiển thị số lượng kết quả lên tiêu đề Form
                    this.Text = $"Quản lý hóa đơn - Tìm thấy {dt.Rows.Count} kết quả";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Chặn nhập chữ vào ô tiền
        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            // 1. Lấy trạng thái
            if (cbbTrangThai.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string trangThaiChon = cbbTrangThai.SelectedItem.ToString();

            // 2. Lấy ngày (Mặc định lấy từ DateTimePicker như bạn yêu cầu)
            DateTime ngayChon = dtpNgay.Value;

            // 3. Lấy số tiền (Kiểm tra xem người dùng có nhập không)
            decimal? tienChon = null;
            if (!string.IsNullOrWhiteSpace(txtTongTien.Text))
            {
                decimal temp;
                if (decimal.TryParse(txtTongTien.Text, out temp))
                {
                    tienChon = temp;
                }
                else
                {
                    MessageBox.Show("Số tiền nhập vào không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // GỌI HÀM LOAD VỚI CÁC THAM SỐ
            // Lưu ý: Ở đây ta LUÔN truyền ngày vào, nghĩa là khi bấm tìm kiếm thì bắt buộc lọc theo ngày
            LoadHoaDon(trangThaiChon, ngayChon, tienChon);
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            cbbTrangThai.SelectedIndex = 0; // Chọn tất cả
            txtTongTien.Clear();            // Xóa ô tiền
            dtpNgay.Value = DateTime.Now;   // Reset ngày

            // Gọi hàm không tham số -> Load toàn bộ danh sách
            LoadHoaDon();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng có chọn dòng nào chưa
            if (dgvHoaDon.CurrentRow == null || dgvHoaDon.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Lấy ID từ dòng đang chọn
            // Lưu ý: "Mã HĐ" là tên cột bạn đã đặt trong câu SQL ở hàm LoadHoaDon
            // Nếu bạn đổi tên cột trong SQL thì ở đây phải đổi theo
            string idString = dgvHoaDon.CurrentRow.Cells["Mã HĐ"].Value.ToString();
            int idHoaDon = int.Parse(idString);

            // 3. Khởi tạo Form chi tiết và truyền ID sang
            Form_ChiTietHoaDon f = new Form_ChiTietHoaDon(idHoaDon);

            // 4. Hiển thị Form lên
            f.ShowDialog(); // Dùng ShowDialog để khi tắt form chi tiết mới quay lại form chính được
        }
    }
}