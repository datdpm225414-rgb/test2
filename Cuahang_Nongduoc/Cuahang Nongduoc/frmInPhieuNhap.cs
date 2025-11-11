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
    public partial class frmInPhieuNhap : Form
    {
        CuahangNongduoc.BusinessObject.PhieuNhap m_PhieuNhap = null;
        public frmInPhieuNhap(CuahangNongduoc.BusinessObject.PhieuNhap ph)
        {
            m_PhieuNhap = ph;
            InitializeComponent();

            //reportViewer.LocalReport.ExecuteReportInCurrentAppDomain(System.Reflection.Assembly.GetExecutingAssembly().Evidence);
            this.reportViewer.LocalReport.SubreportProcessing += new Microsoft.Reporting.WinForms.SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
        }

        void LocalReport_SubreportProcessing(object sender, Microsoft.Reporting.WinForms.SubreportProcessingEventArgs e)
        {
            //e.DataSources.Clear();
            //e.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("CuahangNongduoc_BusinessObject_MaSanPham", m_PhieuNhap.ChiTiet));
            var chiTietPN = m_PhieuNhap.ChiTiet.Select(r => new
            {
                Id = r.Id,
                SanPham = r.SanPham.TenSanPham,
                GiaNhap = r.GiaNhap,
                SoLuong = r.SoLuong,
                ThanhTien = r.ThanhTien,
                NgaySanXuat = r.NgaySanXuat,
                NgayHetHan = r.NgayHetHan,
            }).ToList();

            e.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("CuahangNongduoc_BusinessObject_MaSanPham", chiTietPN));
        }

        private void frmInPhieuNhap_Load(object sender, EventArgs e)
        {
            Num2Str num = new Num2Str();
            IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            CuahangNongduoc.BusinessObject.CuaHang ch = ThamSo.LayCuaHang();
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("ten_cua_hang", ch.TenCuaHang));
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("dia_chi", ch.DiaChi));
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("dien_thoai", ch.DienThoai));

            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("bang_chu", num.NumberToString(m_PhieuNhap.TongTien.ToString())));

            var data = new
            {
                m_PhieuNhap.Id,
                m_PhieuNhap.NgayNhap,
                m_PhieuNhap.TongTien,
                m_PhieuNhap.DaTra,
                m_PhieuNhap.ConNo,
                NhaCungCap = m_PhieuNhap.NhaCungCap.HoTen,
                NguoiDung = m_PhieuNhap.NgDung.TenNguoiDung
            };

            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("nhan_vien", data.NguoiDung));
            this.reportViewer.LocalReport.SetParameters(param);
            this.PhieuNhapBindingSource.DataSource = data;

            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;
            this.reportViewer.RefreshReport();
        }
    }
}