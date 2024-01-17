from time import gmtime, strftime
import os
#=====================================================================
#cai dat thoi gian
#=====================================================================
def Set_RTC(data):
    os.system(data)
    os.system('sudo hwclock -w')
    os.system('sudo hwclock -s')
#=====================================================================
#doc thoi gian day du
#=====================================================================    
def Full_Time():
    Time_Full = strftime("%w, %d %m %Y %H:%M:%S")#lay thoi gian tu he thong
    '''
    '2, 12, 03, 2015, 12, 45, 23'
    %w: day of week 0-6 (0:Sunday)
    %d: day of month 01-31
    %m: month 01-12
    %Y: year xxxx
    %H: hour 00-23
    %M: minute 00-59
    %S: second 00-61
    '''
    Time_Array[0] = int(Time_Full[0]) #day of week
    Time_Array[1] = int(Time_Full[3:5]) #day of month
    Time_Array[2] = int(Time_Full[6:8]) #month
    Time_Array[3] = int(Time_Full[9:13]) #year
    Time_Array[4] = int(Time_Full[14:16]) #hour
    Time_Array[5] = int(Time_Full[17:19]) #minute
    Time_Array[6] = int(Time_Full[20:]) #second
    return(Time_Array)
#=====================================================================
#doc thu trong tuan
#=====================================================================
def Day_Of_Week():
    return(int(strftime('%w')))
#=====================================================================
#doc ngay trong thang
#=====================================================================
def Day_Of_Month():
    return(int(strftime('%d')))
#=====================================================================
#doc thang
#=====================================================================
def Month():
    return(int(strftime('%m')))
#=====================================================================
#doc nam
#=====================================================================
def Year():
    return(int(strftime('%Y')))
#=====================================================================
#doc gio
#=====================================================================
def Hour():
    return(int(strftime('%H')))
#=====================================================================
#doc phut
#=====================================================================
def Minute():
    return(int(strftime('%M')))
#=====================================================================
#doc giay
#=====================================================================
def Second():
    return(int(strftime('%S')))
