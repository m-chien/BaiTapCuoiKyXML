namespace WFA_Quanlyquancafe
{
    partial class Form_Dangnhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelCard = new System.Windows.Forms.Panel();
            this.lblRegister = new System.Windows.Forms.Label();
            this.lblNoAccount = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.panelVaiTro = new System.Windows.Forms.Panel();
            this.cobvaitro = new System.Windows.Forms.ComboBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.lblForgotPassword = new System.Windows.Forms.Label();
            this.btnShowHide = new System.Windows.Forms.Button();
            this.txtmatkhau = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelEmail = new System.Windows.Forms.Panel();
            this.txttendangnhap = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelCard.SuspendLayout();
            this.panelVaiTro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panelPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelMain.Controls.Add(this.panelCard);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1197, 799);
            this.panelMain.TabIndex = 0;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // panelCard
            // 
            this.panelCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.lblRegister);
            this.panelCard.Controls.Add(this.lblNoAccount);
            this.panelCard.Controls.Add(this.btn_login);
            this.panelCard.Controls.Add(this.panelVaiTro);
            this.panelCard.Controls.Add(this.panelPassword);
            this.panelCard.Controls.Add(this.panelEmail);
            this.panelCard.Controls.Add(this.label3);
            this.panelCard.Controls.Add(this.lblWelcome);
            this.panelCard.Location = new System.Drawing.Point(365, 85);
            this.panelCard.Name = "panelCard";
            this.panelCard.Size = new System.Drawing.Size(497, 600);
            this.panelCard.TabIndex = 0;
            // 
            // lblRegister
            // 
            this.lblRegister.AutoSize = true;
            this.lblRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRegister.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.lblRegister.Location = new System.Drawing.Point(252, 535);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(129, 25);
            this.lblRegister.TabIndex = 8;
            this.lblRegister.Text = "Đăng ký ngay";
            // 
            // lblNoAccount
            // 
            this.lblNoAccount.AutoSize = true;
            this.lblNoAccount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNoAccount.ForeColor = System.Drawing.Color.Gray;
            this.lblNoAccount.Location = new System.Drawing.Point(93, 535);
            this.lblNoAccount.Name = "lblNoAccount";
            this.lblNoAccount.Size = new System.Drawing.Size(163, 25);
            this.lblNoAccount.TabIndex = 7;
            this.lblNoAccount.Text = "Chưa có tài khoản?";
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.FlatAppearance.BorderSize = 0;
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_login.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn_login.ForeColor = System.Drawing.Color.White;
            this.btn_login.Location = new System.Drawing.Point(40, 468);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(427, 50);
            this.btn_login.TabIndex = 6;
            this.btn_login.Text = "Đăng nhập";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // panelVaiTro
            // 
            this.panelVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.panelVaiTro.Controls.Add(this.cobvaitro);
            this.panelVaiTro.Controls.Add(this.pictureBox3);
            this.panelVaiTro.Controls.Add(this.label4);
            this.panelVaiTro.Location = new System.Drawing.Point(40, 365);
            this.panelVaiTro.Name = "panelVaiTro";
            this.panelVaiTro.Size = new System.Drawing.Size(407, 75);
            this.panelVaiTro.TabIndex = 5;
            // 
            // cobvaitro
            // 
            this.cobvaitro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.cobvaitro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobvaitro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cobvaitro.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cobvaitro.ForeColor = System.Drawing.Color.Black;
            this.cobvaitro.FormattingEnabled = true;
            this.cobvaitro.Location = new System.Drawing.Point(10, 31);
            this.cobvaitro.Name = "cobvaitro";
            this.cobvaitro.Size = new System.Drawing.Size(385, 38);
            this.cobvaitro.TabIndex = 2;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Location = new System.Drawing.Point(360, 5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Vai trò";
            // 
            // panelPassword
            // 
            this.panelPassword.BackColor = System.Drawing.Color.Transparent;
            this.panelPassword.Controls.Add(this.lblForgotPassword);
            this.panelPassword.Controls.Add(this.btnShowHide);
            this.panelPassword.Controls.Add(this.txtmatkhau);
            this.panelPassword.Controls.Add(this.pictureBox2);
            this.panelPassword.Controls.Add(this.label2);
            this.panelPassword.Location = new System.Drawing.Point(45, 245);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(402, 80);
            this.panelPassword.TabIndex = 4;
            // 
            // lblForgotPassword
            // 
            this.lblForgotPassword.AutoSize = true;
            this.lblForgotPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblForgotPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblForgotPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.lblForgotPassword.Location = new System.Drawing.Point(248, 5);
            this.lblForgotPassword.Name = "lblForgotPassword";
            this.lblForgotPassword.Size = new System.Drawing.Size(151, 25);
            this.lblForgotPassword.TabIndex = 9;
            this.lblForgotPassword.Text = "Quên mật khẩu?";
            // 
            // btnShowHide
            // 
            this.btnShowHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.btnShowHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowHide.FlatAppearance.BorderSize = 0;
            this.btnShowHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHide.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnShowHide.ForeColor = System.Drawing.Color.Gray;
            this.btnShowHide.Location = new System.Drawing.Point(359, 36);
            this.btnShowHide.Name = "btnShowHide";
            this.btnShowHide.Size = new System.Drawing.Size(40, 35);
            this.btnShowHide.TabIndex = 3;
            this.btnShowHide.Text = "👁";
            this.btnShowHide.UseVisualStyleBackColor = false;
            // 
            // txtmatkhau
            // 
            this.txtmatkhau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtmatkhau.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtmatkhau.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtmatkhau.ForeColor = System.Drawing.Color.Black;
            this.txtmatkhau.Location = new System.Drawing.Point(5, 36);
            this.txtmatkhau.Multiline = true;
            this.txtmatkhau.Name = "txtmatkhau";
            this.txtmatkhau.PasswordChar = '●';
            this.txtmatkhau.Size = new System.Drawing.Size(394, 35);
            this.txtmatkhau.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(340, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mật khẩu";
            // 
            // panelEmail
            // 
            this.panelEmail.BackColor = System.Drawing.Color.Transparent;
            this.panelEmail.Controls.Add(this.txttendangnhap);
            this.panelEmail.Controls.Add(this.pictureBox1);
            this.panelEmail.Controls.Add(this.label1);
            this.panelEmail.Location = new System.Drawing.Point(45, 145);
            this.panelEmail.Name = "panelEmail";
            this.panelEmail.Size = new System.Drawing.Size(402, 80);
            this.panelEmail.TabIndex = 3;
            // 
            // txttendangnhap
            // 
            this.txttendangnhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txttendangnhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txttendangnhap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txttendangnhap.ForeColor = System.Drawing.Color.Black;
            this.txttendangnhap.Location = new System.Drawing.Point(8, 32);
            this.txttendangnhap.Multiline = true;
            this.txttendangnhap.Name = "txttendangnhap";
            this.txttendangnhap.Size = new System.Drawing.Size(382, 35);
            this.txttendangnhap.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(340, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email hoặc Tên đăng nhập";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(3, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(491, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Đăng nhập để tiếp tục mua sắm";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblWelcome.Location = new System.Drawing.Point(3, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(491, 64);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Chào mừng trở lại";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_Dangnhap
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1197, 799);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_Dangnhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Football Shop - Đăng Nhập";
            this.Load += new System.EventHandler(this.Form_Dangnhap_Load_1);
            this.panelMain.ResumeLayout(false);
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            this.panelVaiTro.ResumeLayout(false);
            this.panelVaiTro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelEmail.ResumeLayout(false);
            this.panelEmail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelEmail;
        private System.Windows.Forms.TextBox txttendangnhap;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.Button btnShowHide;
        private System.Windows.Forms.TextBox txtmatkhau;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelVaiTro;
        private System.Windows.Forms.ComboBox cobvaitro;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Label lblNoAccount;
        private System.Windows.Forms.Label lblRegister;
        private System.Windows.Forms.Label lblForgotPassword;
    }
}