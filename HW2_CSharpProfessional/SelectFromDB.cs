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

            Console.WriteLine("Таблица buyer:");
            var sql1 = "SELECT* From public.buyer;";
            using var cmd1 = new NpgsqlCommand(sql1, connection);
            var reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    Console.WriteLine($"[" +
                        $"id={reader1[0].ToString()}," +
                        $"first_name={reader1[1].ToString()}," +
                        $"second_name={reader1[2].ToString()}," +
                        $"email={reader1[3].ToString()}," +
                        $"product_id={reader1[4].ToString()}," +
                        $"count={reader1[5].ToString()}]");
                }
            }
            reader1.Close();

            Console.WriteLine("Таблица salesman:");
            var sql2 = "SELECT* From public.salesman;";
            using var cmd2 = new NpgsqlCommand(sql2, connection);
            var reader2 = cmd2.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    Console.WriteLine($"[" +
                        $"id={reader2[0].ToString()}," +
                        $"shop={reader2[1].ToString()}]");
                }
            }
            reader2.Close();

            Console.WriteLine("Таблица product:");
            var sql3 = "SELECT* From public.product;";
            using var cmd3 = new NpgsqlCommand(sql3, connection);
            var reader3 = cmd3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    Console.WriteLine($"[" +
                        $"id={reader3[0].ToString()}," +
                        $"salesman_id={reader3[1].ToString()}," +
                        $"name={reader3[2].ToString()}," +
                        $"price={reader3[3].ToString()}]");
                }
            }
            reader3.Close();
        }
    }
}
