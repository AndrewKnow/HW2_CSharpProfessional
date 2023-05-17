using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_CSharpProfessional
{
    public static class SelectFromDB
    {
        public static void OutputToTheConsole()
        {
            var connectionString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql1 = "SELECT* From public.buyer;";
            using var cmd1 = new NpgsqlCommand(sql1, connection);
            var reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    Console.WriteLine($"[" +
                        $"id={reader1.GetString(0)}," +
                        $"first_name={reader1.GetString(1)}," +
                        $"second_name={reader1.GetString(2)}," +
                        $"email={reader1.GetString(3)}," +
                        $"product_id={reader1.GetString(4)}," +
                        $"count={reader1.GetString(5)}]");
                }
            }

            var sql2 = "SELECT* From public.salesman;";
            using var cmd2 = new NpgsqlCommand(sql2, connection);
            var reader2 = cmd2.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    Console.WriteLine($"[" +
                        $"id={reader2.GetString(0)}," +
                        $"shop={reader2.GetString(1)}]");
                }
            }

            var sql3 = "SELECT* From public.product;";
            using var cmd3 = new NpgsqlCommand(sql3, connection);
            var reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    Console.WriteLine($"[" +
                        $"id={reader3.GetString(0)}," +
                        $"salesman_id={reader3.GetString(1)}," +
                        $"name={reader3.GetString(2)}," +
                        $"price={reader3.GetString(3)}]");
                }
            }
        }
    }
}
