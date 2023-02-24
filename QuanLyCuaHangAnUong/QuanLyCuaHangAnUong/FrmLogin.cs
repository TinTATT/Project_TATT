using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using DAO;

namespace QuanLyCuaHangAnUong
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            txtTenDangNhap.Text = "NV001";
            txtMatkhau.Text = "tin123";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Thông Báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }

        }

        private void btn_login_Click(object sender, EventArgs e)
        {


            SqlConnection conn;
            try
            {
                conn = DataProvider.MoKetNoi();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TaiKhoan";
                cmd.Parameters.AddWithValue("@TenDN", txtTenDangNhap.Text);
                cmd.Parameters.AddWithValue("@MK", txtMatkhau.Text);
                cmd.Connection = conn;
                string tenDN = txtTenDangNhap.Text;
                object kq = cmd.ExecuteScalar();
                int code = Convert.ToInt32(kq);

                if (code == 0)
                {
                    FrmTrangChu frm = new FrmTrangChu(tenDN);
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
               
                else if (code == 1)
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatkhau.Text = "";
                    txtTenDangNhap.Text = "";
                    txtTenDangNhap.Focus();
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatkhau.Text = "";
                    txtTenDangNhap.Text = "";
                    txtTenDangNhap.Focus();
                }
                DataProvider.DongKetNoi(conn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }

    internal class Account
    {
        public Account()
        {
        }
    }
}
