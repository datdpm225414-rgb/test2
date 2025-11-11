using System;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class NguoiDungFactory
    {
        private DataService m_Ds = new DataService();
        private SqlDataAdapter adapter;
        private SqlCommandBuilder builder;

        public DataTable DanhSachNguoiDung()
        {
            DataService.OpenConnection();

            string sql = "SELECT * FROM NGUOI_DUNG WHERE TRANG_THAI = 1";
            adapter = new SqlDataAdapter(sql, DataService.m_ConnectString);
            builder = new SqlCommandBuilder(adapter);

            m_Ds.Clear();
            adapter.Fill(m_Ds);
            return m_Ds;
        }

        public DataRow NewRow()
        {
            return m_Ds.NewRow();
        }

        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }

        public DataTable LayNguoiDungTheoTenDangNhap(string tenDangNhap)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG WHERE TEN_DANG_NHAP = @tenDangNhap");
            cmd.Parameters.Add("tenDangNhap", SqlDbType.VarChar).Value = tenDangNhap;
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public DataTable TimMaNguoiDung(String id)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM SAN_PHAM WHERE ID LIKE '%' + @id + '%'");
            //cmd.Parameters.Add("id", OleDbType.VarChar).Value = id;
            SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG WHERE ID LIKE '%' + @id + '%'");
            cmd.Parameters.Add("id", SqlDbType.VarChar).Value = id;
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public DataTable TimTenNguoiDung(String ten)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM SAN_PHAM WHERE ID LIKE '%' + @id + '%'");
            //cmd.Parameters.Add("id", OleDbType.VarChar).Value = id;
            SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG WHERE TEN_NGUOI_DUNG LIKE '%' + @ten + '%'");
            cmd.Parameters.Add("ten", SqlDbType.VarChar).Value = ten;
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public bool Save()
        {
            if (adapter == null) return false;
            try
            {
                adapter.Update(m_Ds);
                m_Ds.AcceptChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
