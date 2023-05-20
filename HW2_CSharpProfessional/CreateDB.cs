
using Npgsql;
using Dapper;
using HW2_CSharpProfessional.Model;
using System.Diagnostics;
using System.Xml.Linq;

namespace HW2_CSharpProfessional
{
    public static class CreateDB
    {

        /// <summary>
        /// Заполнение таблиц базы произволными пятью значениями, содержащими рандомный FOREIGN KEY
        /// </summary>
        /// <param name="salesmanMinId">Минимальный ID в таблице salesman</param>
        /// <param name="salesmanMaxId">Максимальный ID в таблице salesman</param>
        /// <param name="productMinId">Минимальный ID в таблице product</param>
        /// <param name="productMaxId">Максимальный ID в таблице product</param>

        public static void InsertInDB()
        {
            var connectionString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            for (int i = 1; i <= 5; i++)
            {
                Random rnd = new();

                var sql1 = @"INSERT INTO salesman(shop) VALUES (@shop);";
                connection.QueryFirstOrDefault<Salesman>(sql1, new { 
                    @shop = $"Магазин {i}" });
            }

            var sqlSalesmanMinId = "SELECT MIN(Id) From public.salesman;";
            var sqlSalesmanMaxId = "SELECT MAX(Id) From public.salesman;";

            int salesmanMinId = connection.ExecuteScalar<int>(sqlSalesmanMinId, null);
            int salesmanMaxId = connection.ExecuteScalar<int>(sqlSalesmanMaxId, null);

            for (int i = 1; i <= 5; i++)
            {
                Random rnd = new();

                var sql2 = @"INSERT INTO product(salesman_id, name, price) VALUES (@salesmanId, @name, @price);";
                connection.QueryFirstOrDefault<Product>(sql2, new
                {
                    @salesmanId = rnd.Next(salesmanMinId, salesmanMaxId),
                    @name = $"Товар {rnd.Next(1, 100)}",
                    @price = rnd.Next(100, 1000)
                });
            }

            var sqlProductMinId = "SELECT MIN(Id) From public.product;";
            var sqlProductMaxId = "SELECT MAX(Id) From public.product;";

            int productMinId = connection.ExecuteScalar<int>(sqlProductMinId, null);
            int productMaxId = connection.ExecuteScalar<int>(sqlProductMaxId, null);

            for (int i = 1; i <= 5; i++)
            {
                Random rnd = new();
                var sql3 = @"INSERT INTO buyer(first_name, second_name, email, product_id, count) VALUES (@firstName, @secondName, @email, @productId, @count);";
                connection.QueryFirstOrDefault<Buyer>(sql3, new
                {
                    @firstName = $"Имя {rnd.Next(1, 100)}",
                    @secondName = $"Фамилия {rnd.Next(1, 100)}",
                    @email = $"email{i}@otus.ru",
                    @productId = rnd.Next(productMinId, productMaxId),
                    @count = rnd.Next(1, 3)
                });
            }
        }

        public static void CreateTables()
        {
            // Проверка развертывания базы ранее по таблице salesman
            var canCreate = CanCreateBase();

            if (canCreate) 
            {
                // Создание базы "авито" (как я себе представил)
                var connectionString = DBConnection.ConnectionString();
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();

                var sql1 = @"
                        CREATE SEQUENCE salesman_id_seq;

                        CREATE TABLE salesman
                        (
                            id              BIGINT                      NOT NULL    DEFAULT NEXTVAL('salesman_id_seq'),
                            shop            CHARACTER VARYING(255)      NOT NULL,
                          
                            CONSTRAINT salesman_pkey PRIMARY KEY (id)
                        );
                        
                        CREATE INDEX salesman_shop_idx ON salesman(shop);
                        ";

                var sql2 = @"
                        CREATE SEQUENCE product_id_seq;
                        
                        CREATE TABLE product
                        (
                            id              BIGINT                      NOT NULL    DEFAULT NEXTVAL('product_id_seq'),
                            salesman_id     BIGINT                      NOT NULL,  
                            name            CHARACTER VARYING(255)      NOT NULL,
                            price           MONEY                       NOT NULL,

                            CONSTRAINT product_pkey PRIMARY KEY (id),
                            CONSTRAINT product_fk_salesman_id FOREIGN KEY (salesman_id) REFERENCES salesman(id) ON DELETE SET NULL
                        );

                        CREATE INDEX product_name_unq_idx ON product(name);
                        ";

                var sql3 = @"
                        CREATE SEQUENCE buyer_id_seq;
                        
                        CREATE TABLE buyer
                        (
                            id              BIGINT                      NOT NULL    DEFAULT NEXTVAL('buyer_id_seq'),
                            first_name      CHARACTER VARYING(255)      NOT NULL,
                            second_name     CHARACTER VARYING(255)      NOT NULL,
                            email           CHARACTER VARYING(255)      NOT NULL,
                            product_id      BIGINT                      NULL,  
                            count           INTEGER                     NULL,

                            CONSTRAINT buyer_pkey PRIMARY KEY (id),
                            CONSTRAINT buyer_fk_product_id FOREIGN KEY (product_id) REFERENCES product(id) ON DELETE SET NULL
                        );
                        CREATE INDEX buyer_email_unq_idx ON buyer(lower(email));
                        ";

                var sql = $"BEGIN;\nSAVEPOINT my_savepoint;\n{sql1}\n{sql2}\n{sql3}\nCOMMIT;";

                using var cmd = new NpgsqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Таблицы созданы");
            }
        }

        static bool CanCreateBase()
        {
            var connectionString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var sql = "SELECT EXISTS(SELECT * FROM pg_tables WHERE tablename = 'salesman' AND schemaname = 'public');";
            using var cmd = new NpgsqlCommand(sql, connection);

            var result = cmd.ExecuteScalar().ToString();

            if (result.ToLower() == "true")
            {
                Console.WriteLine($"Таблицы созданы ранее {result}");
                return false;
            }
            else
            {
                Console.WriteLine($"Таблицы созданы ранее {result}");
                return true;
            }    
        }
    }
}
