using System;
using System.Data;

namespace QuanLyShopBanDoDaBong.Class
{
    class TaiKhoan
    {
        TaoXML db = new TaoXML();
        string fileName = "NguoiDung.xml";
        string tableName = "NguoiDung";
        string colID = "IDNguoiDung";

        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        public bool KiemTraEmail(string email)
        {
            return db.KiemTra(fileName, "Email", email);
        }
        public void ThemTK(string email, string pass, string role)
        {
            // BƯỚC 1: TÍNH TOÁN ID TỰ TĂNG (Cho XML)
            DataTable dt = LayDanhSach(); // Lấy dữ liệu hiện tại
            int nextID = 1; // Mặc định là 1 nếu danh sách rỗng

            if (dt.Rows.Count > 0)
            {
                // Tìm ID lớn nhất trong cột IDNguoiDung
                try
                {
                    int max = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        int currentID = int.Parse(row["IDNguoiDung"].ToString());
                        if (currentID > max) max = currentID;
                    }
                    nextID = max + 1;
                }
                catch
                {
                    // Nếu lỗi ép kiểu thì random tạm để không crash
                    nextID = new Random().Next(1000, 9999);
                }
            }

            // BƯỚC 2: TẠO XML VỚI ID MỚI TÍNH ĐƯỢC
            string xml = "<NguoiDung>" +
                            "<IDNguoiDung>" + nextID + "</IDNguoiDung>" +
                            "<Email>" + email + "</Email>" +
                            "<password>" + pass + "</password>" +
                            "<sdt></sdt>" +
                            "<DiaChi></DiaChi>" +
                            "<AvatarURL></AvatarURL>" +
                            "<VaiTro>" + role + "</VaiTro>" +
                            "<gioitinh></gioitinh>" +
                         "</NguoiDung>";

            // Thêm vào file XML
            db.Them(fileName, xml);

            // BƯỚC 3: ĐỒNG BỘ XUỐNG SQL
            db.Them_Database(tableName, fileName, "IDNguoiDung");

            // BƯỚC 4: ĐỒNG BỘ NGƯỢC LẠI
            KhoiTaoXML();
        }

        // Hàm SỬA TÀI KHOẢN
        public void SuaTK(string id, string email, string pass, string role)
        {
            // BƯỚC 1: Lấy dữ liệu cũ để không bị mất thông tin (SĐT, Địa chỉ...)
            DataTable dt = LayDanhSach();
            DataRow[] rows = dt.Select($"{colID} = '{id}'");

            string sdt = "", diaChi = "", avatar = "", gioiTinh = "";

            if (rows.Length > 0)
            {
                // Lấy lại giá trị cũ
                // Dùng .ToString() và kiểm tra null an toàn
                sdt = rows[0]["sdt"] != DBNull.Value ? rows[0]["sdt"].ToString() : "";
                diaChi = rows[0]["DiaChi"] != DBNull.Value ? rows[0]["DiaChi"].ToString() : "";
                avatar = rows[0]["AvatarURL"] != DBNull.Value ? rows[0]["AvatarURL"].ToString() : "";
                gioiTinh = rows[0]["gioitinh"] != DBNull.Value ? rows[0]["gioitinh"].ToString() : "";
            }

            // BƯỚC 2: Tạo chuỗi XML mới
            // Phải có cặp thẻ <NguoiDung> bọc ở ngoài cùng
            string xml = "<NguoiDung>" +
                            "<IDNguoiDung>" + id + "</IDNguoiDung>" +
                            "<Email>" + email + "</Email>" +
                            "<password>" + pass + "</password>" +
                            "<sdt>" + sdt + "</sdt>" +
                            "<DiaChi>" + diaChi + "</DiaChi>" +
                            "<AvatarURL>" + avatar + "</AvatarURL>" +
                            "<VaiTro>" + role + "</VaiTro>" +
                            "<gioitinh>" + gioiTinh + "</gioitinh>" +
                         "</NguoiDung>";

            // Gọi hàm sửa XML
            db.Sua(fileName, tableName, colID, id, xml);

            // Đồng bộ xuống SQL
            db.Sua_Database(tableName, fileName, colID, id);
        }

        public void XoaTK(string id)
        {
            db.Xoa(fileName, tableName, colID, id);
            db.Xoa_Database(tableName, colID, id);
        }

        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM NguoiDung", tableName, fileName);
        }
    }
}