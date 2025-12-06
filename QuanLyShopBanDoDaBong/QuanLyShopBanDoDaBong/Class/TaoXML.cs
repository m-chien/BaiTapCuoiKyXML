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
        private string strCon = "Data Source=localhost; Initial Catalog=FootballShop; Integrated Security=True";
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

                    string filePath = Path.Combine(Application.StartupPath, _FileXML);
                    dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo XML: " + ex.Message);
            }
        }

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
                    MessageBox.Show("File XML chưa tồn tại. Hãy bấm 'Tạo XML' hoặc tải dữ liệu trước.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc XML: " + ex.Message);
            }
            return dt;
        }

        public void Them(string _FileXML, string xmlContent)
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, _FileXML);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlDocumentFragment fragment = doc.CreateDocumentFragment();
                fragment.InnerXml = xmlContent;
                doc.DocumentElement.AppendChild(fragment);
                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm vào XML: " + ex.Message);
            }
        }
        public void Sua(string _FileXML, string tenBang, string cotID, string giaTriID, string noiDungMoi)
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, _FileXML);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                string xpath = $"//{tenBang}[{cotID}='{giaTriID}']";
                XmlNode oldNode = doc.SelectSingleNode(xpath);

                if (oldNode != null)
                {
                    XmlDocumentFragment newNode = doc.CreateDocumentFragment();
                    newNode.InnerXml = noiDungMoi;
                    oldNode.ParentNode.ReplaceChild(newNode, oldNode);
                    doc.Save(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa XML: " + ex.Message);
            }
        }
        public void Xoa(string _FileXML, string tenBang, string cotID, string giaTriID)
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, _FileXML);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
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
        public bool KiemTra(string _FileXML, string tenCot, string giaTri)
        {
            try
            {
                DataTable dt = loadDataGridView(_FileXML);
                DataView dv = new DataView(dt);
                dv.RowFilter = $"{tenCot} = '{giaTri}'";
                return dv.Count > 0;
            }
            catch
            {
                return false;
            }
        }
        public DataTable TimKiem(string _FileXML, string tenCot, string giaTri)
        {
            DataTable dt = loadDataGridView(_FileXML);
            DataView dv = new DataView(dt);
            dv.RowFilter = $"{tenCot} LIKE '%{giaTri}%'";
            return dv.ToTable();
        }

        private void ExecuteSQL(string sql)
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }

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

                        if (colName.Equals(cotID, StringComparison.OrdinalIgnoreCase)) continue;

                        setClause += $"{colName} = N'{SafeSqlLiteral(val)}',";
                    }
                    setClause = setClause.TrimEnd(',');

                    string sql = $"UPDATE {tenBang} SET {setClause} WHERE {cotID} = '{giaTriID}'";
                    ExecuteSQL(sql);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi đồng bộ Sửa SQL: " + ex.Message); }
        }

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