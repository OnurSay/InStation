using InstationFinalVersion.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InstationFinalVersion.DataAccess
{
    public class DA_FollowingTable : CBase
    {
        MySqlConnection con;
        public List<FollowingTable> GetFollowingChannels(int UserID)
        {
            List<FollowingTable> UserFollowing = new List<FollowingTable>();
            con = new MySqlConnection(ConnectionString());
            FollowingTable FollowingTableInfo = new FollowingTable();
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT FollowedChannelID
                    FROM FollowingTable
                    WHERE FollowingUserID = @UserID)", con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FollowingTableInfo.FollowedChannelID = Convert.ToInt32(rdr["FollowedChannelID"]);
                    FollowingTableInfo.FollowingUserID = Convert.ToInt32(rdr["FollowingUserID"]);
                    UserFollowing.Add(FollowingTableInfo);
                }
                rdr.Close();
                con.Close();
            }
            return UserFollowing;
        }

        public List<FollowingTable> GetFollowers(int ChannelID)
        {
            List<FollowingTable> ChannelFollowers = new List<FollowingTable>();
            con = new MySqlConnection(ConnectionString());
            FollowingTable FollowingTableInfo = new FollowingTable();
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT FollowingUserID
                    FROM FollowingTable
                    WHERE FollowedChannelID = @ChannelID)", con);
                cmd.Parameters.AddWithValue("@ChannelID", ChannelID);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FollowingTableInfo.FollowedChannelID = Convert.ToInt32(rdr["FollowedChannelID"]);
                    FollowingTableInfo.FollowingUserID = Convert.ToInt32(rdr["FollowingUserID"]);
                    ChannelFollowers.Add(FollowingTableInfo);
                }
                rdr.Close();
                con.Close();
            }
            return ChannelFollowers;
        }
        public bool InsertFollower(int UserID, int ChannelID)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());

            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                INSERT INTO FollowingTable
                    (FollowedChannelID, FollowingUserID)
                    VALUES 
                    (@FollowedChannelID, @FollowingUserID)", con);
                cmd.Parameters.AddWithValue("@FollowedChannelID", ChannelID);
                cmd.Parameters.AddWithValue("@FollowingUserID", UserID);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }
        public bool UpdateFollower(FollowingTable UpdateInfo)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                    UPDATE FollowingTable
                    SET ", con);
                cmd.CommandText += " FollowedChannelID = @FollowedChannelID,";
                cmd.CommandText += " FollowingUserID = @FollowingUserID,";
                cmd.CommandText += " WHERE FollowingUserID = @FollowingUserID)";
                cmd.Parameters.AddWithValue("@FollowingUserID", UpdateInfo.FollowingUserID);
                cmd.Parameters.AddWithValue("@FollowedChannelID", UpdateInfo.FollowedChannelID);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }
    }
}
