using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
namespace CuahangNongduoc.DataLayer
{
    public class PhieuBanFactory
    {
        // DataService m_Ds = new DataService(); 

        // private string connectionString = DataService.m_ConnectString; 
        public DataTable TimPhieuBan(String idKh, DateTime dt)
        {
            DataService ds = new DataService(); // Tạo mới
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_BAN WHERE CONVERT(date, NGAY_BAN) = CONVERT(date, @ngay) AND ID_KHACH_HANG=@kh");
            cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = dt.Date;
            cmd.Parameters.Add("@kh", SqlDbType.VarChar).Value = idKh;
            ds.Load(cmd);
            return ds; // Trả về ds mới
        }

        public DataTable DanhsachPhieuBanLe()
        {
            DataService ds_le = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT PB.*,KH.*,ND.*,DV.* FROM PHIEU_BAN PB, KHACH_HANG KH, NGUOI_DUNG ND, DICH_VU DV WHERE PB.ID_DICH_VU = DV.ID AND PB.ID_KHACH_HANG=KH.ID AND ND.ID = PB.ID_NGUOI_DUNG AND KH.LOAI_KH = 0 AND PB.Trang_Thai = 1");
            ds_le.Load(cmd);
            return ds_le;
        }
        public DataTable DanhsachPhieuBanSi()
        {
            DataService ds_si = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT PB.*,KH.*,ND.*,DV.* FROM PHIEU_BAN PB, KHACH_HANG KH, NGUOI_DUNG ND, DICH_VU DV WHERE PB.ID_DICH_VU = DV.ID AND PB.ID_KHACH_HANG=KH.ID AND ND.ID = PB.ID_NGUOI_DUNG AND KH.LOAI_KH = 1 AND PB.Trang_Thai = 1");
            ds_si.Load(cmd);
            return ds_si;
        }


        public DataTable LayPhieuBan(String id)
        {
            DataService ds = new DataService(); 
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_BAN WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = id;
            ds.Load(cmd);
            return ds; 
        }
        // thêm
        public DataTable LayBaoCaoPhieuBanTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            DataService ds = new DataService(); 
            string query = @"
            SELECT *
            FROM PHIEU_BAN PB
            WHERE PB.NGAY_BAN >= @tuNgay AND PB.NGAY_BAN <= @denNgay
            ORDER BY NGAY_BAN";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.Add("tuNgay", SqlDbType.Date).Value = tuNgay;
            cmd.Parameters.Add("denNgay", SqlDbType.Date).Value = denNgay;

            ds.Load(cmd);
            return ds; 
        }
        // thêm
        public DataTable LayPhieuBanNguoiDungTheoNgay(DateTime tuNgay, DateTime denNgay, string idNgDung)
        {
            DataService ds = new DataService();
            string query = @"
            SELECT *
            FROM PHIEU_BAN
            WHERE NGAY_BAN >= @tuNgay AND NGAY_BAN <= @denNgay AND ID_NGUOI_DUNG = @idNgDung
            ORDER BY NGAY_BAN";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.Add("tuNgay", SqlDbType.Date).Value = tuNgay;
            cmd.Parameters.Add("denNgay", SqlDbType.Date).Value = denNgay;
            cmd.Parameters.Add("idNgDung", SqlDbType.VarChar,50).Value = idNgDung;
            ds.Load(cmd);
            return ds;
        }

        public DataTable LayChiTietPhieuBan(String idPhieuBan)
        {
            DataService ds = new DataService(); 
            SqlCommand cmd = new SqlCommand("SELECT * FROM CHI_TIET_PHIEU_BAN WHERE ID_PHIEU_BAN = @id");
            cmd.Parameters.Add("id", SqlDbType.VarChar, 50).Value = idPhieuBan;
            ds.Load(cmd);
            return ds; 
        }

        public static long LayConNo(String kh, int thang, int nam)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand(
                @"SELECT SUM(CON_NO) 
                  FROM PHIEU_BAN 
                  WHERE ID_KHACH_HANG = @kh 
                        AND MONTH(NGAY_BAN) = @thang 
                        AND YEAR(NGAY_BAN) = @nam");
            cmd.Parameters.Add("kh", SqlDbType.VarChar, 50).Value = kh;
            cmd.Parameters.Add("thang", SqlDbType.Int).Value = thang;
            cmd.Parameters.Add("nam", SqlDbType.Int).Value = nam;

            object obj = ds.ExecuteScalar(cmd);
            if (obj == null || obj == DBNull.Value)
                return 0;
            else
                return Convert.ToInt64(obj);
        }

