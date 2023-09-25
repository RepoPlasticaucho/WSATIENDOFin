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
    [RoutePrefix("api/tipo_comprobante")]
    public class Tipo_ComprobanteController : ApiController
    {
        [HttpGet]
        [Route("ObtenerTipos")]
        public Tipo_Comprobante ObtenerTipos()
        {
            return DALTipo_Comprobante.ObtenerTipos();
        }

        [HttpPost]
        [Route("ObtenerTipoN")]
        public Tipo_Comprobante ObtenerTipoN(Tipo_ComprobanteEntity nombre)
        {
            return DALTipo_Comprobante.ObtenerTipoN(nombre.nombre);
        }
    }
}