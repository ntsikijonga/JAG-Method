using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JAG.DevTest2019.DBPoll
{
    class Program
    {
        static void Main(string[] args)
        {
            int currentRecordsCount = 0;
            while (true)
            {
                int latestRecordsCount = ReturnDBCount();
                Console.WriteLine("New Records:" + (latestRecordsCount - currentRecordsCount).ToString());
                currentRecordsCount = latestRecordsCount;
                Thread.Sleep(60000);
            }
        }

        private static int ReturnDBCount()
        {
            string _connectionString = @"Server=localhost\SQLEXPRESS;Database=JAG2019;Trusted_Connection=True;";
            string query = "SELECT COUNT(*) from dbo.Lead";

            using (SqlConnection cn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                var result = Convert.ToInt32(reader[0].ToString());
                cn.Close();
                return result;

            }

        }
    }
}
