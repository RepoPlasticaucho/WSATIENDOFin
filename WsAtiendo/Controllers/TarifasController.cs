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
    [RoutePrefix("api/tarifas")]
    public class TarifasController : ApiController
    {
        [HttpGet]
        [Route("ObtenerTarifas")]
        public Tarifas ObtenerTarifas()
        {
            return DALTarifas.ObtenerTarifas();
        }

        [HttpPost]
        [Route("ObtenerTarifasN")]
        public Tarifas ObtenerTarifasN(TarifasEntity tarifa)
        {
            return DALTarifas.ObtenerTarifasN(tarifa.descripcion);
        }
    }
}