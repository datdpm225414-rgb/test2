using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
namespace CuahangNongduoc.DataLayer
{
    public class NhaCungCapFactory
    {
        DataService m_Ds = new DataService();

        public DataTable DanhsachNCC()
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHA_CUNG_CAP");
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI FROM NHA_CUNG_CAP WHERE Trang_Thai = 1");
            m_Ds.Load(cmd);

            return m_Ds;
        }
        public DataTable TimDiaChi(String diachi)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHA_CUNG_CAP WHERE DIA_CHI LIKE '%' + @diachi + '%' ");
            //cmd.Parameters.Add("diachi", OleDbType.VarChar).Value = diachi;
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI FROM NHA_CUNG_CAP WHERE DIA_CHI LIKE N'%' + @diachi + '%' ");
            cmd.Parameters.Add("diachi", SqlDbType.VarChar).Value = diachi;
            m_Ds.Load(cmd);

            return m_Ds;
        }
        public DataTable TimHoTen(String hoten)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHA_CUNG_CAP WHERE HO_TEN LIKE '%' + @hoten + '%' ");
            //cmd.Parameters.Add("hoten", OleDbType.VarChar).Value = hoten;
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI FROM NHA_CUNG_CAP WHERE HO_TEN LIKE N'%' + @hoten + '%' ");
            cmd.Parameters.Add("hoten", SqlDbType.VarChar).Value = hoten;
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public DataTable LayNCC(String id)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHA_CUNG_CAP WHERE ID = @id");
            //cmd.Parameters.Add("id", OleDbType.VarChar,50).Value = id;
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI FROM NHA_CUNG_CAP WHERE ID = @id");
            cmd.Parameters.Add("id", SqlDbType.VarChar, 50).Value = id;
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
