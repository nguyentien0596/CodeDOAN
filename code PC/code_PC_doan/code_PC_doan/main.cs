using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using SP_Ketnoi;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace code_PC_doan
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        public static String _strconnection = "Server='192.168.10.117';Database=nurs_home;Port=3306;User ID=root;Password=toor";
        public string strconnection
        {
            get { return _strconnection; }
            set { _strconnection = value; }
        }
        Ketnoi ketnoi = new Ketnoi();
        DataTable databanghi_old = new DataTable();
        DataTable datathietbi_old = new DataTable();
        DataTable datathemthietbi_old = new DataTable();
        private void main_Load(object sender, EventArgs e)
        {
            ketnoi.strconnection = _strconnection;
            timer1.Enabled = true;
            if (tabControl1.SelectedIndex == 0)
            {
                TB_xoa.Visible = false;
                BT_sua.Visible = false;
                Loadbanghi();

            }
            else if (tabControl1.SelectedIndex == 1)
            {
                TB_xoa.Visible = true;
                BT_sua.Visible = true;
                Loadthietbi();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                TB_xoa.Visible = false;
                BT_sua.Visible = false;
                Loadthemthietbi();
            }
        }
    #region function click 
        private void BT_sua_Click(object sender, EventArgs e)
        {
            string id_zig = TB_idthietbi.Text;
            DialogResult result;
            result = MessageBox.Show("Bạn có muốn sửa " + id_zig + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DataTable dataID = ketnoi.Getdanhsachthietbi_ID(id_zig);
                if (dataID.Rows.Count == 1)
                {
                    string diachizig = TB_diachithietbi.Text;
                    string ten = TB_hovaten.Text;
                    string ngaysinh = TB_ngaysinh.Text;
                    string diachi = dataID.Rows[0]["NGAYSINH"].ToString();
                    string sdt = TB_sodienthoai.Text;
                    ketnoi.update_profile(id_zig, diachizig, ten, ngaysinh, diachi, sdt);
                    Loadthietbi_1();
                    MessageBox.Show("Sửa thành công " + id_zig + " .", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Không thể sửa " + id_zig + " .", "Thông báo");
                }
            }
            Loadthietbi();

        }
        private void TB_xoa_Click(object sender, EventArgs e)
        {
            string idxoa = TB_idthietbi.Text;

            DialogResult result;
            result = MessageBox.Show("Bạn có muốn Xóa " + idxoa + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ketnoi.delete_profile(idxoa);
                Loadthietbi();
                MessageBox.Show("Xóa thành công " + idxoa + " .", "Thông báo");
            }
        }
        // chọn hàng trong DGV_trangtha
        private void DGV_trangthai_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            TB_idthietbi.Text       = DGV_trangthai.CurrentRow.Cells[1].Value.ToString();
            TB_diachithietbi.Text   = DGV_trangthai.CurrentRow.Cells[2].Value.ToString();
            TB_hovaten.Text         = DGV_trangthai.CurrentRow.Cells[3].Value.ToString();
            TB_ngaysinh.Text        = DGV_trangthai.CurrentRow.Cells[4].Value.ToString();
            TB_sodienthoai.Text     = DGV_trangthai.CurrentRow.Cells[5].Value.ToString();
            PT_hinhanh.ImageLocation = Application.StartupPath + "\\data\\img\\" + TB_idthietbi.Text + ".png";
        }
        // chọn hàng trong DGV_dsthietbi
        private void DGV_dsthietbi_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            TB_idthietbi.Text       = DGV_dsthietbi.CurrentRow.Cells[0].Value.ToString();
            TB_diachithietbi.Text   = DGV_dsthietbi.CurrentRow.Cells[1].Value.ToString();
            TB_hovaten.Text         = DGV_dsthietbi.CurrentRow.Cells[2].Value.ToString();
            TB_ngaysinh.Text        = DGV_dsthietbi.CurrentRow.Cells[3].Value.ToString();
            TB_sodienthoai.Text     = DGV_dsthietbi.CurrentRow.Cells[5].Value.ToString();
            PT_hinhanh.ImageLocation = Application.StartupPath + "\\data\\img\\" + TB_idthietbi.Text + ".png";
        }
        // lựa chọn tab
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                TB_xoa.Visible = false;
                BT_sua.Visible = false;
                Loadbanghi();

            }
            else if (tabControl1.SelectedIndex == 1)
            {
                TB_xoa.Visible = true;
                BT_sua.Visible = true;
                Loadthietbi();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                TB_xoa.Visible = false;
                BT_sua.Visible = false;
                Loadthemthietbi();
            }

        }
        // thêm một thiết bị
        private void BT_them_Click(object sender, EventArgs e)
        {

        }
        // xóa khỏi danh sách thêm 
        private void BT_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                string idxoa = DGV_themthietbi.CurrentRow.Cells[0].Value.ToString();

                DialogResult result;
                result = MessageBox.Show("Bạn có muốn Xóa " + idxoa + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ketnoi.delete_adddevice(idxoa);
                    Loadthemthietbi();
                    MessageBox.Show("Xóa thành công " + idxoa + " .", "Thông báo");
                }
            }
            catch {
            }

        }
        #endregion
        #region function load dữ liệu
        private void Loadbanghi()   // load bản ghi lên view DGV_trangthai
        {
            DataTable tabbanghi = new DataTable();
            tabbanghi = ketnoi.GetBanGhi("50");
            try
            {
                if (tabbanghi.Rows.Count != databanghi_old.Rows.Count || tabbanghi.Rows[0]["THOIGIAN"].ToString() != databanghi_old.Rows[0]["THOIGIAN"].ToString())
                {
                    DGV_trangthai.DataSource = tabbanghi;
                    DGV_trangthai.Columns[0].HeaderText = "STT";
                    DGV_trangthai.Columns[1].HeaderText = "ID thiết bị";
                    DGV_trangthai.Columns[2].HeaderText = "Địa chỉ Thết bị";
                    DGV_trangthai.Columns[3].HeaderText = "Họ và Tên";
                    DGV_trangthai.Columns[4].HeaderText = "Ngày Sinh";
                    DGV_trangthai.Columns[5].HeaderText = "Số điện thoại";
                    DGV_trangthai.Columns[6].HeaderText = "Báo động";
                    DGV_trangthai.Columns[7].HeaderText = "Trạng thái";
                    DGV_trangthai.Columns[8].HeaderText = "Thời Gian";
                    databanghi_old = tabbanghi;
                }
            }
            catch
            {
                DGV_trangthai.DataSource = tabbanghi;
                DGV_trangthai.Columns[0].HeaderText = "STT";
                DGV_trangthai.Columns[1].HeaderText = "ID thiết bị";
                DGV_trangthai.Columns[2].HeaderText = "Địa chỉ Thết bị";
                DGV_trangthai.Columns[3].HeaderText = "Họ và Tên";
                DGV_trangthai.Columns[4].HeaderText = "Ngày Sinh";
                DGV_trangthai.Columns[5].HeaderText = "Số điện thoại";
                DGV_trangthai.Columns[6].HeaderText = "Báo động";
                DGV_trangthai.Columns[7].HeaderText = "Trạng thái";
                DGV_trangthai.Columns[8].HeaderText = "Thời Gian";
                databanghi_old = tabbanghi;
            }
        }
        private void Loadthietbi()   // load bản ghi lên view DGV_trangthai
        {
            DataTable tabthietbi = new DataTable();
            tabthietbi = ketnoi.Getdanhsachthietbi("50");
            try
            {
                if (tabthietbi.Rows.Count != datathietbi_old.Rows.Count || tabthietbi.Rows[0]["ID_ZIGBEE"].ToString() != datathietbi_old.Rows[0]["ID_ZIGBEE"].ToString())
                {
                    DGV_dsthietbi.DataSource = tabthietbi;
                    DGV_dsthietbi.Columns[0].HeaderText = "ID Thiết bị";
                    DGV_dsthietbi.Columns[1].HeaderText = "Địa chỉ Thết bị";
                    DGV_dsthietbi.Columns[2].HeaderText = "Họ và Tên";
                    DGV_dsthietbi.Columns[3].HeaderText = "Ngày Sinh";
                    DGV_dsthietbi.Columns[4].HeaderText = "Địa chỉ";
                    DGV_dsthietbi.Columns[5].HeaderText = "Số điện thoại";
                    datathietbi_old = tabthietbi;
                }
            }
            catch {
                DGV_dsthietbi.DataSource = tabthietbi;
                DGV_dsthietbi.Columns[0].HeaderText = "ID Thiết bị";
                DGV_dsthietbi.Columns[1].HeaderText = "Địa chỉ Thết bị";
                DGV_dsthietbi.Columns[2].HeaderText = "Họ và Tên";
                DGV_dsthietbi.Columns[3].HeaderText = "Ngày Sinh";
                DGV_dsthietbi.Columns[4].HeaderText = "Địa chỉ";
                DGV_dsthietbi.Columns[5].HeaderText = "Số điện thoại";
                datathietbi_old = tabthietbi;
            }

        }
        private void Loadthemthietbi()   // load bản ghi lên view DGV_trangthai
        {
            DataTable tabthemthietbi = new DataTable();
            tabthemthietbi = ketnoi.Getthemthietbi("50");
            try
            {
                if (tabthemthietbi.Rows.Count != datathemthietbi_old.Rows.Count || tabthemthietbi.Rows[0]["ID_ZIGBEE"].ToString() != datathemthietbi_old.Rows[0]["ID_ZIGBEE"].ToString())
                {
                    DGV_themthietbi.DataSource = tabthemthietbi;
                    DGV_themthietbi.Columns[0].HeaderText = "ID Thiết bị";
                    DGV_themthietbi.Columns[1].HeaderText = "Địa chỉ Thết bị";
                    DGV_themthietbi.Columns[1].HeaderText = "Thời gian";
                    datathemthietbi_old = tabthemthietbi;
                }
            }
            catch
            {
                DGV_themthietbi.DataSource = tabthemthietbi;
                DGV_themthietbi.Columns[0].HeaderText = "ID Thiết bị";
                DGV_themthietbi.Columns[1].HeaderText = "Địa chỉ Thết bị";
                DGV_themthietbi.Columns[1].HeaderText = "Thời gian";
                datathemthietbi_old = tabthemthietbi;
            }
        }
        private void Loadthietbi_1()   // load bản ghi lên view DGV_trangthai
        {
            DataTable tabthietbi = new DataTable();
            tabthietbi = ketnoi.Getdanhsachthietbi("50");
            DGV_dsthietbi.DataSource = tabthietbi;
            DGV_dsthietbi.Columns[0].HeaderText = "ID Thiết bị";
            DGV_dsthietbi.Columns[1].HeaderText = "Địa chỉ Thết bị";
            DGV_dsthietbi.Columns[2].HeaderText = "Họ và Tên";
            DGV_dsthietbi.Columns[3].HeaderText = "Ngày Sinh";
            DGV_dsthietbi.Columns[4].HeaderText = "Địa chỉ";
            DGV_dsthietbi.Columns[5].HeaderText = "Số điện thoại";
            datathietbi_old = tabthietbi;
        }
        private void Loadthemthietbi_1()   // load bản ghi lên view DGV_trangthai
        {
            DataTable tabthemthietbi = new DataTable();
            tabthemthietbi = ketnoi.Getthemthietbi("50"); 
            DGV_themthietbi.DataSource = tabthemthietbi;
            DGV_themthietbi.Columns[0].HeaderText = "ID Thiết bị";
            DGV_themthietbi.Columns[1].HeaderText = "Địa chỉ Thết bị";
            DGV_themthietbi.Columns[1].HeaderText = "Thời gian";
            datathemthietbi_old = tabthemthietbi;
        }





        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                Loadbanghi();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                Loadthietbi();
            }
            else {
                Loadthemthietbi();
            }
        }
    }
}
