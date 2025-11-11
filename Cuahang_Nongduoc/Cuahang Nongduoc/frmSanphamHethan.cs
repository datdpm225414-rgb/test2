using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmSanphamHethan : Form
    {
        public frmSanphamHethan()
        {
            InitializeComponent();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            IList<CuahangNongduoc.BusinessObject.MaSanPham> data = CuahangNongduoc.Controller.MaSanPhamController.LayMaSanPhamHetHan(dt.Value.Date);
            IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();

            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("ngay_tinh", dt.Value.Date.ToString("dd/MM/yyyy")));
            var spHetHan = data.Select(r => new
            {
                r.Id,
                SanPham = r.SanPham.TenSanPham,
                r.GiaNhap,
                r.NgayNhap,
                r.SoLuong,
                r.NgaySanXuat,
                r.NgayHetHan,
            });
            this.MaSanPhamBindingSource.DataSource = spHetHan;
            this.reportViewer.LocalReport.SetParameters(param);

            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;
            this.reportViewer.RefreshReport();
        }

        private void frmSanphamHethan_Load(object sender, EventArgs e)
        {

        }
    }
}