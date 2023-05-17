
namespace HW2_CSharpProfessional
{
    internal class Program
    {
        static void Main()
        {
            // проверка соединения
            if (CheckConnectionDB.CheckConnectionToDB())
            {
                // п.1 Создать базу данных
                CreateDB.CreateTables();

                // п.2 Написать скрипт заполнения таблиц данными
                CreateDB.InsertInDB();

                // п.3 Выводить содержимое всех таблиц
                SelectFromDB.OutputToTheConsole();

                // п.4 Возможность добавления в таблицу на выбор

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ввод в таблицу:\n 0 - Выход из программы \n 1 - salesman \n 2 - buyer\n 3 product");

                    var num = Console.ReadLine();

                    bool tryParseNum;
                    tryParseNum = int.TryParse(num, out _);

                    if (tryParseNum)
                    {
                        int intNum = int.Parse(num);

                        if (intNum == 0) Environment.Exit(0);

                        if (intNum == 1)
                        {
                            InsertIntoDB.InsertInTable("salesman");
                        }
                        if (intNum == 2)
                        {
                            InsertIntoDB.InsertInTable("buyer");
                        }

                        if (intNum == 3)
                        {
                            InsertIntoDB.InsertInTable("product");
                        }
                    }
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}