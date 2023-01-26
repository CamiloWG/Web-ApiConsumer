using ApiConsumer.Models;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Text;

namespace ApiConsumer.Services
{
    public class ServiceAPI : IServiceAPI
    {
        private readonly ConnectionAPI connection;
        public ServiceAPI() 
        {
            connection = new ConnectionAPI();
        }

        public async Task<bool> DeleteCustommer(string id)
        {
            var cliente = connection.Connection;
            var response = await cliente.DeleteAsync($"api/Customers/Delete/{id}");

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<List<Custommer>> GetAllCustommers()
        {
            List<Custommer> list = new List<Custommer>();
            var cliente = connection.Connection;
            var response = await cliente.GetAsync("api/Customers/GetAll");

            if(response.IsSuccessStatusCode)
            {
                var json_res = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ApiResponse>(json_res);
                list = resultado.data;
            }
            return list;
        }

        public async Task<Custommer> GetCustommerById(string id)
        {
            Custommer data = new Custommer();
            var cliente = connection.Connection;
            var response = await cliente.GetAsync($"api/Customers/Get/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json_res = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ApiSingleResponse>(json_res);
                data = resultado.data;
            }
            return data;
        }

        public async Task<bool> InsertCustommer(Custommer custommer)
        {
            var cliente = connection.Connection;
            custommer.Region = "na";
            custommer.PostalCode = "na";
            custommer.Country = "na";
            custommer.Fax = "na";
            custommer.ContactTitle = "na";

            var content = new StringContent(JsonConvert.SerializeObject(custommer), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync($"api/Customers/Insert/", content);

            if (response.IsSuccessStatusCode) return true;
            else
            {
                Console.WriteLine(response.ToString());
                Console.WriteLine(JsonConvert.SerializeObject(custommer));
                return false;
            }

        }

        public async Task<bool> UpdateCustommer(Custommer custommer)
        {
            var cliente = connection.Connection;
            custommer.Region = "na";
            custommer.PostalCode = "na";
            custommer.Country = "na";
            custommer.Fax = "na";
            custommer.ContactTitle = "na";
            var content = new StringContent(JsonConvert.SerializeObject(custommer), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Customers/Update", content);


            if (response.IsSuccessStatusCode) return true;
            else
            {
                Console.WriteLine(response.ToString());
                Console.WriteLine(JsonConvert.SerializeObject(custommer));
                return false;
            }
        }
    }
}
