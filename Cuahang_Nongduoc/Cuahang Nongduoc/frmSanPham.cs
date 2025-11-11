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
    public partial class frmSanPham : Form
    {
        SanPhamController ctrl = new SanPhamController();
        DonViTinhController ctrlDVT = new DonViTinhController();

        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            ctrlDVT.HienthiAutoComboBox(cmbDVT);
            dataGridView.Columns.Add(ctrlDVT.HienthiDataGridViewComboBoxColumn());
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator,
                 txtMaSanPham, txtTenSanPham, cmbDVT, numDonGiaNhap, numGiaBanSi, numGiaBanLe);
        }


        private void toolLuu_Click(object sender, EventArgs e)
        {
            txtMaSanPham.BringToFront();
            DataRow row = ctrl.NewRow();
            long maso = ThamSo.SanPham;
            ThamSo.SanPham = maso + 1;
            row["ID"] = maso;
            row["TEN_SAN_PHAM"] = txtTenSanPham.Text.Trim();
            row["SO_LUONG"] = 0;
            row["DON_GIA_NHAP"] = numDonGiaNhap.Value;
            row["GIA_BAN_SI"] = numGiaBanSi.Value;
            row["GIA_BAN_LE"] = numGiaBanLe.Value;
            row["ID_DON_VI_TINH"] = cmbDVT.SelectedValue;
            row["GIA_BINH_QUAN"] = 0;
            ctrl.Add(row);
            bindingNavigator.BindingSource.MoveLast();
            bindingNavigatorPositionItem.Focus();
            ctrl.Save();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            txtMaGia.BringToFront();
            txtMaGia.Text = ThamSo.SanPham.ToString();
            numDonGiaNhap.Value = 0;
            numGiaBanSi.Value = 0;
            numGiaBanLe.Value = 0;
            cmbDVT.SelectedIndex = 0;
        }


        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "San Pham", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;

        }

        private void btnThemDVT_Click(object sender, EventArgs e)
        {
            frmDonViTinh DVT = new frmDonViTinh();
            DVT.ShowDialog();
            ctrlDVT.HienthiAutoComboBox(cmbDVT);
        }


        private void toolTimMaSanPham_Click(object sender, EventArgs e)
        {
            toolTimMaSanPham.Checked = true;
            toolTimTenSanPham.Checked = false;
            toolTimSanPham.Text = "";

        }

        private void mnuTimTenSanPham_Click(object sender, EventArgs e)
        {
            toolTimMaSanPham.Checked = false;
            toolTimTenSanPham.Checked = true;
            toolTimSanPham.Text = "";
        }

        private void toolTimSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimSanPham();
            }
        }

        private void toolTimSanPham_Leave(object sender, EventArgs e)
        {
            TimSanPham();
        }

        void TimSanPham()
        {
            if (toolTimMaSanPham.Checked == true)
            {
                ctrl.TimMaSanPham(toolTimSanPham.Text);
            }
            else
            {
                ctrl.TimTenSanPham(toolTimSanPham.Text);
            }
        }

        private void toolTimSanPham_Enter(object sender, EventArgs e)
        {
            toolTimSanPham.Text = "";
            toolTimSanPham.ForeColor = Color.Black;
        }

  
    }
}