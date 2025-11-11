using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;
using CuahangNongduoc.DataSet;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmInDichVu : Form
    {
        CHNDDataSet.PhieuBanDataTable phieuBanDataTable = new CHNDDataSet.PhieuBanDataTable();
        public frmInDichVu()
        {
            InitializeComponent();
        }
        string reportFolder = Application.StartupPath.Replace("\\bin\\Debug", "\\Report");

        PhieuBanController ctrl = new PhieuBanController();
        private void btnXemNgay_Click(object sender, EventArgs e)
        {
            var data = ctrl.LayPhieuBan(dtpTuNgay.Value, dtpDenNgay.Value)
                .Select(r => new
                {
                    r.Id,
                    KhachHang = r.KhachHang.HoTen,
                    r.NgayBan,
                    r.GiamGia,
                    r.PhiDichVu,
                    TongTienCuoi = r.TongTien + r.PhiDichVu +r.PhiVanChuyen - r.GiamGia,
                    r.TongTien,
                    r.DaTra,
                    r.ConNo,
                    r.PhiVanChuyen
                }).ToList();

            phieuBanDataTable.Clear();
            foreach (var row in data)
            {
                phieuBanDataTable.AddPhieuBanRow(row.Id,
                    row.KhachHang,
                    row.NgayBan,
                    row.TongTienCuoi,
                    row.DaTra,
                    row.ConNo,
                    row.PhiDichVu,
                    row.GiamGia,
                    row.TongTien, 
                    row.PhiVanChuyen
                );
            }
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "dsPhieuBan";
            reportDataSource.Value = phieuBanDataTable;

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            reportViewer.LocalReport.ReportPath = Path.Combine(reportFolder, "rptInDichVu.rdlc");

            ReportParameter reportParameter = new ReportParameter("Ngay", "Từ ngày: " + dtpTuNgay.Text + " - Đến ngày: " + dtpDenNgay.Text);
            reportViewer.LocalReport.SetParameters(reportParameter);

            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;
            this.reportViewer.RefreshReport();
        }

        private void frmInDichVu_Load(object sender, EventArgs e)
        {

        }
    }
}
