using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace SP_Ketnoi
{
    class Ketnoi
    {
/*        private string _ip_host="localhost";
        private string _database = "mydb";
        private string _user = "root";
        private string _password = "toor";*/
        private string _ip_host = "192.168.10.117";
        private string _database = "nurs_home";
        private string _user = "root";
        private string _password = "toor";
        public static String _strconnection = "Server='192.168.10.117';Database=nurs_home;Port=3306;User ID=root;Password=toor";
        public string strconnection
        {
            get { return _strconnection; }
            set { _strconnection = value; }
        }
        public string ip_host
        {
            get { return _ip_host; }
            set { _ip_host = value; }
        }
        public string database
        {
            get { return _database; }
            set { _database = value; }
        }
        public string user
        {
            get { return _user; }
            set { _user = value; }
        }
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
        MySqlConnection conecttion;
        MySqlCommand command;
        public void OpenConnection()
        {
            conecttion = new MySqlConnection(_strconnection);
            conecttion.Open();
        }
        public void CloseConnection()
        {
            conecttion.Clone();
            // conecttion.Dispose();
        }
        public void DisposeConnection()
        {
            conecttion.Dispose();
        }
        public void ExecuteNonQuery(string strQuery)
        {
            OpenConnection();
            command = conecttion.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = strQuery;
            command.ExecuteNonQuery();
            CloseConnection();
            DisposeConnection();

        }
        public MySqlDataAdapter GetDataAdapter(String strQuery)
        {
            OpenConnection();
            command = conecttion.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = strQuery;
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            CloseConnection();
            return dataAdapter;
        }
        public MySqlDataReader GetDataReader(string strQuery)
        {
            OpenConnection();
            command = conecttion.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = strQuery;
            MySqlDataReader dataReader = command.ExecuteReader();
            CloseConnection();
            return dataReader;
        }
    #region function load dữ liệu
        public DataTable GetBanGhi(string nunberSelection) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT *FROM ban_ghi  ORDER BY STT DESC LIMIT " + nunberSelection + "");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable Getdanhsachthietbi(string nunberSelection) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT *FROM profile  ORDER BY ID_ZIGBEE DESC LIMIT " + nunberSelection + "");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable Getdanhsachthietbi_ID(string ID_zigbee) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT *FROM profile where ID_ZIGBEE='" + ID_zigbee + "'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable Getthemthietbi(string nunberSelection) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT *FROM add_device  ORDER BY TIME DESC");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        #endregion
        #region function xoa dữ liệu
        public void delete_adddevice(string ID_device) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            ExecuteNonQuery("DELETE FROM add_device WHERE `ID_ZIGBEE`= '" + ID_device + "'");
        }
        public void delete_profile(string ID_device) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            ExecuteNonQuery("DELETE FROM profile WHERE `ID_ZIGBEE`= '" + ID_device + "'");
        }
        #endregion
        #region function sua dữ liệu 
        public void update_profile(string ID_device,string ADDR_zigbee, string Name, string Ngaysinh , string Diachi, string Sodienthoai) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            ExecuteNonQuery("UPDATE profile SET `ADDR_ZIGBEE`= '"+ ADDR_zigbee + "', `NAME`= '"+ Name + "', `NGAYSINH`= '"+ Ngaysinh + "', `DIACHI`= '"+ Diachi + "', `SDT`= '"+ Sodienthoai + "' WHERE `ID_ZIGBEE`= '"+ ID_device + "'");
        }
        #endregion






























        public DataTable GetBanGhi_MaSo(string MaSo) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT *FROM `mydb`.`ban_ghi_phong_hoc` where Ma_Quan_Ly like '%" + MaSo + "%' or Ma_The like '%" + MaSo + "%'  ORDER BY Stt DESC ");
            dataadapter.Fill(datareturn);
            return datareturn;
        }

        public DataTable GetBanGhi_Ten(string Ten) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT *FROM `mydb`.`ban_ghi_phong_hoc` where `Ho_Ten` like '%" + Ten + "%'  ORDER BY Stt DESC ");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetHoSo(string ChucVu) // lấy hồ sơ theo chức vụ , GIANGVIEN hoặc tên lớp 
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM mydb.du_lieu_quan_ly where Ma_Lop_Chuc_Vu='" + ChucVu + "'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
       
        public DataTable GetMonHocPhongHoc(string PhongHoc) // lấy tất cả Môn hoc theo phòng học
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM mydb.mon_hoc where Ma_Phong_Hoc like '%" + PhongHoc + "%'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }

        public DataTable GetMonHocMaMonHoc(string MonHoc) // lấy tất cả Môn hoc theo phòng học
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM mydb.mon_hoc where Ma_Mon_Hoc like '%" + MonHoc + "%'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }

        public DataTable GetPhongHocSlave() // lấy tất cả phong Hoc va slave
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT phonghoc_slave.Id_Slave,phonghoc_slave.Ma_Phong_Hoc,phong_hoc.Ma_Giang_Duong,phong_hoc.Dia_Chi FROM mydb.phonghoc_slave LEFT JOIN mydb.phong_hoc ON phonghoc_slave.Ma_Phong_Hoc=phong_hoc.Ma_Phong_Hoc ;");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetPhongHocSlave_Slave(int Slave) // Lấy thông tin phòng học qua slave 
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`phonghoc_slave` where `Id_Slave` = '" + Slave.ToString() + "'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetNameMonHoc() // lấy tất cả tên Môn hoc
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT distinct Ma_Lop_Mon_Hoc FROM mydb.mon_hoc");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetDiemDanhMonHoc(string MonHoc) // bang diem danh cua mon hoc
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM mydb.`" + MonHoc + "` ORDER BY Stt ASC");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetDiemDanhGiangVien() // bang diem danh cua giảng viên
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * `FROM mydb`.`diemdanh_giangvien`");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public void AddColumnDiemDanhMonHoc(string Monhoc, string Namecolumn) //thêm cột ngày điểm danh vào cuối tạo khi sinh viên điểm danh đầu tiên , truy vấn có cột hay chưa bằng cách so sánh ngày hệ thống với tên cột cuối cùng
        {
            ExecuteNonQuery("ALTER TABLE `mydb`.`" + Monhoc + "`; ADD COLUMN `" + Namecolumn + "` VARCHAR(45) NULL;");
        }
        public void InSetHoSo(string Maquanly, string Mathe, string Hoten, string Ngaysinh, string Machucvu, string Gioitinh, string Sodienthoai)  // thêm một hồ sơ 
        {
            ExecuteNonQuery("INSERT INTO `mydb`.`du_lieu_quan_ly` (`Ma_Quan_Ly`, `Ma_The`, `Ho_Va_Ten`, `Ngay_Sinh`, `Ma_Lop_Chuc_Vu`, `Gioi_Tinh`, `So_Dien_Thoai`) VALUES ('" + Maquanly + "', '" + Mathe + "', '" + Hoten + "', '" + Ngaysinh + "', '" + Machucvu + "', '" + Gioitinh + "', '" + Sodienthoai + "')");
        }
        public void UpdateHoSo(string Maquanly, string NewMaquanly, string Mathe, string Hoten, string Ngaysinh, string Machucvu, string Gioitinh, string Sodienthoai) // sửa một hồ sơ với mã quản lý 
        {
            ExecuteNonQuery("UPDATE `mydb`.`du_lieu_quan_ly` SET `Ma_Quan_Ly`='" + NewMaquanly + "', `Ma_The`='" + Mathe + "', `Ho_Va_Ten`='" + Hoten + "', `Ngay_Sinh`='" + Ngaysinh + "', `Ma_Lop_Chuc_Vu`='" + Machucvu + "', `Gioi_Tinh`='" + Gioitinh + "', `So_Dien_Thoai`='" + Sodienthoai + "' WHERE `Ma_Quan_Ly`='" + Maquanly + "'");
        }
        public void XoaHoSo(string Maquanly) // Xóa một hồ sơ với mã quản lý 
        {
            ExecuteNonQuery("DELETE FROM `mydb`.`du_lieu_quan_ly` WHERE `Ma_Quan_Ly`='" + Maquanly + "'");
        }


        // Tiến Viết Thêm
        public DataTable GetHoSoMathe(string MaThe) // lấy hồ sơ theo mã thẻ 
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`du_lieu_quan_ly` where `Ma_The`='" + MaThe + "'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }


        public DataTable GetBanGhi(string MaThe, string MaSinhVien)
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`ban_ghi_phong_hoc` where Ma_Quan_Ly='" + MaSinhVien + "' and Ma_The ='" + MaThe + "'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetBanGhiThoiGianRaNull(string MaThe, string MaQuanLy) // láy bản ghi có thời gian ra = null
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`ban_ghi_phong_hoc` where `Ma_The`='" + MaThe + "' and `Ma_Quan_Ly`='" + MaQuanLy + "' and `Thoi_Gian_Ra`='';");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public void updateBanGhi(string STT, string ThoiGianRa)// updatebanghi thời gian ra vào bản ghi thời gian trống 
        {
            ExecuteNonQuery("UPDATE `mydb`.`ban_ghi_phong_hoc` SET `Thoi_Gian_Ra`='" + ThoiGianRa + "' WHERE `Stt`='" + STT + "'");
        }
        public void InsertBanGhi(string MaQuanLy, string MaThe, string HoTen, string NgaySinh, string LopChucVu, string MaMonHoc, string MaPhongHoc, string MaGiangDuong, string ThoiGian) // thêm một bản ghi thời gian vào = ThơiGian , thời gian ra = null
        {
            ExecuteNonQuery("INSERT INTO `mydb`.`ban_ghi_phong_hoc` (`Ma_Quan_Ly`, `Ma_The`, `Ho_Ten`, `Ngay_Sinh`, `Ma_Lop_Chuc_vu`, `Ma_Lop_Hoc`,`Ma_Phong_Hoc`, `Ma_Giang_Duong`, `Thoi_Gian_Vao`, `Thoi_Gian_Ra`) VALUES ('" + MaQuanLy + "', '" + MaThe + "', '" + HoTen + "', '" + NgaySinh + "', '" + LopChucVu + "','" + MaMonHoc + "', '" + MaPhongHoc + "', '" + MaGiangDuong + "', '" + ThoiGian + "', '')");
        }
    }
}
