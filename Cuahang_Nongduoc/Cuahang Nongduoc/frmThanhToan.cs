using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    public partial class frmThanhToan : Form
    {
        KhachHangController ctrlKH = new KhachHangController();
        NguoiDungController ctrlND = new NguoiDungController();
        PhieuThanhToanController ctrl = new PhieuThanhToanController();
        public frmThanhToan()
        {
            InitializeComponent();
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            ctrlKH.HienthiChungAutoComboBox(cmbKhachHang);
            ctrlKH.HienthiKhachHangChungDataGridviewComboBox(colKhachHang);
           ctrlND.HienthiAutoComboBox(cmbNV);
            ctrlND.HienthiNguoiDungDataGridviewComboBox(colNguoiDung); //Người lập
            ctrl.HienthiPhieuThanhToan(bindingNavigator, dataGridView,cmbNV, cmbKhachHang, txtMaPhieu, dtNgayThanhToan, numTongTien, txtGhiChu);
            bindingNavigator.BindingSource.AddingNew += new AddingNewEventHandler(BindingSource_AddingNew);
        }

        void BindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            
        }

        private void toolAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hãy nhập dữ liệu rồi ấn lưu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            long maphieu = ThamSo.LayMaPhieuThanhToan();
            txtGhiChu.Text = "";
            cmbKhachHang.Text = ""; 
            numTongTien.Value = 0;
            txtMaPhieu.Text = (maphieu + 1).ToString();
            DataTable dt = ctrlND.LayNguoiDungTheoTDN(ThamSo.Session.TenDangNhap);
            if (dt.Rows.Count > 0)
            {
                cmbNV.SelectedValue = Convert.ToInt64(dt.Rows[0]["ID"]);
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn xóa phiếu thanh toán này không?", "Phieu Thanh Toan",   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn xóa phiếu thanh toán này không?", "Phieu Thanh Toan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    try
                    {


                        DataGridViewRow row = dataGridView.SelectedRows[0];
                        string id = row.Cells["colMaPhieu"].Value.ToString();
                        ThamSo.Delete(id, "PHIEU_THANH_TOAN");

                        MessageBox.Show("Xóa thành công!", "Phieu thanh toan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmThanhToan_Load(sender, e);
                    }
                    catch
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }



                }
                //bindingNavigator.BindingSource.RemoveCurrent();
                //ctrl.Save();
            }
        }

        private void toolSave_Click(object sender, EventArgs e)
        {

            try
            {
                long maphieu = ThamSo.LayMaPhieuThanhToan(); ;
                ThamSo.GanMaPhieuThanhToan(maphieu + 1);

                DataRow row = ctrl.NewRow();
                row["ID"] = maphieu;
                row["NGAY_THANH_TOAN"] = dtNgayThanhToan.Value;
                row["TONG_TIEN"] = numTongTien.Value;
                row["GHI_CHU"] = txtGhiChu.Text;
                row["ID_KHACH_HANG"] = cmbKhachHang.SelectedValue;
                row["ID_NGUOI_DUNG"] = cmbNV.SelectedValue;

                ctrl.Add(row);
                bindingNavigator.BindingSource.MoveLast();
                txtMaPhieu.Focus();
                bindingNavigator.BindingSource.MoveNext();
                ctrl.Save();
                
                MessageBox.Show("Lưu phiếu thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            frmThanhToan_Load(sender, e);
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void toolIn_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;
            if (row != null)
            {
                PhieuThanhToanController ctrlTT = new PhieuThanhToanController();
                String ma_phieu = row["ID"].ToString();
                CuahangNongduoc.BusinessObject.PhieuThanhToan ph = ctrlTT.LayPhieuThanhToan(ma_phieu);
                frmInPhieuThanhToan PhieuThanhToan = new frmInPhieuThanhToan(ph);
                PhieuThanhToan.Show();
            }
        }

        private void toolTimKiem_Click(object sender, EventArgs e)
        {
            frmTimPhieuThu Tim = new frmTimPhieuThu();
            Point p = PointToScreen(toolTimKiem.Bounds.Location);
            p.X += toolTimKiem.Width;
            p.Y += toolTimKiem.Height;
            Tim.Location = p;
            Tim.ShowDialog();
            if (Tim.DialogResult == DialogResult.OK)
            {
                ctrl.TimPhieuThanhToan(bindingNavigator, dataGridView,cmbNV, cmbKhachHang, txtMaPhieu, dtNgayThanhToan, numTongTien, txtGhiChu,
                    Tim.cmbKhachHang.SelectedValue.ToString(), Tim.dtNgayThu.Value.Date);
            }
        }

    }
}