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
    [RoutePrefix("api/generos")]
    public class GenerosController : ApiController
    {

        [HttpGet]
        [Route("ObtenerGeneros")]
        public Generos ObtenerGeneros()
        {
            return DALGeneros.ObtenerGeneros();
        }

        [HttpPost]
        [Route("InsertarGeneros")]
        public Generos InsertarGeneros(GenerosEntity genero)
        {
            return DALGeneros.InsertarGenero(genero);
        }

        [HttpPost]
        [Route("ModificarGeneros")]
        public Generos ModificarGeneros(GenerosEntity genero)
        {
            return DALGeneros.ModificarGenero(genero);
        }

        [HttpPost]
        [Route("EliminarGeneros")]
        public Generos EliminarGeneros(GenerosEntity genero)
        {
            return DALGeneros.EliminarGenero(genero);
        }

        [Route("ObtenerGenerosNombre")]
        public Generos ObtenerGenerosNombre(GenerosEntity genero)
        {
            return DALGeneros.ObtenerGenerosNombre(genero.genero);
        }
    }
}