using System;
using System.Data;

namespace QuanLyShopBanDoDaBong.Class
{
    class KhachHang
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

        public void ThemTK(string email, string sdt, string diaChi, string avatar, string gioitinh)
        {
            DataTable dt = LayDanhSach();
            int nextID = 1;

            if (dt.Rows.Count > 0)
            {
                try
                {
                    int max = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["IDNguoiDung"] != DBNull.Value && !string.IsNullOrEmpty(row["IDNguoiDung"].ToString()))
                        {
                            int currentID = int.Parse(row["IDNguoiDung"].ToString());
                            if (currentID > max) max = currentID;
                        }
                    }
                    nextID = max + 1;
                }
                catch
                {
                    nextID = new Random().Next(1000, 9999);
                }
            }

            string defaultPass = "123456";
            string defaultRole = "User";

            string xml = "<NguoiDung>" +
                            "<IDNguoiDung>" + nextID + "</IDNguoiDung>" +
                            "<Email>" + email + "</Email>" +
                            "<password>" + defaultPass + "</password>" +
                            "<sdt>" + sdt + "</sdt>" +
                            "<DiaChi>" + diaChi + "</DiaChi>" +
                            "<AvatarURL>" + avatar + "</AvatarURL>" +
                            "<VaiTro>" + defaultRole + "</VaiTro>" +
                            "<gioitinh>" + gioitinh + "</gioitinh>" +
                         "</NguoiDung>";

            db.Them(fileName, xml);
            db.Them_Database(tableName, fileName, "IDNguoiDung");
            KhoiTaoXML();
        }
        public void SuaTK(string id, string email, string sdt, string diaChi, string avatar, string gioitinh)
        {
            DataTable dt = LayDanhSach();
            DataRow[] rows = dt.Select($"{colID} = '{id}'");
            string oldPass = "";
            string oldRole = "User";

            if (rows.Length > 0)
            {
                oldPass = rows[0]["password"] != DBNull.Value ? rows[0]["password"].ToString() : "123456";
                oldRole = rows[0]["VaiTro"] != DBNull.Value ? rows[0]["VaiTro"].ToString() : "User";
            }

            string xml = "<NguoiDung>" +
                            "<IDNguoiDung>" + id + "</IDNguoiDung>" +
                            "<Email>" + email + "</Email>" +
                            "<password>" + oldPass + "</password>" +
                            "<sdt>" + sdt + "</sdt>" +
                            "<DiaChi>" + diaChi + "</DiaChi>" +
                            "<AvatarURL>" + avatar + "</AvatarURL>" +
                            "<VaiTro>" + oldRole + "</VaiTro>" +
                            "<gioitinh>" + gioitinh + "</gioitinh>" +
                         "</NguoiDung>";

            db.Sua(fileName, tableName, colID, id, xml);
            db.Sua_Database(tableName, fileName, colID, id);
        }

        public void XoaTK(string id)
        {
            db.Xoa(fileName, tableName, colID, id);
            db.Xoa_Database(tableName, colID, id);
        }

        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM NguoiDung where VaiTro = N'User'", tableName, fileName);
        }
    }
}