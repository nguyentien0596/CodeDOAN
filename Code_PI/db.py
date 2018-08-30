from time import strftime,localtime
import MySQLdb
DEBUG_APP=True
# Mysql connection
def connect():
    return MySQLdb.connect(host="localhost" , user="root", passwd="toor" , db="nurs_home")
def getdatacur(id_zigbee):
	db = connect()
	cur=db.cursor()
	cur.execute("""SELECT * FROM nurs_home.profile where ID_ZIGBEE=%s;""",id_zigbee)
	row_count = cur.rowcount
	if DEBUG_APP==True:
		print ("getdata.row_count:"+str(row_count))
	return cur
	db.close()
	
def checknulldata(curcheck) :
	row_count = curcheck.rowcount
	if DEBUG_APP==True:
		print (row_count)
	if row_count==1:
		if DEBUG_APP==True:
			print ("checknull:check ok)")
		return 1;
	else :
		if DEBUG_APP==True:
			print ("checknull:check not")
		return 0;

def inserttobanghi(id_zigbee,addr_zigbee,name,ngaysinh,sdt,thongbao,trangthai):
	try : # neu loi insert thi thong bao loi 
		thoigian= strftime("%d-%m-%Y %H:%M:%S", localtime())
		db = connect()
		cur=db.cursor()
		cur.execute("""INSERT INTO ban_ghi (ID_ZIGBEE, ADDR_ZIGBEE, TEN, NGAYSINH, SODT, THONGBAO, TRANGTHAI, THOIGIAN) VALUES (%s, %s, %s,%s, %s, %s, %s, %s)""",(id_zigbee,addr_zigbee,name,ngaysinh,sdt,thongbao,trangthai,thoigian))
		db.commit()
		db.close()
		action =True
	except :
		action =False
	return action	
		
def inserttoadd_device(id_zigbee,addr_zigbee):
	try : # neu loi insert thi thong bao loi 
		thoigian= strftime("%d-%m-%Y %H:%M:%S", localtime())
		db = connect()
		cur=db.cursor()
		cur.execute("""INSERT INTO add_device (ID_ZIGBEE, ADDR_ZIGBEE, TIME) VALUES (%s, %s, %s)""",(id_zigbee,addr_zigbee,thoigian))
		db.commit()
		db.close()
		action =True

	except :
		action =False
	return action	
	
	