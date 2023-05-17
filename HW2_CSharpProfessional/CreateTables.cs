
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
                var connectionString = DBConnection.ConnectionString();
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();

                var sql1 = @"
                        CREATE SEQUENCE salesman_id_seq;

                        CREATE TABLE salesman
                        (
                            id              BIGINT                      NOT NULL    DEFAULT NEXTVAL('salesman_id_seq'),
                            shop            CHARACTER VARYING(255)      NOT NULL,
                          
                            CONSTRAINT salesman_pkey PRIMARY KEY (id),
                            CONSTRAINT salesman_product_unique UNIQUE (product)
                        );
                        
                        CREATE INDEX salesman_shop_idx ON salesman(shop);
                        ";

                using var cmd1 = new NpgsqlCommand(sql1, connection);
                cmd1.ExecuteNonQuery();

                var sql2 = @"
                        CREATE SEQUENCE product_id_seq;
                        
                        CREATE TABLE product
                        (
                            id              BIGINT                      NOT NULL    DEFAULT NEXTVAL('product_id_seq'),
                            salesman_id     BIGINT                      NOT NULL,  
                            name            CHARACTER VARYING(255)      NOT NULL,
                            price           MONEY                       NOT NULL,
                            count           INTEGER                     NOT NULL,

                            CONSTRAINT product_pkey PRIMARY KEY (id),
                            CONSTRAINT product_fk_salesman_id FOREIGN KEY (salesman_id) REFERENCES salesman(id) ON DELETE CASCADE
                        );

                        CREATE INDEX product_name_idx ON product(name);
                        ";

                using var cmd2 = new NpgsqlCommand(sql2, connection);
                cmd2.ExecuteNonQuery();


                var sql3 = @"
                        CREATE SEQUENCE buyer_id_seq;
                        
                        CREATE TABLE product
                        (
                            id              BIGINT                      NOT NULL    DEFAULT NEXTVAL('buyer_id_seq'),
                            first_name      CHARACTER VARYING(255)      NOT NULL,
                            second_name     CHARACTER VARYING(255)      NOT NULL,
                            email           CHARACTER VARYING(255)      NOT NULL,
                            product_id      BIGINT                      NOT NULL,  
                            count           INTEGER                     NOT NULL,

                            CONSTRAINT buyer_pkey PRIMARY KEY (id),
                            CONSTRAINT buyer_fk_product_id FOREIGN KEY (product_id) REFERENCES product(id) ON DELETE SET NULL
                        );
                        CREATE UNIQUE INDEX clients_email_unq_idx ON clients(lower(email));
                        ";

                using var cmd3 = new NpgsqlCommand(sql3, connection);
                cmd3.ExecuteNonQuery();

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
    }
}
