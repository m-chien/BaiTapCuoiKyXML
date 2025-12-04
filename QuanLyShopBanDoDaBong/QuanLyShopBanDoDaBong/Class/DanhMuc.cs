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

        // 1. Lấy danh sách
        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        // 2. Thêm danh mục (ID tự tăng + Đồng bộ SQL)
        public void ThemDM(string ten, string moTa, string ngayTao)
        {
            // BƯỚC 1: Tính ID tạm cho XML
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

            // BƯỚC 2: Tạo XML (Lưu ý tên thẻ phải khớp tên cột SQL: TenDanhmuc)
            string xml = "<DanhMuc>" +
                            "<IDDanhMuc>" + nextID + "</IDDanhMuc>" +
                            "<TenDanhmuc>" + ten + "</TenDanhmuc>" +
                            "<MoTa>" + moTa + "</MoTa>" +
                            "<NgayTao>" + ngayTao + "</NgayTao>" +
                         "</DanhMuc>";

            db.Them(fileName, xml);

            // BƯỚC 3: Đồng bộ SQL
            db.Them_Database(tableName, fileName, "IDDanhMuc");

            // BƯỚC 4: Tải lại để lấy ID chuẩn từ SQL
            KhoiTaoXML();
        }

        // 3. Sửa danh mục
        public void SuaDM(string id, string ten, string moTa, string ngayTao)
        {
            // Tạo chuỗi XML update
            string xml = "<DanhMuc>" +
                            "<IDDanhMuc>" + id + "</IDDanhMuc>" +
                            "<TenDanhmuc>" + ten + "</TenDanhmuc>" +
                            "<MoTa>" + moTa + "</MoTa>" +
                            "<NgayTao>" + ngayTao + "</NgayTao>" +
                         "</DanhMuc>";

            db.Sua(fileName, tableName, colID, id, xml);
            db.Sua_Database(tableName, fileName, colID, id);
        }

        // 4. Xóa danh mục
        public void XoaDM(string id)
        {
            db.Xoa(fileName, tableName, colID, id);
            db.Xoa_Database(tableName, colID, id);
        }

        // 5. Tìm kiếm
        public DataTable TimKiem(string tuKhoa)
        {
            DataTable dt = LayDanhSach();
            DataView dv = new DataView(dt);
            // Tìm theo Tên hoặc Mô tả
            dv.RowFilter = $"TenDanhmuc LIKE '%{tuKhoa}%' OR MoTa LIKE '%{tuKhoa}%'";
            return dv.ToTable();
        }

        // 6. Reset XML từ SQL
        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM DanhMuc", tableName, fileName);
        }
    }
}