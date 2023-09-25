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
    [RoutePrefix("api/atributos")]
    public class AtributosController : ApiController
    {

        [HttpGet]
        [Route("ObtenerAtributos")]
        public Atributos ObtenerAtributos()
        {
            return DALAtributos.ObtenerAtributos();
        }

        [HttpPost]
        [Route("InsertarAtributos")]
        public Atributos InsertarAtributos(AtributosEntity atributo)
        {
            return DALAtributos.InsertarAtributo(atributo);
        }

        [HttpPost]
        [Route("ModificarAtributos")]
        public Atributos ModificarAtributos(AtributosEntity atributo)
        {
            return DALAtributos.ModificarAtributo(atributo);
        }

        [HttpPost]
        [Route("EliminarAtributos")]
        public Atributos EliminarAtributos(AtributosEntity atributo)
        {
            return DALAtributos.EliminarAtributo(atributo);
        }

        [HttpPost]
        [Route("ObtenerAtributosNombre")]
        public Atributos ObtenerAtributosNombre(AtributosEntity atributo)
        {
            return DALAtributos.ObtenerAtributosNombre(atributo.atributo);
        }

    }
}