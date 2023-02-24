using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;
using DAO;
using System.Data.SqlClient;

namespace QuanLyCuaHangAnUong
{
    public partial class FrmMonAn : Form
    {
        SqlConnection conn;
        public FrmMonAn()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void FrmMonAn_Load(object sender, EventArgs e)
        {
            LoadSP();
            LoadLoai();
        }

        public List<MonAn_DTO> danhsachSP;
        void LoadSP()
        {
            danhsachSP = MonAn_BUS.LoadMonAn();
            dgvSP.DataSource = danhsachSP;
            
        }

    
        void LoadLoai()
        {
            conn = DataProvider.MoKetNoi();
            SqlDataAdapter da = new SqlDataAdapter("Select TenLoai From LOAISP", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbTenLoai.DataSource = dt;
            cbTenLoai.DisplayMember = "TenLoai";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            MonAn_DTO sp = new MonAn_DTO();
            sp.TenSP = txtTenSP.Text;
            sp.DonGia = int.Parse(txtDonGia.Text);
            sp.DonVi = txtDonVi.Text;
            sp.MoTa = txtMota.Text;
            sp.MaLoaiSP = MonAn_BUS.layloai(cbTenLoai.Text);

            if (MonAn_BUS.ThemSP(sp))
            {
                LoadSP();
                MessageBox.Show("Thêm thành công!!!");
                return;
            }
            else
            {
                MessageBox.Show("Thêm thất bại!!!");
            }

        }
        DataGridViewRow dr;

        private void dgvSP_Click(object sender, EventArgs e)
        {
            try
            {
                dr = dgvSP.SelectedRows[0];

            }
            catch (Exception)
            {
                return;
            }

            txtMaSP.Text = dr.Cells["MaSP"].Value.ToString();
            txtTenSP.Text = dr.Cells["TenSP"].Value.ToString();
            txtDonGia.Text = dr.Cells["DonGia"].Value.ToString();
            txtDonVi.Text = dr.Cells["DonVi"].Value.ToString();
            txtMota.Text = dr.Cells["MoTa"].Value.ToString();
            
            
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            MonAn_DTO sp = new MonAn_DTO();
            sp.MaSP = int.Parse(txtMaSP.Text);
            sp.TenSP = txtTenSP.Text;
            sp.DonGia = int.Parse(txtDonGia.Text);
            sp.DonVi = txtDonVi.Text;
            sp.MoTa = txtMota.Text;
            sp.MaLoaiSP = MonAn_BUS.layloai(cbTenLoai.Text);


            try
            {
                if (MonAn_BUS.SuaSP(sp))
                {
                    //sp.MaSP = int.Parse("");
                    //sp.TenSP = "";
                    //sp.DonGia = int.Parse("");
                    //sp.DonVi = "";
                    //sp.MoTa = "";

                    LoadSP();
                    MessageBox.Show("Sửa thành công!!!");

                    return;
                }
                MessageBox.Show("Sửa thất bại!!!");

            }
            catch (Exception)
            {
                return;
            }
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MonAn_DTO sp = new MonAn_DTO();
            sp.MaSP = int.Parse(txtMaSP.Text);

            if (MessageBox.Show("Xác nhận xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (MonAn_BUS.XoaSP(sp))
                    {
                    //sp.MaSP = int.Parse("");
                    //sp.TenSP = "";
                    //sp.DonGia = int.Parse("");
                    //sp.DonVi = "";
                    //sp.MoTa = "";

                        LoadSP();
                        MessageBox.Show("Xóa thành công!!!");
                    
                    return;
                    }
                MessageBox.Show("Xóa thất bại!!!");

                }
                catch (Exception)
                {
                    return;
                }
                
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            List<MonAn_DTO> ketQua = MonAn_BUS.TimMonAn(txtTimKiem.Text);
            if (ketQua == null)
                return;
            dgvSP.DataSource = ketQua;
        }
    }
}
