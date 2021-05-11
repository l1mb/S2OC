using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HabitCracker.Model.Database
{
    internal class DataBase : IDisposable
    {
        private static readonly Lazy<DataBase> Instance = new(() => new DataBase());

        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private static async Task ConnectWithDB()
        {
            await using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Подключение открыто");
            }
            Console.WriteLine("Подключение закрыто...");
        }

        public SqlConnection Conn;

        public static DataBase GetInstance()
        {
            return Instance.Value;
        }

        public DataBase()
        {
            Conn = new SqlConnection(ConnectionString);
        }

        public void Dispose()
        {
            Conn?.Close();
        }
    }
}