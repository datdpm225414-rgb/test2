using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Strategy
{
    public interface  Xoa
    {
        int Execute(DataService ds, string table, string id);
    }
}
