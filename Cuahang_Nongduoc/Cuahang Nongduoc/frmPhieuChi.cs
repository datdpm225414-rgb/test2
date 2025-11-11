using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;
using CuahangNongduoc.Strategy;

namespace CuahangNongduoc
{
    public partial class frmPhieuChi : Form
    {
        LyDoChiController ctrlLyDo = new LyDoChiController();
        PhieuChiController ctrl = new PhieuChiController();
        NguoiDungController ctrlND = new NguoiDungController();
        public frmPhieuChi()
        {
            InitializeComponent();
        }
        //Co sua

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            ctrlLyDo.HienthiAutoComboBox(cmbLyDoChi);
            ctrlLyDo.HienthiDataGridviewComboBox(colLyDoChi);
            ctrlND.HienthiAutoComboBox(cmbNV);
            ctrlND.HienthiNguoiDungDataGridviewComboBox(colNguoiDung);
            ctrl.HienthiPhieuChi(bindingNavigator, dataGridView,cmbNV, cmbLyDoChi, txtMaPhieu, dtNgayChi, numTongTien, txtGhiChu);
        }

        //Co sua
        private void toolAdd_Click(object sender, EventArgs e)
        {   
            MessageBox.Show("Hãy nhập dữ liệu rồi ấn lưu","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            long maphieu = ThamSo.PhieuChi;
            cmbLyDoChi.Text = "";
            numTongTien.Value = 0;
            txtGhiChu.Text = "";
            txtMaPhieu.Text = (maphieu+1).ToString();
            DataTable dt = ctrlND.LayNguoiDungTheoTDN(ThamSo.Session.TenDangNhap);
            if(dt.Rows.Count > 0)
            {
                cmbNV.SelectedValue = Convert.ToInt64(dt.Rows[0]["ID"]);
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn xóa phiếu chi này không?", "Phieu Chi",   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        //Co sua
        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn xóa phiếu chi này không?", "Phieu Chi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Xoá phiếu bán ( cập nhật trạng thái)
                if (dataGridView.SelectedRows.Count > 0)
                {
                    try
                    {

                        var policy = new XoaMem();

                        DataGridViewRow row = dataGridView.SelectedRows[0];
                    string id = row.Cells["colMaPhieu"].Value.ToString();
                    ThamSo.Delete(id, "PHIEU_CHI",policy);
                    
                        MessageBox.Show("Xóa thành công!", "Phieu chi ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        //Co sua
        private void toolSave_Click(object sender, EventArgs e)
        {
            ThamSo.PhieuChi = Convert.ToInt64(txtMaPhieu.Text);
            DataRow row = ctrl.NewRow();
            row["ID"] = txtMaPhieu.Text;
            row["NGAY_CHI"] = dtNgayChi.Value.Date;
            row["TONG_TIEN"] = numTongTien.Value;
            row["GHI_CHU"] = txtGhiChu.Text;
            row["ID_LY_DO_CHI"] = cmbLyDoChi.SelectedValue;
            row["ID_NGUOI_DUNG"] = cmbNV.SelectedValue; 
            ctrl.Add(row);
            bindingNavigator.BindingSource.MoveLast();
            txtMaPhieu.Focus();
            bindingNavigator.BindingSource.MoveNext();
            ctrl.Save();
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
                PhieuChiController ctrlChi = new PhieuChiController();
                String ma_phieu = row["ID"].ToString();
                CuahangNongduoc.BusinessObject.PhieuChi ph = ctrlChi.LayPhieuChi(ma_phieu);
                frmInPhieuChi InPhieu = new frmInPhieuChi(ph);
                InPhieu.Show();
            }
        }

        private void btnThemLyDoChi_Click(object sender, EventArgs e)
        {
            frmLyDoChi Chi = new frmLyDoChi();
            Chi.ShowDialog();
            ctrlLyDo.HienthiAutoComboBox(cmbLyDoChi);
        }

        private void toolTimKiem_Click(object sender, EventArgs e)
        {
            frmTimPhieuChi Tim = new frmTimPhieuChi();
            Point p = PointToScreen(toolTimKiem.Bounds.Location);
            p.X += toolTimKiem.Width;
            p.Y += toolTimKiem.Height;
            Tim.Location = p;
            Tim.ShowDialog();
            if (Tim.DialogResult == DialogResult.OK)
            {
                ctrl.TimPhieuChi(bindingNavigator, dataGridView, cmbLyDoChi, txtMaPhieu, dtNgayChi, numTongTien, txtGhiChu, Convert.ToInt32(Tim.cmbLyDo.SelectedValue), dtNgayChi.Value.Date);
                
            }
        }


    }
}