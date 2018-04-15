using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Ketnoi;

namespace code_PC_doan
{
    public partial class Dangnhapdatabase : Form
    {
        public Dangnhapdatabase()
        {
            InitializeComponent();
        }
        Ketnoi ketnoi = new Ketnoi();
        private void BT_connect_Click(object sender, EventArgs e)
        {
            //ketnoi.ip_host = TB_iphost.Text;
            //ketnoi.database = TB_database.Text;
            //ketnoi.user = TB_user.Text;
            //ketnoi.password = TB_password.Text;
            try
            {
                ketnoi.OpenConnection();
                MessageBox.Show("Ok.", "Đăng nhập thành công!");
                    main main = new main();
                    this.Hide();
                    main.ShowDialog();
            }
            catch
            {
                MessageBox.Show("No Ok.", "Đăng nhập thất bại!");
            }
        }
    }
}
