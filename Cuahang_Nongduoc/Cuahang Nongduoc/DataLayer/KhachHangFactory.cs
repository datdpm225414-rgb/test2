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
            // Đã bỏ 'Trang_Thai'
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH FROM KHACH_HANG KH " +
                                          "WHERE LOAI_KH = @loai " +
                                          "ORDER BY CAST(ID AS BIGINT) ASC");
            cmd.Parameters.Add("loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);
            return m_Ds;
        }
        public DataTable TimHoTen(String hoten, bool loai)
        {
            // Đã bỏ 'Trang_Thai'
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH FROM KHACH_HANG KH " +
                                          "WHERE HO_TEN LIKE N'%' + @hoten + '%' AND KH.LOAI_KH = @loai");
            cmd.Parameters.Add("hoten", SqlDbType.VarChar).Value = hoten;
            cmd.Parameters.Add("loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable TimDiaChi(String diachi, bool loai)
        {
            // Đã bỏ 'Trang_Thai'
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH FROM KHACH_HANG " +
                                          "WHERE DIA_CHI LIKE N'%' + @diachi + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("diachi", SqlDbType.VarChar).Value = diachi;
            cmd.Parameters.Add("loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable DanhsachKhachHang()
        {
            // Đã bỏ 'Trang_Thai'
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH FROM KHACH_HANG " +
                                          "ORDER BY CAST(ID AS BIGINT) ASC");
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable LayKhachHang(String id)
        {
            // Đã bỏ 'Trang_Thai'
            SqlCommand cmd = new SqlCommand("SELECT ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH FROM KHACH_HANG " +
                                          "WHERE ID = @id");
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

        public long GetMaxKhachHangID()
        {
            long maxID = 0;
            DataService tempDs = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT MAX(CAST(ID AS BIGINT)) FROM KHACH_HANG");
            tempDs.Load(cmd);

            if (tempDs.Rows.Count > 0 && tempDs.Rows[0][0] != DBNull.Value)
            {
                maxID = Convert.ToInt64(tempDs.Rows[0][0]);
            }
            return maxID;
        }
    }
}