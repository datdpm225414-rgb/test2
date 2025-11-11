using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;
using CuahangNongduoc.DataLayer;
using Microsoft.Win32;

namespace CuahangNongduoc
{
    public partial class frmMain : Form
    {
        private string vaiTroHienTai;
        private string tenNguoiDungHienTai;
        frmDangNhap DangNhap = null;
        NguoiDungController ctrlNguoiDung = new NguoiDungController();
        public frmMain()
        {
            Flash flash = new Flash();
            flash.ShowDialog();
            InitializeComponent();
        }
        frmDonViTinh DonViTinh = null;

        private void mnuDonViTinh_Click(object sender, EventArgs e)
        {
            if (DonViTinh == null || DonViTinh.IsDisposed)
            {
                DonViTinh = new frmDonViTinh();
                DonViTinh.MdiParent = this;
                DonViTinh.Show();

            }
            else
                DonViTinh.Activate();
        }



        public frmMain(string vaiTro, string tenNguoiDung)
        {

            InitializeComponent();
            // Lưu lại thông tin được truyền từ form đăng nhập
            this.vaiTroHienTai = vaiTro;
            this.tenNguoiDungHienTai = tenNguoiDung;

            // Đặt tên thanh tiêu đề để chào mừng người dùng
            this.Text = "Cửa hàng Nông dược - Chào " + this.tenNguoiDungHienTai + " - Chức vụ: " + this.vaiTroHienTai;
        }




        private void frmMain_Load(object sender, EventArgs e)
        {
            //RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\CoolSoft\\CuahangNongduoc");

            //if (regKey == null)
            //{
            //    DataService.m_ConnectString = "";
            //}
            //else
            //{
            //    try
            //    {
            //        DataService.m_ConnectString = (String)regKey.GetValue("ConnectString");
            //    }
            //    catch
            //    {
            //    }
            //    finally
            //    {
            //        regKey.Close();
            //    }
            //}

            //if (DataService.OpenConnection() == false)
            //{
            //    MessageBox.Show("Không thể kết nối dữ liệu!", "Cua hang Nong duoc", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
            dangNhap();
            DataService.OpenConnection();
            PhanQuyenNguoiDung(vaiTroHienTai);

        }
        private void dangNhap()
        {
            while (true)
            {
                DangNhap = new frmDangNhap();
                if (DangNhap.ShowDialog() == DialogResult.OK)
                {
                    string tenDangNhap = DangNhap.txtTenDangNhap.Text.Trim();
                    string matKhau = DangNhap.txtMatKhau.Text.Trim();

                    if (string.IsNullOrEmpty(tenDangNhap))
                    {
                        MessageBox.Show("Tên đăng nhập không được bỏ trống!");
                        continue;
                    }

                    if (string.IsNullOrEmpty(matKhau))
                    {
                        MessageBox.Show("Mật khẩu không được bỏ trống!");
                        continue;
                    }

                    string vaiTro, tenNguoiDung;
                    if (ctrlNguoiDung.KiemTraDangNhap(tenDangNhap, matKhau, out vaiTro, out tenNguoiDung))
                    {
                        //MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                        vaiTroHienTai = vaiTro;
                        tenNguoiDungHienTai = tenNguoiDung;
                        ThamSo.Session.TenDangNhap = tenDangNhap;
                        PhanQuyenNguoiDung(vaiTro);
                        this.Text = $"Cửa hàng Nông dược - Chào {tenNguoiDung} - Chức vụ: {vaiTro}";
                        break; // THOÁT vòng lặp
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.");
                    }
                }
                else
                {
                    // Người dùng ấn Thoát
                    Application.Exit();
                    break;
                }
            }
        }


        private void PhanQuyenNguoiDung(string vaiTro)
        {
            switch (vaiTro)
            {
                case "Admin":
                    expando1.Enabled = true;
                    toolSanPham.Enabled = true;
                    toolKhachHang.Enabled = true;
                    toolDaiLy.Enabled = true;
                    toolNhaCungCap.Enabled = true;
                    mnuBaocao.Enabled = true;
                    mnuQuanLy.Enabled = true;
                    expando3.Enabled = true;
                    toolBanLe.Enabled = true;
                    toolBanSi.Enabled = true;
                    toolNhapHang.Enabled = true;
                    toolPhieuChi.Enabled = true;
                    mnuHienThi.Enabled = true;
                    toolTonKho.Enabled = true;
                    toolThanhtoan.Enabled = true;
                    ToolDangXuat.Enabled = true;
                    ToolDangNhap.Enabled = false;
                    break;

                case "Nhan vien":
                    expando1.Enabled = false;
                    toolSanPham.Enabled = false;
                    toolKhachHang.Enabled = false;
                    toolDaiLy.Enabled = false;
                    toolNhaCungCap.Enabled = false;
                    mnuBaocao.Enabled = true;
                    mnuQuanLy.Enabled = false;
                    expando3.Enabled = true;
                    ToolDangXuat.Enabled = true;
                    ToolDangNhap.Enabled = false;
                    toolNhapHang.Enabled = true;
                    toolBanLe.Enabled = true;
                    toolBanSi.Enabled = true;
                    toolPhieuChi.Enabled = true;
                    mnuHienThi.Enabled = true;
                    toolTonKho.Enabled = true;
                    toolThanhtoan.Enabled = true;

                    break;

                default:
                    expando1.Enabled = false;
                    toolSanPham.Enabled = false;
                    toolKhachHang.Enabled = false;
                    toolDaiLy.Enabled = false;
                    toolNhaCungCap.Enabled = false;
                    mnuBaocao.Enabled = false;
                    mnuQuanLy.Enabled = false;
                    expando3.Enabled = false;
                    toolBanLe.Enabled = false;
                    toolBanSi.Enabled = false;
                    toolNhapHang.Enabled = false;
                    toolPhieuChi.Enabled = false;
                    mnuHienThi.Enabled = false;
                    toolTonKho.Enabled = false;
                    toolThanhtoan.Enabled = false;
                    ToolDangXuat.Enabled = false;
                    ToolDangNhap.Enabled = true;
                    break;
            }
        }

