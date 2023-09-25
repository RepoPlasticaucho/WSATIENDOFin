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
    [RoutePrefix("api/formaspago")]
    public class FormasPagoController : ApiController
    {
        [HttpGet]
        [Route("ObtenerFormasPago")]
        public FormasPago ObtenerFormasPago()
        {
            return DALFormasPago.ObtenerFormasPago();
        }

        [HttpPost]
        [Route("ObtenerFormasPagoN")]
        public FormasPago ObtenerFormasPagoN(FormasPagoEntity name)
        {
            return DALFormasPago.ObtenerFormasPagoN(name.nombre);
        }
    }
}