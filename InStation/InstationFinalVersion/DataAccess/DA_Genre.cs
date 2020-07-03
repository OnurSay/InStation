using InstationFinalVersion.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InstationFinalVersion.DataAccess
{
    public class DA_Genre : CBase
    {
        MySqlConnection con;
        public Genre GetGenre(int GenreID)
        {
            con = new MySqlConnection(ConnectionString());
            Genre GenreInfo = new Genre();
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT *
                    FROM Genre
                    WHERE GenreID = @GenreID)", con);
                cmd.Parameters.AddWithValue("@GenreID", GenreID);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    GenreInfo.ID = Convert.ToInt32(rdr["ID"]);
                    GenreInfo.GenreName = Convert.ToString(rdr["GenreName"]);
                }

                rdr.Close();
                con.Close();
            }
            return GenreInfo;
        }
        public bool InsertGenre(string GenreName)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());

            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                INSERT INTO Genre
                    (GenreName)
                    VALUES 
                    (@GenreName)", con);
                cmd.Parameters.AddWithValue("@GenreName", GenreName);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }
        public bool UpdateGenre(Genre UpdateInfo)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                    UPDATE Genre
                    SET ", con);
                cmd.CommandText += " GenreName = @GenreName,";
                cmd.CommandText += " WHERE ID = @ID)";
                cmd.Parameters.AddWithValue("@ID", UpdateInfo.ID);
                cmd.Parameters.AddWithValue("@GenreName", UpdateInfo.GenreName);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }
    }
}
