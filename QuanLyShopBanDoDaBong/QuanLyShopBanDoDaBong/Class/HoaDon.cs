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

        // 1. Lấy danh sách Hóa đơn
        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        // Lấy danh sách Người dùng để đổ vào ComboBox
        public DataTable LayDanhSachNguoiDung()
        {
            return db.loadDataGridView("NguoiDung.xml");
        }

        // 2. Thêm hóa đơn
        public void ThemHoaDon(string idUser, string tongTien, string diaChiGiaoHang, string ngayDat, string trangThai)
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

            string xml = "<HoaDon>" +
                            "<IDHoaDon>" + nextID + "</IDHoaDon>" +
                            "<IdUser>" + idUser + "</IdUser>" +
                            "<TongTien>" + tongTien + "</TongTien>" +
                            "<DiaChiGiaoHang>" + diaChiGiaoHang + "</DiaChiGiaoHang>" +
                            "<NgayDat>" + ngayDat + "</NgayDat>" +
                            "<TrangThai>" + trangThai + "</TrangThai>" +
                         "</HoaDon>";

            db.Them(fileName, xml);
            db.Them_Database(tableName, fileName, "IDHoaDon");
            KhoiTaoXML();
        }

        // 3. Sửa hóa đơn
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

        // 4. Xóa hóa đơn
        public void XoaHoaDon(string id)
        {
            db.Xoa(fileName, tableName, colID, id);
            db.Xoa_Database(tableName, colID, id);
        }

        // 5. Tìm kiếm hóa đơn theo địa chỉ hoặc trạng thái
        public DataTable TimKiem(string tuKhoa)
        {
            DataTable dt = db.loadDataGridView(fileName);
            DataView dv = new DataView(dt);
            dv.RowFilter = $"DiaChiGiaoHang LIKE '%{tuKhoa}%' OR TrangThai LIKE '%{tuKhoa}%'";
            return dv.ToTable();
        }

        // 6. Lấy hóa đơn theo User
        public DataTable LayHoaDonTheoUser(string idUser)
        {
            DataTable dt = db.loadDataGridView(fileName);
            DataView dv = new DataView(dt);
            dv.RowFilter = $"IdUser = '{idUser}'";
            return dv.ToTable();
        }

        // 7. Lấy hóa đơn theo trạng thái
        public DataTable LayHoaDonTheoTrangThai(string trangThai)
        {
            DataTable dt = db.loadDataGridView(fileName);
            DataView dv = new DataView(dt);
            dv.RowFilter = $"TrangThai = '{trangThai}'";
            return dv.ToTable();
        }

        // 8. Khởi tạo XML từ Database
        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM HoaDon", tableName, fileName);
        }
    }
}