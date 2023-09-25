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
    [RoutePrefix("api/proveedoresproductos")]
    public class ProveedoresProductosController : ApiController
    {
        [HttpPost]
        [Route("AgregarProductosProv")]
        public ProveedoresProductos AgregarProductosProv(ProveedoresProductosEntity producto)
        {
            return DALProveedoresProductos.AgregarProductosProv(producto);
        }

        [HttpPost]
        [Route("ObtenerProveedoresProductosProv")]
        public ProveedoresProductos ObtenerProveedoresProductosProv(ProveedoresProductosEntity proveedor)
        {
            return DALProveedoresProductos.ObtenerProveedoresProductosProv(proveedor.provedor_id);
        }

        [HttpPost]
        [Route("ObtenerProductosProveedores")]
        public ProveedoresProductos ObtenerProductosProveedores(ProveedoresProductosEntity productoproveedor)
        {
            return DALProveedoresProductos.ObtenerProductosProveedores(productoproveedor.provedor_id,productoproveedor.producto_id);
        }

        [HttpPost]
        [Route("ActualizarProductosProveedores")]
        public ProveedoresProductos ActualizarProductosProveedores(ProveedoresProductosEntity productoproveedor)
        {
            return DALProveedoresProductos.ActualizarProductosProveedores(productoproveedor);
        }
    }
}