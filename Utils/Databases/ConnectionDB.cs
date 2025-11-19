using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Utils.Databases
{
    public class ConnectionDB
    {
        private static readonly string _connectionString =
            "Data Source=localhost;Initial Catalog = LocadoraBD; User Id = sa; Password = SqlServer@2022; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True";
        // readonly > Só pode leitura (get), não pode alterar (set)


        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
