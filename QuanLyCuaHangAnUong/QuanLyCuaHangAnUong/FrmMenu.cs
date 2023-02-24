using BUS;
using DAO;
using DTO;
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

namespace QuanLyCuaHangAnUong
{
    public partial class FrmMenu : Form
    {
        public int indexTable = -1;
        public int chiSolstHoaDon;
        public List<Ban_DTO> danhsachban;
        DataGridViewRow dr;
        public List<MonAn_DTO> danhsachmon;
        public List<HoaDon_DTO> danhsachHoaDon;
        ListViewItem ban;
        ListViewItem lstItem;
        int soLuong = 1;

        public FrmMenu()
        {
            InitializeComponent();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
        } 

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            LoadBan();
            LoadMonAn();
        }

        // Hiển thị danh sách bàn ăn
        private void LoadBan()
        {
            danhsachban = Ban_BUS.LoadBan();
            if (danhsachban == null)
                return;

            for (int i = 0; i < danhsachban.Count; i++)
            {
                ban = new ListViewItem();
                if (danhsachban[i].TrangThai == "Trống")
                {
                    ban.ImageIndex = 0; //ảnh = 0 trống và ngược lại 1
                } else
                {
                    ban.ImageIndex = 1;
                }

                ban.Text = danhsachban[i].TenBan;


                lstBanAn.Items.Add(ban);
            }
        }

        private void LoadMonAn()
        {
            danhsachmon = MonAn_BUS.LoadMonAn();
            dgvMonAn.DataSource = danhsachmon;
        }

        private void lstBanAn_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            indexTable = e.ItemIndex;
            
