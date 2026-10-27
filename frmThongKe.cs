using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace mautest
{
    public partial class frmThongKe : Form
    {
        private DataTable _dtKetQua;

        public frmThongKe()
        {
            InitializeComponent();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            ThucHienTimKiem(isInitialLoad: true);
        }

        private void LoadComboBoxes()
        {
            try
            {
                string sql = "SELECT MaPB, TenPB FROM PhongBan ORDER BY TenPB";
                DataTable dtPB = DB.ExecuteQuery(sql);

                DataTable dtCboPB = new DataTable();
                dtCboPB.Columns.Add("MaPB", typeof(string));
                dtCboPB.Columns.Add("TenPB", typeof(string));

                DataRow rowAll = dtCboPB.NewRow();
                rowAll["MaPB"] = "ALL";
                rowAll["TenPB"] = "Tất cả";
                dtCboPB.Rows.Add(rowAll);

                foreach (DataRow r in dtPB.Rows)
                {
                    DataRow nr = dtCboPB.NewRow();
                    nr["MaPB"] = r["MaPB"];
                    nr["TenPB"] = r["TenPB"];
                    dtCboPB.Rows.Add(nr);
                }

                cboPhongBan.DataSource = dtCboPB;
                cboPhongBan.DisplayMember = "TenPB";
                cboPhongBan.ValueMember = "MaPB";
                cboPhongBan.SelectedIndex = 0;

                if (cboGioiTinh.Items.Count > 0)
                {
                    cboGioiTinh.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp điều kiện tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ThucHienTimKiem(bool isInitialLoad = false)
        {
            try
            {
                string sql = @"
                    SELECT 
                        nv.MaNV, 
                        nv.HoTen, 
                        nv.GioiTinh, 
                        nv.NgaySinh, 
                        nv.MaPB, 
                        pb.TenPB, 
                        nv.HeSoLuong, 
                        nv.NgayVaoLam
                    FROM NhanVien nv
                    LEFT JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                    WHERE 1 = 1";

                List<SqlParameter> paramList = new List<SqlParameter>();

                string selectedMaPB = cboPhongBan.SelectedValue?.ToString();
                if (!string.IsNullOrEmpty(selectedMaPB) && selectedMaPB != "ALL" && cboPhongBan.Text != "Tất cả")
                {
                    sql += " AND nv.MaPB = @MaPB";
                    paramList.Add(new SqlParameter("@MaPB", selectedMaPB));
                }

                string selectedGioiTinh = cboGioiTinh.Text;
                if (selectedGioiTinh == "Nam")
                {
                    sql += " AND nv.GioiTinh = 1";
                }
                else if (selectedGioiTinh == "Nữ")
                {
                    sql += " AND nv.GioiTinh = 0";
                }

                sql += " ORDER BY pb.TenPB, nv.MaNV";

                DataTable dtRaw = DB.ExecuteQuery(sql, paramList.ToArray());

                _dtKetQua = new DataTable();
                _dtKetQua.Columns.Add("MaNV", typeof(string));
                _dtKetQua.Columns.Add("HoTen", typeof(string));
                _dtKetQua.Columns.Add("GioiTinh", typeof(bool));
                _dtKetQua.Columns.Add("GioiTinhText", typeof(string));
                _dtKetQua.Columns.Add("NgaySinh", typeof(DateTime));
                _dtKetQua.Columns.Add("NgaySinhFormatted", typeof(string));
                _dtKetQua.Columns.Add("MaPB", typeof(string));
                _dtKetQua.Columns.Add("TenPB", typeof(string));
                _dtKetQua.Columns.Add("HeSoLuong", typeof(double));
                _dtKetQua.Columns.Add("NgayVaoLam", typeof(DateTime));
                _dtKetQua.Columns.Add("NgayVaoLamFormatted", typeof(string));
                _dtKetQua.Columns.Add("Luong", typeof(double));
                _dtKetQua.Columns.Add("LuongFormatted", typeof(string));

                double tongLuong = 0;
                double maxHsl = 0;
                bool hasData = dtRaw.Rows.Count > 0;

                foreach (DataRow r in dtRaw.Rows)
                {
                    DataRow nr = _dtKetQua.NewRow();
                    nr["MaNV"] = r["MaNV"];
                    nr["HoTen"] = r["HoTen"];

                    bool gioiTinh = false;
                    if (r["GioiTinh"] != DBNull.Value)
                    {
                        gioiTinh = Convert.ToBoolean(r["GioiTinh"]);
                    }
                    nr["GioiTinh"] = gioiTinh;
                    nr["GioiTinhText"] = gioiTinh ? "Nam" : "Nữ";

                    DateTime ns = DateTime.MinValue;
                    if (r["NgaySinh"] != DBNull.Value)
                    {
                        ns = Convert.ToDateTime(r["NgaySinh"]);
                        nr["NgaySinh"] = ns;
                        nr["NgaySinhFormatted"] = ns.ToString("dd/MM/yyyy");
                    }

                    nr["MaPB"] = r["MaPB"];
                    nr["TenPB"] = r["TenPB"] != DBNull.Value ? r["TenPB"] : "";

                    double hsl = 0;
                    if (r["HeSoLuong"] != DBNull.Value)
                    {
                        hsl = Convert.ToDouble(r["HeSoLuong"]);
                    }
                    nr["HeSoLuong"] = hsl;
                    if (hsl > maxHsl) maxHsl = hsl;

                    DateTime nvl = DateTime.MinValue;
                    if (r["NgayVaoLam"] != DBNull.Value)
                    {
                        nvl = Convert.ToDateTime(r["NgayVaoLam"]);
                        nr["NgayVaoLam"] = nvl;
                        nr["NgayVaoLamFormatted"] = nvl.ToString("dd/MM/yyyy");
                    }

                    double luong = Math.Round(hsl * 2340000.0);
                    nr["Luong"] = luong;
                    nr["LuongFormatted"] = luong.ToString("#,##0");
                    tongLuong += luong;

                    _dtKetQua.Rows.Add(nr);
                }

                dgvKetQua.AutoGenerateColumns = false;
                dgvKetQua.DataSource = _dtKetQua;

                if (!hasData && !isInitialLoad)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phù hợp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CapNhatThongKe(_dtKetQua.Rows.Count, maxHsl, tongLuong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatThongKe(int count, double maxHsl, double tongLuong)
        {
            lblSoNhanVien.Text = "Số nhân viên: " + count;
            lblHeSoLuongMax.Text = "Hệ số lương cao nhất: " + (count > 0 ? maxHsl.ToString("0.##") : "0");
            lblTongLuong.Text = "Tổng lương: " + (count > 0 ? tongLuong.ToString("#,##0") : "0");
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ThucHienTimKiem(isInitialLoad: false);
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            if (_dtKetQua == null || _dtKetQua.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất báo cáo", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dieuKienLoc = $"Phòng ban: {cboPhongBan.Text}; Giới tính: {cboGioiTinh.Text}";
            using (frmBaoCao frm = new frmBaoCao(_dtKetQua, dieuKienLoc))
            {
                frm.ShowDialog(this);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
