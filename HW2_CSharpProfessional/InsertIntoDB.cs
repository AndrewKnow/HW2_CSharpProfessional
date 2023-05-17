using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HW2_CSharpProfessional
{
    public static class InsertIntoDB
    {
        public static void InsertInTable(string table)
        {
            string sql;

            if (table == "salesman")
            {
                sql = @"INSERT INTO product(shop) VALUES (:shop);";

                var connectionString = DBConnection.ConnectionString();
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();

                Console.WriteLine("Введите название магазина:");
                var shop = Console.ReadLine();

                using var cmd = new NpgsqlCommand(sql, connection);
                var parameters = cmd.Parameters;
                parameters.Add(new NpgsqlParameter(":shop", shop));

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"Добавлена строка в salesman: {affectedRowsCount}");
                return;
            }

            if (table == "product")
            {
                sql = @"INSERT INTO product(salesman_id, name, price) VALUES (:salesmanId, :name, :price);";

                var connectionString = DBConnection.ConnectionString();
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();

                Console.WriteLine("Введите код магазина:");
                var shopId = Console.ReadLine();

                Console.WriteLine("Введите название товара:");
                var name = Console.ReadLine();

                Console.WriteLine("Введите стоимость товара:");
                var price = Console.ReadLine();

                using var cmd = new NpgsqlCommand(sql, connection);
                var parameters = cmd.Parameters;
                parameters.Add(new NpgsqlParameter(":salesmanId", shopId));
                parameters.Add(new NpgsqlParameter(":name", name));
                parameters.Add(new NpgsqlParameter(":price", price));

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"Добавлена строка в product: {affectedRowsCount}");

                return;
            }

            if (table == "buyer")
            {
                sql = @"INSERT INTO buyer(first_name, second_name, email, product_id, count) VALUES (:firstName, :secondName, :email, :productId, :count);";

                var connectionString = DBConnection.ConnectionString();
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();

                Console.WriteLine("Введите имя:");
                var firstName = Console.ReadLine();

                Console.WriteLine("Введите Фамилию:");
                var secondName = Console.ReadLine();

                Console.WriteLine("Введите почту:");
                var email = Console.ReadLine();

                Console.WriteLine("Введите код товара:");
                var productId = Console.ReadLine();

                Console.WriteLine("Введите кол-во товара:");
                var count = Console.ReadLine();

                using var cmd = new NpgsqlCommand(sql, connection);
                var parameters = cmd.Parameters;
                parameters.Add(new NpgsqlParameter(":firstName", firstName));
                parameters.Add(new NpgsqlParameter(":secondName", secondName));
                parameters.Add(new NpgsqlParameter(":email", email));
                parameters.Add(new NpgsqlParameter(":productId", productId));
                parameters.Add(new NpgsqlParameter(":count", count));

                var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine($"Добавлена строка в buyer: {affectedRowsCount}");

                return;
            }
        }

        static void Insert(string sql)
        {
            var connectionString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var cmd1 = new NpgsqlCommand(sql, connection);
            var parameters = cmd1.Parameters;
            parameters.Add(new NpgsqlParameter(":shop", "Иван"));
            parameters.Add(new NpgsqlParameter("last_name", "Петров"));
            parameters.Add(new NpgsqlParameter("middle_name", "Петрович"));
            parameters.Add(new NpgsqlParameter("email", "petr@yandex.ru"));

            var affectedRowsCount = cmd1.ExecuteNonQuery().ToString();

            Console.WriteLine($"Insert into CLIENTS table. Affected rows count: {affectedRowsCount}");
        }
    }
}
