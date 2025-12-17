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

        public DataTable LayDanhSach()
        {
            return db.loadDataGridView(fileName);
        }

        public DataTable LayChiTietTheoHoaDon(string idHoaDon)
        {
            DataTable dt = db.loadDataGridView(fileName);

            if (dt.Rows.Count == 0)
                return dt;

            string colName = "";
            if (dt.Columns.Contains("IdHoaDon"))
                colName = "IdHoaDon";
            else if (dt.Columns.Contains("IDHoaDon"))
                colName = "IDHoaDon";
            else if (dt.Columns.Contains("idHoaDon"))
                colName = "idHoaDon";
            else
            {
                string allCols = string.Join(", ", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                throw new Exception($"Không tìm thấy cột hóa đơn! Các cột có sẵn: {allCols}");
            }

            DataView dv = new DataView(dt);
            dv.RowFilter = $"{colName} = '{idHoaDon}'";
            return dv.ToTable();
        }

        public void KhoiTaoXML()
        {
            db.taoXML("SELECT * FROM ChiTietHoaDon", tableName, fileName);
        }
    }
}