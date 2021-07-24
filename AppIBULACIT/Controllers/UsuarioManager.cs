using AppPagoBus.Models;
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
    public class UsuarioManager
    {

        string UrlBase = "http://localhost:49220/api/usuario/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<Persona> GetId(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<Persona>(response);
        }

        public async Task<IEnumerable<Persona>> GetAll(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<Persona>>(response);
        }

        public async Task<Persona> Ingresar(Persona persona, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase, new StringContent(JsonConvert.SerializeObject(persona), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Persona>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Persona> Actualizar(Persona persona, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase, new StringContent(JsonConvert.SerializeObject(persona), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Persona>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string token, string id)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, id));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}