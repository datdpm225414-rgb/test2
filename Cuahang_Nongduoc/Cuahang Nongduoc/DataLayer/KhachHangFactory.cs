using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class KhachHangFactory
    {
        DataService m_Ds = new DataService();

        public DataTable DanhsachKhachHang(bool loai)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM KHACH_HANG WHERE LOAI_KH = " + loai);
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH FROM KHACH_HANG KH WHERE LOAI_KH = @loai AND Trang_Thai = 1 ");
            cmd.Parameters.Add("loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);

            return m_Ds;
        }
        public DataTable TimHoTen(String hoten, bool loai)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM KHACH_HANG WHERE HO_TEN LIKE '%' + @hoten + '%' AND LOAI_KH = " + loai);
            //cmd.Parameters.Add("hoten", OleDbType.VarChar).Value = hoten;

            // ---- ĐÃ SỬA ---- (Bỏ chữ SELECT thừa)
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH FROM KHACH_HANG KH WHERE HO_TEN LIKE N'%' + @hoten + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("hoten", SqlDbType.VarChar).Value = hoten;
            cmd.Parameters.Add("loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public DataTable TimDiaChi(String diachi, bool loai)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM KHACH_HANG WHERE DIA_CHI LIKE '%' + @diachi + '%' AND LOAI_KH = " + loai);
            //cmd.Parameters.Add("diachi", OleDbType.VarChar).Value = diachi;

            // ---- ĐÃ SỬA ---- (Bỏ chữ SELECT thừa)
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH FROM KHACH_HANG WHERE DIA_CHI LIKE N'%' + @diachi + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("diachi", SqlDbType.VarChar).Value = diachi;
            cmd.Parameters.Add("loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public DataTable DanhsachKhachHang()
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM KHACH_HANG");
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG");
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public DataTable LayKhachHang(String id)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM KHACH_HANG WHERE ID = @id");
            //cmd.Parameters.Add("id", OleDbType.VarChar,50).Value = id;
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH FROM KHACH_HANG WHERE ID = @id");
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