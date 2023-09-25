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
    [RoutePrefix("api/tipo_usuario")]
    public class TipoUsuarioController : ApiController
    {
        [HttpGet]
        [Route("ObtenerTipo_Usuario")]
        public Tipo_Usuario ObtenerTipo_Usuario()
        {
            return DALTipoUsuario.ObtenerTipo_Usuario();
        }

        [HttpPost]
        [Route("ObtenerTipo_UsuarioN")]
        public Tipo_Usuario ObtenerTipo_UsuarioN(Tipo_UsuarioEntity usuario)
        {
            return DALTipoUsuario.ObtenerTipo_UsuarioN(usuario.usuario);
        }
    }
}
