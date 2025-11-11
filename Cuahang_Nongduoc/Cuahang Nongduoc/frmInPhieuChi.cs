using CuahangNongduoc.BusinessObject;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmInPhieuChi : Form
    {
        CuahangNongduoc.BusinessObject.PhieuChi m_PhieuChi;
        public frmInPhieuChi(CuahangNongduoc.BusinessObject.PhieuChi ph)
        {
            InitializeComponent();
            m_PhieuChi = ph;
        }

        private void frmInPhieuChi_Load(object sender, EventArgs e)
        {
            Num2Str num = new Num2Str();
            IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            CuahangNongduoc.BusinessObject.CuaHang ch = ThamSo.LayCuaHang();
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("ten_cua_hang", ch.TenCuaHang));
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("dia_chi", ch.DiaChi));
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("dien_thoai", ch.DienThoai));

            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("bang_chu", num.NumberToString(m_PhieuChi.TongTien.ToString())));

            var data = new
            {
                m_PhieuChi.Id,
                m_PhieuChi.NgayChi,
                m_PhieuChi.GhiChu,
                m_PhieuChi.TongTien,
                LyDoChi = m_PhieuChi.LyDoChi.LyDo,
                NguoiDung = m_PhieuChi.NgDung.TenNguoiDung
            };

            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("nhan_vien", data.NguoiDung));
            this.reportViewer.LocalReport.SetParameters(param);
            this.PhieuChiBindingSource.DataSource = data;

            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;
            this.reportViewer.RefreshReport();
        }
    }
}