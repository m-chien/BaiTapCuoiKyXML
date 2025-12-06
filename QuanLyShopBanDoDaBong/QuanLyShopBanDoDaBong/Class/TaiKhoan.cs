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
            DataTable dt = LayDanhSach();
            int nextID = 1; 

            if (dt.Rows.Count > 0)
            {
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
                    nextID = new Random().Next(1000, 9999);
                }
            }

            string xml = "<NguoiDung>" +
                            "<IDNguoiDung>" + nextID + "</IDNguoiDung>" +
                            "<Email>" + email + "</Email>" +
                            "<password>" + pass + "</password>" +
                            "<sdt></sdt>" +
                            "<DiaChi></DiaChi>" +
                            "<AvatarURL></AvatarURL>" +
                            "<VaiTro>" + role + "</VaiTro>" +
                            "<gioitinh>Nam</gioitinh>" +
                         "</NguoiDung>";

            db.Them(fileName, xml);

            db.Them_Database(tableName, fileName, "IDNguoiDung");

            KhoiTaoXML();
        }

        public void SuaTK(string id, string email, string pass, string role)
        {
            DataTable dt = LayDanhSach();
            DataRow[] rows = dt.Select($"{colID} = '{id}'");

            string sdt = "", diaChi = "", avatar = "", gioiTinh = "";

            if (rows.Length > 0)
            {
                sdt = rows[0]["sdt"] != DBNull.Value ? rows[0]["sdt"].ToString() : "";
                diaChi = rows[0]["DiaChi"] != DBNull.Value ? rows[0]["DiaChi"].ToString() : "";
                avatar = rows[0]["AvatarURL"] != DBNull.Value ? rows[0]["AvatarURL"].ToString() : "";
                gioiTinh = rows[0]["gioitinh"] != DBNull.Value ? rows[0]["gioitinh"].ToString() : "";
            }

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
            db.taoXML("SELECT * FROM NguoiDung", tableName, fileName);
        }
    }
}