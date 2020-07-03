using InstationFinalVersion.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InstationFinalVersion.DataAccess
{
    public class DA_User : CBase
    {
        MySqlConnection con;
        public User GetUser(int UserID)
        {
            con = new MySqlConnection(ConnectionString());
            User UserInfo = new User();
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT *
                    FROM User
                    WHERE UserID = @UserID)", con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserInfo.ID = Convert.ToInt32(rdr["ID"]);
                    UserInfo.UserName = Convert.ToString(rdr["UserName"]);
                    UserInfo.ChannelID = Convert.ToInt32(rdr["ChannelID"]);
                    UserInfo.Password = Convert.ToString(rdr["Password"]);
                }

                rdr.Close();
                con.Close();
            }
            return UserInfo;
        }
        public bool InsertUser(string UserName, int ChannelID, string Password)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());

            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                INSERT INTO User
                    (UserName, ChannelID, Password)
                    VALUES 
                    (@UserName, @ChannelID, @Password)", con);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@ChannelID", ChannelID);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }

        public bool UpdateUserHistory(User UpdateInfo)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                    UPDATE User
                    SET ", con);
                cmd.CommandText += " UserName = @UserName,";
                cmd.CommandText += " ChannelID = @ChannelID,";
                cmd.CommandText += " Password = @Password,";
                cmd.CommandText += " WHERE ID = @ID)";
                cmd.Parameters.AddWithValue("@ID", UpdateInfo.ID);
                cmd.Parameters.AddWithValue("@UserName", UpdateInfo.UserName);
                cmd.Parameters.AddWithValue("@ChannelID", UpdateInfo.ChannelID);
                cmd.Parameters.AddWithValue("@Password", UpdateInfo.Password);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }
    }
}
