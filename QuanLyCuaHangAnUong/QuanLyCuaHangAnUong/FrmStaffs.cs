using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;
using DAO;

namespace QuanLyCuaHangAnUong
{
    public partial class FrmStaffs : Form
    {
      
        public FrmStaffs()
        {
            InitializeComponent();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void FrmStaffs_Load(object sender, EventArgs e)
        {
            LoadNV();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            NhanVien_DTO nv = new NhanVien_DTO();
            nv.MaNV = txtMaNV.Text;
            nv.TenNV = txtHoTen.Text;
            nv.DiaChi = txtDiaChi.Text;
            nv.NgaySinh = DataProvider.ConvertDateTime(mstNgaySinh.Text);
            nv.GioiTinh = txtSex.Text;
            nv.SDT = txtSDT.Text;
            nv.TenTK = txtTenTk.Text;
            nv.MK = txtMK.Text;
            nv.ChucVu = txtChucVu.Text;
            if (danhsachnv != null)
            {
                foreach (NhanVien_DTO nhanvien in danhsachnv)
                {
                    if (nv.MaNV == nhanvien.MaNV)
                    {
                        MessageBox.Show("Đã có nhân viên!!!");
                        return;
                    }
                }
            }
            if (NhanVien_BUS.ThemNV(nv)) {
                LoadNV();
                MessageBox.Show("Thêm thành công!!!");
                return;
            } else
            {
                MessageBox.Show("Thêm thất bại!!!");
            }
            
            
        }
        public List<NhanVien_DTO> danhsachnv;
        void LoadNV()
        {
            danhsachnv = NhanVien_BUS.LoadNV();
            dgvNV.DataSource = danhsachnv;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            List<NhanVien_DTO> ketQua = NhanVien_BUS.TimNV(txtTimKiem.Text);
            if (ketQua == null)
                return;
            dgvNV.DataSource = ketQua;
        }

        DataGridViewRow dr;
        private void dgvNV_Click(object sender, EventArgs e)
        {
            try
            {
                dr = dgvNV.SelectedRows[0];

            } catch (Exception)
            {
                return;
            }

            txtMaNV.Text = dr.Cells["MaNV"].Value.ToString();
            txtHoTen.Text = dr.Cells["TenNV"].Value.ToString();
            txtDiaChi.Text = dr.Cells["DiaChi"].Value.ToString();
            mstNgaySinh.Text = dr.Cells["NgaySinh"].Value.ToString();
            txtSex.Text = dr.Cells["GioiTinh"].Value.ToString();
            txtSDT.Text = dr.Cells["SDT"].Value.ToString();
            txtTenTk.Text = dr.Cells["TenTK"].Value.ToString();
            txtMK.Text = dr.Cells["MK"].Value.ToString();
            txtChucVu.Text = dr.Cells["ChucVu"].Value.ToString();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            NhanVien_DTO nv = new NhanVien_DTO();
            nv.MaNV = dr.Cells["MaNV"].Value.ToString();
            nv.TenNV = txtHoTen.Text;
            nv.DiaChi = txtDiaChi.Text;
            nv.NgaySinh = DataProvider.ConvertDateTime(mstNgaySinh.Text);
            nv.GioiTinh = txtSex.Text;
            nv.SDT = txtSDT.Text;
            nv.TenTK = txtTenTk.Text;
            nv.MK = txtMK.Text;
            nv.ChucVu = txtChucVu.Text;

            if (NhanVien_BUS.SuaNV(nv))
            {
                nv.TenNV = "";
                nv.DiaChi = "";
                nv.NgaySinh = "";
                nv.GioiTinh = "";
                nv.SDT = "";
                nv.TenTK = "";
                nv.MK = "";
                nv.ChucVu = "";
                LoadNV();
                MessageBox.Show("Sửa thành công!!!");
                return;
            }
            MessageBox.Show("Sửa thất bại!!!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            NhanVien_DTO nv = new NhanVien_DTO();
            nv.MaNV = txtMaNV.Text;

            if (MessageBox.Show("Xác nhận xóa","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (NhanVien_BUS.XoaNV(nv))
                {
                    nv.TenNV = "";
                    nv.DiaChi = "";
                    nv.NgaySinh = "";
                    nv.GioiTinh = "";
                    nv.SDT = "";
                    nv.TenTK = "";
                    nv.MK = "";
                    nv.ChucVu = "";
                    LoadNV();
                    MessageBox.Show("Xóa thành công!!!");
                    return;
                }
                MessageBox.Show("Xóa thất bại!!!");
            }
        }

        private void dgvNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
