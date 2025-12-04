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

        // 1. Lấy danh sách Sản phẩm
        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        // --- MỚI: Hàm lấy danh sách Danh Mục để đổ vào ComboBox ---
        public DataTable LayDanhSachDanhMuc()
        {
            // Load từ file DanhMuc.xml (File này do Form Danh Mục tạo ra)
            return db.loadDataGridView("DanhMuc.xml");
        }

        // 2. Thêm sản phẩm (Cập nhật: Nhận thêm idDanhMuc)
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

            // Lưu ý: Đưa idDanhMuc vào XML
            string xml = "<SanPham>" +
                            "<IDSanPham>" + nextID + "</IDSanPham>" +
                            "<IDdanhMuc>" + idDanhMuc + "</IDdanhMuc>" + // <--- Dữ liệu từ ComboBox
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

        // 3. Sửa sản phẩm (Cập nhật: Nhận thêm idDanhMuc)
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
                            "<IDdanhMuc>" + idDanhMuc + "</IDdanhMuc>" + // <--- Cập nhật ID Danh mục mới
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