        public static int LaySoPhieu()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM PHIEU_BAN");
            object obj = ds.ExecuteScalar(cmd);
            if (obj == null || obj == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(obj);
        }

        public bool InsertPhieuBan(DataRow row)
        {
            DataService ds = new DataService();
            try
            {
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO PHIEU_BAN
                    (ID, ID_NGUOI_DUNG, ID_KHACH_HANG, NGAY_BAN, TONG_TIEN, DA_TRA, CON_NO, 
                     GIAM_GIA, PHI_DICH_VU, PHI_VAN_CHUYEN, ID_DICH_VU,Trang_Thai)
                    VALUES
                    (@ID, @ID_NGUOI_DUNG, @ID_KHACH_HANG, @NGAY_BAN, @TONG_TIEN, @DA_TRA, @CON_NO,
                     @GIAM_GIA, @PHI_DICH_VU, @PHI_VAN_CHUYEN, @ID_DICH_VU,1)");

                cmd.Parameters.AddWithValue("@ID", row["ID"]);
                cmd.Parameters.AddWithValue("@ID_NGUOI_DUNG", row["ID_NGUOI_DUNG"]);
                cmd.Parameters.AddWithValue("@ID_KHACH_HANG", row["ID_KHACH_HANG"] ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NGAY_BAN", row["NGAY_BAN"]);
                cmd.Parameters.AddWithValue("@TONG_TIEN", row["TONG_TIEN"]);
                cmd.Parameters.AddWithValue("@DA_TRA", row["DA_TRA"]);
                cmd.Parameters.AddWithValue("@CON_NO", row["CON_NO"]);
                cmd.Parameters.AddWithValue("@GIAM_GIA", row["GIAM_GIA"]);
                cmd.Parameters.AddWithValue("@PHI_DICH_VU", row["PHI_DICH_VU"]);
                cmd.Parameters.AddWithValue("@PHI_VAN_CHUYEN", row["PHI_VAN_CHUYEN"]);
                cmd.Parameters.AddWithValue("@ID_DICH_VU", row["ID_DICH_VU"] ?? (object)DBNull.Value);

                return ds.ExecuteNoneQuery(cmd) > 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi khi Insert PHIEU_BAN: " + ex.Message);
                return false;
            }
        }

        public bool UpdatePhieuBan(DataRow row)
        {
            DataService ds = new DataService();
            try
            {
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE PHIEU_BAN SET
                        ID_KHACH_HANG = @ID_KHACH_HANG,
                        NGAY_BAN = @NGAY_BAN,
                        TONG_TIEN = @TONG_TIEN,
                        DA_TRA = @DA_TRA,
                        CON_NO = @CON_NO,
                        GIAM_GIA = @GIAM_GIA,
                        PHI_DICH_VU = @PHI_DICH_VU,
                        PHI_VAN_CHUYEN = @PHI_VAN_CHUYEN,
                        ID_DICH_VU = @ID_DICH_VU,
                        ID_NGUOI_DUNG = @ID_NGUOI_DUNG
                    WHERE ID = @ID");

                cmd.Parameters.AddWithValue("@ID_KHACH_HANG", row["ID_KHACH_HANG"] ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NGAY_BAN", row["NGAY_BAN"]);
                cmd.Parameters.AddWithValue("@TONG_TIEN", row["TONG_TIEN"]);
                cmd.Parameters.AddWithValue("@DA_TRA", row["DA_TRA"]);
                cmd.Parameters.AddWithValue("@CON_NO", row["CON_NO"]);
                cmd.Parameters.AddWithValue("@GIAM_GIA", row["GIAM_GIA"]);
                cmd.Parameters.AddWithValue("@PHI_DICH_VU", row["PHI_DICH_VU"]);
                cmd.Parameters.AddWithValue("@PHI_VAN_CHUYEN", row["PHI_VAN_CHUYEN"]);
                cmd.Parameters.AddWithValue("@ID_DICH_VU", row["ID_DICH_VU"] ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_NGUOI_DUNG", row["ID_NGUOI_DUNG"]);

                cmd.Parameters.AddWithValue("@ID", row["ID"]); // Tham số cho WHERE

                return ds.ExecuteNoneQuery(cmd) > 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi khi Update PHIEU_BAN: " + ex.Message);
                return false;
            }
        }



    }
}