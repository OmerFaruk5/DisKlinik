﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo
{
    public class Hastalar
    {
        public void AddSick(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection connection = MyConnection.GetCon();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeletePatient(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection connection = MyConnection.GetCon();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdatePatient(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection connection = MyConnection.GetCon();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }
        public DataSet ShowSick(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection connection = MyConnection.GetCon();
            SqlCommand command = new SqlCommand(query, connection);
            command.Connection = connection;
            command.CommandText = query;
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
    }
}
