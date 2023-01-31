using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PLCPiProject
{
    public class SQLLogger
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
                string[] MangTenCot_Buffer = new string[MangTenCot.Length];
                byte x = 0;
                foreach (string b in MangTenCot)
                {                    
                    MangTenCot_Buffer[x] = b;
                    x++;
                }

                SqlConnection connect = new SqlConnection(chuoiketnoi);
                connect.Open();
                string SqlCmd = "IF  NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + Database + "') CREATE DATABASE [" + Database + "]";
                SqlCommand cmd1 = new SqlCommand(SqlCmd, connect);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                cmd1 = null;
                connect.Close();
                //////////////////////////////////////////////////////////////////////
                connect = new SqlConnection(chuoiketnoi + ";Database=" + Database);
                connect.Open();
                SqlCmd = "IF EXISTS ( SELECT [Name] FROM sys.tables WHERE [name] ='" + TenBang + "') DROP TABLE " + TenBang;
                SqlCommand cmd2 = new SqlCommand(SqlCmd, connect);
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();
                cmd2 = null;
                ////////////////////////////////////////////////////////////////////
                SqlCmd = "create table " + TenBang + "(ThoiGian datetime,";
                for (int i = 0; i < MangTenCot_Buffer.Count(); i++)
                {
                    MangTenCot_Buffer[i] += " varchar(500)";
                }
                SqlCmd += string.Join(",", MangTenCot_Buffer) + ")";
                SqlCommand cmd3 = new SqlCommand(SqlCmd, connect);
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
                SqlConnection connect = new SqlConnection(chuoiketnoi + ";Database=" + Database);
                //SqlConnection connect = new SqlConnection("Data Source=192.168.1.232;Initial Catalog=DOWNTIME;User ID=sa;Password=100100;Connection Timeout=1");
                //Console.WriteLine(connect.ConnectionTimeout);
                connect.Open();
                string SqlCmd = "insert into " + TenBang + " values(" + string.Join(",", MangGiaTRi_Buffer) + ")";
                SqlCommand cmd = new SqlCommand(SqlCmd, connect);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                connect.Close();
                connect = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }
        #region ghi nhieu row vao bang
        //public string GhiMultiRowsVaoBang(string TenBang, DataTable MangGiaTRi)
        //{
        //    try
        //    {
        //        DateTime Chuyendoi;
        //        DataRow DRows;
        //        string[] MangGiaTRi_Buffer = new string[6];//dem so record ghi vao bang
        //        //byte x = 0;
        //        //foreach (string b in MangGiaTRi)
        //        //{
        //        //    MangGiaTRi_Buffer[x] = b;
        //        //    x++;
        //        //}
        //        SqlConnection connect = new SqlConnection(chuoiketnoi + ";Database=" + Database);
        //        connect.Open();
        //        for (int i = 0; i < MangGiaTRi.Rows.Count; i++)
        //        {
        //            DRows = MangGiaTRi.Rows[i];
        //            for (byte j = 0; j < 6; j++)
        //            {
        //                if (j != 0)
        //                {
        //                    MangGiaTRi_Buffer[j] = "'" + DRows[j] + "'";                            
        //                }
        //                else if (j == 0)
        //                {
        //                    Chuyendoi = Convert.ToDateTime(DRows[0]);
        //                    MangGiaTRi_Buffer[0] = "'" + Chuyendoi.ToString("yyyy-MM-dd H:mm:ss") + "'";
        //                }
        //            }
        //            string SqlCmd = "insert into " + TenBang + " values(" + string.Join(",", MangGiaTRi_Buffer) + ")";
        //            SqlCommand cmd = new SqlCommand(SqlCmd, connect);
        //            cmd.ExecuteNonQuery();
        //            cmd.Dispose();
        //            cmd = null;
        //        }
        //        connect.Close();
        //        connect = null;
        //        return "GOOD";
        //    }
        //    catch { return "BAD"; }
        //}
        #endregion
        /// <summary>
        /// Truy vấn dữ liệu từ bảng. Dữ liệu trả về dạng DataTable.
        /// </summary>
        /// <param name="TenBang">Tên bảng cần truy vấn.</param>
        /// <param name="MangTenCot">Mảng tên các cột cần lấy dữ liệu. Nếu bằng {"*"}  thì lấy dữ liệu ở tất cả các cột. </param>
        /// <param name="TenCotDinhVi">Tên cột cần truy vấn.</param>
        /// <param name="GiaTriDau">Giá trị đầu của điều kiện truy vấn.</param>
        /// <param name="GiaTriCuoi">Giá trị cuối của điều kiện truy vấn.</param>
        /// <returns>Trả về kết quả kiểu DataTable. Nếu kết quả trả về = null thất bại.</returns>
        public DataTable TruyvanBaocao(string TenBang, string[] MangTenCot, string TenCotDinhVi, string GiaTriDau, string GiaTriCuoi)
        {
            try
            {
                DataTable TableReturn = new DataTable();
                SqlConnection connect = new SqlConnection(chuoiketnoi + ";Database=" + Database);
                connect.Open();
                string SqlCmd = "select " + string.Join(",", MangTenCot) + " from " + TenBang + " where " + TenCotDinhVi + " between '" + GiaTriDau + "' and '" + GiaTriCuoi + "'";
                SqlCommand cmd = new SqlCommand(SqlCmd, connect);
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
        #region lấy 1 giá trị trong bang
        /// <summary>
        /// Lấy 1 giá trị của 1 cột theo các điều kiện.
        /// </summary>
        /// <param name="TenBang">tên bảng cần lấy dữ liệu. </param>
        /// <param name="TenCot">tên cột muốn lấy dữ liệu.</param>
        /// <param name="MangTenCotDinhVi">tên các cột điều kiện.</param>
        /// <param name="MangGiaTriCotDinhVi">dữ liệu của các cột điều kiện.</param>
        /// <returns>Trả về giá trị của cột muốn lấy. Nếu = "BAD" thất bại.</returns>
        //public string LayDulieu(string TenBang, string TenCot, string[] MangTenCotDinhVi, string[] MangGiaTriCotDinhVi)
        //{
        //    try
        //    {
        //        string[] MangTenCotDinhVi_Buffer = new string[MangTenCotDinhVi.Length];
        //        string[] MangGiaTriCotDinhVi_Buffer = new string[MangGiaTriCotDinhVi.Length];
        //        byte x = 0;
        //        string Data_Return = null;
        //        SqlConnection connect = new SqlConnection(chuoiketnoi + ";Database=" + Database);
        //        connect.Open();

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

        //        for (int j = 0; j < MangTenCotDinhVi_Buffer.Count(); j++)
        //        {
        //            MangTenCotDinhVi_Buffer[j] += "= '" + MangGiaTriCotDinhVi_Buffer[j] + "'";
        //        }

        //        string SqlCmd = "select " + TenCot + " from " + TenBang + " where " + string.Join(" and ", MangTenCotDinhVi_Buffer);
        //        SqlCommand cmd = new SqlCommand(SqlCmd, connect);
        //        IDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Data_Return = reader.GetString(0);
        //        }
        //        reader.Close();
        //        reader = null;
        //        cmd.Dispose();
        //        cmd = null;
        //        connect.Close();
        //        connect = null;
        //        return Data_Return;
        //    }
        //    catch { return "BAD"; }
        //}
        #endregion
        /// <summary>
        /// Cập nhật dữ liệu trong bảng.
        /// </summary>
        /// <param name="TenBang">Tên bảng thao tác cập nhật.</param>
        /// <param name="MangTenCotDinhVi">Tên cột truy vấn.</param>
        /// <param name="MangGiaTriCotDinhVi">Giá trị cột truy vấn.</param>
        /// <param name="MangTenCot">Mảng tên các cột cần cập nhật lại giá trị.</param>
        /// <param name="MangGiaTri">Mảng giá trị mới của các cột tương ứng với mảng tên cột.</param>
        /// <returns>Trả về trạng thái kiểu string. Nếu kết quả trả về = "GOOD" cập nhật thành công; = "BAD" cập nhật thất bại.</returns>
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
                SqlConnection connect = new SqlConnection(chuoiketnoi + ";Database=" + Database);
                connect.Open();
                string SqlCmd = "update " + TenBang + " set ";
                for (int i = 0; i < MangTenCot_Buffer.Count(); i++)
                {
                    MangTenCot_Buffer[i] += "= '" + MangGiaTri_Buffer[i] + "'";
                }
                for (int j = 0; j < MangTenCotDinhVi_Buffer.Count(); j++)
                {
                    MangTenCotDinhVi_Buffer[j] += "= '" + MangGiaTriCotDinhVi_Buffer[j] + "'";
                }
                SqlCmd += string.Join(",", MangTenCot_Buffer) + " where " + string.Join(" and ", MangTenCotDinhVi_Buffer);
                SqlCommand cmd = new SqlCommand(SqlCmd, connect);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                connect.Close();
                connect = null;
                return "GOOD";
            }
            catch { return "BAD"; }

        }
    }
}
