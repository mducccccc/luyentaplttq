using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace mautest
{
    public partial class frmQuanLyNhanVien : Form
    {
        private DataTable _dtNhanVien;

        public frmQuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadPhongBan();
            LoadDanhSachNhanVien();
        }

        private void LoadPhongBan()
        {
            try
            {
                string sql = "SELECT MaPB, TenPB FROM PhongBan ORDER BY TenPB";
                DataTable dtPB = DB.ExecuteQuery(sql);

                cboPhongBan.DataSource = dtPB;
                cboPhongBan.DisplayMember = "TenPB";
                cboPhongBan.ValueMember = "MaPB";

                if (cboPhongBan.Items.Count > 0)
                {
                    cboPhongBan.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục Phòng ban: " + ex.Message, "Lỗi kết nối CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachNhanVien(string selectedMaNV = null)
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
                    ORDER BY nv.MaNV";

                DataTable dtRaw = DB.ExecuteQuery(sql);

                _dtNhanVien = new DataTable();
                _dtNhanVien.Columns.Add("MaNV", typeof(string));
                _dtNhanVien.Columns.Add("HoTen", typeof(string));
                _dtNhanVien.Columns.Add("GioiTinh", typeof(bool));
                _dtNhanVien.Columns.Add("GioiTinhText", typeof(string));
                _dtNhanVien.Columns.Add("NgaySinh", typeof(DateTime));
                _dtNhanVien.Columns.Add("NgaySinhFormatted", typeof(string));
                _dtNhanVien.Columns.Add("MaPB", typeof(string));
                _dtNhanVien.Columns.Add("TenPB", typeof(string));
                _dtNhanVien.Columns.Add("HeSoLuong", typeof(double));
                _dtNhanVien.Columns.Add("NgayVaoLam", typeof(DateTime));
                _dtNhanVien.Columns.Add("NgayVaoLamFormatted", typeof(string));
                _dtNhanVien.Columns.Add("Luong", typeof(double));
                _dtNhanVien.Columns.Add("LuongFormatted", typeof(string));

                foreach (DataRow r in dtRaw.Rows)
                {
                    DataRow nr = _dtNhanVien.NewRow();
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

                    _dtNhanVien.Rows.Add(nr);
                }

                dgvNhanVien.AutoGenerateColumns = false;
                dgvNhanVien.DataSource = _dtNhanVien;

                if (dgvNhanVien.Rows.Count > 0)
                {
                    int selectIdx = 0;
                    if (!string.IsNullOrEmpty(selectedMaNV))
                    {
                        for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                        {
                            if (dgvNhanVien.Rows[i].Cells["colMaNV"].Value?.ToString() == selectedMaNV)
                            {
                                selectIdx = i;
                                break;
                            }
                        }
                    }

                    dgvNhanVien.ClearSelection();
                    dgvNhanVien.Rows[selectIdx].Selected = true;
                    HienThiChiTietDong(selectIdx);
                }
                else
                {
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message, "Lỗi kết nối CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiChiTietDong(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvNhanVien.Rows.Count) return;

            DataGridViewRow row = dgvNhanVien.Rows[rowIndex];

            txtMaNV.Text = row.Cells["colMaNV"].Value?.ToString();
            txtMaNV.ReadOnly = true; 

            txtHoTen.Text = row.Cells["colHoTen"].Value?.ToString();

            string gioiTinh = row.Cells["colGioiTinh"].Value?.ToString();
            if (gioiTinh == "Nữ")
            {
                rdoNu.Checked = true;
            }
            else
            {
                rdoNam.Checked = true;
            }

            DataRowView drv = row.DataBoundItem as DataRowView;
            if (drv != null && drv["MaPB"] != DBNull.Value)
            {
                cboPhongBan.SelectedValue = drv["MaPB"].ToString();
            }

            txtHeSoLuong.Text = row.Cells["colHeSoLuong"].Value?.ToString();

            if (drv != null && drv["NgaySinh"] != DBNull.Value)
            {
                dtpNgaySinh.Value = Convert.ToDateTime(drv["NgaySinh"]);
            }

            if (drv != null && drv["NgayVaoLam"] != DBNull.Value)
            {
                dtpNgayVaoLam.Value = Convert.ToDateTime(drv["NgayVaoLam"]);
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                HienThiChiTietDong(e.RowIndex);
            }
        }

        private bool ValidateDuLieu(bool isThem, out double heSoLuong)
        {
            heSoLuong = 0;

            string maNV = txtMaNV.Text.Trim();
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return false;
            }

            if (!Regex.IsMatch(maNV, @"^NV\d{3}$"))
            {
                MessageBox.Show("Mã NV phải có dạng 'NV' kèm 3 chữ số (ví dụ: NV008)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                txtMaNV.SelectAll();
                return false;
            }

            if (isThem)
            {
                string checkSql = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
                SqlParameter[] checkParams = { new SqlParameter("@MaNV", maNV) };
                int count = Convert.ToInt32(DB.ExecuteScalar(checkSql, checkParams));
                if (count > 0)
                {
                    MessageBox.Show($"Mã nhân viên '{maNV}' đã tồn tại trong CSDL! Vui lòng nhập mã khác.", "Trùng mã nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNV.Focus();
                    txtMaNV.SelectAll();
                    return false;
                }
            }

            string hoTen = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập Họ tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (cboPhongBan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Phòng ban!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPhongBan.Focus();
                return false;
            }

            string strHsl = txtHeSoLuong.Text.Trim().Replace(',', '.');
            if (string.IsNullOrEmpty(strHsl) || !double.TryParse(strHsl, NumberStyles.Any, CultureInfo.InvariantCulture, out heSoLuong) || heSoLuong < 2.34 || heSoLuong > 8.0)
            {
                MessageBox.Show("Hệ số lương phải là số thực trong khoảng 2,34 – 8,0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHeSoLuong.Focus();
                txtHeSoLuong.SelectAll();
                return false;
            }

            DateTime ngaySinh = dtpNgaySinh.Value.Date;
            DateTime ngayVaoLam = dtpNgayVaoLam.Value.Date;
            if (ngayVaoLam < ngaySinh.AddYears(18))
            {
                MessageBox.Show("Nhân viên phải đủ 18 tuổi tính đến ngày vào làm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayVaoLam.Focus();
                return false;
            }

            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDuLieu(isThem: true, out double heSoLuong))
                {
                    return;
                }

                string sql = @"
                    INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, NgaySinh, MaPB, HeSoLuong, NgayVaoLam)
                    VALUES (@MaNV, @HoTen, @GioiTinh, @NgaySinh, @MaPB, @HeSoLuong, @NgayVaoLam)";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaNV", txtMaNV.Text.Trim()),
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@GioiTinh", rdoNam.Checked ? 1 : 0),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value.Date),
                    new SqlParameter("@MaPB", cboPhongBan.SelectedValue.ToString()),
                    new SqlParameter("@HeSoLuong", heSoLuong),
                    new SqlParameter("@NgayVaoLam", dtpNgayVaoLam.Value.Date)
                };

                DB.ExecuteNonQuery(sql, parameters);
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string newMaNV = txtMaNV.Text.Trim();
                LoadDanhSachNhanVien(newMaNV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhanVien.CurrentRow == null || !txtMaNV.ReadOnly)
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên trên danh sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateDuLieu(isThem: false, out double heSoLuong))
                {
                    return;
                }

                string sql = @"
                    UPDATE NhanVien 
                    SET HoTen = @HoTen,
                        GioiTinh = @GioiTinh,
                        NgaySinh = @NgaySinh,
                        MaPB = @MaPB,
                        HeSoLuong = @HeSoLuong,
                        NgayVaoLam = @NgayVaoLam
                    WHERE MaNV = @MaNV";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaNV", txtMaNV.Text.Trim()),
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@GioiTinh", rdoNam.Checked ? 1 : 0),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value.Date),
                    new SqlParameter("@MaPB", cboPhongBan.SelectedValue.ToString()),
                    new SqlParameter("@HeSoLuong", heSoLuong),
                    new SqlParameter("@NgayVaoLam", dtpNgayVaoLam.Value.Date)
                };

                int rowsAffected = DB.ExecuteNonQuery(sql, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien(txtMaNV.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa thông tin nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhanVien.CurrentRow == null || string.IsNullOrEmpty(txtMaNV.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên trên danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maNV = txtMaNV.Text.Trim();
                DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {maNV} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    string sql = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                    SqlParameter[] parameters = { new SqlParameter("@MaNV", maNV) };

                    DB.ExecuteNonQuery(sql, parameters);
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LamMoi();
                    LoadDanhSachNhanVien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LamMoi()
        {
            txtMaNV.Text = "";
            txtMaNV.ReadOnly = false;
            txtHoTen.Text = "";
            rdoNam.Checked = true;
            rdoNu.Checked = false;
            dtpNgaySinh.Value = new DateTime(1995, 1, 1);
            if (cboPhongBan.Items.Count > 0)
            {
                cboPhongBan.SelectedIndex = 0;
            }
            txtHeSoLuong.Text = "";
            dtpNgayVaoLam.Value = DateTime.Today;

            dgvNhanVien.ClearSelection();
            txtMaNV.Focus();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            using (frmThongKe frm = new frmThongKe())
            {
                frm.ShowDialog(this);
            }
            LoadDanhSachNhanVien(txtMaNV.Text.Trim());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
