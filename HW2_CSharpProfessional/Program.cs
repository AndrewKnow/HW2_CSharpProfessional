
namespace HW2_CSharpProfessional
{
    internal class Program
    {
        static void Main()
        {
            // проверка соединения
            if (CheckConnectionDB.CheckConnectionToDB())
            {
                // создание базы
                CreateTables.CreateDB();

            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}