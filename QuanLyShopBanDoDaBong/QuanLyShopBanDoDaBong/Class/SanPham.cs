using System;
using System.Data;

namespace QuanLyShopBanDoDaBong.Class
{
    class SanPham
    {
        TaoXML db = new TaoXML();
        string fileName = "SanPham.xml";
        string tableName = "SanPham";
        string colID = "IDSanPham";

        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        public DataTable LayDanhSachDanhMuc()
        {
            return db.loadDataGridView("DanhMuc.xml");
        }

        public void ThemSP(string ten, string idDanhMuc, string hang, string dvt, string soLuong)
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
                        if (row[colID] != DBNull.Value && int.TryParse(row[colID].ToString(), out int currentID))
                        {
                            if (currentID > max) max = currentID;
                        }
                    }
                    nextID = max + 1;
                }
                catch { nextID = new Random().Next(1000, 9999); }
            }

            string xml = "<SanPham>" +
                            "<IDSanPham>" + nextID + "</IDSanPham>" +
                            "<IDdanhMuc>" + idDanhMuc + "</IDdanhMuc>" +
                            "<Hang>" + hang + "</Hang>" +
                            "<KichThuoc></KichThuoc>" +
                            "<mausac></mausac>" +
                            "<mota>" + ten + "</mota>" +
                            "<hinhanh></hinhanh>" +
                            "<SoLuongTonKho>" + soLuong + "</SoLuongTonKho>" +
                            "<DonViTinh>" + dvt + "</DonViTinh>" +
                         "</SanPham>";

            db.Them(fileName, xml);
            db.Them_Database(tableName, fileName, "IDSanPham");
            KhoiTaoXML();
        }

        public void SuaSP(string id, string ten, string idDanhMuc, string hang, string dvt, string soLuong)
        {
            DataTable dt = LayDanhSach();
            DataRow[] rows = dt.Select($"{colID} = '{id}'");

            string kichThuoc = "", mauSac = "", hinhAnh = "";

            if (rows.Length > 0)
            {
                kichThuoc = rows[0]["KichThuoc"] != DBNull.Value ? rows[0]["KichThuoc"].ToString() : "";
                mauSac = rows[0]["mausac"] != DBNull.Value ? rows[0]["mausac"].ToString() : "";
                hinhAnh = rows[0]["hinhanh"] != DBNull.Value ? rows[0]["hinhanh"].ToString() : "";
            }

            string xml = "<SanPham>" +
                            "<IDSanPham>" + id + "</IDSanPham>" +
                            "<IDdanhMuc>" + idDanhMuc + "</IDdanhMuc>" + 
                            "<Hang>" + hang + "</Hang>" +
                            "<KichThuoc>" + kichThuoc + "</KichThuoc>" +
                            "<mausac>" + mauSac + "</mausac>" +
                            "<mota>" + ten + "</mota>" +
                            "<hinhanh>" + hinhAnh + "</hinhanh>" +
                            "<SoLuongTonKho>" + soLuong + "</SoLuongTonKho>" +
                            "<DonViTinh>" + dvt + "</DonViTinh>" +
                         "</SanPham>";

            db.Sua(fileName, tableName, colID, id, xml);
            db.Sua_Database(tableName, fileName, colID, id);
        }

        public void XoaSP(string id)
        {
            db.Xoa(fileName, tableName, colID, id);
            db.Xoa_Database(tableName, colID, id);
        }

        public DataTable TimKiem(string tuKhoa)
        {
            DataTable dt = db.loadDataGridView(fileName);
            DataView dv = new DataView(dt);
            dv.RowFilter = $"mota LIKE '%{tuKhoa}%' OR Hang LIKE '%{tuKhoa}%'";
            return dv.ToTable();
        }

        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM SanPham", tableName, fileName);
        }
    }
}