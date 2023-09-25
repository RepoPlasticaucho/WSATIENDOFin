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
    [RoutePrefix("api/catalogos")]
    public class CatalogosController : ApiController
    {
        [HttpGet]
        [Route("ObtenerCatalogos")]
        public Catalogos ObtenerCatologos()
        {
            return DALCatalogos.ObtenerCatologos();
        }

        [HttpGet]
        [Route("ObtenerCatalogosLineas")]
        public Catalogos ObtenerCatologosLineas()
        {
            return DALCatalogos.ObtenerCatologosLineas();
        }

        [HttpGet]
        [Route("ObtenerCatalogosModelos")]
        public Catalogos ObtenerCatalogosModelos()
        {
            return DALCatalogos.ObtenerCatalogosModelos();
        }

        [HttpGet]
        [Route("ObtenerCatalogosModelosProductos")]
        public Catalogos ObtenerCatalogosModelosProductos()
        {
            return DALCatalogos.ObtenerCatalogosModelosProductos();
        }

        [HttpGet]
        [Route("ObtenerCatalogosProductos")]
        public Catalogos ObtenerCatalogosProductos()
        {
            return DALCatalogos.ObtenerCatalogosProductos();
        }
    }
}
