using InstationFinalVersion.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InstationFinalVersion.DataAccess
{
    public class DA_UserHistory : CBase
    {
        MySqlConnection con;
        public List<UserHistory> GetUserHistory(int UserID)
        {
            List<UserHistory> Histories = new List<UserHistory>();
            con = new MySqlConnection(ConnectionString());

            con.Open();
            if (con.State == ConnectionState.Open)
            {
                UserHistory History = new UserHistory();

                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT *
                    FROM UserHistory
                    WHERE UserID = @UserID)", con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    History.ID = Convert.ToInt32(rdr["ID"]);
                    History.CategoryID = Convert.ToInt32(rdr["CategoryID"]);
                    History.UserID = Convert.ToInt32(rdr["UserID"]);
                    History.ChannelID = Convert.ToInt32(rdr["ChannelID"]);
                    History.GenreID = Convert.ToInt32(rdr["GenreID"]);
                    History.UpdatedDate = Convert.ToDateTime(rdr["UpdatedDate"]);
                    Histories.Add(History);
                }

                rdr.Close();
                con.Close();
            }
            return Histories;
        }
        public bool InsertHistory(int CategoryID, int UserID, int ChannelID, int GenreID)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());

            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                INSERT INTO UserHistory
                    (CategoryID, UserID, UpdatedDate, ChannelID, GenreID)
                    VALUES 
                    (@CategoryID, @UserID, @UpdatedDate, @ChannelID, @GenreID)", con);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ChannelID", ChannelID);
                cmd.Parameters.AddWithValue("@GenreID", GenreID);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }

        public bool UpdateUserHistory(UserHistory UpdateInfo)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                    UPDATE UserHistory
                    SET ", con);
                cmd.CommandText += " CategoryID = @CategoryID,";
                cmd.CommandText += " UserID = @UserID,";
                cmd.CommandText += " UpdatedDate = @UpdatedDate,";
                cmd.CommandText += " ChannelID = @ChannelID,";
                cmd.CommandText += " GenreID = @GenreID,";
                cmd.CommandText += " WHERE ID = @ID)";
                cmd.Parameters.AddWithValue("@ID", UpdateInfo.ID);
                cmd.Parameters.AddWithValue("@CategoryID", UpdateInfo.CategoryID);
                cmd.Parameters.AddWithValue("@UserID", UpdateInfo.UserID);
                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ChannelID", UpdateInfo.ChannelID);
                cmd.Parameters.AddWithValue("@GenreID", UpdateInfo.GenreID);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }
    }
}
