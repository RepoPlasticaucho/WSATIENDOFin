using System.Web.Http;
using System.Web.Http.Cors;
using WsAtiendo.DAL;
using WsAtiendo.Models;

namespace WsAtiendo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {
        [HttpGet]
        [Route("ObtenerRoles")]
        public Roles ObtenerGrupos()
        {
            return DALRoles.ObtenerRoles();
        }
    }
}