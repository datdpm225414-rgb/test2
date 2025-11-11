using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient; // Dùng SQL Server
using System.Data.OleDb;   // Giữ lại để tương thích (nếu cần)

namespace CuahangNongduoc.DataLayer
{
    public class DichVuFactory
    {
        DataService m_Ds = new DataService();

        public DataTable LayDsDichVu()
        {

            SqlCommand cmd = new SqlCommand("SELECT * FROM DICH_VU WHERE Trang_Thai = 1");

            m_Ds.Load(cmd);
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

        public bool Save()
        {

            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}