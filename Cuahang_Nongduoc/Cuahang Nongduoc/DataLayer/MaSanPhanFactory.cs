using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace CuahangNongduoc.DataLayer
{
    public class MaSanPhanFactory
    {
        DataService m_Ds = new DataService();

        public void LoadSchema()
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM MA_SAN_PHAM WHERE ID = '-1'");
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE ID = '-1'");
            m_Ds.Load(cmd);
        }

        public DataTable DanhsachMaSanPham(String sp)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM MA_SAN_PHAM WHERE ID_SAN_PHAM=@id AND SO_LUONG > 0");
            //cmd.Parameters.Add("id", OleDbType.VarChar, 50).Value = sp;
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE ID_SAN_PHAM=@id AND SO_LUONG > 0");
            cmd.Parameters.Add("id", SqlDbType.VarChar, 50).Value = sp;
            m_Ds.Load(cmd);

            return m_Ds;
        }
        public DataTable DanhsachChiTiet(String sp)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM MA_SAN_PHAM WHERE ID_PHIEU_NHAP=@id");
            //cmd.Parameters.Add("id", OleDbType.VarChar, 50).Value = sp;
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE ID_PHIEU_NHAP=@id");
            cmd.Parameters.Add("id", SqlDbType.VarChar, 50).Value = sp;
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public DataTable LaySanPham(String idMaSanPham)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT SP.* FROM SAN_PHAM SP INNER JOIN MA_SAN_PHAM MSP ON SP.ID = MSP.ID_SAN_PHAM WHERE MSP.ID = @id");
            //cmd.Parameters.Add("id", OleDbType.VarChar, 50).Value = idMaSanPham;
            SqlCommand cmd = new SqlCommand("SELECT SP.* FROM SAN_PHAM SP INNER JOIN MA_SAN_PHAM MSP ON SP.ID = MSP.ID_SAN_PHAM WHERE MSP.ID = @id");
            cmd.Parameters.Add("id", SqlDbType.VarChar, 50).Value = idMaSanPham;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable LayMaSanPham(String idMaSanPham)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM MA_SAN_PHAM MSP WHERE MSP.ID = @id");
            //cmd.Parameters.Add("id", OleDbType.VarChar, 50).Value = idMaSanPham;
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM MSP WHERE MSP.ID = @id");
            cmd.Parameters.Add("id", SqlDbType.VarChar, 50).Value = idMaSanPham;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable DanhsachMaSanPhamHetHan(DateTime dt)
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM MA_SAN_PHAM WHERE SO_LUONG > 0 AND NGAY_HET_HAN <= @ngay");
            //cmd.Parameters.Add("ngay", OleDbType.Date).Value = dt;
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE SO_LUONG > 0 AND NGAY_HET_HAN <= @ngay");
            cmd.Parameters.Add("ngay", SqlDbType.Date).Value = dt;
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public DataTable DanhsachMaSanPham()
        {
            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM MA_SAN_PHAM WHERE SO_LUONG > 0");
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE SO_LUONG > 0");
            m_Ds.Load(cmd);

            return m_Ds;
        }

        public static void CapNhatSoLuong(String masp, int so_luong)
        {
            DataService ds = new DataService();
            //OleDbCommand cmd = new OleDbCommand("UPDATE MA_SAN_PHAM SET SO_LUONG = SO_LUONG + @so WHERE ID = @id");
            //cmd.Parameters.Add("so", OleDbType.Integer).Value = so_luong;
            //cmd.Parameters.Add("id", OleDbType.VarChar).Value = masp;
            SqlCommand cmd = new SqlCommand("UPDATE MA_SAN_PHAM SET SO_LUONG = SO_LUONG + @so WHERE ID = @id");
            cmd.Parameters.Add("so", SqlDbType.Int).Value = so_luong;
            cmd.Parameters.Add("id", SqlDbType.VarChar).Value = masp;
            ds.ExecuteNoneQuery(cmd);
        }

        public DataRow NewRow()
        {
            return m_Ds.NewRow();
        }
        public void Add(DataRow row)
        {
            try
            {
                m_Ds.Rows.Add(row);
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi thêm mã sản phẩm!", "Mã sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool Save()
        {
            return m_Ds.ExecuteNoneQuery() > 0;
        }

        #region code yeu cau
        public DataTable LaySanPham_NgayNhap_NgayHH(string idSanPham)
        {
            //OleDbCommand cmd = new OleDbCommand(@"SELECT * FROM MA_SAN_PHAM WHERE ID_SAN_PHAM = @masp AND SO_LUONG > 0 ORDER BY NGAY_HET_HAN ASC, NGAY_NHAP ASC");
            //cmd.Parameters.Add("masp", OleDbType.VarChar).Value = idSanPham;
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM MA_SAN_PHAM WHERE ID_SAN_PHAM = @masp AND SO_LUONG > 0 ORDER BY NGAY_HET_HAN ASC, NGAY_NHAP ASC");
            cmd.Parameters.Add("masp", SqlDbType.VarChar).Value = idSanPham;
            m_Ds.Load(cmd);
            return m_Ds;
        }
        #endregion
    }
}