            for (int i = 0; i < lstBanAn.Items.Count; i++)
            {
                if (lstBanAn.Items[i].Selected)
                {
                    if (lstBanAn.Items[i].ImageIndex == 0)
                    {
                        lbTrangThai.ForeColor = Color.ForestGreen;
                        lbLoaiBan.ForeColor = Color.ForestGreen;
                    }
                    else
                    {
                        lbTrangThai.ForeColor = Color.Red;
                        lbLoaiBan.ForeColor = Color.Red;

                    }
                    lbTrangThai.Text = danhsachban[i].TrangThai.ToString();
                    lbLoaiBan.Text = danhsachban[i].LoaiBan.ToString();

                    LoadHoaDon(danhsachban[indexTable].MaBan);
                }
            }
        }

        private void LoadHoaDon(int MaBan)
        {
            int TongTien = 0;
            lstViewDatMon.Items.Clear();
            danhsachHoaDon = HoaDon_BUS.LoadHoaDon(MaBan);
            if (danhsachHoaDon == null)
            {
                txtTongTien.Text = "0";
                return;
            }
            else
            {
                for (int i = 0; i < danhsachHoaDon.Count; i++)
                {
                    ListViewItem it = new ListViewItem(danhsachHoaDon[i].TenMonAn.ToString());
                    it.SubItems.Add(danhsachHoaDon[i].SoLuong.ToString());
                    it.SubItems.Add(danhsachHoaDon[i].Gia.ToString());
                    it.SubItems.Add(danhsachHoaDon[i].ThanhTien.ToString());
                    it.SubItems.Add(danhsachHoaDon[i].GhiChu.ToString());
                    TongTien += int.Parse(it.SubItems[3].Text.ToString());
                    lstViewDatMon.Items.Add(it);
                }
                txtTongTien.Text = TongTien.ToString();
            }
        }

        private void dgvMonAn_Click(object sender, EventArgs e)
        {
            try
            {
                dr = dgvMonAn.SelectedRows[0];
            }
            catch (Exception)
            {
                return;
            }
        }
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (indexTable == -1)
            {
                MessageBox.Show("Chọn Bàn Muốn Thêm!!!");
                return;
            }
            if (lstBanAn.Items.Count > 0)
            {
                for (int i = 0; i < lstViewDatMon.Items.Count; i++)
                {
                    try
                    {
                        if (lstViewDatMon.Items[i].SubItems[0].Text.ToString() == dr.Cells["TenSP"].Value.ToString())
                        {
                            MessageBox.Show("Món ăn đã có rồi", "Thông Báo");
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Món ăn đã có rồi", "Thông Báo");
                        return;
                    }
                }
            }
            try
            {
                lstItem = new ListViewItem(dr.Cells["TenSP"].Value.ToString());
                lstItem.SubItems.Add("1");
                lstItem.SubItems.Add(dr.Cells["DonGia"].Value.ToString());
                lstItem.SubItems.Add((soLuong * int.Parse(dr.Cells["DonGia"].Value.ToString())).ToString());
                lstItem.SubItems.Add(txtGhiChu.Text.ToString()).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Chọn Món Ăn Muốn Thêm", "Thông Báo");
                return;
            }
            lstViewDatMon.Items.Add(lstItem);

            HoaDon_BUS.ThemThongTinHoaDon(HoaDon_BUS.layIDHoaDon(danhsachban[indexTable].MaBan), int.Parse(dr.Cells["MaSP"].Value.ToString()), int.Parse(lstItem.SubItems[1].Text.ToString()), int.Parse(lstItem.SubItems[3].Text.ToString()), lstItem.SubItems[4].Text.ToString());

            LoadHoaDon(danhsachban[indexTable].MaBan);
        }

        private void btnHuyMon_Click(object sender, EventArgs e)
        {
            if (lstViewDatMon.Items.Count < 1)
            {
                return;
            }
            if (chiSolstHoaDon != -1)
            {
                try
                {
                    HoaDon_BUS.XoaMonAn(HoaDon_BUS.layIDHoaDon(danhsachban[indexTable].MaBan), MonAn_BUS.layIDMonAn(lstViewDatMon.Items[chiSolstHoaDon].SubItems[0].Text.ToString()));
                }
                catch (Exception)
                {
                    return;
                }

                lstViewDatMon.Items.RemoveAt(chiSolstHoaDon);

                LoadHoaDon(danhsachban[indexTable].MaBan);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn để xóa!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




       
            private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            if (indexTable == -1)
            {
                MessageBox.Show("Chọn 1 bàn để chuyển!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lstBanAn.Items[indexTable].ImageIndex == 0)
            {
                MessageBox.Show("Mở bàn để chuyển bàn khác!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            FrmChuyenBan frm = new FrmChuyenBan();
            frm.ShowDialog();
            int maBanToTable = frm.MaBanToTable;
            if (frm.chuyenBan == false)
            {
                return;
            }
            if (HoaDon_BUS.ChuyenBan(danhsachban[indexTable].MaBan, maBanToTable))
            {

                Ban_BUS.SuaTrangThaiBanTrong(danhsachban[indexTable].MaBan);
                Ban_BUS.SuaTrangThaiBan(maBanToTable);
                lstBanAn.Clear();
                LoadBan();
                LoadHoaDon(danhsachban[indexTable].MaBan);

                lbTrangThai.ForeColor = Color.Red;
                lbLoaiBan.ForeColor = Color.Red;
                lbTrangThai.Text = danhsachban[indexTable].TrangThai.ToString();
                lbLoaiBan.Text = danhsachban[indexTable].LoaiBan.ToString();

                MessageBox.Show("Đã chuyển bàn thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void btnTang_Click(object sender, EventArgs e)
        {
            int sl, thanhtien;
            try
            {
                sl = int.Parse(lstViewDatMon.Items[chiSolstHoaDon].SubItems[1].Text.ToString());
            }
            catch (Exception)
            {
                return;
            }

            if (chiSolstHoaDon != -1)
            {
                sl = sl + 1;
                thanhtien = int.Parse(dr.Cells["DonGia"].Value.ToString()) * sl;
                lstViewDatMon.Items[chiSolstHoaDon].SubItems[1].Text = sl.ToString();
                lstViewDatMon.Items[chiSolstHoaDon].SubItems[3].Text = thanhtien.ToString();
                HoaDon_BUS.CapNhatSoLuongMonAn(sl, thanhtien, HoaDon_BUS.layIDHoaDon(danhsachban[indexTable].MaBan), MonAn_BUS.layIDMonAn(lstViewDatMon.Items[chiSolstHoaDon].SubItems[0].Text.ToString()));
                LoadHoaDon(danhsachban[indexTable].MaBan);
            }
        }

        private void btnGiam_Click(object sender, EventArgs e)
        {
            int sl, thanhtien;
            try
            {
                sl = int.Parse(lstViewDatMon.Items[chiSolstHoaDon].SubItems[1].Text.ToString());
            }
            catch (Exception)
            {
                return;
            }

            if (chiSolstHoaDon != -1)
            {
                if (sl > 1)
                {
                    sl = sl - 1;
                    thanhtien = int.Parse(dr.Cells["DonGia"].Value.ToString()) * sl;
                    lstViewDatMon.Items[chiSolstHoaDon].SubItems[1].Text = sl.ToString();
                    lstViewDatMon.Items[chiSolstHoaDon].SubItems[3].Text = thanhtien.ToString();
                    HoaDon_BUS.CapNhatSoLuongMonAn(sl, thanhtien, HoaDon_BUS.layIDHoaDon(danhsachban[indexTable].MaBan), MonAn_BUS.layIDMonAn(lstViewDatMon.Items[chiSolstHoaDon].SubItems[0].Text.ToString()));
                    LoadHoaDon(danhsachban[indexTable].MaBan);
                }
                else
                {
                    MessageBox.Show("Không thể giảm số lượng!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void lstViewDatMon_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            chiSolstHoaDon = e.ItemIndex;
        }

        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            txtTimKiem.ForeColor = Color.Black;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            List<MonAn_DTO> ketQua = MonAn_BUS.TimMonAn(txtTimKiem.Text);
            if (ketQua == null)
                return;
            dgvMonAn.DataSource = ketQua;
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Nhập tên món ăn ... ";
            txtTimKiem.ForeColor = Color.Gray;
        }

       
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Nhập tên khách hàng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            KhachHang_DTO khachHang = new KhachHang_DTO();
            khachHang.TenKH = txtTenKH.Text.ToString();


            List<KhachHang_DTO> ketqua = Ban_BUS.LayKH(txtTenKH.Text);
            if (ketqua != null)
            {
                MessageBox.Show("Đã có tên khách hàng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Ban_BUS.ThemKhachHang(khachHang))
            {
                MessageBox.Show("Đã thêm khách hàng");
                return;
            }
        }

        private void btnDatBan_Click(object sender, EventArgs e)
        {
            int maKh = Ban_BUS.layMaKH(txtTenKH.Text);
            if (indexTable == -1)
            {
                MessageBox.Show("Chọn 1 bàn để đặt!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (lstBanAn.Items[indexTable].ImageIndex == 1)
            {
                MessageBox.Show("Bàn đã có người đặt. Vui lòng chọn bàn khác!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (HoaDon_BUS.ThemHoaDon(danhsachban[indexTable].MaBan, maKh) == true && Ban_BUS.SuaTrangThaiBan(danhsachban[indexTable].MaBan) == true)
            {

                lstBanAn.Items[indexTable].ImageIndex = 1;
                lbTrangThai.Text = "Có Người";
                lbTrangThai.ForeColor = Color.Red;
                lbLoaiBan.ForeColor = Color.Red;
                lstBanAn.Clear();
                LoadBan();
                MessageBox.Show("Đã đặt bàn thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private void btnHuyBan_Click(object sender, EventArgs e)
        {
            if (indexTable == -1)
            {
                MessageBox.Show("Chọn 1 bàn để hủy!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (lstBanAn.Items[indexTable].ImageIndex == 0)
            {
                MessageBox.Show("Bàn không có người đặt. Vui lòng chọn bàn khác!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Ban_BUS.SuaTrangThaiBanTrong(danhsachban[indexTable].MaBan) == true)
            {
                lstBanAn.Items[indexTable].ImageIndex = 0;
                lbTrangThai.Text = "Trống";
                lbTrangThai.ForeColor = Color.ForestGreen;
                lbLoaiBan.ForeColor = Color.ForestGreen;
                lstBanAn.Clear();
                LoadBan();
                MessageBox.Show("Đã hủy bàn thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (indexTable == -1)
            {
                return;
            }

            int tien = int.Parse(txtTongTien.Text);

            if (MessageBox.Show("Tổng tiền là " + tien + "VND","Thanh toán",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) ==DialogResult.OK)
            {
                if (Ban_BUS.SuaTrangThaiBanTrong(danhsachban[indexTable].MaBan) == true)
                {
                    lbTrangThai.Text = "Trống";
                    lbTrangThai.ForeColor = Color.ForestGreen;
                    lbLoaiBan.ForeColor = Color.ForestGreen;
                }

                HoaDon_BUS.XoaCTHD(HoaDon_BUS.layIDHoaDon(danhsachban[indexTable].MaBan));

                DateTime date = DateTime.Now;
                string ngayban = date.ToString("yyyy/MM/dd");
                try
                {
                    HoaDon_BUS.UpdateHoaDon(ngayban,tien,"Đã thanh toán", danhsachban[indexTable].MaBan, Ban_BUS.layMaKH(txtTenKH.Text));
                } catch (Exception)
                {
                    return;
                }
                LoadHoaDon(danhsachban[indexTable].MaBan);              
            }
        }

        private void lstViewDatMon_ItemSelectionChanged_1(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            chiSolstHoaDon = e.ItemIndex;
        }

        private void txtTimKiem_TextChanged_1(object sender, EventArgs e)
        {
            

            List<MonAn_DTO> ketQua = MonAn_BUS.TimMonAn(txtTimKiem.Text);
            if (ketQua == null)
                return;
            dgvMonAn.DataSource = ketQua;
        }
    }
    
}