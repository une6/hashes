using HashRestAPI.Interfaces;
using HashRestAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace HashRestAPI.Utils
{
    public class DBServices : IDBServices
    {
        private readonly string? _dbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
        private SqlConnection? _sqlConnection;

        public DBServices()
        {

        }

        public List<HashResult> GetHashes()
        {
            var results = new List<HashResult>();

            using (_sqlConnection = new SqlConnection(_dbConnectionString))
            {
                _sqlConnection.Open();
                var query = "SELECT count(1) AS count, format(date,'yyyy-MM-dd') AS date FROM hashes WITH (NOLOCK) GROUP BY format(date,'yyyy-MM-dd')";

                var command = new SqlCommand(query, _sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var result = new HashResult
                    {
                        count = Convert.ToInt32(reader["count"]),
                        date = reader["date"].ToString()
                    };
                    results.Add(result);
                }

                _sqlConnection.Close();

            }

            return results;
        }
    }
}
