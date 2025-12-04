using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace QuanLyShopBanDoDaBong
{
    class TaoXML
    {
        // Chuỗi kết nối SQL (Dùng chung cho toàn class)
        private string strCon = "Data Source=localhost; Initial Catalog=FootballShop; Integrated Security=True";

        // =================================================================================
        // PHẦN 1: TƯƠNG TÁC GIỮA SQL VÀ XML (TẠO MỚI, ĐỌC FILE)
        // =================================================================================

        // 1. Hàm lấy dữ liệu từ SQL và tạo ra file XML
        public void taoXML(string sql, string bang, string _FileXML)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    SqlDataAdapter ad = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable(bang);
                    ad.Fill(dt);

                    // Luôn tạo file XML dù có dữ liệu hay không (để tránh lỗi file not found)
                    string filePath = Path.Combine(Application.StartupPath, _FileXML);
                    dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo XML: " + ex.Message);
            }
        }

        // 2. Hàm đọc file XML đổ vào DataTable (để hiện lên DataGridView)
        public DataTable loadDataGridView(string _FileXML)
        {
            DataTable dt = new DataTable();
            try
            {
                string filePath = Path.Combine(Application.StartupPath, _FileXML);
                if (File.Exists(filePath))
                {
                    dt.ReadXml(filePath);
                }
                else
                {
                    // Nếu chưa có file thì tạo file rỗng có cấu trúc
                    MessageBox.Show("File XML chưa tồn tại. Hãy bấm 'Tạo XML' hoặc tải dữ liệu trước.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc XML: " + ex.Message);
            }
            return dt;
        }

        // =================================================================================
        // PHẦN 2: THAO TÁC TRỰC TIẾP TRÊN FILE XML (THÊM, SỬA, XÓA)
        // =================================================================================

        // 3. Hàm THÊM một nút mới vào XML
        // xmlContent: Chuỗi XML đầy đủ (VD: <NguoiDung><ID>1</ID>...</NguoiDung>)
        public void Them(string _FileXML, string xmlContent)
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, _FileXML);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                // Tạo một đoạn XML từ chuỗi string
                XmlDocumentFragment fragment = doc.CreateDocumentFragment();
                fragment.InnerXml = xmlContent;

                // Thêm vào nút gốc (Root)
                doc.DocumentElement.AppendChild(fragment);
                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm vào XML: " + ex.Message);
            }
        }

        // 4. Hàm SỬA một nút trong XML (Dùng XPath để tìm)
        public void Sua(string _FileXML, string tenBang, string cotID, string giaTriID, string noiDungMoi)
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, _FileXML);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                // Tạo XPath để tìm nút cần sửa
                string xpath = $"//{tenBang}[{cotID}='{giaTriID}']";
                XmlNode oldNode = doc.SelectSingleNode(xpath);

                if (oldNode != null)
                {
                    // Tạo nút mới từ nội dung sửa
                    XmlDocumentFragment newNode = doc.CreateDocumentFragment();
                    newNode.InnerXml = noiDungMoi;

                    // Thay thế nút cũ bằng nút mới
                    oldNode.ParentNode.ReplaceChild(newNode, oldNode);
                    doc.Save(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa XML: " + ex.Message);
            }
        }

        // 5. Hàm XÓA một nút trong XML
        public void Xoa(string _FileXML, string tenBang, string cotID, string giaTriID)
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, _FileXML);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                // Tìm nút cần xóa
                string xpath = $"//{tenBang}[{cotID}='{giaTriID}']";
                XmlNode nodeDel = doc.SelectSingleNode(xpath);

                if (nodeDel != null)
                {
                    doc.DocumentElement.RemoveChild(nodeDel);
                    doc.Save(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa XML: " + ex.Message);
            }
        }

        // 6. Hàm KIỂM TRA sự tồn tại (Trả về True nếu tìm thấy)
        public bool KiemTra(string _FileXML, string tenCot, string giaTri)
        {
            try
            {
                DataTable dt = loadDataGridView(_FileXML);
                // Dùng RowFilter để lọc
                DataView dv = new DataView(dt);
                dv.RowFilter = $"{tenCot} = '{giaTri}'";
                return dv.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        // 7. Hàm TÌM KIẾM trong XML trả về DataTable
        public DataTable TimKiem(string _FileXML, string tenCot, string giaTri)
        {
            DataTable dt = loadDataGridView(_FileXML);
            DataView dv = new DataView(dt);
            // Tìm kiếm tương đối (LIKE)
            dv.RowFilter = $"{tenCot} LIKE '%{giaTri}%'";
            return dv.ToTable();
        }

        // =================================================================================
        // PHẦN 3: ĐỒNG BỘ DỮ LIỆU TỪ XML VỀ SQL SERVER
        // =================================================================================

        // Thực thi câu lệnh SQL không trả về dữ liệu (Insert, Update, Delete)
        private void ExecuteSQL(string sql)
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }

        // --- CẬP NHẬT TRONG CLASS TaoXML ---

        // Hàm xử lý dấu nháy đơn để tránh lỗi SQL
        private string SafeSqlLiteral(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input.Replace("'", "''");
        }

        public void Them_Database(string tenBang, string _FileXML, string cotKhoaChinh)
        {
            try
            {
                DataTable dt = loadDataGridView(_FileXML);
                if (dt.Rows.Count > 0)
                {
                    int lastRowIdx = dt.Rows.Count - 1;

                    System.Collections.Generic.List<string> columns = new System.Collections.Generic.List<string>();
                    System.Collections.Generic.List<string> values = new System.Collections.Generic.List<string>();

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string colName = dt.Columns[i].ColumnName;
                        string val = dt.Rows[lastRowIdx][i].ToString().Trim();

                        if (colName.Equals(cotKhoaChinh, StringComparison.OrdinalIgnoreCase)) continue;

                        columns.Add(colName);
                        values.Add($"N'{SafeSqlLiteral(val)}'");
                    }

                    string sql = $"INSERT INTO {tenBang} ({string.Join(",", columns)}) VALUES ({string.Join(",", values)})";
                    ExecuteSQL(sql);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi đồng bộ Thêm SQL: " + ex.Message); }
        }

        public void Sua_Database(string tenBang, string _FileXML, string cotID, string giaTriID)
        {
            try
            {
                DataTable dt = loadDataGridView(_FileXML);
                DataRow[] rows = dt.Select($"{cotID} = '{giaTriID}'");

                if (rows.Length > 0)
                {
                    DataRow row = rows[0];
                    string setClause = "";

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string colName = dt.Columns[i].ColumnName;
                        string val = row[i].ToString().Trim();

                        // Bỏ qua cột khóa chính
                        if (colName.Equals(cotID, StringComparison.OrdinalIgnoreCase)) continue;

                        setClause += $"{colName} = N'{SafeSqlLiteral(val)}',";
                    }

                    // Xóa dấu phẩy cuối
                    setClause = setClause.TrimEnd(',');

                    string sql = $"UPDATE {tenBang} SET {setClause} WHERE {cotID} = '{giaTriID}'";
                    ExecuteSQL(sql);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi đồng bộ Sửa SQL: " + ex.Message); }
        }

        // Xóa bản ghi trong Database dựa trên điều kiện từ XML
        public void Xoa_Database(string tenBang, string cotID, string giaTriID)
        {
            try
            {
                string sql = $"DELETE FROM {tenBang} WHERE {cotID} = '{giaTriID}'";
                ExecuteSQL(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đồng bộ xóa SQL: " + ex.Message);
            }
        }
    }
}