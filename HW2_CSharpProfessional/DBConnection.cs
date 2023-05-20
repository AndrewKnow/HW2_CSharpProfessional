
namespace HW2_CSharpProfessional
{
    public static class DBConnection
    {

        private static readonly string _host = "dumbo.db.elephantsql.com";
        private static readonly string _user = "lsakasyr";
        private static readonly string _dbName = "lsakasyr";
        private static readonly string _port = "5432";
        private static string? _password;


        //private static readonly string _host = "localhost";
        //private static readonly string _user = "postgres";
        //private static readonly string _dbName = "HW2_ProCSharp";
        //private static readonly string _port = "5432";
        //private static string? _password;
        public static string ConnectionString()
        {
            _password = Password();

            string connString = string.Format
            (
              "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
              _host,
              _user,
              _dbName,
              _port,
              _password);

            return connString;
        }

        public static string Password()
        {
            StreamReader sr = new StreamReader("Key.txt");
            string password = sr.ReadToEnd();
            sr.Close();
            return password;
        }
    }
}
