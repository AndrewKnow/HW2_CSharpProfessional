
using Npgsql;

namespace HW2_CSharpProfessional
{
    public static class CreateTables
    {
        public static void CreateDB()
        {
            // Проверка развертывания базы по таблице salesman
            var canCreate = CanCreateBase();
            if (canCreate) 
            {
                // Создание базы "авито"
                CreateTableSalesman();
                CreateTableProduct();
                CreateTableBuyer();
            }
        }

        static bool CanCreateBase()
        {
            var connectionString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var sql = "SELECT EXISTS(SELECT * FROM pg_tables WHERE tablename = 'salesman' AND schemaname = 'public');";
            using var cmd = new NpgsqlCommand(sql, connection);

            var affected = cmd.ExecuteScalar().ToString();

            if (affected.ToLower() == "true")
            {
                Console.WriteLine($"Таблицы созданы ранее {affected}");
                return false;
            }
            else
            {
                Console.WriteLine($"Создание таблиц:");
                return true;
            }    
        }


        static void CreateTableSalesman()
        {
            var connectionString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql = @"
                        CREATE SEQUENCE salesman_id_seq;

                        CREATE TABLE salesman
                        (
                            id              BIGINT                      NOT NULL    DEFAULT NEXTVAL('salesman_id_seq'),
                            shop            CHARACTER VARYING(255)      NOT NULL,
                            product         CHARACTER VARYING(255)      NOT NULL,
                            price           MONEY                       NOT NULL,
                            count           INTEGER                     NOT NULL,
                          
                            CONSTRAINT salesman_pkey PRIMARY KEY (id),
                            CONSTRAINT salesman_product_unique UNIQUE (product)
                        );
                        
                        CREATE INDEX salesman_product_idx ON salesman(product);
                        ";

            using var cmd = new NpgsqlCommand(sql, connection);

            var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

            Console.WriteLine("Создана таблица SALESMAN");

        }

        static void CreateTableProduct()
        {

        }
        static void CreateTableBuyer()
        {

        }

    }
}
