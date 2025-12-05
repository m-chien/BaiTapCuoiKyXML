using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyShopBanDoDaBong
{
    public partial class Form_QuanLy : Form
    {
        private Form currentFormChild;

        public Form_QuanLy()
        {
            InitializeComponent();
        }

        // Hàm hiển thị form con chung
        private void Hienthiformcon(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            currentFormChild = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        private void Form_QuanLy_Load(object sender, EventArgs e)
        {
            // Có thể mặc định mở form trang chủ hoặc thống kê ở đây nếu muốn
        }

        // 1. Quản lý Tài khoản (Đã đổi tên hàm cho chuẩn)
        private void quanLyTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hienthiformcon(new Form_QLTaiKhoan());
            this.Text = "Hệ thống quản lý - Quản lý Tài Khoản";
        }

        // 2. Quản lý Danh mục (MỚI THÊM)
        private void quanLyDanhMucToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hienthiformcon(new Form_QLDanhMuc());
            this.Text = "Hệ thống quản lý - Quản lý Danh Mục";
        }

        // 3. Quản lý Sản phẩm (Đã đổi tên hàm cho chuẩn)
        private void quanLySanPhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hienthiformcon(new Form_QLSanPham());
            this.Text = "Hệ thống quản lý - Quản lý Sản Phẩm";
        }

        // 4. Quản lý Hóa đơn (Đã đổi tên hàm cho chuẩn)
        private void quanLyHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Nếu chưa có Form_HoaDon thì comment dòng dưới lại để tránh lỗi
            // Hienthiformcon(new Form_HoaDon()); 
            Hienthiformcon(new Form_HoaDon());
            this.Text = "Hệ thống quản lý - Quản lý Hóa Đơn";
        }

        // 5. Quản lý Bình luận (Đã đổi tên hàm cho chuẩn)
        private void quanLyBinhLuanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Nếu chưa có Form_BinhLuan thì comment dòng dưới lại
            // Hienthiformcon(new Form_BinhLuan());
            Hienthiformcon(new Form_BinhLuan());
            this.Text = "Hệ thống quản lý - Quản lý Bình Luận";
        }

        // 6. Báo cáo Thống kê (Đã đổi tên hàm cho chuẩn)
        private void baoCaoThongKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Nếu chưa có Form_ThongKe thì comment dòng dưới lại
            // Hienthiformcon(new Form_ThongKe());
            Hienthiformcon(new Form_ThongKe());
            this.Text = "Hệ thống quản lý - Thống kê & Báo cáo";
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}