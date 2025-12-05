using System;
using System.Data;

namespace QuanLyShopBanDoDaBong.Class
{
    class BinhLuan
    {
        TaoXML db = new TaoXML();
        string fileName = "BinhLuan.xml";
        string tableName = "BinhLuan";
        string colID = "IDBinhLuan";

        // 1. Lấy danh sách Bình luận
        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        // Lấy danh sách Người dùng để đổ vào ComboBox
        public DataTable LayDanhSachNguoiDung()
        {
            return db.loadDataGridView("NguoiDung.xml");
        }

        // Lấy danh sách Sản phẩm để đổ vào ComboBox
        public DataTable LayDanhSachSanPham()
        {
            return db.loadDataGridView("SanPham.xml");
        }

        // 2. Thêm bình luận
        public void ThemBinhLuan(string idNguoiDung, string idSanPham, string noiDung, string ngayBinhLuan, string tinhTrang)
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

            string xml = "<BinhLuan>" +
                            "<IDBinhLuan>" + nextID + "</IDBinhLuan>" +
                            "<IDNguoiDung>" + idNguoiDung + "</IDNguoiDung>" +
                            "<IdSanPham>" + idSanPham + "</IdSanPham>" +
                            "<NoiDung>" + noiDung + "</NoiDung>" +
                            "<NgayBinhLuan>" + ngayBinhLuan + "</NgayBinhLuan>" +
                            "<TinhTrang>" + tinhTrang + "</TinhTrang>" +
                         "</BinhLuan>";

            db.Them(fileName, xml);
            db.Them_Database(tableName, fileName, "IDBinhLuan");
            KhoiTaoXML();
        }

        // 3. Sửa bình luận
        public void SuaBinhLuan(string id, string idNguoiDung, string idSanPham, string noiDung, string ngayBinhLuan, string tinhTrang)
        {
            string xml = "<BinhLuan>" +
                            "<IDBinhLuan>" + id + "</IDBinhLuan>" +
                            "<IDNguoiDung>" + idNguoiDung + "</IDNguoiDung>" +
                            "<IdSanPham>" + idSanPham + "</IdSanPham>" +
                            "<NoiDung>" + noiDung + "</NoiDung>" +
                            "<NgayBinhLuan>" + ngayBinhLuan + "</NgayBinhLuan>" +
                            "<TinhTrang>" + tinhTrang + "</TinhTrang>" +
                         "</BinhLuan>";

            db.Sua(fileName, tableName, colID, id, xml);
            db.Sua_Database(tableName, fileName, colID, id);
        }

        // 4. Xóa bình luận
        public void XoaBinhLuan(string id)
        {
            db.Xoa(fileName, tableName, colID, id);
            db.Xoa_Database(tableName, colID, id);
        }

        // 5. Tìm kiếm bình luận theo nội dung
        public DataTable TimKiem(string tuKhoa)
        {
            DataTable dt = db.loadDataGridView(fileName);
            DataView dv = new DataView(dt);
            dv.RowFilter = $"NoiDung LIKE '%{tuKhoa}%'";
            return dv.ToTable();
        }

        // 6. Lấy bình luận theo sản phẩm
        public DataTable LayBinhLuanTheoSanPham(string idSanPham)
        {
            DataTable dt = db.loadDataGridView(fileName);
            DataView dv = new DataView(dt);
            dv.RowFilter = $"IdSanPham = '{idSanPham}'";
            return dv.ToTable();
        }

        // 7. Khởi tạo XML từ Database
        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM BinhLuan", tableName, fileName);
        }
    }
}