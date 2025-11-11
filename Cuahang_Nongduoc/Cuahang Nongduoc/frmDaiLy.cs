using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // <<< Nhớ thêm dòng này
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmDaiLy : Form
    {
        CuahangNongduoc.Controller.KhachHangController ctrl = new CuahangNongduoc.Controller.KhachHangController();

        const string COL_ID = "ID";
        const string COL_TEN = "HO_TEN";
        const string COL_DIACHI = "DIA_CHI";
        const string COL_SDT = "DIEN_THOAI";

        public frmDaiLy()
        {
            InitializeComponent();
        }

        // <<< SỬ DỤNG TÊN HÀM THEO YÊU CẦU CỦA BẠN >>>
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            // Gọi hàm riêng cho Đại Lý (true)
            ctrl.HienthiDaiLyDataGridview(dataGridView, bindingNavigator);

            RestoreDataBindings();

            txtMaDL.ReadOnly = true; // <<< Sửa lại tên TextBox Mã của bạn

            try
            {
                ThamSo.KhachHang = ctrl.GetMaxKhachHangID() + 1;
            }
            catch (Exception)
            {
                ThamSo.KhachHang = 1;
            }
        }

        #region "Các hàm trợ giúp (Helper Methods)"

        private void RestoreDataBindings()
        {
            // <<< Sửa lại tên 4 TextBox của bạn >>>
            txtMaDL.DataBindings.Clear();
            txtTenDL.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtSDT.DataBindings.Clear();

            txtMaDL.DataBindings.Add("Text", bindingNavigator.BindingSource, COL_ID);
            txtTenDL.DataBindings.Add("Text", bindingNavigator.BindingSource, COL_TEN);
            txtDiaChi.DataBindings.Add("Text", bindingNavigator.BindingSource, COL_DIACHI);
            txtSDT.DataBindings.Add("Text", bindingNavigator.BindingSource, COL_SDT);
        }

        // Hàm Xóa (Bao gồm Reset ID và Bắt lỗi)
        private void XoaKhachHangHienTai()
        {
            try
            {
                bindingNavigator.BindingSource.RemoveCurrent();
                ctrl.Save();
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // RESET LẠI ID TỰ TĂNG
                try
                {
                    ThamSo.KhachHang = ctrl.GetMaxKhachHangID() + 1;
                }
                catch (Exception)
                {
                    ThamSo.KhachHang = 1;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show("Không thể xóa Đại lý này.\n\nLý do: Đại lý đã có phát sinh 'Phiếu Bán' hoặc 'Phiếu Thanh Toán'.", "Lỗi Ràng Buộc Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrl.HienthiDaiLyDataGridview(dataGridView, bindingNavigator);
                    RestoreDataBindings();
                }
                else
                {
                    MessageBox.Show("Lỗi SQL khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Các sự kiện Click"

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            long maso = ThamSo.KhachHang;
            ThamSo.KhachHang = maso + 1;

            DataRowView row = (DataRowView)bindingNavigator.BindingSource.AddNew();
            row[COL_ID] = maso;

            txtTenDL.Focus(); // <<< Sửa lại tên TextBox Họ Tên
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            // <<< Sửa lại tên TextBox Họ Tên >>>
            string hoTen = txtTenDL.Text;
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show("Vui lòng nhập Tên Đại lý!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDL.Focus();
                return;
            }

            try
            {
                bindingNavigator.BindingSource.EndEdit();
                ctrl.Save();
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn XÓA VĨNH VIỄN Đại lý này không?", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                XoaKhachHangHienTai();
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Dai Ly", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = true;
                XoaKhachHangHienTai();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region "Các sự kiện Tìm kiếm"

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
            toolTimDiaChi.Checked = !toolTimDiaChi.Checked;
            toolTimKhachHang.Text = "Tìm theo Địa chỉ";
            bindingNavigator.Focus();
        }

        private void toolTimKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (toolTimHoTen.Checked)
                    ctrl.TimHoTen(toolTimKhachHang.Text, true); // <<< Tham số 'true' cho Đại lý
                else
                    ctrl.TimDiaChi(toolTimKhachHang.Text, true); // <<< Tham số 'true' cho Đại lý
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

        #endregion
    }
}