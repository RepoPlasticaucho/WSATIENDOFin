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
    [RoutePrefix("api/portafolios")]

    public class PortafoliosController : ApiController
    {
        [HttpGet]
        [Route("ObtenerPortafolios")]
        public Portafolios ObtenerPortafolios()
        {
            return DALPortafolios.ObtenerPortafolios();
        }
    }
}
