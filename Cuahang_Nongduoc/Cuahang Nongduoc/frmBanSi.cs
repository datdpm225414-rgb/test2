using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc
{
    public partial class frmBanSi : Form
    {
        SanPhamController ctrlSanPham = new SanPhamController();
        KhachHangController ctrlKhachHang = new KhachHangController();
        MaSanPhamController ctrlMaSanPham = new MaSanPhamController();
        PhieuBanController ctrlPhieuBan = new PhieuBanController();
        ChiTietPhieuBanController ctrlChiTiet = new ChiTietPhieuBanController();
        DichVuController dvController = new DichVuController();
        NguoiDungController ctrNguoiDung = new NguoiDungController();
        Dictionary<string, int> viTriLo = new Dictionary<string, int>();
        Dictionary<string, decimal> soLuongLo = new Dictionary<string, decimal>();
        Decimal bqgq;

        IList<MaSanPham> deleted = new List<MaSanPham>();


        Controll status = Controll.Normal;

        public frmBanSi()
        {
            InitializeComponent();

            status = Controll.AddNew;
        }


        public frmBanSi(PhieuBanController ctrlPB)
            : this()
        {
            this.ctrlPhieuBan = ctrlPB;
            status = Controll.Normal;
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {            

        }
        private void frmBanSi_Load(object sender, EventArgs e)
        {
            dgvDanhsachSP.AutoGenerateColumns = false;
            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);
            dvController.HienthiAutoComboBox(cmbDichVu);
            ctrNguoiDung.HienthiAutoComboBox(cmbNV);

            cmbSanPham.SelectedIndexChanged += new EventHandler(cmbSanPham_SelectedIndexChanged);

            ctrlKhachHang.HienthiAutoComboBox(cmbKhachHang, true);

            //ctrlPhieuBan.HienthiPhieuBan(bindingNavigator,cmbKhachHang, txtMaPhieu, dtNgayLapPhieu, numTongTien, numDaTra, numConNo); // CODE GOC
            ctrlPhieuBan.HienthiPhieuBan(
                bindingNavigator,
                cmbNV,
                cmbKhachHang,
                txtMaPhieu,
                dtNgayLapPhieu,
                cmbDichVu,
                numPhiDichVu,
                numPhiVanChuyen,
                numGiamGia,
                numTongTien,
                numDaTra,
                numConNo
            );


            bindingNavigator.BindingSource.CurrentChanged -= new EventHandler(BindingSource_CurrentChanged);
            bindingNavigator.BindingSource.CurrentChanged += new EventHandler(BindingSource_CurrentChanged);

            ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, txtMaPhieu.Text);


            if (status == Controll.AddNew)
            {
                txtMaPhieu.Text = ThamSo.LayMaPhieuBan().ToString();
                DataTable dt = ctrNguoiDung.LayNguoiDungTheoTDN(ThamSo.Session.TenDangNhap);
                if (dt.Rows.Count > 0)
                {
                    //cmbNV.ValueMember = "ID";
                    //cmbNV.DisplayMember = "TEN_NGUOI_DUNG";
                    cmbNV.DataSource = dt;
                    cmbNV.SelectedValue = int.Parse(dt.Rows[0]["ID"].ToString());
                    cmbNV.Text = dt.Rows[0]["TEN_NGUOI_DUNG"].ToString();
                }
            }
            else
            {
                this.Allow(false);
            }

            numPhiVanChuyen.ValueChanged += new EventHandler(CacChiPhi_ValueChanged);
            numPhiDichVu.ValueChanged += new EventHandler(CacChiPhi_ValueChanged);
            numGiamGia.ValueChanged += new EventHandler(CacChiPhi_ValueChanged);
            numDaTra.ValueChanged += new EventHandler(CacChiPhi_ValueChanged);
            cmbDichVu.SelectedIndexChanged += new EventHandler(cmbDichVu_SelectedIndexChanged); // Thêm
            numDonGia.ValueChanged += new EventHandler(UpdateThanhTien);
            numSoLuong.ValueChanged += new EventHandler(UpdateThanhTien);
        }
        void BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (status == Controll.Normal)
            {
                ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, txtMaPhieu.Text);
            }
        }


        void cmbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSanPham.SelectedValue != null)
            {
                bqgq = BinhQuanGiaQuyen(cmbSanPham.SelectedValue.ToString());
                string idSP = cmbSanPham.SelectedValue.ToString();

                if (!viTriLo.ContainsKey(idSP))
                {
                    viTriLo[idSP] = 0;
                }

                MaSanPhamController ctrl = new MaSanPhamController();
                List<MaSanPham> masp = ctrl.LayDanhSachMaSanPham(idSP);

                int index = viTriLo[idSP];

                if (masp.Count > 0 && index < masp.Count)
                {
                    numDonGia.Value = masp[index].SanPham.GiaBanSi;
                    txtGiaNhap.Text = masp[index].GiaNhap.ToString("#,###0");
                    txtGiaBanSi.Text = masp[index].SanPham.GiaBanSi.ToString("#,###0");
                    txtGiaBanLe.Text = masp[index].SanPham.GiaBanLe.ToString("#,###0");
                    txtGiaBQGQ.Text = bqgq.ToString("#,###0");
                }

                if (viTriLo.ContainsKey(idSP))
                    viTriLo[idSP] = 0;

                if (soLuongLo.ContainsKey(idSP))
                    soLuongLo[idSP] = masp.Count > 0 ? masp[0].SoLuong : 0;
            }
        }
        /*
        void cmbMaSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        */
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (numSoLuong.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng!", "Phiếu bán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string idSP = cmbSanPham.SelectedValue.ToString();
            List<MaSanPham> danhSachLo = ctrlMaSanPham.LayDanhSachMaSanPham(idSP);
            decimal soLuongCanXuat = numSoLuong.Value;

            if (!viTriLo.ContainsKey(idSP))
                viTriLo.Add(idSP, 0);

            foreach (MaSanPham lo in danhSachLo)
            {
                if (!soLuongLo.ContainsKey(lo.Id))
                    soLuongLo.Add(lo.Id, lo.SoLuong);
            }

            while (soLuongCanXuat > 0 && viTriLo[idSP] < danhSachLo.Count)
            {
                int viTriHienTai = viTriLo[idSP];
                MaSanPham loHienTai = danhSachLo[viTriHienTai];
                string idLo = loHienTai.Id;

                decimal tonHienTai = soLuongLo[idLo];
                decimal soLuongXuat = Math.Min(soLuongCanXuat, tonHienTai);

                if (soLuongXuat > 0)
                {
                    soLuongLo[idLo] -= soLuongXuat;
                    soLuongCanXuat -= soLuongXuat;

                    decimal thanhTien = numDonGia.Value * soLuongXuat;

                    DataRow row = ctrlChiTiet.NewRow();
                    row["ID_MA_SAN_PHAM"] = idLo;
                    row["ID_PHIEU_BAN"] = txtMaPhieu.Text;
                    row["DON_GIA"] = numDonGia.Value; // Logic Bán sỉ: Lưu giá sỉ
                    row["SO_LUONG"] = soLuongXuat;
                    row["THANH_TIEN"] = thanhTien;
                    row["NGAY_HET_HAN"] = loHienTai.NgayHetHan;

                    ctrlChiTiet.Add(row);
                    KTraDongTrung();
                }

                if (soLuongLo[idLo] == 0)
                {
                    viTriLo[idSP]++;
                }
            }

            numSoLuong.Value = 0;
            numThanhTien.Value = 0;

            TinhToanTongTienCuoiCung();
        }

        public decimal BinhQuanGiaQuyen(string idSP)
        {
            decimal tongGiaTri = 0;
            decimal tongSoLuong = 0;
            List<MaSanPham> danhSachLo = ctrlMaSanPham.LayDanhSachMaSanPham(idSP);
            foreach (MaSanPham lo in danhSachLo)
            {
                tongGiaTri += lo.GiaNhap * lo.SoLuong;
                tongSoLuong += lo.SoLuong;
            }
            if (tongSoLuong == 0) return 0;
            return tongGiaTri / tongSoLuong;
        }

        public void KTraDongTrung()
        {
            DataTable tbl = null;
            if (dgvDanhsachSP.DataSource is BindingSource)
            {
                tbl = (DataTable)((BindingSource)dgvDanhsachSP.DataSource).DataSource;
            }
            else
            {
                tbl = (DataTable)dgvDanhsachSP.DataSource;
            }
            if (tbl == null || tbl.Rows.Count == 0) return;

            for (int i = 0; i < tbl.Rows.Count - 1; i++)
            {
                for (int j = i + 1; j < tbl.Rows.Count; j++)
                {
                    if (tbl.Rows[i]["ID_MA_SAN_PHAM"].ToString() == tbl.Rows[j]["ID_MA_SAN_PHAM"].ToString())
                    {
                        decimal sl1 = Convert.ToDecimal(tbl.Rows[i]["SO_LUONG"]);
                        decimal sl2 = Convert.ToDecimal(tbl.Rows[j]["SO_LUONG"]);
                        tbl.Rows[i]["SO_LUONG"] = sl1 + sl2;

                        decimal tt1 = Convert.ToDecimal(tbl.Rows[i]["THANH_TIEN"]);
                        decimal tt2 = Convert.ToDecimal(tbl.Rows[j]["THANH_TIEN"]);
                        tbl.Rows[i]["THANH_TIEN"] = tt1 + tt2;

                        tbl.Rows.RemoveAt(j);
                        j--;
                    }
                }
            }
            dgvDanhsachSP.DataSource = tbl;
            dgvDanhsachSP.Refresh();
        }

        private void UpdateThanhTien(object sender, EventArgs e)
        {
            numThanhTien.Value = numDonGia.Value * numSoLuong.Value;
        }


        private void numTongTien_ValueChanged(object sender, EventArgs e)
        {
            TinhToanConNo();
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            this.Luu();

                       status = Controll.Normal;
            this.Allow(false); // Thêm dòng này từ frmBanLe
             DataTable dt = ctrlMaSanPham.DanhSachMSP();
             Dictionary<string, long> tongTheoMaSP = new Dictionary<string, long>();

            foreach (DataRow row in dt.Rows)
            {
                string maSP = row["ID_SAN_PHAM"].ToString();
                long soLuong = Convert.ToInt64(row["SO_LUONG"]);

                if (tongTheoMaSP.ContainsKey(maSP))
                    tongTheoMaSP[maSP] += soLuong;
                else
                    tongTheoMaSP[maSP] = soLuong;
            }
            // Sau khi cộng dồn xong, với mỗi sản phẩm trùng, cộng lại vào tồn kho
            foreach (var item in tongTheoMaSP)
            {
                // Giả sử UpdateSoLuong(maSP, soLuong) là hàm "cập nhật tồn kho"
                // bạn nên tạo riêng một hàm kiểu: Cộng lại tồn kho
                ctrlSanPham.UpdateSoLuong(item.Key, item.Value);
            }
        }
        void Luu()
        {
            if (status == Controll.AddNew)
            {
                ThemMoi();
            }
            else
            {
                CapNhat();
            }
        }

        void CapNhat()
        {
            foreach (MaSanPham masp in deleted)
            {
                CuahangNongduoc.DataLayer.MaSanPhanFactory.CapNhatSoLuong(masp.Id, Convert.ToInt32(masp.SoLuong));
            }
            deleted.Clear();

            ctrlChiTiet.Save();

            DataRowView currentRow = (DataRowView)bindingNavigator.BindingSource.Current;

            currentRow["ID_KHACH_HANG"] = cmbKhachHang.SelectedValue;
            currentRow["NGAY_BAN"] = dtNgayLapPhieu.Value.Date;
            currentRow["TONG_TIEN"] = numTongTien.Value;
            currentRow["DA_TRA"] = numDaTra.Value;
            currentRow["CON_NO"] = numConNo.Value;
            currentRow["PHI_DICH_VU"] = numPhiDichVu.Value;
            currentRow["GIAM_GIA"] = numGiamGia.Value;
            currentRow["PHI_VAN_CHUYEN"] = numPhiVanChuyen.Value;
            currentRow["ID_DICH_VU"] = cmbDichVu.SelectedValue;
            currentRow["ID_NGUOI_DUNG"] = 1; // Tạm gán cứng

            // ctrlPhieuBan.Update(); 

            ctrlPhieuBan.CapNhatPhieuBan(currentRow.Row);
        }

        void ThemMoi()
        {
            // Tạo DataTable tạm để chứa dòng mới
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID");
            tbl.Columns.Add("ID_NGUOI_DUNG", typeof(int));
            tbl.Columns.Add("ID_KHACH_HANG");
            tbl.Columns.Add("NGAY_BAN", typeof(DateTime));
            tbl.Columns.Add("TONG_TIEN", typeof(long));
            tbl.Columns.Add("DA_TRA", typeof(long));
            tbl.Columns.Add("CON_NO", typeof(long));
            tbl.Columns.Add("GIAM_GIA", typeof(long));
            tbl.Columns.Add("PHI_DICH_VU", typeof(long));
            tbl.Columns.Add("PHI_VAN_CHUYEN", typeof(long));
            tbl.Columns.Add("ID_DICH_VU", typeof(int));

            DataRow row = tbl.NewRow();

            // Lấy các giá trị từ form
            row["ID"] = txtMaPhieu.Text;
            row["ID_KHACH_HANG"] = cmbKhachHang.SelectedValue;
            row["NGAY_BAN"] = dtNgayLapPhieu.Value.Date;
            row["TONG_TIEN"] = numTongTien.Value;
            row["DA_TRA"] = numDaTra.Value;
            row["CON_NO"] = numConNo.Value;
            row["PHI_DICH_VU"] = numPhiDichVu.Value;
            row["GIAM_GIA"] = numGiamGia.Value;
            row["PHI_VAN_CHUYEN"] = numPhiVanChuyen.Value;
            row["ID_DICH_VU"] = cmbDichVu.SelectedValue;
            row["ID_NGUOI_DUNG"] = cmbNV.SelectedValue; // Tạm gán cứng ID 1


            PhieuBanController ctrl = new PhieuBanController();

            // if (ctrl.LayPhieuBan(txtMaPhieu.Text) != null) // Hàm cũ
            if (ctrl.LayPhieuBan(DateTime.MinValue, txtMaPhieu.Text) != null) // Hàm mới
            {
                MessageBox.Show("Mã Phiếu bán này đã tồn tại!", "Phiếu bán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (ctrlPhieuBan.LuuPhieuBanMoi(row))
            {
                MessageBox.Show("Lưu phiếu bán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrlChiTiet.Save();
                if (ThamSo.LaSoNguyen(txtMaPhieu.Text))
                {
                    long so = Convert.ToInt64(txtMaPhieu.Text);
                    if (so >= ThamSo.LayMaPhieuBan())
                    {
                        ThamSo.GanMaPhieuBan(so + 1);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lưu phiếu bán thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void toolLuu_Them_Click(object sender, EventArgs e)
        {
            ctrlPhieuBan = new PhieuBanController();
            status = Controll.AddNew;
            txtMaPhieu.Text = ThamSo.LayMaPhieuBan().ToString();
            numTongTien.Value = 0;
            numPhiDichVu.Value = 0;
            numPhiVanChuyen.Value = 0;
            numGiamGia.Value = 0;
            numDaTra.Value = 0;
            numConNo.Value = 0;
            if (cmbDichVu.Items.Count > 0) cmbDichVu.SelectedIndex = 0;
            if (cmbKhachHang.Items.Count > 0) cmbKhachHang.SelectedIndex = 0;

            ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, txtMaPhieu.Text);
            this.Allow(true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Si", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //BindingSource bs = ((BindingSource)dgvDanhsachSP.DataSource);
                //DataRowView row = (DataRowView)bs.Current;

                //deleted.Add(new MaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]), Convert.ToInt32(row["SO_LUONG"])));
                //bs.RemoveCurrent();
                string idLo = dgvDanhsachSP.SelectedRows[0].Cells["colMaSanPham"].Value.ToString();
                decimal soLuongXoa = Convert.ToDecimal(dgvDanhsachSP.SelectedRows[0].Cells["colSoLuong"].Value);

                if (soLuongLo.ContainsKey(idLo))
                {
                    soLuongLo[idLo] += soLuongXoa;
                }

                dgvDanhsachSP.Rows.Remove(dgvDanhsachSP.SelectedRows[0]);

                if (cmbSanPham.SelectedValue != null)
                {
                    string idSP = cmbSanPham.SelectedValue.ToString();
                    viTriLo[idSP] = 0;
                }

                TinhToanTongTienCuoiCung(); // Thêm
            }
        }

        private void dgvDanhsachSP_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Si", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                BindingSource bs = ((BindingSource)dgvDanhsachSP.DataSource);
                DataRowView row = (DataRowView)bs.Current;
                deleted.Add(new MaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]), Convert.ToInt32(row["SO_LUONG"])));

                this.BeginInvoke(new Action(TinhToanTongTienCuoiCung)); // Thêm
            }
        }

        private void toolLuuIn_Click(object sender, EventArgs e)
        {
            if (status != Controll.Normal)
            {
                MessageBox.Show("Vui lòng lưu lại Phiếu bán hiện tại!", "Phieu Ban Si", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                String ma_phieu = txtMaPhieu.Text;

                PhieuBanController ctrlPB = new PhieuBanController();

                // CuahangNongduoc.BusinessObject.PhieuBan ph = ctrlPB.LayPhieuBan(ma_phieu); // Hàm cũ
                CuahangNongduoc.BusinessObject.PhieuBan ph = ctrlPB.LayPhieuBan(DateTime.MinValue, ma_phieu); // Hàm mới

                frmInPhieuBan InPhieuBan = new frmInPhieuBan(ph);

                InPhieuBan.Show();

            }
        }

        private void dgvDanhsachSP_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void toolChinhSua_Click(object sender, EventArgs e)
        {
            status = Controll.Edit;
            this.Allow(true);
        }

        void Allow(bool val)
        {
            // Tương tự frmBanLe
            // txtMaPhieu.Enabled = val;
            dtNgayLapPhieu.Enabled = val;
            // numTongTien.Enabled = val;

            cmbKhachHang.Enabled = val;
            numPhiVanChuyen.Enabled = val;
            numGiamGia.Enabled = val;
            numDaTra.Enabled = val;
            cmbDichVu.Enabled = val;
            numPhiDichVu.Enabled = val;

            btnAdd.Enabled = val;
            btnRemove.Enabled = val;
            dgvDanhsachSP.Enabled = val;

            cmbSanPham.Enabled = val;
            numSoLuong.Enabled = val;
            numDonGia.Enabled = val;
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            if (status != Controll.Normal)
            {
                if (MessageBox.Show("Bạn có muốn lưu lại Phiếu bán này không?", "Phieu Ban Si", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Luu();
                }

            }
            this.Close();
        }

        private void toolXoa_Click(object sender, EventArgs e)
        {
            btnRemove_Click(sender, e);
        }
        private void numDonGia_ValueChanged(object sender, EventArgs e)
        {
            numThanhTien.Value = numDonGia.Value * numSoLuong.Value;
        }

        private void toolXemLai_Click(object sender, EventArgs e)
        {
            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);
            //ctrlMaSanPham.HienThiDataGridViewComboBox(colMaSanPham);
            ctrlKhachHang.HienthiAutoComboBox(cmbKhachHang, true);
        }

        private void btnThemDaiLy_Click(object sender, EventArgs e)
        {
            // frmDaiLy DaiLy = new frmDaiLy(); /
            // DaiLy.ShowDialog();

            frmKhachHang KhachHang = new frmKhachHang();
            KhachHang.ShowDialog();

            ctrlKhachHang.HienthiAutoComboBox(cmbKhachHang, true);

        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            frmSanPham SanPham = new frmSanPham();
            SanPham.ShowDialog();
            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);
        }


        private void cmbDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDichVu.SelectedItem != null && cmbDichVu.SelectedValue != null)
            {
                try
                {
                    DataRowView drv = (DataRowView)cmbDichVu.SelectedItem;
                    numPhiDichVu.Value = Convert.ToDecimal(drv.Row["GIA_MAC_DINH"]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lấy giá dịch vụ: " + ex.Message);
                    numPhiDichVu.Value = 0;
                }
            }
        }

        private void TinhToanTongTienCuoiCung()
        {
            decimal tienHang = 0;
            foreach (DataGridViewRow row in dgvDanhsachSP.Rows)
            {
                if (!row.IsNewRow && row.Cells["colThanhTien"].Value != null && row.Cells["colThanhTien"].Value != DBNull.Value)
                {
                    tienHang += Convert.ToDecimal(row.Cells["colThanhTien"].Value);
                }
            }

            decimal phiVanChuyen = numPhiVanChuyen.Value;
            decimal phiDichVu = numPhiDichVu.Value;
            decimal giamGia = numGiamGia.Value;
            decimal daTra = numDaTra.Value;

            decimal tongTienCuoiCung = tienHang + phiVanChuyen + phiDichVu - giamGia;

            numTongTien.ValueChanged -= CacChiPhi_ValueChanged;
            numConNo.ValueChanged -= CacChiPhi_ValueChanged;

            numTongTien.Value = tongTienCuoiCung;
            numConNo.Value = tongTienCuoiCung - daTra;

            numTongTien.ValueChanged += CacChiPhi_ValueChanged;
            numConNo.ValueChanged += CacChiPhi_ValueChanged;
        }

        private void TinhToanConNo()
        {
            numConNo.Value = numTongTien.Value - numDaTra.Value;
        }

        private void CacChiPhi_ValueChanged(object sender, EventArgs e)
        {
            TinhToanTongTienCuoiCung();
        }

        
    }
}