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
    public class PersonaManager
    {
        string UrlAuthenticate = "http://localhost:49220/api/login/authenticate/";
        string UrlRegister = "http://localhost:49220/api/login/register/";
        string UrlGetChofer = "http://localhost:49220/api/login/chofer";

        public async Task<Persona> Autenticar(LoginRequest loginRequest)
        {
            HttpClient httpClient = new HttpClient();

            var response = await
                httpClient.PostAsync(UrlAuthenticate, new StringContent(JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Persona>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Persona> Registrar(Persona persona)
        {
            HttpClient httpClient = new HttpClient();

            var response = await
                httpClient.PostAsync(UrlRegister, new StringContent(JsonConvert.SerializeObject(persona),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Persona>(await response.Content.ReadAsStringAsync());
        }

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<IEnumerable<Persona>> GetAll(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlGetChofer);

            return JsonConvert.DeserializeObject<IEnumerable<Persona>>(response);
        }

        public async Task<Persona> Ingresar(Persona persona, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlGetChofer, new StringContent(JsonConvert.SerializeObject(persona), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Persona>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Persona> Actualizar(Persona persona, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlGetChofer, new StringContent(JsonConvert.SerializeObject(persona), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Persona>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string token, string id)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlGetChofer, id));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}