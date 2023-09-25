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
    [RoutePrefix("api/ciudades")]
    public class CiudadesController : ApiController
    {
        [HttpGet]
        [Route("ObtenerCiudadesAll")]
        public Ciudades ObtenerCiudadesAll()
        {
            return DALCiudades.ObtenerCiudadesAll();
        }

        [HttpPost]
        [Route("ObtenerCiudades")]
        public Ciudades ObtenerCiudades(ProvinciasEntity provincia)
        {
            return DALCiudades.ObtenerCiudades(provincia.provincia);
        }

        [HttpPost]
        [Route("ObtenerCiudadesN")]
        public Ciudades ObtenerCiudadesN(CiudadesEntity name)
        {
            return DALCiudades.ObtenerCiudadesN(name.ciudad);
        }
    }
}
