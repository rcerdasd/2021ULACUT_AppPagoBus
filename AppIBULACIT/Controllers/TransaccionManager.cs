using AppPagoBus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppPagoBus.Controllers
{
    public class TransaccionManager
    {

        string UrlBase = "http://localhost:49220/api/transaccion/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<Transaccion> GetId(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<Transaccion>(response);
        }

        public async Task<IEnumerable<Transaccion>> GetAll(string token, string clienteId)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, clienteId));

            return JsonConvert.DeserializeObject<IEnumerable<Transaccion>>(response);
        }

        public async Task<Transaccion> Ingresar(Transaccion transaccion, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase, new StringContent(JsonConvert.SerializeObject(transaccion), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Transaccion>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Transaccion> Actualizar(Transaccion transaccion, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase, new StringContent(JsonConvert.SerializeObject(transaccion), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Transaccion>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string token, string id)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, id));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }

    }
}