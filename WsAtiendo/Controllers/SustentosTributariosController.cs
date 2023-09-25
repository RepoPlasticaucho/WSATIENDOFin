using System.Web.Http;
using System.Web.Http.Cors;
using WsAtiendo.DAL;
using WsAtiendo.Models;

namespace WsAtiendo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/sustentos_tributarios")]
    public class SustentosTributariosController : ApiController
    {
        [HttpGet]
        [Route("ObtenerSustentos")]
        public SustentosTributarios ObtenerSustentos()
        {
            return DALSustentosTributarios.ObtenerSustentos();
        }

        [HttpPost]
        [Route("ObtenerSustentosN")]
        public SustentosTributarios ObtenerSustentosN(SustentosTributariosEntity sustento)
        {
            return DALSustentosTributarios.ObtenerSustentosN(sustento.sustento);
        }
    }
}