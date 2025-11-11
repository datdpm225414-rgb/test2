using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Strategy
{
    public class XoaMem : Xoa
    {
        public int Execute(DataService ds, string table, string id)
        {

            string sql = $"UPDATE {table} SET TRANG_THAI = 0 WHERE ID = @ID";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@ID", id);
            return ds.ExecuteNoneQuery(cmd);
        }
    }
    
}
