using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    public partial class frmNguoiDung : Form
    {
        NguoiDungController ctrl = new NguoiDungController();

        public frmNguoiDung()
        {
            InitializeComponent();
        }

        private void frmNguoiDung_Load(object sender, EventArgs e)
        {
            ctrl.HienthiDataGridview(dgvNguoiDung, bindingNavigator,
                 txtTenDangNhap, txtMatKhau, cmbVaiTro, txtTenNguoiDung, txtEmail, txtSDT, chkTrangThai);

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                dgvNguoiDung.EndEdit();
                bindingNavigator.BindingSource.EndEdit();

                DataTable dt = (DataTable)bindingNavigator.BindingSource.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        if (row["MAT_KHAU_HASH"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["MAT_KHAU_HASH"].ToString()))
                        {
                            string plainPassword = row["MAT_KHAU_HASH"].ToString();
                            row["MAT_KHAU_HASH"] = HashSHA256(plainPassword);
                        }
                    }
                }

                if (ctrl.Save())
                    MessageBox.Show("Đã lưu thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Không có thay đổi nào được lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ctrl.NewRow();

            row["TEN_DANG_NHAP"] = "";
            row["MAT_KHAU_HASH"] = "";
            row["VAI_TRO"] = "";
            row["TEN_NGUOI_DUNG"] = "";
            row["EMAIL"] = "";
            row["SO_DIEN_THOAI"] = "";
            row["TRANG_THAI"] = true;

            ctrl.Add(row);
            bindingNavigator.BindingSource.MoveLast();

            txtTenDangNhap.Focus();
        }

        private void toolTimMaNguoiDung_Click(object sender, EventArgs e)
        {
            toolTimMaNguoiDung.Checked = true;
            toolTimTenNguoiDung.Checked = false;
            toolTimSanPham.Text = "";

        }

        private void mnuTimTenNguoiDung_Click(object sender, EventArgs e)
        {
            toolTimMaNguoiDung.Checked = false;
            toolTimTenNguoiDung.Checked = true;
            toolTimSanPham.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Nguoi Dung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }
        private void toolTimNguoiDung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimNguoiDung();
            }
        }

        void TimNguoiDung()
        {
            if (toolTimMaNguoiDung.Checked == true)
            {
                ctrl.TimMaNguoiDung(toolTimSanPham.Text);
            }
            else
            {
                ctrl.TimTenNguoiDung(toolTimSanPham.Text);
            }
        }

        private void toolTimNguoiDung_Leave(object sender, EventArgs e)
        {
            TimNguoiDung();
        }

        private void toolTimNguoiDung_Enter(object sender, EventArgs e)
        {
            toolTimSanPham.Text = "";
            toolTimSanPham.ForeColor = Color.Black;
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

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
