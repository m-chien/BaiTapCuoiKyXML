using System;
using System.Data;

namespace QuanLyShopBanDoDaBong.Class
{
    class BinhLuan
    {
        TaoXML db = new TaoXML();
        string fileName = "BinhLuan.xml";
        string tableName = "BinhLuan";

        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM BinhLuan", tableName, fileName);
        }
        public DataTable TimKiem(string tinhTrang,
                                 DateTime? ngayBinhLuan)
        {
            return db.TimKiemNhieuDieuKien(
                fileName,
                "TinhTrang", tinhTrang,
                "NgayBinhLuan", ngayBinhLuan
            );
        }
    }
}