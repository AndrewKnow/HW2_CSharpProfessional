using Npgsql;

namespace HW2_CSharpProfessional
{
    public class CheckConnectionDB
    {
        public static bool CheckConnectionToDB()
        {
            var connectionString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
