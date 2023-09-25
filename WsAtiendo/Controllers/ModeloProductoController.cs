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
    [RoutePrefix("api/modeloProducto")]
    public class ModeloProductoController : ApiController
    {

        [HttpGet]
        [Route("ObtenerModeloProductos")]
        public ModeloProductos ObtenerModeloProductos()
        {
            return DALModelo_Productos.ObtenerModeloProductos();
        }

        [HttpPost]
        [Route("InsertarModeloProductos")]
        public ModeloProductos InsertarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            return DALModelo_Productos.InsertarModeloProductos(modeloProductosEntity);
        }

        [HttpPost]
        [Route("ModificarModeloProductos")]
        public ModeloProductos ModificarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            return DALModelo_Productos.ModificarModeloProductos(modeloProductosEntity);
        }

        [HttpPost]
        [Route("ActualizarModeloProductos")]
        public ModeloProductos ActualizarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            return DALModelo_Productos.ActualizarModeloProductos(modeloProductosEntity);
        }

        [HttpPost]
        [Route("EliminarModeloProductos")]
        public ModeloProductos EliminarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            return DALModelo_Productos.EliminarModeloProductos(modeloProductosEntity);
        }

        [HttpPost]
        [Route("DeshabilitarModeloProductos")]
        public ModeloProductos DeshabilitarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            return DALModelo_Productos.DeshabilitarModeloProductos(modeloProductosEntity);
        }

        [HttpPost]
        [Route("ObtenerModeloProductosModelos")]
        public ModeloProductos ObtenerModeloProductosModelos(ModelosEntity modelo)
        {
            return DALModelo_Productos.ObtenerModeloProductosModelos(modelo.modelo, modelo.almacen_id);

        }

        [HttpPost]
        [Route("ObtenerModeloProductosModelosAdm")]
        public ModeloProductos ObtenerModeloProductosModelosAdm(ModelosEntity modelo)
        {
            return DALModelo_Productos.ObtenerModeloProductosModelosAdm(modelo.modelo);

        }

        [HttpPost]
        [Route("ObtenerModeloProductosMarca")]
        public ModeloProductos ObtenerModeloProductosMarca(MarcasEntity marca)
        {
            return DALModelo_Productos.ObtenerModeloProductosMarca(marca.marca);

        }

        [HttpPost]
        [Route("ObtenerModeloProductosAlmacen")]
        public ModeloProductos ObtenerModeloProductosAlmacen(AlmacenesEntity almacen)
        {
            return DALModelo_Productos.ObtenerModeloProductosAlmacen(almacen.idAlmacen);

        }

        [HttpPost]
        [Route("ObtenerModeloProductosColor")]
        public ModeloProductos ObtenerModeloProductosColor(ModeloProductosEntity modelo_producto)
        {
            return DALModelo_Productos.ObtenerModeloProductosColor(modelo_producto.cod_familia);

        }

        [HttpPost]
        [Route("ObtenerCatalogoModeloProductos")]
        public ModeloProductos ObtenerCatalogoModeloProductos(ModeloProductosEntity modelo_producto)
        {
            return DALModelo_Productos.ObtenerCatalogoModeloProductos( modelo_producto.modelo_id, modelo_producto.color_id,modelo_producto.atributo_id,modelo_producto.genero_id);

        }

        [HttpPost]
        [Route("ObtenerModeloProductosNombre")]
        public ModeloProductos ObtenerModeloProductosNombre(ModeloProductosEntity modelo_producto)
        {
            return DALModelo_Productos.ObtenerModeloProductosNombre(modelo_producto.modelo_producto);

        }
    }
}