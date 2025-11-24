
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Dangnhap));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txttendangnhap = new System.Windows.Forms.TextBox();
            this.txtmatkhau = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cobvaitro = new System.Windows.Forms.ComboBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(446, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(446, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mật khẩu";
            // 
            // txttendangnhap
            // 
            this.txttendangnhap.BackColor = System.Drawing.Color.White;
            this.txttendangnhap.Location = new System.Drawing.Point(449, 274);
            this.txttendangnhap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txttendangnhap.Name = "txttendangnhap";
            this.txttendangnhap.Size = new System.Drawing.Size(292, 26);
            this.txttendangnhap.TabIndex = 2;
            this.txttendangnhap.TextChanged += new System.EventHandler(this.txttendangnhap_TextChanged);
            // 
            // txtmatkhau
            // 
            this.txtmatkhau.BackColor = System.Drawing.Color.White;
            this.txtmatkhau.Location = new System.Drawing.Point(449, 344);
            this.txtmatkhau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtmatkhau.Name = "txtmatkhau";
            this.txtmatkhau.Size = new System.Drawing.Size(292, 26);
            this.txtmatkhau.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(465, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(291, 52);
            this.label3.TabIndex = 4;
            this.label3.Text = "CAFE SHOP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(446, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Vai trò";
            // 
            // cobvaitro
            // 
            this.cobvaitro.BackColor = System.Drawing.Color.White;
            this.cobvaitro.FormattingEnabled = true;
            this.cobvaitro.Location = new System.Drawing.Point(449, 414);
            this.cobvaitro.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cobvaitro.Name = "cobvaitro";
            this.cobvaitro.Size = new System.Drawing.Size(292, 28);
            this.cobvaitro.TabIndex = 6;
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.Transparent;
            this.btn_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.Location = new System.Drawing.Point(502, 476);
            this.btn_login.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(184, 54);
            this.btn_login.TabIndex = 7;
            this.btn_login.Text = "Đăng nhập";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // Form_Dangnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1212, 709);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.cobvaitro);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtmatkhau);
            this.Controls.Add(this.txttendangnhap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_Dangnhap";
            this.Text = "Form_Dangnhap";
            this.Load += new System.EventHandler(this.Form_Dangnhap_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txttendangnhap;
        private System.Windows.Forms.TextBox txtmatkhau;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobvaitro;
        private System.Windows.Forms.Button btn_login;
    }
}

