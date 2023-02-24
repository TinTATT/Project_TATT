using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangAnUong
{
    public partial class FrmChuyenBan : Form
    {
        public FrmChuyenBan()
        {
            InitializeComponent();
            LoadBan();
        }

        public List<Ban_DTO> lstBan;

        void LoadBan()
        {
            lstBan = Ban_BUS.LoadBan();
            drbChuyenBan.DataSource = lstBan;
            drbChuyenBan.SelectedIndex = 0;
            drbChuyenBan.DisplayMember = "TenBan";
            drbChuyenBan.ValueMember = "MaBan";
        }

        public int MaBanToTable;
        public bool chuyenBan;
        public int inDexTable;

        private void drbChuyenBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            inDexTable = drbChuyenBan.SelectedIndex;
        }

        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            if (lstBan[inDexTable].TrangThai == "Có Người")
            {
                MessageBox.Show("Bàn này đã có người đặt!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chuyenBan = false;
            }
            chuyenBan = true;
            MaBanToTable = int.Parse(drbChuyenBan.SelectedValue.ToString());
            this.Close();
        }

        private void FrmChuyenBan_Load(object sender, EventArgs e)
        {

        }
    }
}
