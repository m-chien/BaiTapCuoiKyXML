using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
<<<<<<< HEAD
namespace WFA_Quanlyquancafe
=======
using System.Configuration;
namespace QuanLyShopBanDoDaBong
>>>>>>> f733cb8033062cfa1bbcad5177f21c571b13faec
{
    public partial class Form_DangNhap : Form
    {
        // Chuỗi kết nối tới cơ sở dữ liệu SQL Server
<<<<<<< HEAD
        private string connectionString = "Data Source=DESKTOP-ER8LV7D; Initial Catalog=FootballShop; Integrated Security=True";
        public Form_Dangnhap()
=======
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnect"].ConnectionString;
        public Form_DangNhap()
>>>>>>> f733cb8033062cfa1bbcad5177f21c571b13faec
        {
            InitializeComponent();
        }
        private void LoadRoles()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT VaiTro FROM NguoiDung";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        cobvaitro.Items.Clear();
                        while (reader.Read())
                        {
                            cobvaitro.Items.Add(reader["VaiTro"].ToString());
                        }
                    }
                }

                if (cobvaitro.Items.Count > 0)
                {
                    cobvaitro.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải vai trò: " + ex.Message);
            }
        }



        private void btn_login_Click(object sender, EventArgs e)
        {
            string username = txttendangnhap.Text;
            string password = txtmatkhau.Text;

            if (cobvaitro.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn vai trò!");
                return;
            }

            string selectedRole = cobvaitro.SelectedItem.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT IDNguoiDung, Email, Password, VaiTro
                FROM NguoiDung
                WHERE Email = @Email AND VaiTro = @Role";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", username);
                        cmd.Parameters.AddWithValue("@Role", selectedRole);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Kiểm tra mật khẩu
                            if (reader["Password"].ToString() != password)
                            {
                                MessageBox.Show("Mật khẩu không đúng.");
                                return;
                            }

                            // Đăng nhập thành công
                            MessageBox.Show(
                                $"Đăng nhập thành công với vai trò: {selectedRole}"
                            );

                            Form_QuanLy f = new Form_QuanLy();
                            f.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Email hoặc vai trò không đúng!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng nhập: " + ex.Message);
            }
        }



        // Khi form được load, gọi LoadRoles để nạp các vai trò
        private void Form_Dangnhap_Load(object sender, EventArgs e)
        {
            
        }

        private void Form_Dangnhap_Load_1(object sender, EventArgs e)
        {
            LoadRoles();
        }

        private void txttendangnhap_TextChanged(object sender, EventArgs e)
        {

        }

<<<<<<< HEAD
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
=======
        private void pnlRight_Paint(object sender, PaintEventArgs e)
>>>>>>> f733cb8033062cfa1bbcad5177f21c571b13faec
        {

        }
    }
}
