
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
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}