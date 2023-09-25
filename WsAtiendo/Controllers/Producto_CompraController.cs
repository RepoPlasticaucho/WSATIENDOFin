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
    [RoutePrefix("api/productocompra")]

    public class Producto_CompraController : ApiController
    {
        [HttpGet]
        [Route("ObtenerProducto_Compra")]
        public Producto_Compra ObtenerProducto_Compra()
        {
            return DALProducto_Compra.ObtenerProducto_Compra();
        }
    }
}
