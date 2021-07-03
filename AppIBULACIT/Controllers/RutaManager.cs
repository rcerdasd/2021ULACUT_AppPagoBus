using AppIBULACIT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppIBULACIT.Controllers
{
    public class RutaManager
    {
        string UrlBase = "http://localhost:49220/api/ruta/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<RutaModel> GetId(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<RutaModel>(response);
        }

        public async Task<IEnumerable<RutaModel>> GetAll(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<RutaModel>>(response);
        }

        public async Task<RutaModel> Ingresar(RutaModel ruta, string token)
        {
           HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase, new StringContent(JsonConvert.SerializeObject(ruta), Encoding.UTF8, "application/jon"));

            return JsonConvert.DeserializeObject<RutaModel>(await response.Content.ReadAsStringAsync());
        }

        public async Task<RutaModel> Actualizar(Ruta ruta, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase, new StringContent(JsonConvert.SerializeObject(ruta), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<RutaModel>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string token, string id)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, id));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}