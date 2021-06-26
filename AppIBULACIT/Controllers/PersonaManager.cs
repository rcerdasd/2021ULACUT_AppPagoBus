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
    }
}