using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace DOC_GHI_DATABASE
{
    public class GhiMySQL
    {
        public string ChuoiKetnoiMySQL ;
        public  MySqlConnection connectmysql;
        static SqlConnection connectmysqlserver;
        static string TGBDTemp = "";

        public string KetnoiMySQL()
        {
            try
            {
                connectmysql = new MySqlConnection(ChuoiKetnoiMySQL);
                connectmysql.Open();
                return "GOOD";
            }
            catch { return "BAD"; }
        }

       

        public string NgatKetnoiMySQL()
        {
            try
            {
                connectmysql.Dispose();
                return "GOOD";
            }
            catch { return "BAD"; }

        }

        public string NgatKetnoiMySQLserver()
        {
            try
            {
                connectmysqlserver.Dispose();
                return "GOOD";
            }
            catch { return "BAD"; }

        }
        /// <summary>
        /// Doc du lieu MySQL, doc toan bo du lieu cua mysql.
        /// </summary>
        /// <returns>tra ve Datatable, neu = null doc ko thanh cong.</returns>
        public DataTable DocMySQLserver(string tenbang)
        {
            try
            {
                DataTable TableReturn = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM " + tenbang  , connectmysqlserver);
                ad.Fill(TableReturn);

                return TableReturn;
            }
            catch { return null; }
        }
        public DataTable DocMySQL(string tenbang)
        {
            try
            {
                DataTable TableReturn = new DataTable();
                MySqlDataAdapter ad = new MySqlDataAdapter("SELECT * FROM " + tenbang, connectmysql);
                ad.Fill(TableReturn);

                return TableReturn;
            }
            catch { return null; }
        }


       
        

        public string GhiDuLieuVaoBangdata(string[] MangGiaTRi)
        {
            try
            {
                string ttreturn = "";
                if (MangGiaTRi[0] != TGBDTemp)
                {
                    int y = 0;
                    string MySqlCmd = "insert into data values(";
                    foreach (string b in MangGiaTRi)
                    {
                        if (y < MangGiaTRi.Length - 1)
                            MySqlCmd = MySqlCmd + "'" + b + "',";
                        else
                            MySqlCmd = MySqlCmd + "'" + b + "')";

                        y++;
                    }
                    MySqlCommand cmd = new MySqlCommand(MySqlCmd, connectmysql);
                    cmd.ExecuteNonQuery();
                    TGBDTemp = MangGiaTRi[0];
                    ttreturn = "GOOD";
                }
                else
                {                    
                    ttreturn = "Trung";
                }
                return ttreturn;
            }
            catch { return "BAD"; }
        }

        public string GhiDuLieuVaoBangalarm(string[] MangGiaTRi)
        {
            try
            {
                string ttreturn = "";
                if (MangGiaTRi[0] != TGBDTemp)
                {
                    int y = 0;
                    string MySqlCmd = "insert into alarm values(";
                    foreach (string b in MangGiaTRi)
                    {
                        if (y < MangGiaTRi.Length - 1)
                            MySqlCmd = MySqlCmd + "'" + b + "',";
                        else
                            MySqlCmd = MySqlCmd + "'" + b + "')";

                        y++;
                    }
                    MySqlCommand cmd = new MySqlCommand(MySqlCmd, connectmysql);
                    cmd.ExecuteNonQuery();
                    TGBDTemp = MangGiaTRi[0];
                    ttreturn = "GOOD";
                }
                else
                {
                    ttreturn = "Trung";
                }
                return ttreturn;
            }
            catch { return "BAD"; }
        }


        public string XoaRecord(string sodong)
        {
            try
            {
                string MySqlCmd = "delete from data where Thoigian>0 limit " + sodong;
                MySqlCommand cmd = new MySqlCommand(MySqlCmd, connectmysql);
                cmd.ExecuteNonQuery();
                return "GOOD";
            }
            catch { return "BAD"; }
        }

        public string DemRecord()
        {
            try
            {
                string MySqlCmd = "SELECT COUNT(*) FROM data";
                MySqlCommand cmd = new MySqlCommand(MySqlCmd, connectmysql);
                return Convert.ToString(cmd.ExecuteScalar());
            }
            catch { return "BAD"; }
        }

        //ket noi bang user
        public DataTable DocMySQL1()
        {
            try
            {
                DataTable TableReturn = new DataTable();
                MySqlDataAdapter ad = new MySqlDataAdapter("SELECT * FROM user", connectmysql);
                ad.Fill(TableReturn);

                return TableReturn;
            }
            catch { return null; }
        }

        
        public string CapnhatDulieu(string giatriupdate)
        {
            try
            {
                string MysqlCmd = "update user set pass='" + giatriupdate + "'" + "where name='admin'";
                MySqlCommand cmd = new MySqlCommand(MysqlCmd, connectmysql);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }

        public string CapnhatDulieunguong(string gt1, string gt2)
        {
            try
            {
                string MysqlCmd = "update nguong set high='" + gt1 + "'" + "," + "low='" + gt2 + "'" + " where stt='1'";
                MySqlCommand cmd = new MySqlCommand(MysqlCmd, connectmysql);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }


        public string CapnhatDulieudatalocal(string tenbang, string mahang, string tg, string gt4, string gt5, string gt6, string gt7, string gt8, string gt9, string gt10, string gt11, string gt12, string gt13, string gt14, string gt15, string gt16, string gt17, string gt18)
        //public string CapnhatDulieudatalocal(string tenbang, string mahang, string gt4)
        {
            try
            {
                string MysqlCmd = "update " + tenbang + " set soluongkiemdat1='" + gt4 + "'" + "," + "soluongkiemdat2='" + gt5 + "'" + "," + "soluongkiemdat3='" + gt6 + "'" + "," + "soluongkiemdat4='" + gt7 + "'" + "," + "soluongkiemdat5='" + gt8 + "'" + "," + "soluongkiemdat6='" + gt9 + "'" + "," + "soluongkiemdat7='" + gt10 + "'" + "," + "soluongkiemdat8='" + gt11 + "'" + "," + "soluongkiemdat9='" + gt12 + "'" + "," + " thuchienthantruoc='" + gt13 + "'" + "," + "thuchienthansau='" + gt14 + "'" + "," + "thuchienhamay='" + gt15 + "'" + "," + "qcdat='" + gt16 + "'" + "," + "songayconlai='" + gt17 + "'" + "," + "qcerror='" + gt18 + "'" + " where mahang='" + mahang + "' and thoigian='" + tg + "'";
                
                MySqlCommand cmd = new MySqlCommand(MysqlCmd, connectmysql);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }

        public string CapnhatDulieuweblocal(string tenbang, string gt1, string gt2, string gt3, string gt4, string gt5, string gt6, string gt7, string gt8, string gt9, string gt10, string gt11, string gt12, string gt13, string gt14, string gt15, string gt16, string gt17, string gt18, string gt19,string gt20, string gt21, string gt22, string gt23, string gt24, string gt25)        
        {
            try
            {
                string MysqlCmd = "update " + tenbang + " set mahang='" + gt1 + "'" + "," + "songay='" + gt2 + "'" + "," + "songayconlai='" + gt3 + "'" + "," + "btpvaochuyen='" + gt4 + "'" + "," + "dinhmucgio='" + gt5 + "'" + "," + "thuchien_dinhmuc='" + gt6 + "'" + "," + "ndth_nddm='" + gt7 + "'" + "," + "ththantruoc='" + gt8 + "'" + "," + "ththansau='" + gt9 + "'" + "," + "thhamay='" + gt10 + "'" + "," + "qcdat='" + gt11 + "'" + "," + "ketnoi='" + gt12 + "'" + "," + "tile_thuchien_dinhmuc='" + gt13 + "'" + "," + "tile_ndth_nddm='" + gt14 + "'" + "," + "tile_thantruoc='" + gt15 + "'" + "," + "tile_thansau='" + gt16 + "'" + "," + "tile_hamay='" + gt17 + "'" + "," + "tile_qcdat='" + gt18 + "'" + "," + "nhipdodinhmuc='" + gt19 + "',qcerror='" + gt20 + "',qcdat_hangngay='" + gt21 + "',thantruoc_hangngay='" + gt22 + "',thansau_hangngay='" + gt23 + "',hamay_hangngay='" + gt24 + "',qcerror_hangngay='" + gt25 + "' where stt='1'";
                MySqlCommand cmd = new MySqlCommand(MysqlCmd, connectmysql);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }
        public string CapnhatDulieunhipdolocal(string tenbang,string dulieu)
        {
            try
            {
                string MysqlCmd = "update " + tenbang + " set tile_ndth_nddm='" + dulieu + "'" + " where stt='1'";
                MySqlCommand cmd = new MySqlCommand(MysqlCmd, connectmysql);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }



        //SERVER
        public DataTable DocMySQLcaidatline()
        {
            try
            {
                DataTable TableReturn = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM sg_nm1_line1_caidat", connectmysqlserver);
                ad.Fill(TableReturn);

                return TableReturn;
            }
            catch { return null; }
        }
        // cap nhat bien chot server
        public string CapnhatDulieuchotserver(string tenbang, string giatriupdate)
        {
            try
            {
                string MysqlCmd = "update "+ tenbang +" set chot='" + giatriupdate + "'" + "where stt='1'";
                SqlCommand cmd = new SqlCommand(MysqlCmd, connectmysqlserver);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }

        public string CapnhatDulieunhipdoserver(string tenbang, string mahang, string tg, string giatriupdate)
        {
            try
            {
                string MysqlCmd = "update " + tenbang + " set tile_ndth_nddm='" + giatriupdate + "'" + " where mahang='" + mahang + "' and thoigian='" + tg + "'";
                SqlCommand cmd = new SqlCommand(MysqlCmd, connectmysqlserver);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }
        public string CapnhatDulieudataserver(string tenbang, string mahang, string tg,  string gt4, string gt5, string gt6, string gt7, string gt8, string gt9, string gt10, string gt11, string gt12, string gt13, string gt14, string gt15, string gt16, string gt17, string gt18, string gt19, string gt20, string gt21, string gt22, string gt23, string gt24, string gt25, string gt26, string gt27, string gt28, string gt29, string gt30, string gt31)
        //public string CapnhatDulieudatalocal(string tenbang, string mahang, string gt4)
        {
            try
            {
                string MysqlCmd = "update " + tenbang + " set soluongkiemdat1='" + gt4 + "'" + "," + "soluongkiemdat2='" + gt5 + "'" + "," + "soluongkiemdat3='" + gt6 + "'" + "," + "soluongkiemdat4='" + gt7 + "'" + "," + "soluongkiemdat5='" + gt8 + "'" + "," + "soluongkiemdat6='" + gt9 + "'" + "," + "soluongkiemdat7='" + gt10 + "'" + "," + "soluongkiemdat8='" + gt11 + "'" + "," + "soluongkiemdat9='" + gt12 + "'" + "," + " thuchienthantruoc='" + gt13 + "'" + "," + "thuchienthansau='" + gt14 + "'" + "," + "thuchienhamay='" + gt15 + "'" + "," + "qcdat='" + gt16 + "'" + "," + "songayconlai='" + gt17 + "'" + "," + "qcerror='" + gt18 + "'" + "," + "thuchien_dinhmuc='" + gt19 + "'" + "," + "ndth_nddm='" + gt20 + "'" + "," + "tile_thuchien_dinhmuc='" + gt21 + "'" + "," + "tile_ndth_nddm='" + gt22 + "'" + "," + "tile_thantruoc='" + gt23 + "'" + "," + "tile_thansau='" + gt24 + "'" + "," + "tile_hamay='" + gt25 + "'" + "," + "tile_qcdat='" + gt26 + "'" + "," + "qcdat_hangngay='" + gt27 + "'" + "," + "thantruoc_hangngay='" + gt28 + "'" + "," + "thansau_hangngay='" + gt29 + "'" + "," + "hamay_hangngay='" + gt30 + "'" + "," + "qcerror_hangngay='" + gt31 + "'" + " where mahang='" + mahang + "' and thoigian='" + tg + "'";

                SqlCommand cmd = new SqlCommand(MysqlCmd, connectmysqlserver);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }
    }
}