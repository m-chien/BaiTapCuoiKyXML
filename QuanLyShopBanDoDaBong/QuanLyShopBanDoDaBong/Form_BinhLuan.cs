using QuanLyShopBanDoDaBong.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_BinhLuan : Form
    {
        BinhLuan objBL = new BinhLuan();

        public Form_BinhLuan()
        {
            InitializeComponent();
        }

        private void Form_BinhLuan_Load(object sender, EventArgs e)
        {
            // Khởi tạo ComboBox Tình trạng
            cbbTinhTrang.Items.Clear();
            cbbTinhTrang.Items.AddRange(new string[] { "Tất cả", "Chờ duyệt", "Đã duyệt" });
            cbbTinhTrang.SelectedIndex = 0;

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
                dgvBinhLuan.DataSource = objBL.LayDanhSach();

                if (dgvBinhLuan.Columns.Count > 0)
                {
                    // Đặt tiêu đề cột
                    if (dgvBinhLuan.Columns.Contains("IDBinhLuan"))
                        dgvBinhLuan.Columns["IDBinhLuan"].HeaderText = "Mã BL";
                    if (dgvBinhLuan.Columns.Contains("IDNguoiDung"))
                        dgvBinhLuan.Columns["IDNguoiDung"].HeaderText = "Mã người dùng";
                    if (dgvBinhLuan.Columns.Contains("IdSanPham"))
                        dgvBinhLuan.Columns["IdSanPham"].HeaderText = "Mã sản phẩm";
                    if (dgvBinhLuan.Columns.Contains("NoiDung"))
                        dgvBinhLuan.Columns["NoiDung"].HeaderText = "Nội dung";
                    if (dgvBinhLuan.Columns.Contains("NgayBinhLuan"))
                        dgvBinhLuan.Columns["NgayBinhLuan"].HeaderText = "Ngày bình luận";
                    if (dgvBinhLuan.Columns.Contains("TinhTrang"))
                        dgvBinhLuan.Columns["TinhTrang"].HeaderText = "Tình trạng";

                    // Auto size columns
                    dgvBinhLuan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }

                this.Text = $"Quản lý bình luận - Tìm thấy {dgvBinhLuan.Rows.Count} kết quả";
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
                DataTable dt = objBL.LayDanhSach();
                DataView dv = new DataView(dt);

                string filter = "";

                // Lọc theo tình trạng
                string trangThai = cbbTinhTrang.SelectedItem?.ToString() ?? "Tất cả";
                if (trangThai != "Tất cả")
                {
                    filter = $"TinhTrang = '{trangThai}'";
                }

                // Lọc theo ngày (nếu checkbox được chọn)
                if (chkLocNgay != null && chkLocNgay.Checked)
                {
                    string ngayChon = dtpNgay.Value.ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(filter))
                        filter += " AND ";
                    filter += $"NgayBinhLuan = '{ngayChon}'";
                }

                dv.RowFilter = filter;
                dgvBinhLuan.DataSource = dv.ToTable();

                this.Text = $"Quản lý bình luận - Tìm thấy {dv.Count} kết quả";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // --- LÀM MỚI ---
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbbTinhTrang.SelectedIndex = 0;
            if (chkLocNgay != null)
            {
                chkLocNgay.Checked = false;
                dtpNgay.Enabled = false;
            }
            LoadData();
        }

        // --- TẠO XML ---
        private void btnXuatXML_Click(object sender, EventArgs e)
        {
            try
            {
                objBL.KhoiTaoXML();
                MessageBox.Show("Đã đồng bộ lại từ SQL!");
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

        private void dgvBinhLuan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Để trống hoặc xử lý click cell nếu cần
        }
    }
}