using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace DataAccessLayer
{
    public class DBHelper
    {
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Host=aws-0-ap-northeast-2.pooler.supabase.com;" + //host address
                "Port=6543;" +
                "Database=postgres;" +
                "Username=postgres.ydihqbigntggnohaogxf;" +
                "Password=Sibghat@123;" +
                "SSL Mode=Require;" +
                "Trust Server Certificate=true");// setting for secure communication
        }
    }
}

