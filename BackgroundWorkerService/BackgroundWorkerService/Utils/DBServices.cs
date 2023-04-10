using BackgroundWorkerService.Interfaces;
using BackgroundWorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BackgroundWorkerService.Utils
{
    internal class DBServices : IDBServices
    {
        private readonly string? _dbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
        private SqlConnection? _sqlConnection;

        public DBServices()
        {

        }

        public List<HashItem> GetHashesInDB() //for unit test only
        {
            return new List<HashItem>();
        }

        public void SaveHashesInDB(List<HashItem> hashItems)
        {
            using (_sqlConnection = new SqlConnection(_dbConnectionString))
            {
                _sqlConnection.Open();
                var query = "INSERT INTO hashes (date, sha1) VALUES (GETUTCDATE(), @sha1)";

                foreach (var item in hashItems)
                {
                    var adapter = new SqlDataAdapter();
                    var command = new SqlCommand(query, _sqlConnection);

                    command.Parameters.AddWithValue("@sha1", item.Sha1);

                    adapter.InsertCommand = command;
                    adapter.InsertCommand.ExecuteNonQuery();

                    command.Dispose();
                }


                _sqlConnection.Close();

            }
        }
    }
}
