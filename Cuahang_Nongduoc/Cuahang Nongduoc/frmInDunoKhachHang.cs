using CuahangNongduoc.BusinessObject;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmInDunoKhachHang : Form
    {
        CuahangNongduoc.BusinessObject.DuNoKhachHang m_DuNo;
        IList<DuNoKhachHang> dNKHData;
        string reportFolder = Application.StartupPath.Replace("\\bin\\Debug", "\\Report");
        public frmInDunoKhachHang(CuahangNongduoc.BusinessObject.DuNoKhachHang dn)
        {
            InitializeComponent();
            m_DuNo = dn;
        }
        public frmInDunoKhachHang(IList<DuNoKhachHang> dn)
        {
            InitializeComponent();
            dNKHData = dn;
        }

        private void frmInDunoKhachHang_Load(object sender, EventArgs e)
        {
            if (m_DuNo != null)
            {
                Num2Str num = new Num2Str();
                IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                CuahangNongduoc.BusinessObject.CuaHang ch = ThamSo.LayCuaHang();
                param.Add(new Microsoft.Reporting.WinForms.ReportParameter("ten_cua_hang", ch.TenCuaHang));
                param.Add(new Microsoft.Reporting.WinForms.ReportParameter("dia_chi", ch.DiaChi));
                param.Add(new Microsoft.Reporting.WinForms.ReportParameter("dien_thoai", ch.DienThoai));

                param.Add(new Microsoft.Reporting.WinForms.ReportParameter("bang_chu", num.NumberToString(m_DuNo.CuoiKy.ToString())));
                var data = new
                {
                    KhachHang = m_DuNo.KhachHang.HoTen,
                    DiaChi = m_DuNo.KhachHang.DiaChi,
                    DienThoai = m_DuNo.KhachHang.DienThoai,
                    m_DuNo.Thang,
                    m_DuNo.Nam,
                    m_DuNo.PhatSinh,
                    m_DuNo.DaTra,
                    m_DuNo.DauKy,
                    m_DuNo.CuoiKy
                };
                param.Add(new Microsoft.Reporting.WinForms.ReportParameter("dienthoai_kh", data.DienThoai));
                param.Add(new Microsoft.Reporting.WinForms.ReportParameter("diachi_kh", data.DiaChi));

                this.reportViewer.LocalReport.SetParameters(param);
                this.DuNoKhachHangBindingSource.DataSource = data;

            }
            else
            {
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "CuahangNongduoc_BusinessObject_DuNoKhachHang";
                reportDataSource.Value = dNKHData.Select(r => new
                {
                    KhachHang = r.KhachHang.HoTen,
                    r.Thang,
                    r.Nam,
                    r.DauKy,
                    r.CuoiKy,
                    r.DaTra,
                    r.PhatSinh
                });

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                reportViewer.LocalReport.ReportPath = Path.Combine(reportFolder, "rptDsDuNoKhachHang.rdlc");

                ReportParameter reportParameter = new ReportParameter("ngay", "Ngày: "+DateTime.Now.ToString("dd/MM/yyyy"));
                reportViewer.LocalReport.SetParameters(reportParameter);
            }
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;
            this.reportViewer.RefreshReport();
        }
    }
}