        frmSanPham SanPham = null;
        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            if (SanPham == null || SanPham.IsDisposed)
            {
                SanPham = new frmSanPham();
                SanPham.MdiParent = this;
                SanPham.Show();
            }
            else
                SanPham.Activate();
        }
        frmKhachHang KhachHang = null;
        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            if (KhachHang == null || KhachHang.IsDisposed)
            {
                KhachHang = new frmKhachHang();
                KhachHang.MdiParent = this;
                KhachHang.Show();
            }
            else
                KhachHang.Activate();
        }
        frmDaiLy DaiLy = null;
        private void mnuDaiLy_Click(object sender, EventArgs e)
        {
            if (DaiLy == null || DaiLy.IsDisposed)
            {
                DaiLy = new frmDaiLy();
                DaiLy.MdiParent = this;
                DaiLy.Show();
            }
            else
                DaiLy.Activate();

        }
        frmDanhsachPhieuNhap NhapHang = null;
        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            if (NhapHang == null || NhapHang.IsDisposed)
            {
                NhapHang = new frmDanhsachPhieuNhap();
                NhapHang.MdiParent = this;
                NhapHang.Show();
            }
            else
                NhapHang.Activate();
        }
        frmDanhsachPhieuBanLe BanLe = null;
        private void mnuBanHangKH_Click(object sender, EventArgs e)
        {
            if (BanLe == null || BanLe.IsDisposed)
            {
                BanLe = new frmDanhsachPhieuBanLe();
                BanLe.MdiParent = this;
                BanLe.Show();
            }
            else
                BanLe.Activate();
        }
        frmDanhsachPhieuBanSi BanSi = null;
        private void mnuBanHangDL_Click(object sender, EventArgs e)
        {
            if (BanSi == null || BanSi.IsDisposed)
            {
                BanSi = new frmDanhsachPhieuBanSi();
                BanSi.MdiParent = this;
                BanSi.Show();
            }
            else
                BanSi.Activate();
        }

        private void mnuThanhCongCu_Click(object sender, EventArgs e)
        {
            mnuThanhCongCu.Checked = !mnuThanhCongCu.Checked;
            toolStrip.Visible = mnuThanhCongCu.Checked;
        }

        private void mnuThanhChucNang_Click(object sender, EventArgs e)
        {
            mnuThanhChucNang.Checked = !mnuThanhChucNang.Checked;
            taskPane.Visible = mnuThanhChucNang.Checked;
        }
        frmThanhToan ThanhToan = null;
        private void mnuThanhtoan_Click(object sender, EventArgs e)
        {
            if (ThanhToan == null || ThanhToan.IsDisposed)
            {
                ThanhToan = new frmThanhToan();
                ThanhToan.MdiParent = this;
                ThanhToan.Show();
            }
            else
                ThanhToan.Activate();
        }
        frmDunoKhachhang DunoKhachhang = null;
        private void mnuTonghopDuno_Click(object sender, EventArgs e)
        {
            if (DunoKhachhang == null || DunoKhachhang.IsDisposed)
            {
                DunoKhachhang = new frmDunoKhachhang();
                DunoKhachhang.MdiParent = this;
                DunoKhachhang.Show();
            }
            else
                DunoKhachhang.Activate();
        }
        frmDoanhThu DoanhThu = null;
        private void mnuBaocaoDoanhThu_Click(object sender, EventArgs e)
        {
            if (DoanhThu == null || DoanhThu.IsDisposed)
            {
                DoanhThu = new frmDoanhThu();
                DoanhThu.MdiParent = this;
                DoanhThu.Show();
            }
            else
                DoanhThu.Activate();

        }

        frmSoLuongTon SoLuongTon = null;
        private void mnuBaocaoSoluongton_Click(object sender, EventArgs e)
        {

            if (SoLuongTon == null || SoLuongTon.IsDisposed)
            {
                SoLuongTon = new frmSoLuongTon();
                SoLuongTon.MdiParent = this;
                SoLuongTon.Show();
            }
            else
                SoLuongTon.Activate();

        }
        frmSoLuongBan SoLuongBan = null;
        private void mnuSoLuongBan_Click(object sender, EventArgs e)
        {
            if (SoLuongBan == null || SoLuongBan.IsDisposed)
            {
                SoLuongBan = new frmSoLuongBan();
                SoLuongBan.MdiParent = this;
                SoLuongBan.Show();
            }
            else
                SoLuongBan.Activate();
        }
        frmSanphamHethan SanphamHethan = null;
        private void mnuSanphamHethan_Click(object sender, EventArgs e)
        {
            if (SanphamHethan == null || SanphamHethan.IsDisposed)
            {
                SanphamHethan = new frmSanphamHethan();
                SanphamHethan.MdiParent = this;
                SanphamHethan.Show();
            }
            else
                SanphamHethan.Activate();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        frmThongtinCuahang ThongtinCuahang = null;
        private void mnuTuychinhThongtin_Click(object sender, EventArgs e)
        {

            if (ThongtinCuahang == null || ThongtinCuahang.IsDisposed)
            {
                ThongtinCuahang = new frmThongtinCuahang();
                ThongtinCuahang.MdiParent = this;
                ThongtinCuahang.Show();
            }
            else
                ThongtinCuahang.Activate();
        }
        frmThongtinLienhe ThongtinLienhe = null;
        private void mnuTrogiupLienhe_Click(object sender, EventArgs e)
        {
            if (ThongtinLienhe == null || ThongtinLienhe.IsDisposed)
            {
                ThongtinLienhe = new frmThongtinLienhe();
                ThongtinLienhe.MdiParent = this;
                ThongtinLienhe.Show();
            }
            else
                ThongtinLienhe.Activate();
        }

        frmNhaCungCap NhaCungCap = null;
        private void mnuNhaCungCap_Click(object sender, EventArgs e)
        {
            if (NhaCungCap == null || NhaCungCap.IsDisposed)
            {
                NhaCungCap = new frmNhaCungCap();
                NhaCungCap.MdiParent = this;
                NhaCungCap.Show();
            }
            else
                NhaCungCap.Activate();
        }
        frmLyDoChi LyDoChi = null;
        private void mnuLyDoChi_Click(object sender, EventArgs e)
        {
            if (LyDoChi == null || LyDoChi.IsDisposed)
            {
                LyDoChi = new frmLyDoChi();
                LyDoChi.MdiParent = this;
                LyDoChi.Show();
            }
            else
                LyDoChi.Activate();
        }

        frmPhieuChi PhieuChi = null;
        private void mnuPhieuChi_Click(object sender, EventArgs e)
        {
            if (PhieuChi == null || PhieuChi.IsDisposed)
            {
                PhieuChi = new frmPhieuChi();
                PhieuChi.MdiParent = this;
                PhieuChi.Show();
            }
            else
                PhieuChi.Activate();
        }

        private void mnuTrogiupHuongdan_Click(object sender, EventArgs e)
        {
            // Help.ShowHelp(this, "CPP.CHM");
        }


        frmNguoiDung NguoiDung = null;
        private void toolQLTK_Click(object sender, EventArgs e)
        {
            if (NguoiDung == null || NguoiDung.IsDisposed)
            {
                NguoiDung = new frmNguoiDung();
                NguoiDung.MdiParent = this;
                NguoiDung.Show();
            }
            else
                NguoiDung.Activate();
        }



        private void ToolDangNhap_Click(object sender, EventArgs e)
        {
            dangNhap();
        }

        private void ToolDangXuat_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }

            // Reset quyền
            PhanQuyenNguoiDung("");
            this.Text = "Cửa hàng Nông dược";

            // Gọi lại đăng nhập
            dangNhap();
        }
        frmInDichVu frmInDichVu = null;
        frmInPhieuBanGiamGia frmInPhieuBanGiamGia = null;
        private void mnuDichVuPhatSinh_Click_1(object sender, EventArgs e)
        {
            if (frmInDichVu == null || frmInDichVu.IsDisposed)
            {
                frmInDichVu = new frmInDichVu();
                frmInDichVu.MdiParent = this;
                frmInDichVu.Show();
            }
            else
                frmInDichVu.Activate();
        }

        private void mnuHDGiamGIa_Click(object sender, EventArgs e)
        {
            if (frmInPhieuBanGiamGia == null || frmInPhieuBanGiamGia.IsDisposed)
            {
                frmInPhieuBanGiamGia = new frmInPhieuBanGiamGia();
                frmInPhieuBanGiamGia.MdiParent = this;
                frmInPhieuBanGiamGia.Show();
            }
            else
                frmInPhieuBanGiamGia.Activate();
        }
        frmDoanhThu frmDoanhThu = null;
        private void toolDoanhThu_Click(object sender, EventArgs e)
        {

            if (frmDoanhThu == null || frmDoanhThu.IsDisposed)
            {
                frmDoanhThu = new frmDoanhThu();
                frmDoanhThu.MdiParent = this;
                frmDoanhThu.Show();
            }
            else
                frmDoanhThu.Activate();
        }
    }
}