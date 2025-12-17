using System;
using System.Data;

namespace QuanLyShopBanDoDaBong.Class
{
    class HoaDon
    {
        TaoXML db = new TaoXML();
        string fileName = "HoaDon.xml";
        string tableName = "HoaDon";
        string colID = "IDHoaDon";

        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        public DataTable LayDanhSachNguoiDung()
        {
            return db.loadDataGridView("NguoiDung.xml");
        }

        public void SuaHoaDon(string id, string idUser, string tongTien, string diaChiGiaoHang, string ngayDat, string trangThai)
        {
            string xml = "<HoaDon>" +
                            "<IDHoaDon>" + id + "</IDHoaDon>" +
                            "<IdUser>" + idUser + "</IdUser>" +
                            "<TongTien>" + tongTien + "</TongTien>" +
                            "<DiaChiGiaoHang>" + diaChiGiaoHang + "</DiaChiGiaoHang>" +
                            "<NgayDat>" + ngayDat + "</NgayDat>" +
                            "<TrangThai>" + trangThai + "</TrangThai>" +
                         "</HoaDon>";

            db.Sua(fileName, tableName, colID, id, xml);
            db.Sua_Database(tableName, fileName, colID, id);
        }

        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM HoaDon", tableName, fileName);
        }
        public DataTable TimKiem(string trangThai,
                                 DateTime? ngayDat,
                                 decimal? tongTien)
        {
            return db.TimKiemNhieuDieuKien(
                fileName,
                "TrangThai", trangThai,
                "NgayDat", ngayDat,
                "TongTien", tongTien
            );
        }
    }
}