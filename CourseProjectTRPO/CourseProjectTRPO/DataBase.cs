﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CourseProjectTRPO
{
    class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DIKHNICHHONOR\SQLEXPRESS;Initial Catalog=CourseProjectTRPO1;Integrated Security=True");


        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();
        }

        public void closedConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
    }
}
