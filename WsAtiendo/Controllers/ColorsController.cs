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
    [RoutePrefix("api/colores")]
    public class ColorsController : ApiController
    {

        [HttpGet]
        [Route("ObtenerColores")]
        public Colors ObtenerColores()
        {
            return DALColores.ObtenerColores();
        }

        [HttpPost]
        [Route("InsertarColores")]
        public Colors InsertarColores(ColorsEntity color)
        {
            return DALColores.InsertarColor(color);
        }

        [HttpPost]
        [Route("ModificarColores")]
        public Colors ModificarColores(ColorsEntity color)
        {
            return DALColores.ModificarColor(color);
        }

        [HttpPost]
        [Route("EliminarColores")]
        public Colors EliminarColores(ColorsEntity color)
        {
            return DALColores.EliminarColor(color);
        }

        [HttpPost]
        [Route("ObtenerColoresNombre")]
        public Colors ObtenerColoresNombre(ColorsEntity color)
        {
            return DALColores.ObtenerColoresNombre(color.color);
        }

        [HttpPost]
        [Route("ObtenerColoresUno")]
        public Colors ObtenerColoresUno(ColorsEntity color)
        {
            return DALColores.ObtenerColoresUno(color.color);
        }
    }
}