using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class StoredProcedures
{
    [SqlProcedure]
    public static void SPTest()
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.CommandText =
                "Select * from table01";

            selectCommand.Connection = conn;

            conn.Open();
            selectCommand.ExecuteNonQuery();
            conn.Close();
        }

    }
};
