using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace JAG.DevTest2019.Host.signalr.hubs
{
    public class JAGMethodHub : Hub
    {
        public void Send(string message)
        {
            int currentRecordsCount = 0;

            while (true)
            {
                //string sessionID = HttpContext.Session.SessionID;
                int latestRecordsCount = ReturnDBCount();
                if (latestRecordsCount - currentRecordsCount > 0)
                    Clients.All.respondToPage(message);

                Thread.Sleep(6000);
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