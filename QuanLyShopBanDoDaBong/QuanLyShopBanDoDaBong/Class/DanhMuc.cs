using System;
using System.Data;

namespace QuanLyShopBanDoDaBong.Class
{
    class DanhMuc
    {
        TaoXML db = new TaoXML();
        string fileName = "DanhMuc.xml";
        string tableName = "DanhMuc";
        string colID = "IDDanhMuc";

        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        public void ThemDM(string ten, string moTa, string ngayTao)
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
                        if (row[colID] != DBNull.Value)
                        {
                            int currentID = int.Parse(row[colID].ToString());
                            if (currentID > max) max = currentID;
                        }
                    }
                    nextID = max + 1;
                }
                catch { nextID = new Random().Next(100, 999); }
            }

            string xml = "<DanhMuc>" +
                            "<IDDanhMuc>" + nextID + "</IDDanhMuc>" +
                            "<TenDanhmuc>" + ten + "</TenDanhmuc>" +
                            "<MoTa>" + moTa + "</MoTa>" +
                            "<NgayTao>" + ngayTao + "</NgayTao>" +
                         "</DanhMuc>";

            db.Them(fileName, xml);

            db.Them_Database(tableName, fileName, "IDDanhMuc");

            KhoiTaoXML();
        }

        public void SuaDM(string id, string ten, string moTa, string ngayTao)
        {
            string xml = "<DanhMuc>" +
                            "<IDDanhMuc>" + id + "</IDDanhMuc>" +
                            "<TenDanhmuc>" + ten + "</TenDanhmuc>" +
                            "<MoTa>" + moTa + "</MoTa>" +
                            "<NgayTao>" + ngayTao + "</NgayTao>" +
                         "</DanhMuc>";

            db.Sua(fileName, tableName, colID, id, xml);
            db.Sua_Database(tableName, fileName, colID, id);
        }

        public void XoaDM(string id)
        {
            db.Xoa(fileName, tableName, colID, id);
            db.Xoa_Database(tableName, colID, id);
        }

        public DataTable TimKiem(string tuKhoa)
        {
            DataTable dt = LayDanhSach();
            DataView dv = new DataView(dt);
            dv.RowFilter = $"TenDanhmuc LIKE '%{tuKhoa}%' OR MoTa LIKE '%{tuKhoa}%'";
            return dv.ToTable();
        }

        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM DanhMuc", tableName, fileName);
        }
    }
}