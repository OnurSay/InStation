using InstationFinalVersion.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace InstationFinalVersion
{
    public class DA_Channel : CBase
    {
        MySqlConnection con;
        public List<Channel> GetChannelsWithWeights(List<RecommendationWeight> Weights)
        {
            List<Channel> ChannelList = new List<Channel>();
            List<Channel> ReturnList = new List<Channel>();
            Weights = Weights.OrderBy(x => x.Weight).ToList();
            //Get highest viewed/rated channels by Category
            for (int i = 0; i < Weights.Count; i++)
            {
                if (Weights[i].Weight > 1)
                {
                    ReturnList = SelectChannelWithCategory(Weights[i].CategoryID, i);
                    for (int j = 0; j < ReturnList.Count; j++)
                        ChannelList.Add(ReturnList[j]);
                }
            }
            return ChannelList;
        }

        public List<Channel> SelectChannelWithCategory(int CategoryID, int i)
        {
            List<Channel> channels = new List<Channel>();
            con = new MySqlConnection(ConnectionString());

            con.Open();
            if (con.State == ConnectionState.Open)
            {
                Channel channel = new Channel();

                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT *
                    FROM Channel
                    ORDER BY FollowerCount
                    LIMIT @Limit;
                    WHERE CategoryID = @CategoryID)", con);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                if (i == 0)
                    cmd.Parameters.AddWithValue("@Limit", 3);
                if (i > 0 && i < 3)
                    cmd.Parameters.AddWithValue("@Limit", 2);
                if (i > 3 && i < 6)
                    cmd.Parameters.AddWithValue("@Limit", 1);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    channel.ID = Convert.ToInt32(rdr["ID"]);
                    channel.ChannelName = Convert.ToString(rdr["ChannelName"]);
                    channel.ChannelDescription = Convert.ToString(rdr["ChannelDescription"]);
                    channel.CurrentCategoryID = Convert.ToInt32(rdr["CurrentCategoryID"]);
                    channel.FollowerCount = Convert.ToInt32(rdr["FollowerCount"]);
                    channel.FollowerCount = Convert.ToInt32(rdr["ViewerCount"]);
                    /* if (!string.IsNullOrEmpty(rdr["ExitTime"].ToString()))
                     {
                         card.exitTime = Convert.ToDateTime(rdr["ExitTime"]);
                     }
                     */
                    channels.Add(channel);
                }

                rdr.Close();
                con.Close();
            }
            return channels;
        }
        public bool InsertChannel(string ChannelName, string ChannelDescription, int CurrentCategoryID)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());

            con.Open();
            if (con.State == ConnectionState.Open)
            {
                Channel channel = new Channel();

                MySqlCommand cmd = new MySqlCommand(@"
                INSERT INTO Channel
                    (ChannelName,ChannelDescription,CurrentCategoryID,FollowerCount,ViewerCount,ChannelPicture)
                    VALUES 
                    (@ChannelName,@ChannelDescription,@CurrentCategoryID,@FollowerCount,@ViewerCount,@ChannelPicture)", con);
                cmd.Parameters.AddWithValue("@ChannelName", ChannelName);
                cmd.Parameters.AddWithValue("@ChannelDescription", ChannelDescription);
                cmd.Parameters.AddWithValue("@CurrentCategoryID", CurrentCategoryID);
                cmd.Parameters.AddWithValue("@FollowerCount", 0);
                cmd.Parameters.AddWithValue("@ViewerCount", 0);
                cmd.Parameters.AddWithValue("@ChannelPicture", "");
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }

        public bool UpdateChannel(Channel UpdateInfo)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                Channel channel = new Channel();
                MySqlCommand cmd = new MySqlCommand(@"
                    UPDATE Channel
                    SET ", con);
                cmd.CommandText += " ChannelName = @ChannelName, ";
                cmd.CommandText += " ChannelDescription = @ChannelDescription,";
                cmd.CommandText += " CurrentCategoryID = @CurrentCategoryID,";
                cmd.CommandText += " FollowerCount = @FollowerCount,";
                cmd.CommandText += " ViewerCount = @ViewerCount,";
                cmd.CommandText += " ChannelPicture = @ChannelPicture,";
                cmd.CommandText += " Where ID = @ID)";
                cmd.Parameters.AddWithValue("@ID", UpdateInfo.ID);
                cmd.Parameters.AddWithValue("@ChannelName", UpdateInfo.ChannelName);
                cmd.Parameters.AddWithValue("@ChannelDescription", UpdateInfo.ChannelDescription);
                cmd.Parameters.AddWithValue("@CurrentCategoryID", UpdateInfo.CurrentCategoryID);
                cmd.Parameters.AddWithValue("@FollowerCount", UpdateInfo.FollowerCount);
                cmd.Parameters.AddWithValue("@ViewerCount", UpdateInfo.ViewerCount);
                cmd.Parameters.AddWithValue("@ChannelPicture", UpdateInfo.ChannelPicture);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }

            return result;
        }
    }
}