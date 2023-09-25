using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WsAtiendo.DAL;
using WsAtiendo.Models;

namespace WsAtiendo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/grupos")]
    public class GruposController : ApiController
    {

        [HttpGet]
        [Route("ObtenerGrupos")]
        public Grupos ObtenerGrupos()
        {
            return DALGrupos.ObtenerGrupos();
        }

        [HttpPost]
        [Route("InsertarGrupo")]
        public Grupos InsertarGrupo(GruposEntity grupo)
        {
            return DALGrupos.InsertarGrupo(grupo);
        }

        [HttpPost]
        [Route("ModificarGrupo")]
        public Grupos ModificarGrupo(GruposEntity grupo)
        {
            return DALGrupos.ModificarGrupo(grupo);
        }

        [HttpPost]
        [Route("EliminarGrupo")]
        public Grupos EliminarGrupo(GruposEntity grupo)
        {
            return DALGrupos.EliminarGrupo(grupo);
        }

        [HttpGet]
        [Route("GenerarClave")]
        public async void GenerarClave()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://bcrypt.online/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                Encrypt data = new Encrypt()
                {
                    plain_text = "venus21",
                    cost_factor = "10"
                };
                HttpResponseMessage response = await client.PostAsJsonAsync(
                "calculate", data);
                if (response.IsSuccessStatusCode)
                {
                    var data2 = response.Content.ReadAsAsync<EncryptResponse>().Result;
                }

            }
            catch (Exception ex)
            {

            }

        }

        public class Encrypt
        {
            public string plain_text { get; set; }
            public string cost_factor { get; set; }

            public Encrypt(string plain_text, string cost_factor)
            {
                this.plain_text = plain_text;
                this.cost_factor = cost_factor;
            }

            public Encrypt()
            {
            }
        }

        public class EncryptResponse
        {
            public string hash { get; set; }

            public EncryptResponse(string hash)
            {
                this.hash = hash;
            }

            public EncryptResponse()
            {
            }
        }
    }
}