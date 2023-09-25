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
    [RoutePrefix("api/provincias")]
    public class ProvinciasController : ApiController
    {
        [HttpGet]
        [Route("ObtenerProvincias")]
        public Provincias ObtenerProvincias()
        {
            return DALProvincias.ObtenerProvincias();
        }
    }
}
