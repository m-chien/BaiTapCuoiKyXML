using QuanLyShopBanDoDaBong.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_HoaDon : Form
    {
        HoaDon objHD = new HoaDon();

        public Form_HoaDon()
        {
            InitializeComponent();
        }

        private void Form_HoaDon_Load(object sender, EventArgs e)
        {
            // Khởi tạo ComboBox Trạng thái
            cbbTrangThai.Items.Clear();
            cbbTrangThai.Items.AddRange(new string[] { "Tất cả", "Đã thanh toán", "Chờ thanh toán", "Hủy thanh toán" });
            cbbTrangThai.SelectedIndex = 0;

            // Khởi tạo DateTimePicker
            dtpNgay.Format = DateTimePickerFormat.Custom;
            dtpNgay.CustomFormat = "dd/MM/yyyy";

            if (chkLocNgay != null)
            {
                chkLocNgay.Checked = false;
                dtpNgay.Enabled = false;
            }

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvHoaDon.DataSource = objHD.LayDanhSach();

                if (dgvHoaDon.Columns.Count > 0)
                {
                    // Đặt tiêu đề cột
                    if (dgvHoaDon.Columns.Contains("IDHoaDon"))
                        dgvHoaDon.Columns["IDHoaDon"].HeaderText = "Mã HĐ";
                    if (dgvHoaDon.Columns.Contains("IdUser"))
                        dgvHoaDon.Columns["IdUser"].HeaderText = "Mã khách hàng";
                    if (dgvHoaDon.Columns.Contains("TongTien"))
                    {
                        dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng tiền";
                        dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "#,### VNĐ";
                    }
                    if (dgvHoaDon.Columns.Contains("DiaChiGiaoHang"))
                        dgvHoaDon.Columns["DiaChiGiaoHang"].HeaderText = "Địa chỉ";
                    if (dgvHoaDon.Columns.Contains("NgayDat"))
                        dgvHoaDon.Columns["NgayDat"].HeaderText = "Ngày đặt";
                    if (dgvHoaDon.Columns.Contains("TrangThai"))
                        dgvHoaDon.Columns["TrangThai"].HeaderText = "Trạng thái";

                    // Auto size columns
                    dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }

                this.Text = $"Quản lý hóa đơn - Tìm thấy {dgvHoaDon.Rows.Count} kết quả";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        // --- TÌM KIẾM ---
        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = objHD.LayDanhSach();
                DataView dv = new DataView(dt);

                string filter = "";

                // Lọc theo trạng thái
                string trangThai = cbbTrangThai.SelectedItem?.ToString() ?? "Tất cả";
                if (trangThai != "Tất cả")
                {
                    filter = $"TrangThai = '{trangThai}'";
                }

                // Lọc theo ngày (nếu checkbox được chọn)
                if (chkLocNgay != null && chkLocNgay.Checked)
                {
                    string ngayChon = dtpNgay.Value.ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(filter))
                        filter += " AND ";
                    filter += $"NgayDat = '{ngayChon}'";
                }

                // Lọc theo tổng tiền (nếu có nhập)
                if (!string.IsNullOrWhiteSpace(txtTongTien.Text))
                {
                    if (decimal.TryParse(txtTongTien.Text, out decimal tienChon))
                    {
                        if (!string.IsNullOrEmpty(filter))
                            filter += " AND ";
                        filter += $"TongTien >= {tienChon}";
                    }
                    else
                    {
                        MessageBox.Show("Số tiền không hợp lệ!");
                        return;
                    }
                }

                dv.RowFilter = filter;
                dgvHoaDon.DataSource = dv.ToTable();

                // Format lại cột Tổng tiền
                if (dgvHoaDon.Columns.Contains("TongTien"))
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "#,### VNĐ";

                this.Text = $"Quản lý hóa đơn - Tìm thấy {dv.Count} kết quả";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // --- LÀM MỚI ---
        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            cbbTrangThai.SelectedIndex = 0;
            txtTongTien.Clear();
            if (chkLocNgay != null)
            {
                chkLocNgay.Checked = false;
                dtpNgay.Enabled = false;
            }
            LoadData();
        }

        // --- XEM CHI TIẾT HÓA ĐƠN ---
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow != null && dgvHoaDon.CurrentRow.Index >= 0)
            {
                var cellValue = dgvHoaDon.CurrentRow.Cells["IDHoaDon"].Value;

                if (cellValue != null && cellValue != DBNull.Value)
                {
                    int idHoaDon = Convert.ToInt32(cellValue);

                    // Mở form chi tiết hóa đơn
                    Form_ChiTietHoaDon f = new Form_ChiTietHoaDon(idHoaDon);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không lấy được mã hóa đơn!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xem!");
            }
        }

        // --- TẠO XML ---
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo XML cho HoaDon
                objHD.KhoiTaoXML();

                // Tạo XML cho ChiTietHoaDon (THIẾU CÁI NÀY!)
                ChiTietHoaDon objCTHD = new ChiTietHoaDon();
                objCTHD.KhoiTaoXML();

                MessageBox.Show("Đã đồng bộ HoaDon và ChiTietHoaDon từ SQL!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo XML: " + ex.Message);
            }
        }

        // --- BẬT/TẮT DateTimePicker khi checkbox thay đổi ---
        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (dtpNgay != null)
            {
                dtpNgay.Enabled = chkLocNgay.Checked;
            }
        }

        // --- CHỈ CHO PHÉP NHẬP SỐ VÀO TEXTBOX TỔNG TIỀN ---
        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Để trống hoặc xử lý click cell nếu cần
        }
    }
}