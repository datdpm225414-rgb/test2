namespace CuahangNongduoc
{
    partial class frmGioiThieu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.btnDong = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblTieuDe.Location = new System.Drawing.Point(80, 20);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(400, 30);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "GIỚI THIỆU PHẦN MỀM NÔNG DƯỢC";

            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNoiDung.Location = new System.Drawing.Point(10, 60);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.ReadOnly = true;
            this.txtNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoiDung.Size = new System.Drawing.Size(580, 320);
            this.txtNoiDung.TabIndex = 1;
            this.txtNoiDung.Text =
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

            // 
            // btnDong
            // 
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDong.Location = new System.Drawing.Point(240, 390);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 35);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            // 
            // frmGioiThieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 440);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "frmGioiThieu";
            this.Text = "Giới thiệu phần mềm";
            this.Load += new System.EventHandler(this.frmGioiThieu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
