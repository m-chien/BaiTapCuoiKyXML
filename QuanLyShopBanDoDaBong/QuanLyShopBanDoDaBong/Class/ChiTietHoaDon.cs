using System;
using System.Data;
using System.Linq;

namespace QuanLyShopBanDoDaBong.Class
{
    class ChiTietHoaDon
    {
        TaoXML db = new TaoXML();
        string fileName = "ChiTietHoaDon.xml";
        string tableName = "ChiTietHoaDon";
        string colID = "IDChiTietHoaDon";

        // 1. Lấy danh sách Chi tiết hóa đơn
        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        // Lấy danh sách Hóa đơn để đổ vào ComboBox
        public DataTable LayDanhSachHoaDon()
        {
            return db.loadDataGridView("HoaDon.xml");
        }

        // Lấy danh sách Sản phẩm để đổ vào ComboBox
        public DataTable LayDanhSachSanPham()
        {
            return db.loadDataGridView("SanPham.xml");
        }

        // 2. Thêm chi tiết hóa đơn
        public void ThemChiTiet(string idHoaDon, string idSanPham, string soLuong, string donGia)
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

            string xml = "<ChiTietHoaDon>" +
                            "<IDChiTietHoaDon>" + nextID + "</IDChiTietHoaDon>" +
                            "<IdHoaDon>" + idHoaDon + "</IdHoaDon>" +
                            "<IdSanPham>" + idSanPham + "</IdSanPham>" +
                            "<SoLuong>" + soLuong + "</SoLuong>" +
                            "<DonGia>" + donGia + "</DonGia>" +
                         "</ChiTietHoaDon>";

            db.Them(fileName, xml);
            db.Them_Database(tableName, fileName, "IDChiTietHoaDon");
            KhoiTaoXML();
        }

        // 3. Sửa chi tiết hóa đơn
        public void SuaChiTiet(string id, string idHoaDon, string idSanPham, string soLuong, string donGia)
        {
            string xml = "<ChiTietHoaDon>" +
                            "<IDChiTietHoaDon>" + id + "</IDChiTietHoaDon>" +
                            "<IdHoaDon>" + idHoaDon + "</IdHoaDon>" +
                            "<IdSanPham>" + idSanPham + "</IdSanPham>" +
                            "<SoLuong>" + soLuong + "</SoLuong>" +
                            "<DonGia>" + donGia + "</DonGia>" +
                         "</ChiTietHoaDon>";

            db.Sua(fileName, tableName, colID, id, xml);
            db.Sua_Database(tableName, fileName, colID, id);
        }

        // 4. Xóa chi tiết hóa đơn
        public void XoaChiTiet(string id)
        {
            db.Xoa(fileName, tableName, colID, id);
            db.Xoa_Database(tableName, colID, id);
        }

        // 5. Lấy chi tiết theo hóa đơn - ĐÃ SỬA TÊN CỘT
        // 5. Lấy chi tiết theo hóa đơn
        public DataTable LayChiTietTheoHoaDon(string idHoaDon)
        {
            DataTable dt = db.loadDataGridView(fileName);

            if (dt.Rows.Count == 0)
                return dt; // Trả về table rỗng nếu không có dữ liệu

            // Tìm tên cột chính xác
            string colName = "";
            if (dt.Columns.Contains("IdHoaDon"))
                colName = "IdHoaDon";
            else if (dt.Columns.Contains("IDHoaDon"))
                colName = "IDHoaDon";
            else if (dt.Columns.Contains("idHoaDon"))
                colName = "idHoaDon";
            else
            {
                // Nếu không tìm thấy, log ra tất cả tên cột
                string allCols = string.Join(", ", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                throw new Exception($"Không tìm thấy cột hóa đơn! Các cột có sẵn: {allCols}");
            }

            DataView dv = new DataView(dt);
            dv.RowFilter = $"{colName} = '{idHoaDon}'";
            return dv.ToTable();
        }

        // 6. Khởi tạo XML từ Database
        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM ChiTietHoaDon", tableName, fileName);
        }
    }
}