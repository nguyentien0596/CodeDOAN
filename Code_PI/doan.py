from time import strftime,localtime
import MySQLdb
import db
import serial
from multiprocessing import Process, Manager, Value
from ctypes import c_char_p,c_bool
DEBUG_APP=True

# @P 0000000007 0x0005^
# @D 0000000007 NGA_NGUA^

def serial_write(buff_serial_write): # gui du lieu 
	port.write(buff_serial_write.value.encode())
	buff_serial_write.value=""

def serial_loop(buff_serial_read,buff_serial_write,buff_serial_coppy,serial_en):
	print(serial_en.value)
	while True:
		serial_process1(buff_serial_read,buff_serial_write,buff_serial_coppy,serial_en)

def serial_process1(buff_serial_read,buff_serial_write,buff_serial_coppy,serial_en): # theard nhan du lieu ghi vao buff nhan duoc ki tu ket thuc thi chay theard xu ly du lieu 
	rev= port.read(1)
	if serial_en.value ==True:
		if rev =="^":
			buff_serial_coppy.value=buff_serial_read.value
			print (buff_serial_coppy.value)
			process_hand = Process(target=serial_hand, args=(buff_serial_write,buff_serial_coppy,))
			process_hand.start()
			process_hand.join()
			serial_en.value=False
			buff_serial_read.value =""
		else :
			if rev == "@":
				buff_serial_read.value =""
			else :
				buff_serial_read.value=buff_serial_read.value + rev
				pass
	else: 
		if rev =="@":
			serial_en.value =True
				
def serial_hand(buff_serial_write,buff_serial_coppy) :
	# try: #  neu co loi thi chay except
	string= buff_serial_coppy.value
	data = string.split( )
	if data[0]=="P":
		ID_zigbee = data[1]
		ADDR_zigbee= data[2]
		cur= db.getdatacur(ID_zigbee)
		row_count = cur.rowcount
		print(row_count)
		if row_count == 1:
			strsent = "O2M 1 " + ADDR_zigbee +" PINGOK\r\n"
			buff_serial_write.value = strsent
			serial_write(buff_serial_write)
			#thong bao ok xuong ADDR_zigbee
			print("Ping ok")
		else :
			a = db.inserttoadd_device(ID_zigbee,ADDR_zigbee)
			print(a)
			# them vao bang add device
			print("Ping error")
		print(ID_zigbee + ADDR_zigbee)
	else :
		if data[0]=="D":
			DID_zigbee = data[1]
			DDATA_zigbee=data[2]
			cur= db.getdatacur(DID_zigbee)
			row_count = cur.rowcount
			print(row_count)
			if row_count == 1:
				for profile in cur.fetchall():
					print (profile)
					print (str(profile[1]))
					DADDR_zigbee = str(profile[1])
					DTEN = str(profile[2])
					DNGAYSINH = str(profile[3])
					DDIACHI = str(profile[4])
					DSDT = str(profile[5])
					a=db.inserttobanghi(DID_zigbee,DADDR_zigbee,DTEN,DNGAYSINH,DSDT,"THONGBAO",DDATA_zigbee)
					print(a)
					print("data ok")
			else :
				# them vao bang add device
				print("data error")
	
		
	# except:
		# print("Loi chuoi json UART: " + str(buff_serial_coppy.value) ) # print ra chuoi loi
		
if __name__ =="__main__" :	
	port = serial.Serial("/dev/ttyS0", baudrate=115200, timeout=3.0) # khoi tao UART
	print(port)
	manager = Manager() # khoi rao bien 
	buff_serial_read = manager.Value(c_char_p,"")  # buff doc du lieu 
	buff_serial_write = manager.Value(c_char_p,"") # buff ghi du lieu
	buff_serial_coppy = manager.Value(c_char_p,"sfasf") # buff xu li du lieu
	
	serial_en = manager.Value(c_bool,False)				# bien cho phep nhan du lieu (khi nhan dc ki tu dau tien $)
	process_serial = Process(target=serial_loop, args=(buff_serial_read,buff_serial_write,buff_serial_coppy,serial_en,))
	process_serial.start()
	process_serial.join() 
	
	
	
	
	
	
