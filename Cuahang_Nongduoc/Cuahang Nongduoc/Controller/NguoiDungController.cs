using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc.Controller
{
    public class NguoiDungController
    {
        NguoiDungFactory factory = new NguoiDungFactory();

        public void HienthiDataGridview(
            DataGridView dg,
            BindingNavigator bn,
            TextBox txtTenDangNhap,
            TextBox txtMatKhau,
            ComboBox cmbVaiTro,
            TextBox txtTenNguoiDung,
            TextBox txtEmail,
            TextBox txtSDT,
            CheckBox chkTrangThai)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = factory.DanhSachNguoiDung();

            txtTenDangNhap.DataBindings.Clear();
            txtTenDangNhap.DataBindings.Add("Text", bs, "TEN_DANG_NHAP");

            txtMatKhau.DataBindings.Clear();
            txtMatKhau.DataBindings.Add("Text", bs, "MAT_KHAU_HASH");

            cmbVaiTro.DataBindings.Clear();
            cmbVaiTro.DataBindings.Add("Text", bs, "VAI_TRO");

            txtTenNguoiDung.DataBindings.Clear();
            txtTenNguoiDung.DataBindings.Add("Text", bs, "TEN_NGUOI_DUNG");

            txtEmail.DataBindings.Clear();
            txtEmail.DataBindings.Add("Text", bs, "EMAIL");

            txtSDT.DataBindings.Clear();
            txtSDT.DataBindings.Add("Text", bs, "SO_DIEN_THOAI");

            chkTrangThai.DataBindings.Clear();
            chkTrangThai.DataBindings.Add("Checked", bs, "TRANG_THAI");

            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        public DataTable DanhSachNguoiDung()
        {
            return factory.DanhSachNguoiDung();
        }

        public bool KiemTraDangNhap(string tenDangNhap, string matKhau, out string vaiTro, out string tenNguoiDung)
        {
            vaiTro = "";
            tenNguoiDung = "";

            DataTable dt = factory.LayNguoiDungTheoTenDangNhap(tenDangNhap);
            if (dt.Rows.Count == 0) return false;
            if (!(bool)dt.Rows[0]["TRANG_THAI"]) return false;

            string hashMatKhau = dt.Rows[0]["MAT_KHAU_HASH"].ToString();
            string inputHash = HashSHA256(matKhau);

            if (string.Equals(hashMatKhau, inputHash, StringComparison.OrdinalIgnoreCase))
            {
                vaiTro = dt.Rows[0]["VAI_TRO"].ToString();
                tenNguoiDung = dt.Rows[0]["TEN_NGUOI_DUNG"].ToString();
                return true;
            }
            return false;
        }

        public DataRow NewRow()
        {
            return factory.NewRow();
        }

        public void Add(DataRow row)
        {
            factory.Add(row);
        }

        public bool Save()
        {
            return factory.Save();
        }

        public void TimMaNguoiDung(String ma)
        {
            factory.TimMaNguoiDung(ma);
        }
        public void TimTenNguoiDung(String ten)
        {
            factory.TimTenNguoiDung(ten);
        }

        private string HashSHA256(string text)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
        public DataTable LayNguoiDungTheoTDN(string tenDangNhap)
        {
            return factory.LayNguoiDungTheoTenDangNhap(tenDangNhap);
        }
        //thêm
        public NguoiDung LayTenNguoiDung(string id)
        {
            DataTable tbl = factory.TimMaNguoiDung(id);
            NguoiDung nd = new NguoiDung();
            if (tbl.Rows.Count > 0)
            {
                nd.UserID = Convert.ToString(tbl.Rows[0]["ID"]);
                nd.TenDangNhap = Convert.ToString(tbl.Rows[0]["Ten_Dang_Nhap"]);
                nd.MatKhauHash = Convert.ToString(tbl.Rows[0]["MAT_KHAU_HASH"]);
                nd.VaiTro = Convert.ToString(tbl.Rows[0]["Vai_Tro"]);
                nd.TenNguoiDung = Convert.ToString(tbl.Rows[0]["Ten_Nguoi_Dung"]);
                nd.Email = Convert.ToString(tbl.Rows[0]["Email"]);
                nd.SoDienThoai = Convert.ToString(tbl.Rows[0]["So_Dien_Thoai"]);
                nd.TrangThai = Convert.ToBoolean(tbl.Rows[0]["Trang_Thai"]);
            }
            return nd;
        }
        public void HienthiNguoiDungDataGridviewComboBox(System.Windows.Forms.DataGridViewComboBoxColumn cmb)
        {

            cmb.DataSource = factory.DanhSachNguoiDung();
            cmb.DisplayMember = "TEN_NGUOI_DUNG";
            cmb.ValueMember = "ID";
            cmb.DataPropertyName = "ID_NGUOI_DUNG";
            cmb.HeaderText = "Nhân viên";

        }
        public void HienthiAutoComboBox(System.Windows.Forms.ComboBox cmb)
        {
            DataTable tbl = factory.DanhSachNguoiDung();
            cmb.DataSource = tbl;
            cmb.DisplayMember = "TEN_NGUOI_DUNG";
            cmb.ValueMember = "ID";
        }
    }
}
