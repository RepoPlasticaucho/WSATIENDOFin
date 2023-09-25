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
    [RoutePrefix("api/tipo_tercero")]
    public class Tipo_TerceroController : ApiController
    {
        [HttpGet]
        [Route("ObtenerTipo_Tercero")]
        public Tipo_Tercero ObtenerTipo_Tercero()
        {
            return DALTipoTercero.ObtenerTipo_Tercero();
        }

        [HttpPost]
        [Route("ObtenerTipo_TerceroN")]
        public Tipo_Tercero ObtenerTipo_TerceroN(Tipo_TerceroEntity descripcion)
        {
            return DALTipoTercero.ObtenerTipo_TerceroN(descripcion.descripcion);
        }


    }

}
