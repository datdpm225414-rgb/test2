using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;
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
    public partial class frmSoLuongBan : Form
    {
        public frmSoLuongBan()
        {
            InitializeComponent();
            //reportViewer.LocalReport.ExecuteReportInCurrentAppDomain(System.Reflection.Assembly.GetExecutingAssembly().Evidence);
        }

        private void frmSoLuongBan_Load(object sender, EventArgs e)
        {
            cmbThang.SelectedIndex = DateTime.Now.Month - 1;
            numNam.Value = DateTime.Now.Year;
        }

        ChiTietPhieuBanController ctrl = new ChiTietPhieuBanController();


        private void btnXemNgay_Click(object sender, EventArgs e)
        {
            IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("ngay", "Ngày: " + dtNgay.Value.Date.ToString("dd/MM/yyyy")));

            this.reportViewer.LocalReport.SetParameters(param);

            var data = ctrl.ChiTietPhieuBan(dtNgay.Value.Date)
                .Select(r => new
                {
                    MaSanPham = r.MaSanPham.Id,
                    r.DonGia,
                    r.SoLuong,
                    r.ThanhTien
                }); 
            // Design pattern DTO
            this.ChiTietPhieuBanBindingSource.DataSource = ctrl.ChiTietPhieuBanDTO(dtNgay.Value.Date);

            //Problem 
            //reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            //reportViewer.ZoomMode = ZoomMode.Percent;
            //reportViewer.ZoomPercent = 100;
            //this.reportViewer.RefreshReport();

            //Sololution: Extract Method
            SetUpReport();
        }

        private void btnXemThang_Click(object sender, EventArgs e)
        {
            IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("ngay",
                "Tháng: " + Convert.ToString(cmbThang.SelectedIndex + 1) + "/" + numNam.Value.ToString()));

            this.reportViewer.LocalReport.SetParameters(param);

            var data = ctrl.ChiTietPhieuBan(cmbThang.SelectedIndex + 1, Convert.ToInt32(numNam.Value))
                .Select(r => new
                {
                    MaSanPham = r.MaSanPham.Id,
                    r.DonGia,
                    r.SoLuong,
                    r.ThanhTien
                });
            this.ChiTietPhieuBanBindingSource.DataSource = data;

            //Problem 
            //reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            //reportViewer.ZoomMode = ZoomMode.Percent;
            //reportViewer.ZoomPercent = 100;
            //this.reportViewer.RefreshReport();
            
            //Sololution: Extract Method
            SetUpReport();
        }

        public void SetUpReport()
        {
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;
            this.reportViewer.RefreshReport();
        }
    }
}