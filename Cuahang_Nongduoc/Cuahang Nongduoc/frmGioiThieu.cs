using System;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmGioiThieu : Form
    {
        public frmGioiThieu()
        {
            InitializeComponent();
        }

        private void frmGioiThieu_Load(object sender, EventArgs e)
        {
            txtNoiDung.Text =
@"Phần mềm Quản lý Cửa hàng Nông Dược
Phiên bản: 1.0.0
Nhóm phát triển: Sinh viên CNTT – AGU

Giới thiệu:

Phần mềm Quản lý Cửa hàng Nông Dược được thiết kế nhằm hỗ trợ quản lý toàn diện các hoạt động kinh doanh nông dược, bao gồm:

Quản lý sản phẩm: thông tin chi tiết, tồn kho, hạn sử dụng, giá bán.

Quản lý khách hàng và đại lý: thông tin liên hệ, lịch sử mua hàng, quản lý nợ công.

Quản lý hóa đơn bán lẻ và bán sỉ: lập hóa đơn nhanh chóng, theo dõi doanh thu.

Quản lý nhập hàng và kho: theo dõi tồn kho, nhập xuất hàng, báo cáo tồn kho định kỳ.

Báo cáo tổng hợp: doanh thu, tồn kho, số lượng bán, nợ khách hàng, sản phẩm hết hạn.

Mục tiêu của phần mềm:

Giảm thời gian quản lý thủ công, tăng độ chính xác dữ liệu.

Nâng cao hiệu quả kinh doanh và theo dõi tài chính.

Hỗ trợ ra quyết định dựa trên các báo cáo chi tiết.

Tích hợp hướng phát triển nâng cao: bán hàng trực tuyến, phân quyền người dùng, AI & Analytics, điện toán đám mây.

Lợi ích:

Chủ cửa hàng có thể theo dõi tình hình kinh doanh mọi lúc mọi nơi.

Nhân viên thao tác nhanh, giảm sai sót trong quản lý dữ liệu.

Khách hàng và đại lý được phục vụ hiệu quả nhờ hệ thống quản lý hiện đại.";
            
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
