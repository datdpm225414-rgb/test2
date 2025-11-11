using CuahangNongduoc.Strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmDaiLy : Form
    {
        CuahangNongduoc.Controller.KhachHangController ctrl = new CuahangNongduoc.Controller.KhachHangController();
        public frmDaiLy()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {

            ctrl.HienthiDaiLyDataGridview(dataGridView, bindingNavigator);
            Allow(true);
        }
        void Allow(bool allow) // Thêm mới hàm này 
        {
            dataGridView.Enabled = allow;
            bindingNavigatorDeleteItem.Enabled = allow;
            toolLuu.Enabled = !allow;
            bindingNavigatorAddNewItem.Enabled = allow;
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            ThamSo.NhaCungCap = masoTemp + 1;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["colHoTen"].Value == null || row.Cells["colHoTen"].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("Họ tên khách hàng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bindingNavigatorPositionItem.Focus();
                    return;
                }
                else
                {

                    bindingNavigatorPositionItem.Focus();
                    ctrl.Save();
                }
            }
            //bindingNavigatorPositionItem.Focus();
            //ctrl.Save();
        }
        long masoTemp;
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            Allow(false);
            long maso = ThamSo.KhachHang;
            masoTemp = maso;
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.AddNew();
            row["ID"] = maso;
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Dai Ly", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    try
                    {

                        var policy = new XoaMem();

                        DataGridViewRow row = dataGridView.SelectedRows[0];
                        string id = row.Cells["colID"].Value.ToString();
                        ThamSo.Delete(id, "Khach_Hang", policy);

                        MessageBox.Show("Xóa thành công!", "Khach hang ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmKhachHang_Load(sender, e);
                    }
                    catch
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }



                }
                //bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Dai Ly", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void toolTimHoTen_Click(object sender, EventArgs e)
        {
            toolTimDiaChi.Checked = !toolTimDiaChi.Checked;
            toolTimHoTen.Checked = !toolTimDiaChi.Checked;
            toolTimKhachHang.Text = "Tìm theo Họ tên";
            bindingNavigator.Focus();
        }

        private void toolTimDiaChi_Click(object sender, EventArgs e)
        {
            toolTimHoTen.Checked = !toolTimHoTen.Checked;
            toolTimDiaChi.Checked = !toolTimHoTen.Checked;
            toolTimKhachHang.Text = "Tìm theo Địa chỉ";
            bindingNavigator.Focus();
        }

        private void toolTimKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (toolTimHoTen.Checked)
                    ctrl.TimHoTen(toolTimKhachHang.Text, true);
                else
                    ctrl.TimDiaChi(toolTimKhachHang.Text, true);
            }
        }

        private void toolTimKhachHang_Leave(object sender, EventArgs e)
        {
            if (toolTimHoTen.Checked == true)
                toolTimKhachHang.Text = "Tìm theo Họ tên";
            else
                toolTimKhachHang.Text = "Tìm theo Địa chỉ";

            toolTimKhachHang.ForeColor = Color.FromArgb(224, 224, 224);
        }

        private void toolTimKhachHang_Enter(object sender, EventArgs e)
        {
            toolTimKhachHang.Text = "";
            toolTimKhachHang.ForeColor = Color.Black;
        }

        private void toolReload_Click(object sender, EventArgs e)
        {
            frmKhachHang_Load(sender, e);
        }
    }
}