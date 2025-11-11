using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc.Controller
{
    public class DichVuController
    {
        DichVuFactory factory = new DichVuFactory();
        BindingSource bs = new BindingSource();

        public DichVuController()
        {
            bs.DataSource = factory.LayDsDichVu();
        }

        public void HienthiAutoComboBox(ComboBox cmb)
        {
            DataTable dt = factory.LayDsDichVu();


            DataRow dr = dt.NewRow();
            dr["ID"] = 0; 
            dr["TEN_DICH_VU"] = "(Không chọn)";
            dr["GIA_MAC_DINH"] = 0;
            dt.Rows.InsertAt(dr, 0);

            cmb.DataSource = dt;
            cmb.DisplayMember = "TEN_DICH_VU";
            cmb.ValueMember = "ID";
            //cmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cmb.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb.SelectedIndex = 0;
        }

        public void HienThi(BindingNavigator bn, DataGridView dg)
        {
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        public DataRow NewRow()
        {
            return factory.NewRow();
        }

        public void Add(DataRow row)
        {
            factory.Add(row);
        }

        public bool Save()
        {
            return factory.Save();
        }
        public void HienthiDichVuDataGridviewComboBox(DataGridViewComboBoxColumn cmb)
        {
           
            cmb.DataSource = factory.LayDsDichVu();
            cmb.DisplayMember = "TEN_DICH_VU";
            cmb.ValueMember = "ID";
            cmb.DataPropertyName = "ID_DICH_VU"; 
            cmb.FlatStyle = FlatStyle.Flat; 
        }
    }
}