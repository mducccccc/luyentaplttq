namespace mautest
{
    partial class frmBaoCao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnXuatExcel = new System.Windows.Forms.ToolStripButton();
            this.btnXuatPdf = new System.Windows.Forms.ToolStripButton();
            this.btnInBaoCao = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDong = new System.Windows.Forms.ToolStripButton();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelPaper = new System.Windows.Forms.Panel();
            this.lblFooterTrang = new System.Windows.Forms.Label();
            this.lblNguoiLapKy = new System.Windows.Forms.Label();
            this.lblNguoiLapTieuDe = new System.Windows.Forms.Label();
            this.pnlTableContainer = new System.Windows.Forms.Panel();
            this.lblDieuKienLoc = new System.Windows.Forms.Label();
            this.lblTieuDeBaoCao = new System.Windows.Forms.Label();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblTenCongTy = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.panelPaper.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnXuatExcel,
            this.btnXuatPdf,
            this.btnInBaoCao,
            this.toolStripSeparator1,
            this.btnDong});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 4, 1, 4);
            this.toolStrip1.Size = new System.Drawing.Size(1020, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(89, 22);
            this.btnXuatExcel.Text = "Xuất &Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXuatPdf
            // 
            this.btnXuatPdf.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXuatPdf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnXuatPdf.Name = "btnXuatPdf";
            this.btnXuatPdf.Size = new System.Drawing.Size(80, 22);
            this.btnXuatPdf.Text = "Xuất &PDF";
            this.btnXuatPdf.Click += new System.EventHandler(this.btnXuatPdf_Click);
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(88, 22);
            this.btnInBaoCao.Text = "&In / Xem trước";
            this.btnInBaoCao.Click += new System.EventHandler(this.btnInBaoCao_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDong
            // 
            this.btnDong.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(46, 22);
            this.btnDong.Text = "Đó&ng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.AutoScroll = true;
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.panelContainer.Controls.Add(this.panelPaper);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 33);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Padding = new System.Windows.Forms.Padding(20);
            this.panelContainer.Size = new System.Drawing.Size(1020, 687);
            this.panelContainer.TabIndex = 1;
            // 
            // panelPaper
            // 
            this.panelPaper.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelPaper.BackColor = System.Drawing.Color.White;
            this.panelPaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPaper.Controls.Add(this.lblFooterTrang);
            this.panelPaper.Controls.Add(this.lblNguoiLapKy);
            this.panelPaper.Controls.Add(this.lblNguoiLapTieuDe);
            this.panelPaper.Controls.Add(this.pnlTableContainer);
            this.panelPaper.Controls.Add(this.lblDieuKienLoc);
            this.panelPaper.Controls.Add(this.lblTieuDeBaoCao);
            this.panelPaper.Controls.Add(this.lblNgayLap);
            this.panelPaper.Controls.Add(this.lblTenCongTy);
            this.panelPaper.Location = new System.Drawing.Point(40, 15);
            this.panelPaper.Name = "panelPaper";
            this.panelPaper.Size = new System.Drawing.Size(920, 645);
            this.panelPaper.TabIndex = 0;
            // 
            // lblFooterTrang
            // 
            this.lblFooterTrang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFooterTrang.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFooterTrang.Location = new System.Drawing.Point(30, 610);
            this.lblFooterTrang.Name = "lblFooterTrang";
            this.lblFooterTrang.Size = new System.Drawing.Size(860, 22);
            this.lblFooterTrang.TabIndex = 7;
            this.lblFooterTrang.Text = "Trang 1/1";
            this.lblFooterTrang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNguoiLapKy
            // 
            this.lblNguoiLapKy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNguoiLapKy.AutoSize = true;
            this.lblNguoiLapKy.Font = new System.Drawing.Font("Times New Roman", 11.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNguoiLapKy.Location = new System.Drawing.Point(710, 560);
            this.lblNguoiLapKy.Name = "lblNguoiLapKy";
            this.lblNguoiLapKy.Size = new System.Drawing.Size(126, 19);
            this.lblNguoiLapKy.TabIndex = 6;
            this.lblNguoiLapKy.Text = "(Ký, ghi rõ họ tên)";
            // 
            // lblNguoiLapTieuDe
            // 
            this.lblNguoiLapTieuDe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNguoiLapTieuDe.AutoSize = true;
            this.lblNguoiLapTieuDe.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNguoiLapTieuDe.Location = new System.Drawing.Point(720, 535);
            this.lblNguoiLapTieuDe.Name = "lblNguoiLapTieuDe";
            this.lblNguoiLapTieuDe.Size = new System.Drawing.Size(107, 19);
            this.lblNguoiLapTieuDe.TabIndex = 5;
            this.lblNguoiLapTieuDe.Text = "Người lập biểu";
            // 
            // pnlTableContainer
            // 
            this.pnlTableContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTableContainer.AutoScroll = true;
            this.pnlTableContainer.Location = new System.Drawing.Point(30, 150);
            this.pnlTableContainer.Name = "pnlTableContainer";
            this.pnlTableContainer.Size = new System.Drawing.Size(860, 360);
            this.pnlTableContainer.TabIndex = 4;
            // 
            // lblDieuKienLoc
            // 
            this.lblDieuKienLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDieuKienLoc.Font = new System.Drawing.Font("Times New Roman", 11.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDieuKienLoc.Location = new System.Drawing.Point(30, 105);
            this.lblDieuKienLoc.Name = "lblDieuKienLoc";
            this.lblDieuKienLoc.Size = new System.Drawing.Size(860, 25);
            this.lblDieuKienLoc.TabIndex = 3;
            this.lblDieuKienLoc.Text = "Điều kiện lọc: Phòng ban: Tất cả; Giới tính: Nữ";
            this.lblDieuKienLoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTieuDeBaoCao
            // 
            this.lblTieuDeBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTieuDeBaoCao.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDeBaoCao.Location = new System.Drawing.Point(30, 68);
            this.lblTieuDeBaoCao.Name = "lblTieuDeBaoCao";
            this.lblTieuDeBaoCao.Size = new System.Drawing.Size(860, 32);
            this.lblTieuDeBaoCao.TabIndex = 2;
            this.lblTieuDeBaoCao.Text = "BẢNG LƯƠNG NHÂN VIÊN";
            this.lblTieuDeBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNgayLap.Font = new System.Drawing.Font("Times New Roman", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayLap.Location = new System.Drawing.Point(670, 25);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(220, 23);
            this.lblNgayLap.TabIndex = 1;
            this.lblNgayLap.Text = "Ngày lập: 28/12/2026";
            this.lblNgayLap.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTenCongTy
            // 
            this.lblTenCongTy.AutoSize = true;
            this.lblTenCongTy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenCongTy.Location = new System.Drawing.Point(30, 25);
            this.lblTenCongTy.Name = "lblTenCongTy";
            this.lblTenCongTy.Size = new System.Drawing.Size(209, 19);
            this.lblTenCongTy.TabIndex = 0;
            this.lblTenCongTy.Text = "CÔNG TY TNHH AN PHÁT";
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 720);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo cáo bảng lương nhân viên";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelContainer.ResumeLayout(false);
            this.panelPaper.ResumeLayout(false);
            this.panelPaper.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnXuatExcel;
        private System.Windows.Forms.ToolStripButton btnXuatPdf;
        private System.Windows.Forms.ToolStripButton btnInBaoCao;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDong;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelPaper;
        private System.Windows.Forms.Label lblTenCongTy;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.Label lblTieuDeBaoCao;
        private System.Windows.Forms.Label lblDieuKienLoc;
        private System.Windows.Forms.Panel pnlTableContainer;
        private System.Windows.Forms.Label lblNguoiLapTieuDe;
        private System.Windows.Forms.Label lblNguoiLapKy;
        private System.Windows.Forms.Label lblFooterTrang;
    }
}
