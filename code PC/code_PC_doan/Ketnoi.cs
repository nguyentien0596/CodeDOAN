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
        //public static String strconnection = "Server='"_"';Database=nurs_home;Port=3306;User ID=root;Password=toor";
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
            conecttion = new MySqlConnection("Server='"+ip_host+"';Database="+_database+";Port=3306;User ID="+_user+";Password="+_password+"");
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


        public string GetLoginLever(string Tennguoidung, string Matkhau) // lấy thông số đăng nhập , trả về admin , user , lỗi mật khẩu  errorPass, lỗi kết nối errorconection
        {
            string Return = "errorconection";
            OpenConnection();
            string strQuery = "SELECT * FROM mydb.tai_khoan where Ten_Nguoi_Dung='" + Tennguoidung + "' and Mat_Khau ='" + Matkhau + "' ";
            command = conecttion.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = strQuery;
            MySqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                Return = dataReader["Ma_Vai_Tro"].ToString();
            }
            else
            {
                Return = "errorPass";
            }
            conecttion.Close();
            conecttion.Dispose();
            return Return;
        }
        public DataTable GetTaiKhoan() // lấy tất cả Tài khoản
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`tai_khoan`");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public bool GetTaiKhoan_TenTruyNhap(string TenTruyNhap) // lấy tất cả Tài khoản
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM mydb.tai_khoan where Ten_Nguoi_Dung='" + TenTruyNhap + "'");
            dataadapter.Fill(datareturn);
            if (datareturn.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }
        public void InsertTaiKhoan(string TenTruyNhap, string MatKhau, string MaVaiTro)
        {
            ExecuteNonQuery("INSERT INTO `mydb`.`tai_khoan` ( `Ten_Nguoi_Dung`, `Mat_Khau`, `Ma_Vai_Tro`) VALUES ('" + TenTruyNhap + "', '" + MatKhau + "', '" + MaVaiTro + "')");
        }
        public void UpdateTaiKhoan(string TenTruyNhap, string TenTruyNhap_new, string MatKhau, string MaVaiTro)
        {
            ExecuteNonQuery("UPDATE `mydb`.`tai_khoan` SET `Ten_Nguoi_Dung`='" + TenTruyNhap_new + "', `Mat_Khau`='" + MatKhau + "', `Ma_Vai_Tro`='" + MaVaiTro + "' WHERE `Ten_Nguoi_Dung`='" + TenTruyNhap + "'");
        }
        public void DelTaiKhoan(string TenTruyNhap)
        {
            ExecuteNonQuery("DELETE FROM `mydb`.`tai_khoan` WHERE `Ten_Nguoi_Dung`='" + TenTruyNhap + "'");
        }
        public DataTable GetNameGiangDuong() // dùng để lấy giá trị tên giảng đường
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT Ma_Phong_Hoc FROM mydb.phonghoc_slave");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetDataGiangDuong(string nameGiangDuong, string nunberSelection)  // lấy ra số lượng nunberSelection bản ghi của bản ghi giảng đường nameGiangDuong
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT *FROM `ban_ghi_phong_hoc` where Ma_Phong_Hoc='" + nameGiangDuong + "' ORDER BY Stt DESC LIMIT " + nunberSelection + "");
            dataadapter.Fill(datareturn);
            return datareturn;
        }

        public DataTable GetNameMaChucVu() // dùng để lấy giá trị tên giảng đường
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT DISTINCT Ma_Lop_Chuc_Vu FROM `mydb`.`du_lieu_quan_ly`");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetBanGhi(string nunberSelection) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT *FROM `ban_ghi_phong_hoc`  ORDER BY Stt DESC LIMIT " + nunberSelection + "");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
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
        public DataTable GetHoSoMaQuanLy(string Maquanly) // lấy hồ sơ theo ma quan ly
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM mydb.du_lieu_quan_ly where `Ma_Quan_Ly` = '" + Maquanly + "'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetHoSoSinhVien() // lấy hồ sơ tất cả sinh viên
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM mydb.du_lieu_quan_ly where Ma_Lop_Chuc_Vu!='GIANGVIEN' AND Ma_Lop_Chuc_Vu!='NHANVIEN'");
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
        public DataTable GetChucVu() // lấy tất cả các tên Lớp học
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT distinct Ma_Lop_Chuc_Vu FROM mydb.du_lieu_quan_ly");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetThongTinMonHoc(string GiangDuong) // lây thông tin môn học của giảng đường hiện tại
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("");
            dataadapter.Fill(datareturn);
            return datareturn;
        }

        public void InSetTaiKhoan(string Maquanly, string Tennguoidung, string Matkhau, string Vaitro)  // thêm một tài khoản
        {
            ExecuteNonQuery("INSERT INTO `mydb`.`tai_khoan` (`Ma_Quan_Ly`, `Ten_Nguoi_Dung`, `Mat_Khau`, `Ma_Vai_Tro`) VALUES ('" + Maquanly + "', '" + Tennguoidung + "', '" + Matkhau + "', '" + Vaitro + "')");
        }

        public DataTable GetMonHoc() // lấy tất cả Môn hoc
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM mydb.mon_hoc;");
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

        public string GetMonhoc_Now(string MaPhongHoc, DateTime thoigian_now)
        {
            string ThoiGianGio_Now = thoigian_now.ToString("HH:mm");

            DataTable tableMonHoc = new DataTable();
            DataTable tableMonHoc_Mamonhoc = new DataTable();
            MySqlDataAdapter dataadapter;
            dataadapter = GetDataAdapter("SELECT * FROM mydb.thoigian_hoc where Thoi_Gian_Bat_Bat_Dau_Diem_Danh < '" + ThoiGianGio_Now + "' and Thoi_Gian_Bat_Ket_Thuc_Diem_Danh >= '" + ThoiGianGio_Now + "' ");
            dataadapter.Fill(tableMonHoc);
            if (tableMonHoc.Rows.Count == 0)
            {  // không có trong thời gian điểm danh

                return "Khong Co";
            }
            else
            {  // trong thoi gian điểm danh , truy vấn lấy thứ 
                int dayofweek = (int)thoigian_now.DayOfWeek + 1; // lấy thứ  1: chủ nhật, 2: thứ 2 , 3 : thứ 3, 4: thứ 4 , 5: thứ 5 , 6: thứ 6, 7: thứ 7  
                if (dayofweek == 1) // nếu chủ nhật 
                {
                    dayofweek = 8;
                }
                dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`mon_hoc` where Ma_Phong_Hoc='" + MaPhongHoc + "' and Thu_Trong_Tuan ='" + dayofweek.ToString() + "' and Thoi_Gian_Bat_Dau='" + tableMonHoc.Rows[0][0].ToString() + "' ");//
                dataadapter.Fill(tableMonHoc_Mamonhoc);
                if (tableMonHoc_Mamonhoc.Rows.Count == 0)
                {
                    return "Khong Co";
                }
                else
                {
                    return tableMonHoc_Mamonhoc.Rows[0]["Ma_Lop_Mon_Hoc"].ToString();
                }

            }
        }


        public bool SetDiemDanhSinhVien(string MaQuanLy, string MonHoc, DateTime ThoiGian)
        {   // truy nhập xem có tên sinh viên không , xem cột cuối cùng có trùng ngày , có thì update điểm danh , không thì thêm cột và update 
            string ngaythang = ThoiGian.ToString("yyyy-MM-dd");
            DataTable tablediemdanh = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`" + MonHoc + "`where `Ma_Quan_Ly`='" + MaQuanLy + "'");
            dataadapter.Fill(tablediemdanh);
            if (tablediemdanh.Rows.Count == 0)
            {
                return false;
            }
            else
            {

                //Int32 songayvang= Int32.Parse(tablediemdanh.Rows[0]["So_Ngay_Vang"].ToString())+1;
                DataTable tablediemdanh_check = new DataTable();
                dataadapter = GetDataAdapter("SHOW columns FROM `mydb`.`" + MonHoc + "`");
                dataadapter.Fill(tablediemdanh_check);
                if (tablediemdanh_check.Rows[tablediemdanh_check.Rows.Count - 1][0].ToString() != ngaythang)
                {

                    // thêm cột ngày tháng
                    DataTable diemdanh = new DataTable();
                    ExecuteNonQuery("ALTER TABLE `mydb`.`" + MonHoc + "` ADD COLUMN `" + ngaythang + "` INT NULL");
                    diemdanh = GetDiemDanhMonHoc(MonHoc);

                    for (int n = 0; n < diemdanh.Rows.Count; n++)
                    {
                        ExecuteNonQuery("UPDATE `mydb`.`" + MonHoc + "` SET `So_Ngay_Vang`='" + ((int)diemdanh.Rows[n][5] + 1).ToString() + "', `" + ngaythang + "`='0' WHERE `Ma_Quan_Ly`='" + diemdanh.Rows[n][1].ToString() + "';");
                    }
                    dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`" + MonHoc + "`where `Ma_Quan_Ly`='" + MaQuanLy + "'");
                    tablediemdanh.Clear();
                    dataadapter.Fill(tablediemdanh);
                    ExecuteNonQuery("UPDATE `mydb`.`" + MonHoc + "` SET `So_Ngay_Vang`='" + ((int)tablediemdanh.Rows[0][5] - 1).ToString() + "', `" + ngaythang + "`='1' WHERE `Ma_Quan_Ly`='" + MaQuanLy + "'");//UPDATE `mydb`.`" + MonHoc + "` SET `" + ngaythang + "`='1' WHERE `Ma_Quan_Ly`='" + MaQuanLy + "'");
                    return true;
                }
                else
                {
                    dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`" + MonHoc + "`where `Ma_Quan_Ly`='" + MaQuanLy + "'");
                    dataadapter.Fill(tablediemdanh);
                    if (tablediemdanh.Rows[0][tablediemdanh.Columns.Count - 1].ToString() != "1")
                    {
                        ExecuteNonQuery("UPDATE `mydb`.`" + MonHoc + "` SET `So_Ngay_Vang`='" + ((int)tablediemdanh.Rows[0][5] - 1).ToString() + "', `" + ngaythang + "`='1' WHERE `Ma_Quan_Ly`='" + MaQuanLy + "'");
                    }
                    return true;
                }

            }

        }
        public DataTable GetTimKiemSinhVien(string TimKiem, string Lever)
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter;
            if (Lever == "Ten")
            {
                dataadapter = GetDataAdapter("SELECT * FROM mydb.du_lieu_quan_ly where `Ho_Va_Ten` like '%" + TimKiem + "%' ;");
                dataadapter.Fill(datareturn);
            }
            else if (Lever == "MaSo")
            {
                dataadapter = GetDataAdapter("SELECT * FROM mydb.du_lieu_quan_ly where `Ma_Quan_Ly` like '%" + TimKiem + "%';");
                dataadapter.Fill(datareturn);
            }
            return datareturn;
        }

        public DataTable GetHoSo_MaQL_MaThe(string MaQuanLy, string MaThe) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`du_lieu_quan_ly` where `Ma_Quan_Ly`= '" + MaQuanLy + "' and `Ma_The`= '" + MaThe + "'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public DataTable GetHoSo_MaThe(string MaThe) // lấy ra nunberSelection bản ghi sắp xếp theo giảm dần không phân loại phòng
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`du_lieu_quan_ly` where `Ma_The`= '" + MaThe + "'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }
        public void DelTableMonHoc(string MonHoc)
        {
            ExecuteNonQuery("DROP TABLE `mydb`.`" + MonHoc + "`");
        }
        public void CreatetableMonHoc(string MonHoc)
        {
            //
        }

        public DataTable GetTimKiemSinhVien_MonHoc(string MonHoc, string TimKiem, string Lever)
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter;
            if (Lever == "Ten")
            {
                dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`" + MonHoc + "` where `Ho_Va_Ten` like '%" + TimKiem + "%';");
                dataadapter.Fill(datareturn);
            }
            else if (Lever == "MaSo")
            {
                dataadapter = GetDataAdapter("SELECT * FROM `mydb`.`" + MonHoc + "` where `Ma_Quan_Ly` like '%" + TimKiem + "%';");
                dataadapter.Fill(datareturn);
            }
            return datareturn;
        }

        public DataTable GetMonHoc_MaLopMonHoc(string MaLopMonHoc)
        {
            DataTable datareturn = new DataTable();
            MySqlDataAdapter dataadapter = GetDataAdapter("SELECT * FROM mydb.mon_hoc where Ma_Lop_Mon_Hoc = '" + MaLopMonHoc + "'");
            dataadapter.Fill(datareturn);
            return datareturn;
        }

        public void InsertMonHoc(string Ma_Lop_Mon_Hoc, string Ma_Mon_Hoc, string Ten_Mon_Hoc, string Giang_Vien_Day, string Ma_Phong_Hoc, string So_Luong_Sinh_Vien, string Thu_Trong_Tuan, string Thoi_Gian_Bat_Dau, string Thoi_Gian_Ket_Thuc, string So_Tin_Chi)
        {
            ExecuteNonQuery("INSERT INTO `mydb`.`mon_hoc` (`Ma_Lop_Mon_Hoc`, `Ma_Mon_Hoc`, `Ten_Mon_Hoc`, `Giang_Vien_Day`, `Ma_Phong_Hoc`, `So_Luong_Sinh_Vien`, `Thu_Trong_Tuan`, `Thoi_Gian_Bat_Dau`, `Thoi_Gian_Ket_Thuc`, `So_Tin_Chi`) VALUES ('" + Ma_Lop_Mon_Hoc + "', '" + Ma_Mon_Hoc + "', '" + Ten_Mon_Hoc + "', '" + Giang_Vien_Day + "', '" + Ma_Phong_Hoc + "', '" + So_Luong_Sinh_Vien + "', '" + Thu_Trong_Tuan + "', '" + Thoi_Gian_Bat_Dau + "', '" + Thoi_Gian_Ket_Thuc + "', '" + So_Tin_Chi + "')");
        }
        public void UpdateMonHoc(string MaLopMonHoc, string Ma_Lop_Mon_Hoc_New, string Ma_Mon_Hoc, string Ten_Mon_Hoc, string Giang_Vien_Day, string Ma_Phong_Hoc, string So_Luong_Sinh_Vien, string Thu_Trong_Tuan, string Thoi_Gian_Bat_Dau, string Thoi_Gian_Ket_Thuc, string So_Tin_Chi)
        {
            ExecuteNonQuery("UPDATE `mydb`.`mon_hoc` SET `Ma_Lop_Mon_Hoc`='" + Ma_Lop_Mon_Hoc_New + "', `Ma_Mon_Hoc`='" + Ma_Mon_Hoc + "', `Ten_Mon_Hoc`='" + Ten_Mon_Hoc + "', `Giang_Vien_Day`='" + Giang_Vien_Day + "', `Ma_Phong_Hoc`='" + Ma_Phong_Hoc + "', `So_Luong_Sinh_Vien`='" + So_Luong_Sinh_Vien + "', `Thu_Trong_Tuan`='" + Thu_Trong_Tuan + "', `Thoi_Gian_Bat_Dau`='" + Thoi_Gian_Bat_Dau + "', `Thoi_Gian_Ket_Thuc`='" + Thoi_Gian_Ket_Thuc + "', `So_Tin_Chi`='" + So_Tin_Chi + "' WHERE `Ma_Lop_Mon_Hoc`='" + MaLopMonHoc + "'");
        }



        public void DelMonHoc(string MaLopMonHoc)
        {
            ExecuteNonQuery("DELETE FROM `mydb`.`mon_hoc` WHERE `Ma_Lop_Mon_Hoc`='" + MaLopMonHoc + "'");
        }
        public void TaobangMonHoc(string MaLopMonHoc)
        {
            ExecuteNonQuery("DROP TABLE IF EXISTS `" + MaLopMonHoc + "`");
            ExecuteNonQuery("CREATE TABLE IF NOT EXISTS `mydb`.`" + MaLopMonHoc + "` ( `Stt` int NOT NULL, `Ma_Quan_Ly` VARCHAR(8) NOT NULL, `Ho_Va_Ten` VARCHAR(45) NULL, `Ngay_Sinh` DATE NULL, `Ghi_Chu` VARCHAR(45) NULL, `So_Ngay_Vang` INT NULL, PRIMARY KEY (`Ma_Quan_Ly`)) ENGINE = InnoDB;");
        }


        public void DoiTenBangMonHoc(string MaLopMonHoc, string MaLopMonHoc_New)
        {
            ExecuteNonQuery("ALTER TABLE `mydb`.`" + MaLopMonHoc + "` RENAME TO  `mydb`.`" + MaLopMonHoc_New + "` ;");
        }

        public void XoaBangMonHoc(string MaLopMonHoc)
        {
            ExecuteNonQuery("DROP TABLE IF EXISTS `" + MaLopMonHoc + "`");
        }

        //Tiến Viết Thêm


    }
}
