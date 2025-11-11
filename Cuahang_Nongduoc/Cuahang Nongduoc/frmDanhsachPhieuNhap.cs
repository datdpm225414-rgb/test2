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
    public partial class frmDanhsachPhieuNhap : Form
    {
        public frmDanhsachPhieuNhap()
        {
            InitializeComponent();
        }

        PhieuNhapController ctrl = new PhieuNhapController();
        NhaCungCapController ctrlNCC = new NhaCungCapController();
        NguoiDungController ctrlND = new NguoiDungController(); // Them
        //Co sua

        private void frmDanhsachPhieuNhap_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            ctrlNCC.HienthiDataGridviewComboBox(colNhaCungCap);
            ctrlND.HienthiNguoiDungDataGridviewComboBox(colNguoiDung); //Người lập
            ctrl.HienthiPhieuNhap(bindingNavigator, dataGridView);
        }
        frmNhapHang NhapHang = null;
        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (NhapHang == null || NhapHang.IsDisposed)
            {
                NhapHang = new frmNhapHang(ctrl);
                NhapHang.Show();
            }
            else
                NhapHang.Activate();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (NhapHang == null || NhapHang.IsDisposed)
            {
                NhapHang = new frmNhapHang();
                NhapHang.Show();
            }
            else
                NhapHang.Activate();
        }

        private void toolIn_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;
            if (row != null)
            {
                PhieuNhapController ctrlPN = new PhieuNhapController();
                String ma_phieu = row["ID"].ToString();
                CuahangNongduoc.BusinessObject.PhieuNhap ph =  ctrlPN.LayPhieuNhap(ma_phieu);
                frmInPhieuNhap PhieuNhap = new frmInPhieuNhap(ph);
                PhieuNhap.Show();
            }
        }
        //Co sua

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Nhap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var policy = new XoaMem();

                    DataGridViewRow row = dataGridView.SelectedRows[0];
                    string id = row.Cells["colId"].Value.ToString();
                    if (ThamSo.Delete(id, "PHIEU_NHAP", policy))
                    {
                        MessageBox.Show("Xóa thành công!", "Phieu Ban Nhap", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmDanhsachPhieuNhap_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                    //bindingNavigator.BindingSource.RemoveCurrent();

                }//ctrl.Save();
                }
        }

        private void toolTimKiem_Click(object sender, EventArgs e)
        {
            
            frmTimPhieuNhap TimPhieu = new frmTimPhieuNhap();
            Point p = PointToScreen(toolTimKiem.Bounds.Location);
            p.X += toolTimKiem.Width;
            p.Y += toolTimKiem.Height;
            TimPhieu.Location = p;
            TimPhieu.ShowDialog();
            if (TimPhieu.DialogResult == DialogResult.OK)
            {
                ctrl.TimPhieuNhap(TimPhieu.cmbNCC.SelectedValue.ToString(), TimPhieu.dtNgayNhap.Value.Date);
            }
        }
    }
}