using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Threading;

namespace PLCPiProject
{
    /// <summary>
    /// Log data vào cơ sở dữ liệu mySQL
    /// </summary>
    public class mySQLLogger
    {
        string chuoiketnoi, Database;
        /// <summary>
        /// Khởi tạo kết nối đến CSDL mySQL
        /// </summary>
        /// <param name="TenChuoiKetNoi">có dạng "Server = servername;Uid = username;Pwd = password"</param>
        /// <param name="TenDatabase">tên Database trong cơ sở dữ liệu</param>
        public void TaoKetNoi(string TenChuoiKetNoi, string TenDatabase)
        {
            chuoiketnoi = TenChuoiKetNoi;
            Database = TenDatabase;
        }
        /// <summary>
        /// Tạo bảng để log dữ liệu vào. Nếu trùng tên bảng thì bảng cũ sẽ bị xóa, thay bằng bảng mới.
        /// Nếu Database chưa tạo thì sẽ tạo mới database theo tên đã truyền vào khi tạo kết nối.
        /// </summary>
        /// <param name="TenBang">Tên bảng muốn tạo.</param>
        /// <param name="MangTenCot">Mảng chứa tên các cột muốn tạo trong bảng.</param>
        /// <returns>Trả về trạng thái kiểu string. Nếu kết quả trả về = "GOOD" tạo bảng thành công; = "BAD" tạo bảng thất bại.</returns>
        public string TaoBang(string TenBang, string[] MangTenCot)
        {
            try
            {
                MySqlConnection connect = new MySqlConnection(chuoiketnoi);
                connect.Open();
                string MySqlCmd = "create database if not exists " + Database;
                MySqlCommand cmd1 = new MySqlCommand(MySqlCmd, connect);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                cmd1 = null;
                connect.Close();
                //////////////////////////////////////////////////////////////////////
                connect = new MySqlConnection(chuoiketnoi + ";Database=" + Database);
                connect.Open();
                MySqlCmd = "drop table if exists " + TenBang;
                MySqlCommand cmd2 = new MySqlCommand(MySqlCmd, connect);
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();
                cmd2 = null;
                ////////////////////////////////////////////////////////////////////
                MySqlCmd = "create table if not exists " + TenBang + "(ThoiGian datetime,";
                for (int i = 0; i < MangTenCot.Count(); i++)
                {
                    MangTenCot[i] += " varchar(500)";
                }
                MySqlCmd += string.Join(",", MangTenCot) + ")";
                MySqlCommand cmd3 = new MySqlCommand(MySqlCmd, connect);
                cmd3.ExecuteNonQuery();
                connect.Close();
                connect = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }
        /// <summary>
        /// Ghi dữ liệu mới vào bảng.
        /// </summary>
        /// <param name="TenBang">Tên bảng cần ghi dữ liệu.</param>
        /// <param name="MangGiaTRi">Mảng giá trị tương ứng với các cột trong bảng bắt đầu từ cột đầu tiên.</param>
        /// <returns>Trả về trạng thái kiểu string. Nếu kết quả trả về = "GOOD" ghi thành công; = "BAD" ghi thất bại.</returns>
        public string GhiDuLieuVaoBang(string TenBang, string[] MangGiaTRi)
        {
            try
            {
                string[] MangGiaTRi_Buffer = new string[MangGiaTRi.Length];
                byte x = 0;
                foreach (string b in MangGiaTRi)
                {
                    MangGiaTRi_Buffer[x] = b;
                    x++;
                }

                for (int i = 0; i < MangGiaTRi_Buffer.Count(); i++)
                {
                    MangGiaTRi_Buffer[i] = "'" + MangGiaTRi_Buffer[i] + "'";
                }
                MySqlConnection connect = new MySqlConnection(chuoiketnoi + ";Database=" + Database);
                connect.Open();
                string MySqlCmd = "insert into " + TenBang + " values(" + string.Join(",", MangGiaTRi_Buffer) + ")";
                MySqlCommand cmd = new MySqlCommand(MySqlCmd, connect);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                connect.Close();
                connect = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }
        /// <summary>
        /// Truy vấn dữ liệu từ bảng. Dữ liệu trả về dạng DataTable.
        /// </summary>
        /// <param name="TenBang">Tên bảng cần truy vấn</param>
        /// <param name="MangTenCot">Mảng tên các cột cần lấy dữ liệu. Nếu bằng {"*"}  thì lấy dữ liệu ở tất cả các cột. </param>
        /// <param name="TenCotDinhVi">Tên cột cần truy vấn</param>
        /// <param name="GiaTriDau">Giá trị đầu của điều kiện truy vấn</param>
        /// <param name="GiaTriCuoi">Giá trị cuối của điều kiện truy vấn</param>
        /// <returns>Trả về kết quả kiểu DataTable. Nếu kết quả trả về = null thất bại.</returns>
        public DataTable TruyvanBaocao(string TenBang, string[] MangTenCot, string TenCotDinhVi, string GiaTriDau, string GiaTriCuoi)
        {
            try
            {
                DataTable TableReturn = new DataTable();
                MySqlConnection connect = new MySqlConnection(chuoiketnoi + ";Database=" + Database);
                connect.Open();
                string CmdMySql = "select " + string.Join(",", MangTenCot) + " from " + TenBang + " where " + TenCotDinhVi + " between '" + GiaTriDau + "' and '" + GiaTriCuoi + "'";
                MySqlCommand cmd = new MySqlCommand(CmdMySql, connect);
                IDataReader reader = cmd.ExecuteReader();
                TableReturn.Load(reader);
                reader.Close();
                reader = null;
                cmd.Dispose();
                cmd = null;
                connect.Close();
                connect = null;
                return TableReturn;
            }
            catch { return null; }
        }
        #region truy van 
        //public string[] Truyvan1Record(string TenBang, Int16 Start, Int16 SoCot)
        //{
        //    try
        //    {
        //        string[] TableReturn = new string[SoCot];
        //        MySqlConnection connect = new MySqlConnection(chuoiketnoi + ";Database=" + Database);
        //        connect.Open();
        //        string CmdMySql = "select * from " + TenBang + " limit " + Convert.ToString(Start) + ",1";
        //        MySqlCommand cmd = new MySqlCommand(CmdMySql, connect);
        //        IDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            for (int i = 0; i < SoCot; i++)
        //            {
        //                TableReturn[i] = reader.GetString(i);
        //            }
        //        }
        //        reader.Close();
        //        reader = null;
        //        cmd.Dispose();
        //        cmd = null;
        //        connect.Close();
        //        connect = null;
        //        return TableReturn;
        //    }
        //    catch { return null; }
        //}
        //public DataTable TruyvanToanbo(string TenBang)
        //{
        //    try
        //    {
        //        byte BienCheck = 0;
        //        DataTable TableReturn = new DataTable();
        //        MySqlConnection connect = new MySqlConnection(chuoiketnoi + ";Database=" + Database);
        //        connect.Open();
        //        // kiem tra bang co du lieu hay ko
        //        string CmdMySql = "select * from " + TenBang + " limit 0,1";
        //        MySqlCommand cmd = new MySqlCommand(CmdMySql, connect);
        //        IDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            if (reader.GetString(0) == null)
        //                BienCheck = 1;
        //        }
        //        reader.Close();
        //        reader = null;
        //        cmd.Dispose();
        //        cmd = null;
        //        if (BienCheck == 0)
        //        {
        //            CmdMySql = "select * from " + TenBang;
        //            cmd = new MySqlCommand(CmdMySql, connect);
        //            reader = cmd.ExecuteReader();
        //            TableReturn.Load(reader);
        //            reader.Close();
        //            reader = null;
        //            cmd.Dispose();
        //            cmd = null;                    
        //        }
        //        else
        //        {
        //            TableReturn = null;
        //        }
        //        connect.Close();
        //        connect = null;
        //        return TableReturn;
        //    }
        //    catch { return null; }
        //}
        #endregion
        ///// <summary>
        ///// Cập nhật dữ liệu trong bảng.
        ///// </summary>
        ///// <param name="TenBang">Tên bảng thao tác cập nhật.</param>
        ///// <param name="TenCotDinhVi">Tên cột truy vấn.</param>
        ///// <param name="GiaTriCotDinhVi">Giá trị cột truy vấn.</param>
        ///// <param name="MangTenCot">Mảng tên các cột cần cập nhật lại giá trị.</param>
        ///// <param name="MangGiaTri">Mảng giá trị mới của các cột tương ứng với mảng tên cột.</param>
        ///// <returns>Trả về trạng thái kiểu string. Nếu kết quả trả về = "GOOD" cập nhật thành công; = "BAD" cập nhật thất bại.</returns>
        public string CapnhatDulieu(string TenBang, string[] MangTenCotDinhVi, string[] MangGiaTriCotDinhVi, string[] MangTenCot, string[] MangGiaTri)
        {
            try
            {
                string[] MangTenCot_Buffer = new string[MangTenCot.Length];
                string[] MangGiaTri_Buffer = new string[MangGiaTri.Length];
                string[] MangTenCotDinhVi_Buffer = new string[MangTenCotDinhVi.Length];
                string[] MangGiaTriCotDinhVi_Buffer = new string[MangGiaTriCotDinhVi.Length];
                byte x = 0;
                //mang ten cot can update
                foreach (string a in MangTenCot)
                {
                    MangTenCot_Buffer[x] = a;
                    x++;
                }
                x = 0;
                //gia tri cac cot can update
                foreach (string b in MangGiaTri)
                {
                    MangGiaTri_Buffer[x] = b;
                    x++;
                }
                //mang cac cot dieu kien
                x = 0;
                foreach (string c in MangTenCotDinhVi)
                {
                    MangTenCotDinhVi_Buffer[x] = c;
                    x++;
                }
                x = 0;
                //gia tri cua cac cot dieu kien
                foreach (string d in MangGiaTriCotDinhVi)
                {
                    MangGiaTriCotDinhVi_Buffer[x] = d;
                    x++;
                }
                MySqlConnection connect = new MySqlConnection(chuoiketnoi + ";Database=" + Database);
                connect.Open();
                string MysqlCmd = "update " + TenBang + " set ";
                for (int i = 0; i < MangTenCot_Buffer.Count(); i++)
                {
                    MangTenCot_Buffer[i] += "= '" + MangGiaTri_Buffer[i] + "'";
                }
                for (int j = 0; j < MangTenCotDinhVi_Buffer.Count(); j++)
                {
                    MangTenCotDinhVi_Buffer[j] += "= '" + MangGiaTriCotDinhVi_Buffer[j] + "'";
                }
                MysqlCmd += string.Join(",", MangTenCot_Buffer) + " where " + string.Join(" and ", MangTenCotDinhVi_Buffer);
                MySqlCommand cmd = new MySqlCommand(MysqlCmd, connect);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                connect.Close();
                connect = null;
                return "GOOD";
            }
            catch { return "BAD"; }

        }
        #region Xoa Record
        //public string CapnhatDulieu(string TenBang, string[] MangTenCotDinhVi, string[] MangGiaTriCotDinhVi, string[] MangTenCot, string[] MangGiaTri)
        //{
        //    try
        //    {
        //        string[] MangTenCot_Buffer = new string[MangTenCot.Length];
        //        string[] MangGiaTri_Buffer = new string[MangGiaTri.Length];
        //        string[] MangTenCotDinhVi_Buffer = new string[MangTenCotDinhVi.Length];
        //        string[] MangGiaTriCotDinhVi_Buffer = new string[MangGiaTriCotDinhVi.Length];
        //        byte x = 0;
        //        //mang ten cot can update
        //        foreach (string a in MangTenCot)
        //        {
        //            MangTenCot_Buffer[x] = a;
        //            x++;
        //        }
        //        x = 0;
        //        //gia tri cac cot can update
        //        foreach (string b in MangGiaTri)
        //        {
        //            MangGiaTri_Buffer[x] = b;
        //            x++;
        //        }
        //        //mang cac cot dieu kien
        //        x = 0;
        //        foreach (string c in MangTenCotDinhVi)
        //        {
        //            MangTenCotDinhVi_Buffer[x] = c;
        //            x++;
        //        }
        //        x = 0;
        //        //gia tri cua cac cot dieu kien
        //        foreach (string d in MangGiaTriCotDinhVi)
        //        {
        //            MangGiaTriCotDinhVi_Buffer[x] = d;
        //            x++;
        //        }
        //        MySqlConnection connect = new MySqlConnection(chuoiketnoi + ";Database=" + Database);
        //        connect.Open();
        //        string SqlCmd = "update " + TenBang + " set ";
        //        for (int i = 0; i < MangTenCot_Buffer.Count(); i++)
        //        {
        //            MangTenCot_Buffer[i] += "= '" + MangGiaTri_Buffer[i] + "'";
        //        }
        //        for (int j = 0; j < MangTenCotDinhVi_Buffer.Count(); j++)
        //        {
        //            MangTenCotDinhVi_Buffer[j] += "= '" + MangGiaTriCotDinhVi_Buffer[j] + "'";
        //        }
        //        SqlCmd += string.Join(",", MangTenCot_Buffer) + " where " + string.Join(" and ", MangTenCotDinhVi_Buffer);
        //        MySqlCommand cmd = new MySqlCommand(SqlCmd, connect);
        //        cmd.ExecuteNonQuery();
        //        cmd.Dispose();
        //        cmd = null;
        //        connect.Close();
        //        connect = null;
        //        return "GOOD";
        //    }
        //    catch { return "BAD"; }
        //}
       

        
        //public string Delete1Record(string TenBang, string[] CotDinhVi, string[] GiatriCotDinhVi)
        //{
        //    try
        //    {
        //        string[] CotDinhVi_Buffer = new string[CotDinhVi.Length];
        //        string[] GiatriCotDinhVi_Buffer = new string[GiatriCotDinhVi.Length];
        //        byte x = 0;
        //        foreach (string a in CotDinhVi)
        //        {
        //            CotDinhVi_Buffer[x] = a;
        //            x++;
        //        }
        //        x = 0;
        //        foreach (string b in GiatriCotDinhVi)
        //        {
        //            GiatriCotDinhVi_Buffer[x] = b;
        //            x++;
        //        }

        //        for (int i = 0; i < CotDinhVi_Buffer.Count(); i++)
        //        {
        //            CotDinhVi_Buffer[i] += "='" + GiatriCotDinhVi_Buffer[i] + "'";
        //        }
        //        MySqlConnection connect = new MySqlConnection(chuoiketnoi + ";Database=" + Database);
        //        connect.Open();
        //        string MySqlCmd = "delete from " + TenBang + " where " + string.Join(" and ", CotDinhVi_Buffer);
        //        MySqlCommand cmd = new MySqlCommand(MySqlCmd, connect);
        //        cmd.ExecuteNonQuery();
        //        cmd.Dispose();
        //        cmd = null;
        //        connect.Close();
        //        connect = null;
        //        return "GOOD";
        //    }
        //    catch { return "BAD"; }
        //}
        //public string DeleteRecord(string TenBang, string[] CotDinhVi, DataTable GiatriCotDinhVi)
        //{
        //    try
        //    {
        //        string[] CotDinhVi_Buffer = new string[CotDinhVi.Length];
        //        DateTime Chuyendoi;
        //        DataRow DRows;
        //        //string[] GiatriCotDinhVi_Buffer = new string[GiatriCotDinhVi.Length];
        //        byte x = 0;
        //        foreach (string a in CotDinhVi)
        //        {
        //            CotDinhVi_Buffer[x] = a;
        //            x++;
        //        }
        //        x = 0;
        //        MySqlConnection connect = new MySqlConnection(chuoiketnoi + ";Database=" + Database);
        //        connect.Open();

        //        for (int i = 0; i < GiatriCotDinhVi.Rows.Count; i++)
        //        {
        //            DRows = GiatriCotDinhVi.Rows[i];
        //            Chuyendoi = Convert.ToDateTime(DRows[0]);
        //            CotDinhVi_Buffer[0] += "='" + Chuyendoi.ToString("yyyy-MM-dd H:mm:ss") + "'";
        //            CotDinhVi_Buffer[1] += "='" + DRows[1].ToString() + "'";

        //            string MySqlCmd = "delete from " + TenBang + " where " + string.Join(" and ", CotDinhVi_Buffer);
        //            MySqlCommand cmd = new MySqlCommand(MySqlCmd, connect);
        //            cmd.ExecuteNonQuery();
        //            cmd.Dispose();
        //            cmd = null;
        //            foreach (string a in CotDinhVi)
        //            {
        //                CotDinhVi_Buffer[x] = a;
        //                x++;
        //            }
        //            x = 0;
        //        }
        //        connect.Close();
        //        connect = null;
        //        return "GOOD";
        //    }
        //    catch { return "BAD"; }
        //}
        #endregion
        /// <summary>
        /// Dùng lấy thời gian thực từ hệ thống đúng format của mySQL có dạng "yyyy-MM-dd hh:mm:ss"
        /// </summary>
        /// <returns></returns>
        public string LayThoiGian()
        {
            try
            {
                string ReturnString = "1988-09-24 00:00:00";
                string NamThangNgay, GioPhutGiay;
                NamThangNgay = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                GioPhutGiay = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                ReturnString = NamThangNgay + " " + GioPhutGiay;
                return ReturnString;
            }
            catch { return "BAD";}
        }
    }
}
