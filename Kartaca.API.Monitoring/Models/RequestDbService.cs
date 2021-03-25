using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Kartaca.API.Monitoring.Models
{
    public class RequestDbService
    {
        private const string CONN_STRING = "Server=db;Database=product-db;User Id=sa;Password=CorrectHorseBatteryStapleFor$;";
        private const string QUERY = "SELECT Id, MethodName, RequestTime,RequestTimeStamp FROM Requests";
        public RequestObj[] GetAll()
        {
            using (var connection = new SqlConnection(CONN_STRING))
            {
                return connection.Query<RequestObj>(QUERY).ToArray();
            }
        }
    }
}
