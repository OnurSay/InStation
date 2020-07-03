using InstationFinalVersion.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace InstationFinalVersion.DataAccess
{
    public class DA_Category : CBase
    {
        MySqlConnection con;
        public List<Category> GetCategory(int CategoryID)
        {
            List<Category> Categories = new List<Category>();
            con = new MySqlConnection(ConnectionString());

            con.Open();
            if (con.State == ConnectionState.Open)
            {
                Category Category = new Category();

                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT *
                    FROM Category
                    WHERE CategoryID = @CategoryID)", con);
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Category.ID = Convert.ToInt32(rdr["ID"]);
                    Category.CategoryName = Convert.ToString(rdr["CategoryName"]);
                    Categories.Add(Category);
                }

                rdr.Close();
                con.Close();
            }
            return Categories;
        }
        public bool InsertCategory(string CategoryName)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());

            con.Open();
            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                INSERT INTO Category
                    (CategoryName)
                    VALUES 
                    (@CategoryName)", con);
                cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }

        public bool UpdateCategory(Category UpdateInfo)
        {
            bool result = false;
            con = new MySqlConnection(ConnectionString());
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(@"
                    UPDATE Category
                    SET ", con);
                cmd.CommandText += " CategoryName = @CategoryName,";
                cmd.CommandText += " WHERE ID = @ID)";
                cmd.Parameters.AddWithValue("@ID", UpdateInfo.ID);
                cmd.Parameters.AddWithValue("@CategoryName", UpdateInfo.CategoryName);
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }
    }
}

