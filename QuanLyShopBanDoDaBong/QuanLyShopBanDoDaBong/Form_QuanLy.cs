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
        }

        private void quanLyTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hienthiformcon(new Form_QLTaiKhoan());
            this.Text = "Hệ thống quản lý - Quản lý Tài Khoản";
        }

        private void quanLyDanhMucToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hienthiformcon(new Form_QLDanhMuc());
            this.Text = "Hệ thống quản lý - Quản lý Danh Mục";
        }

        private void quanLySanPhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hienthiformcon(new Form_QLSanPham());
            this.Text = "Hệ thống quản lý - Quản lý Sản Phẩm";
        }

        private void quanLyHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hienthiformcon(new Form_HoaDon());
            this.Text = "Hệ thống quản lý - Quản lý Hóa Đơn";
        }

        private void quanLyBinhLuanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hienthiformcon(new Form_BinhLuan());
            this.Text = "Hệ thống quản lý - Quản lý Bình Luận";
        }

        private void baoCaoThongKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hienthiformcon(new Form_ThongKe());
            this.Text = "Hệ thống quản lý - Thống kê & Báo cáo";
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}