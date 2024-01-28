using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SolarEcoBackEnd.DB;
using System.Data.SqlClient;

namespace SolarEcoBackEnd.DB
{
    public class CustomerId : Id
    {
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();
        private static readonly ILogger logger;
        private string _customerId;
        public CustomerId()
        {
            this._customerId = _customerId;
        }
        public async Task<string> maxId()
        {
            
            try
            {
                var maxId = await connection.QueryFirstAsync<string>("select max(CustomerId) from Customer");
                return _customerId = maxId;
            }
            catch (Exception ex)
            {
                ex.ToString(); logger.LogError("This is an error message." + ex);
                return ex.ToString();
            }
        }
        public String getId()
        {
            string customerId = null;
            try
            {
                Task.Run(() => maxId()).Wait();

                string Id = _customerId;


                if (Id != null)
                {

                    string idString = Id.Substring(8);
                    int CTR = Int32.Parse(idString);
                    if (CTR >= 1 && CTR <= 9)
                    {
                        CTR = CTR + 1;
                        customerId = "Customer000" + CTR;
                    }
                    else if (CTR >= 10 && CTR <= 99)
                    {
                        CTR = CTR + 1;
                        customerId = "Customer00" + CTR;
                    }
                    else if (CTR >= 10 && CTR <= 999)
                    {
                        CTR = CTR + 1;
                        customerId = "Customer0" + CTR;
                    }
                    else if (CTR >= 100 && CTR <= 9999)
                    {
                        CTR = CTR + 1;
                        customerId = "Customer" + CTR;
                    }

                }

                else { customerId = "Customer0001"; }
            }

            catch (Exception ex) { customerId = ex.ToString(); }


            return customerId;
        }
    }
}


