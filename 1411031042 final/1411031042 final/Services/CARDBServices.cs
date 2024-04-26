using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;
using _1411031042_final.Models;
namespace _1411031042_final.Services
{
    public class CARDBServices
    {
        //建立與資料庫的連線字串
        private readonly static string cnstr =
            ConfigurationManager.ConnectionStrings["CAR"].ConnectionString;
        //建立與資料庫的連線
        private readonly SqlConnection conn = new SqlConnection(cnstr);
        public List<Benz> GetBenzList()
        {
            List<Benz> BenzList = new List<Benz>();
            string sql = $@" SELECT * FROM Benz;";
            try
            {
                conn.Open(); // 開啟資料庫連線
                SqlCommand cmd = new SqlCommand(sql, conn); // 執行Sql 指令
                SqlDataReader dr = cmd.ExecuteReader(); // 取得Sql 資料
                while (dr.Read()) // 獲得下一筆資料直到沒有資料
                {
                    Benz Data = new Benz();
                    Data.ID = Convert.ToInt32(dr["ID"]);
                    Data.Image = dr["Image"].ToString();
                    Data.Model = dr["Model"].ToString();
                    Data.Money = Convert.ToInt32(dr["Money"].ToString());
                    BenzList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return BenzList;
        }
        public List<BMW> GetBMWList()
        {
            List<BMW> BMWList = new List<BMW>();
            string sql = $@" SELECT * FROM BMW;";
            try
            {
                conn.Open(); // 開啟資料庫連線
                SqlCommand cmd = new SqlCommand(sql, conn); // 執行Sql 指令
                SqlDataReader dr = cmd.ExecuteReader(); // 取得Sql 資料
                while (dr.Read()) // 獲得下一筆資料直到沒有資料
                {
                    BMW Data = new BMW();
                    Data.ID = Convert.ToInt32(dr["ID"]);
                    Data.Image = dr["Image"].ToString();
                    Data.Model = dr["Model"].ToString();
                    Data.Money = Convert.ToInt32(dr["Money"].ToString());
                    BMWList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return BMWList;
        }
        public void InsertBenzFile(Benz newData)
        {
            string sql = $@"INSERT INTO Benz (Image, Model, Money) VALUES ('{newData.Image}', '{newData.Model}', '{newData.Money}')";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public void InsertBMWFile(BMW newData)
        {
            string sql = $@"INSERT INTO BMW (Image, Model, Money) VALUES ('{newData.Image}', '{newData.Model}', '{newData.Money}')";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public void DeleteBenz(int ID)
        {
            string sql = $"DELETE FROM Benz WHERE ID = @ID";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("找不到要刪除的 Benz 資料");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteBMW(int ID)
        {
            string sql = $"DELETE FROM BMW WHERE ID = {ID}";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("找不到要刪除的 BMW 資料");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public Benz GetBenzByID(int ID)
        {
            string sql = "SELECT * FROM Benz WHERE ID = @ID";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Benz benz = new Benz
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Image = reader["Image"].ToString(),
                        Model = reader["Model"].ToString(),
                        Money = Convert.ToInt32(reader["Money"])
                    };
                    return benz;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
        public BMW GetBMWByID(int ID)
        {
            string sql = "SELECT * FROM BMW WHERE ID = @ID";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    BMW BMW = new BMW
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Image = reader["Image"].ToString(),
                        Model = reader["Model"].ToString(),
                        Money = Convert.ToInt32(reader["Money"])
                    };
                    return BMW;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
        public void UpdateBenz(Benz benz)
        {
            string sql = $@"UPDATE Benz SET Image = '{benz.Image}', Model = '{benz.Model}', Money = '{benz.Money}' WHERE ID = '{benz.ID}'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("找不到要更新的 Benz 資料");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateBMW(BMW BMW)
        {
            string sql = $@"UPDATE BMW SET Image = '{BMW.Image}', Model = '{BMW.Model}', Money = '{BMW.Money}' WHERE ID = '{BMW.ID}'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("找不到要更新的 BMW 資料");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}