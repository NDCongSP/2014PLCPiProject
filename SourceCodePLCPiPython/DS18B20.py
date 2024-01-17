import os
import glob
import time
import exp_595_Temp #module xuat 595 hien thi led7
base_dir = '/sys/bus/w1/devices/'
error_dir = '/media/error.txt'
def read_temp_raw():
        try:      
                device_folder = glob.glob(base_dir + '28*')[0]
                device_file = device_folder + '/w1_slave'
        except:
                device_file = error_dir
	f = open(device_file, 'r')
	lines = f.readlines()
	f.close()
	return lines
def HienThi(Data,So):
#so duong
        if So == 2:
                Display_Array[7] = 0
                Display_Array[6] = 0
                Display_Array[5] = Data//10
                Display_Array[4] = Data%10
                DauCham = 0
                exp_595_Temp.export_595(Display_Array,DauCham)
        if So == 3:
                Display_Array[7] = 0
                Display_Array[6] = Data//100
                Display_Array[5] = (Data//10)%10
                Display_Array[4] = Data%10
                DauCham = 0
                exp_595_Temp.export_595(Display_Array,DauCham)
        if So == 4:
                Display_Array[7] = Data//1000
                Display_Array[6] = (Data//100)%10
                Display_Array[5] = (Data//10)%10
                Display_Array[4] = Data%10
                DauCham = 0
                exp_595_Temp.export_595(Display_Array,DauCham)
        if So == 5:
                Display_Array[7] = Data//10000
                Display_Array[6] = (Data/1000)%10
                Display_Array[5] = (Data//100)%10
                Display_Array[4] = (Data//10)%10
                DauCham = 1
                exp_595_Temp.export_595(Display_Array,DauCham)
#so am
        if So == 22:
                Display_Array[7] = 11
                Display_Array[6] = 0
                Display_Array[5] = Data//10
                Display_Array[4] = Data%10
                DauCham = 0
                exp_595_Temp.export_595(Display_Array,DauCham)
        if So == 23:
                Display_Array[7] = 11
                Display_Array[6] = Data//100
                Display_Array[5] = (Data%100)//10
                Display_Array[4] = Data%10
                DauCham = 0
                exp_595_Temp.export_595(Display_Array,DauCham)
        if So == 24:
                Display_Array[7] = 11
                Display_Array[6] = Data//100
                Display_Array[5] = (Data%100)//10
                Display_Array[4] = Data%10
                DauCham = 1
                exp_595_Temp.export_595(Display_Array,DauCham)

def ham_error() :# khi ko co cam bien
                Display_Array[7] = 14
                Display_Array[6] = 12
                Display_Array[5] = 13
                Display_Array[4] = 13
                exp_595_Temp.export_595(Display_Array,3)

def ham_khong() :# khi nhiet do =0
                Display_Array[7] = 0
                Display_Array[6] = 0
                Display_Array[5] = 0
                Display_Array[4] = 0
                DauCham = 0
                exp_595_Temp.export_595(Display_Array,DauCham)
        
def Doc_NhietDo():
        lines = read_temp_raw()
	while lines[0].strip()[-3:] != 'YES':
                time.sleep(0.2)
                lines = read_temp_raw()
                temp_c = 955
                ham_error()
                return temp_c
        
	equals_pos = lines[1].find('t=')
	if equals_pos != -1:
                temp_string = lines[1][equals_pos+2:]
                print (temp_string)
                temp_c = float(temp_string) / 1000
                
                if temp_c < 0:
                        if temp_c > -1:
                                Temp = int(temp_string[1]+temp_string[2])
                                So = 22
                                HienThi(Temp,So)
                        if (temp_c <= -1)and(temp_c > -10):
                                Temp = int(temp_string[1]+temp_string[2]+temp_string[3])
                                So = 23
                                HienThi(Temp,So)
                        if temp_c <= -10:
                                Temp = int(temp_string[1]+temp_string[2]+temp_string[3])
                                So = 24
                                HienThi(Temp,So)
                if temp_c == 0:
                        ham_khong()

                if temp_c > 0:
                        Temp = int(temp_c*100)
                        So = len(str(Temp))
                        HienThi(Temp,So)
                
                return temp_c
