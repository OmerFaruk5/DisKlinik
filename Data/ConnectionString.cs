using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Data
{
    public class ConnectionString
    {
        public SqlConnection GetCon()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ozbek\OneDrive\Belgeler\DentalDb.mdf;Integrated Security=True;Connect Timeout=30";
            return connection;
        }
    }
}
