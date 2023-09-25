using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WsAtiendo.DAL;
using WsAtiendo.Models;

namespace WsAtiendo.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/usuarios")]

    public class UsuariosController : ApiController
    {
        [HttpGet]
        [Route("ObtenerUsuarios")]
        public Usuarios ObtenerUsuarios()
        {
            return DALUsuarios.ObtenerUsuarios();
        }
    }
}
