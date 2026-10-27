using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mautest
{
    public partial class frmBaoCao : Form
    {
        private DataTable _dtData;
        private string _dieuKienLoc;
        private DataGridView _dgvReport;
        private double _tongLuong = 0;

        public frmBaoCao(DataTable dtData, string dieuKienLoc)
        {
            InitializeComponent();
            _dtData = dtData != null ? dtData.Copy() : new DataTable();
            _dieuKienLoc = dieuKienLoc;
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            lblTenCongTy.Text = "CÔNG TY TNHH AN PHÁT";
            lblNgayLap.Text = "Ngày lập: " + DateTime.Now.ToString("dd/MM/yyyy");
            lblTieuDeBaoCao.Text = "BẢNG LƯƠNG NHÂN VIÊN";
            lblDieuKienLoc.Text = "Điều kiện lọc: " + _dieuKienLoc;

            HienThiBaoCaoTrenGiaoDien();
        }

        private void HienThiBaoCaoTrenGiaoDien()
        {
            pnlTableContainer.Controls.Clear();

            _dgvReport = new DataGridView();
            _dgvReport.Dock = DockStyle.Fill;
            _dgvReport.BackgroundColor = Color.White;
            _dgvReport.BorderStyle = BorderStyle.FixedSingle;
            _dgvReport.AllowUserToAddRows = false;
            _dgvReport.AllowUserToDeleteRows = false;
            _dgvReport.ReadOnly = true;
            _dgvReport.RowHeadersVisible = false;
            _dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvReport.Font = new Font("Times New Roman", 10.5f, FontStyle.Regular);
            _dgvReport.GridColor = Color.Black;
            _dgvReport.EnableHeadersVisualStyles = false;
            _dgvReport.ColumnHeadersHeight = 35;
            _dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(217, 225, 242), 
                ForeColor = Color.Black,
                Font = new Font("Times New Roman", 11f, FontStyle.Bold)
            };
            _dgvReport.ColumnHeadersDefaultCellStyle = headerStyle;

            var colSTT = new DataGridViewTextBoxColumn { Name = "colSTT", HeaderText = "STT", Width = 65 };
            colSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var colMaNV = new DataGridViewTextBoxColumn { Name = "colMaNV", HeaderText = "Mã NV", Width = 95 };
            colMaNV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var colHoTen = new DataGridViewTextBoxColumn { Name = "colHoTen", HeaderText = "Họ tên", Width = 210, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill };
            colHoTen.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            var colNgaySinh = new DataGridViewTextBoxColumn { Name = "colNgaySinh", HeaderText = "Ngày sinh", Width = 135 };
            colNgaySinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var colHeSoLuong = new DataGridViewTextBoxColumn { Name = "colHeSoLuong", HeaderText = "Hệ số lương", Width = 115 };
            colHeSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var colLuong = new DataGridViewTextBoxColumn { Name = "colLuong", HeaderText = "Lương", Width = 145 };
            colLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            _dgvReport.Columns.AddRange(new DataGridViewColumn[] {
                colSTT, colMaNV, colHoTen, colNgaySinh, colHeSoLuong, colLuong
            });

            var rowsList = _dtData.AsEnumerable().ToList();
            var grouped = rowsList
                .GroupBy(r => r["TenPB"] != DBNull.Value ? r["TenPB"].ToString() : "Khác")
                .OrderBy(g => g.Key);

            int stt = 1;
            _tongLuong = 0;

            foreach (var group in grouped)
            {
                int groupRowIdx = _dgvReport.Rows.Add();
                DataGridViewRow gRow = _dgvReport.Rows[groupRowIdx];
                gRow.Height = 30;
                gRow.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                gRow.DefaultCellStyle.Font = new Font("Times New Roman", 11f, FontStyle.Bold);

                gRow.Cells["colSTT"].Value = "";
                gRow.Cells["colMaNV"].Value = "";
                gRow.Cells["colHoTen"].Value = $"Phòng ban: {group.Key} ({group.Count()} bản ghi)";
                gRow.Cells["colNgaySinh"].Value = "";
                gRow.Cells["colHeSoLuong"].Value = "";
                gRow.Cells["colLuong"].Value = "";

                foreach (var row in group)
                {
                    int rIdx = _dgvReport.Rows.Add();
                    DataGridViewRow dRow = _dgvReport.Rows[rIdx];
                    dRow.Height = 28;

                    dRow.Cells["colSTT"].Value = stt++;
                    dRow.Cells["colMaNV"].Value = row["MaNV"];
                    dRow.Cells["colHoTen"].Value = row["HoTen"];

                    string ns = "";
                    if (row["NgaySinh"] != DBNull.Value)
                    {
                        ns = Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy");
                    }
                    dRow.Cells["colNgaySinh"].Value = ns;

                    double hsl = 0;
                    if (row["HeSoLuong"] != DBNull.Value)
                    {
                        hsl = Convert.ToDouble(row["HeSoLuong"]);
                    }
                    dRow.Cells["colHeSoLuong"].Value = hsl.ToString("0.##");

                    double luong = Math.Round(hsl * 2340000.0);
                    _tongLuong += luong;
                    dRow.Cells["colLuong"].Value = luong.ToString("#,##0");
                }
            }

            int totalRowIdx = _dgvReport.Rows.Add();
            DataGridViewRow totalRow = _dgvReport.Rows[totalRowIdx];
            totalRow.Height = 32;
            totalRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 204); 
            totalRow.DefaultCellStyle.Font = new Font("Times New Roman", 11f, FontStyle.Bold);

            totalRow.Cells["colSTT"].Value = "";
            totalRow.Cells["colMaNV"].Value = "TỔNG CỘNG";
            totalRow.Cells["colHoTen"].Value = "";
            totalRow.Cells["colNgaySinh"].Value = "";
            totalRow.Cells["colHeSoLuong"].Value = "";
            totalRow.Cells["colLuong"].Value = _tongLuong.ToString("#,##0");

            pnlTableContainer.Controls.Add(_dgvReport);

            int calculatedHeight = 250 + (_dgvReport.Rows.Count + 1) * 30 + 170;
            if (calculatedHeight < 645) calculatedHeight = 645;
            panelPaper.Height = calculatedHeight;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xls)|*.xls|HTML File (*.html)|*.html",
                    FileName = "BangLuongNhanVien_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls",
                    Title = "Lưu báo cáo Excel"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html><head><meta charset='utf-8'>");
                sb.AppendLine("<style>");
                sb.AppendLine("body { font-family: 'Times New Roman', serif; margin: 20px; }");
                sb.AppendLine(".company { font-weight: bold; font-size: 14pt; }");
                sb.AppendLine(".date { text-align: right; font-size: 11pt; }");
                sb.AppendLine(".title { text-align: center; font-weight: bold; font-size: 18pt; margin-top: 15px; }");
                sb.AppendLine(".filter { text-align: center; font-style: italic; font-size: 11pt; margin-bottom: 20px; }");
                sb.AppendLine("table { width: 100%; border-collapse: collapse; margin-top: 15px; }");
                sb.AppendLine("th, td { border: 1px solid #000; padding: 6px 8px; font-size: 11pt; }");
                sb.AppendLine("th { background-color: #D9E1F2; font-weight: bold; text-align: center; }");
                sb.AppendLine(".group-header { background-color: #F2F2F2; font-weight: bold; text-align: left; }");
                sb.AppendLine(".total-row { background-color: #FFF2CC; font-weight: bold; }");
                sb.AppendLine(".center { text-align: center; }");
                sb.AppendLine(".right { text-align: right; }");
                sb.AppendLine(".sign-title { font-weight: bold; text-align: right; margin-top: 30px; margin-right: 40px; font-size: 12pt; }");
                sb.AppendLine(".sign-sub { font-style: italic; text-align: right; margin-right: 25px; font-size: 11pt; }");
                sb.AppendLine("</style></head><body>");

                sb.AppendLine("<table style='border:none; width:100%; margin-bottom:10px;'>");
                sb.AppendLine("<tr><td style='border:none;' class='company'>CÔNG TY TNHH AN PHÁT</td>");
                sb.AppendLine($"<td style='border:none;' class='date'>Ngày lập: {DateTime.Now:dd/MM/yyyy}</td></tr>");
                sb.AppendLine("</table>");

                sb.AppendLine("<div class='title'>BẢNG LƯƠNG NHÂN VIÊN</div>");
                sb.AppendLine($"<div class='filter'>Điều kiện lọc: {_dieuKienLoc}</div>");

                sb.AppendLine("<table>");
                sb.AppendLine("<thead><tr>");
                sb.AppendLine("<th style='width:50px;'>STT</th>");
                sb.AppendLine("<th style='width:90px;'>Mã NV</th>");
                sb.AppendLine("<th>Họ tên</th>");
                sb.AppendLine("<th style='width:120px;'>Ngày sinh</th>");
                sb.AppendLine("<th style='width:100px;'>Hệ số lương</th>");
                sb.AppendLine("<th style='width:130px;'>Lương</th>");
                sb.AppendLine("</tr></thead><tbody>");

                var rowsList = _dtData.AsEnumerable().ToList();
                var grouped = rowsList
                    .GroupBy(r => r["TenPB"] != DBNull.Value ? r["TenPB"].ToString() : "Khác")
                    .OrderBy(g => g.Key);

                int stt = 1;
                double total = 0;

                foreach (var group in grouped)
                {
                    sb.AppendLine($"<tr class='group-header'><td colspan='6'>Phòng ban: {group.Key} ({group.Count()} bản ghi)</td></tr>");
                    foreach (var row in group)
                    {
                        string maNV = row["MaNV"].ToString();
                        string hoTen = row["HoTen"].ToString();
                        string ns = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy") : "";
                        double hsl = row["HeSoLuong"] != DBNull.Value ? Convert.ToDouble(row["HeSoLuong"]) : 0;
                        double luong = Math.Round(hsl * 2340000.0);
                        total += luong;

                        sb.AppendLine("<tr>");
                        sb.AppendLine($"<td class='center'>{stt++}</td>");
                        sb.AppendLine($"<td class='center'>{maNV}</td>");
                        sb.AppendLine($"<td>{hoTen}</td>");
                        sb.AppendLine($"<td class='center'>{ns}</td>");
                        sb.AppendLine($"<td class='right'>{hsl:0.##}</td>");
                        sb.AppendLine($"<td class='right' style=\"mso-number-format:'\\#\\,\\#\\#0';\">{luong:#,##0}</td>");
                        sb.AppendLine("</tr>");
                    }
                }

                sb.AppendLine("<tr class='total-row'>");
                sb.AppendLine("<td></td>");
                sb.AppendLine("<td class='center'>TỔNG CỘNG</td>");
                sb.AppendLine("<td colspan='3'></td>");
                sb.AppendLine($"<td class='right' style=\"mso-number-format:'\\#\\,\\#\\#0';\">{total:#,##0}</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</tbody></table>");

                sb.AppendLine("<div class='sign-title'>Người lập biểu</div>");
                sb.AppendLine("<div class='sign-sub'>(Ký, ghi rõ họ tên)</div>");
                sb.AppendLine("<div style='text-align:center; margin-top:50px; font-style:italic;'>Trang 1/1</div>");

                sb.AppendLine("</body></html>");

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                DialogResult dr = MessageBox.Show("Xuất báo cáo thành công!\nBạn có muốn mở file vừa xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatPdf_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = "BangLuongNhanVien_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf",
                    Title = "Xuất báo cáo ra file PDF"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                PrintDocument pd = TaoPrintDocument();
                pd.PrinterSettings.PrintToFile = true;
                pd.PrinterSettings.PrintFileName = sfd.FileName;

                string pdfPrinter = null;
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    if (printer.IndexOf("PDF", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        pdfPrinter = printer;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(pdfPrinter))
                {
                    pd.PrinterSettings.PrinterName = pdfPrinter;
                    pd.Print();
                    MessageBox.Show($"Đã xuất file PDF thành công:\n{sfd.FileName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    if (File.Exists(sfd.FileName))
                    {
                        Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                }
                else
                {
                    MessageBox.Show("Hệ thống chưa cài đặt máy in PDF (Microsoft Print to PDF). Đang mở hộp thoại In để chọn máy in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PrintDialog pDlg = new PrintDialog { Document = pd };
                    if (pDlg.ShowDialog() == DialogResult.OK)
                    {
                        pd.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument pd = TaoPrintDocument();
                PrintPreviewDialog ppd = new PrintPreviewDialog
                {
                    Document = pd,
                    Width = 950,
                    Height = 700,
                    StartPosition = FormStartPosition.CenterParent,
                    Text = "Xem trước khi in báo cáo - Bảng lương nhân viên"
                };
                ppd.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem trước in: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PrintDocument TaoPrintDocument()
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = false;
            pd.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

            pd.PrintPage += (s, ev) =>
            {
                Graphics g = ev.Graphics;
                float left = ev.MarginBounds.Left;
                float top = ev.MarginBounds.Top;
                float width = ev.MarginBounds.Width;
                float currentY = top;

                Font fontCongTy = new Font("Times New Roman", 12f, FontStyle.Bold);
                Font fontDate = new Font("Times New Roman", 11f, FontStyle.Regular);
                Font fontTitle = new Font("Times New Roman", 17f, FontStyle.Bold);
                Font fontFilter = new Font("Times New Roman", 11f, FontStyle.Italic);
                Font fontHeader = new Font("Times New Roman", 10.5f, FontStyle.Bold);
                Font fontGroup = new Font("Times New Roman", 10.5f, FontStyle.Bold);
                Font fontData = new Font("Times New Roman", 10f, FontStyle.Regular);
                Font fontFooter = new Font("Times New Roman", 10f, FontStyle.Italic);

                g.DrawString("CÔNG TY TNHH AN PHÁT", fontCongTy, Brushes.Black, left, currentY);
                string dateStr = "Ngày lập: " + DateTime.Now.ToString("dd/MM/yyyy");
                SizeF dateSize = g.MeasureString(dateStr, fontDate);
                g.DrawString(dateStr, fontDate, Brushes.Black, left + width - dateSize.Width, currentY);
                currentY += 40;

                string titleStr = "BẢNG LƯƠNG NHÂN VIÊN";
                SizeF titleSize = g.MeasureString(titleStr, fontTitle);
                g.DrawString(titleStr, fontTitle, Brushes.Black, left + (width - titleSize.Width) / 2f, currentY);
                currentY += 30;

                string filterStr = "Điều kiện lọc: " + _dieuKienLoc;
                SizeF filterSize = g.MeasureString(filterStr, fontFilter);
                g.DrawString(filterStr, fontFilter, Brushes.Black, left + (width - filterSize.Width) / 2f, currentY);
                currentY += 35;

                float[] colWidths = { 50f, 85f, width - (50f + 85f + 110f + 95f + 120f), 110f, 95f, 120f };
                string[] headers = { "STT", "Mã NV", "Họ tên", "Ngày sinh", "Hệ số lương", "Lương" };

                float rowHeight = 28f;
                float curX = left;
                for (int i = 0; i < headers.Length; i++)
                {
                    RectangleF cellRect = new RectangleF(curX, currentY, colWidths[i], rowHeight);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(217, 225, 242)), cellRect);
                    g.DrawRectangle(Pens.Black, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(headers[i], fontHeader, Brushes.Black, cellRect, sf);
                    curX += colWidths[i];
                }
                currentY += rowHeight;

                var rowsList = _dtData.AsEnumerable().ToList();
                var grouped = rowsList
                    .GroupBy(r => r["TenPB"] != DBNull.Value ? r["TenPB"].ToString() : "Khác")
                    .OrderBy(grp => grp.Key);

                int stt = 1;
                double total = 0;

                foreach (var group in grouped)
                {
                    RectangleF groupRect = new RectangleF(left, currentY, width, rowHeight);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(245, 245, 245)), groupRect);
                    g.DrawRectangle(Pens.Black, groupRect.X, groupRect.Y, groupRect.Width, groupRect.Height);

                    StringFormat sfGroup = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString($"  Phòng ban: {group.Key} ({group.Count()} bản ghi)", fontGroup, Brushes.Black, groupRect, sfGroup);
                    currentY += rowHeight;

                    foreach (var row in group)
                    {
                        string maNV = row["MaNV"].ToString();
                        string hoTen = row["HoTen"].ToString();
                        string ns = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy") : "";
                        double hsl = row["HeSoLuong"] != DBNull.Value ? Convert.ToDouble(row["HeSoLuong"]) : 0;
                        double luong = Math.Round(hsl * 2340000.0);
                        total += luong;

                        string[] vals = {
                            stt++.ToString(),
                            maNV,
                            hoTen,
                            ns,
                            hsl.ToString("0.##"),
                            luong.ToString("#,##0")
                        };

                        curX = left;
                        for (int i = 0; i < vals.Length; i++)
                        {
                            RectangleF cRect = new RectangleF(curX, currentY, colWidths[i], rowHeight);
                            g.DrawRectangle(Pens.Black, cRect.X, cRect.Y, cRect.Width, cRect.Height);

                            StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center };
                            if (i == 0 || i == 1 || i == 3)
                            {
                                sf.Alignment = StringAlignment.Center;
                            }
                            else if (i == 4 || i == 5)
                            {
                                sf.Alignment = StringAlignment.Far;
                            }
                            else
                            {
                                sf.Alignment = StringAlignment.Near;
                            }

                            RectangleF textRect = new RectangleF(cRect.X + 4, cRect.Y, cRect.Width - 8, cRect.Height);
                            g.DrawString(vals[i], fontData, Brushes.Black, textRect, sf);

                            curX += colWidths[i];
                        }
                        currentY += rowHeight;
                    }
                }

                RectangleF totalRect = new RectangleF(left, currentY, width, rowHeight);
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 242, 204)), totalRect);

                curX = left;
                for (int i = 0; i < colWidths.Length; i++)
                {
                    RectangleF cRect = new RectangleF(curX, currentY, colWidths[i], rowHeight);
                    g.DrawRectangle(Pens.Black, cRect.X, cRect.Y, cRect.Width, cRect.Height);

                    if (i == 1)
                    {
                        StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        g.DrawString("TỔNG CỘNG", fontHeader, Brushes.Black, cRect, sf);
                    }
                    else if (i == 5)
                    {
                        StringFormat sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
                        RectangleF textRect = new RectangleF(cRect.X + 4, cRect.Y, cRect.Width - 8, cRect.Height);
                        g.DrawString(total.ToString("#,##0"), fontHeader, Brushes.Black, textRect, sf);
                    }
                    curX += colWidths[i];
                }
                currentY += rowHeight + 25;

                float signX = left + width - 200f;
                g.DrawString("Người lập biểu", fontHeader, Brushes.Black, signX, currentY);
                currentY += 20;
                g.DrawString("(Ký, ghi rõ họ tên)", fontFilter, Brushes.Black, signX - 10, currentY);

                string pageStr = "Trang 1/1";
                SizeF pageSize = g.MeasureString(pageStr, fontFooter);
                g.DrawString(pageStr, fontFooter, Brushes.Black, left + (width - pageSize.Width) / 2f, ev.MarginBounds.Bottom - 15);

                ev.HasMorePages = false;
            };

            return pd;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
