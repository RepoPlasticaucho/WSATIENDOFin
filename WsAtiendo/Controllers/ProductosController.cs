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
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        [HttpGet]
        [Route("ObtenerProductos")]
        public ProductosAdm ObtenerProductos()
        {
            return DALProductos.ObtenerProductos();
        }

        [HttpPost]
        [Route("ObtenerProductosN")]
        public ProductosAdm ObtenerProductosN(ProductosEntity producto)
        {
            return DALProductos.ObtenerProductosN(producto.nombre);
        }

        [HttpPost]
        [Route("ObtenerProductosNomEti")]
        public ProductosAdm ObtenerProductosNomEti(ProductosEntity producto)
        {
            return DALProductos.ObtenerProductosNomEti(producto.nombre, producto.etiquetas);
        }

        [HttpPost]
        [Route("ObtenerProductosProveedor")]
        public ProductosAdm ObtenerProductosProveedor(ProveedoresEntity proveedor)
        {
            return DALProductos.ObtenerProductosProveedor(proveedor.id);
        }

        [HttpGet]
        [Route("VerificarProductosMP")]
        public ProductosAdm VerificarProductosMP(string color_id, string tamanio, string cod_fam)
        {
            return DALProductos.VerificarProductosMP(color_id, tamanio, cod_fam);
        }

        [HttpGet]
        [Route("ObtenerProductosID")]
        public ProductosAdm ObtenerProductosID(string tamanio, string color, string cod_fam)
        {
            return DALProductos.ObtenerProductosID(tamanio, color, cod_fam);
        }

        [HttpPost]
        [Route("ObtenerProductosTamanio")]
        public ProductosAdm ObtenerProductosTamanio(ModeloProductosEntity modelo_producto)
        {
            return DALProductos.ObtenerProductosTamanio(modelo_producto.cod_familia);
        }

        [HttpPost]
        [Route("ObtenerCatalogosProductos")]
        public ProductosAdm ObtenerCatalogosProductos(ProductosEntity producto)
        {
            return DALProductos.ObtenerCatalogosProductos(producto.nombre, producto.cod_sap);
        }

        [HttpPost]
        [Route("ObtenerProductosModeloProducto")]
        public ProductosAdm ObtenerProductosModeloProducto(ModeloProductosEntity modeloproducto)
        {
            return DALProductos.ObtenerProductosModeloProducto(modeloproducto.modelo_producto);
        }

        [HttpPost]
        [Route("InsertarProductos")]
        public ProductosAdm InsertarMarcas(ProductosEntity producto)
        {
            return DALProductos.InsertarProductos(producto);
        }

        [HttpPost]
        [Route("AgregarProductos")]
        public ProductosAdm AgregarProductos(ProductosEntity producto)
        {
            return DALProductos.AgregarProductos(producto);
        }

        [HttpPost]
        [Route("ModificarProductos")]
        public Productos ModificarProductos(ProductosEntity producto)
        {

            return DALProductos.ModificarProductos(producto);
        }

        [HttpPost]
        [Route("ActualizarProductos")]
        public Productos ActualizarProductos(ProductosEntity producto)
        {

            return DALProductos.ActualizarProductos(producto);
        }

        [HttpPost]
        [Route("EliminarProductos")]
        public Productos EliminarProductos(ProductosEntity producto)
        {
            return DALProductos.EliminarProductos(producto);
        }

        [HttpPost]
        [Route("DeshabilitarProductos")]
        public Productos DeshabilitarProductos(ProductosEntity producto)
        {

            return DALProductos.DeshabilitarProductos(producto);
        }
    }
}
