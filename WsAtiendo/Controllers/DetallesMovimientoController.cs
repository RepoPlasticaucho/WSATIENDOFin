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
    [RoutePrefix("api/detallesmovimiento")]
    public class DetallesMovimientoController : ApiController
    {

        [HttpPost]
        [Route("ObtenerDetalleMovimiento")]
        public DetalleMovimientos ObtenerDetalleMovimiento(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.ObtenerDetalleMovimiento(detalle.movimiento_id);
        }

        [HttpPost]
        [Route("ObtenerDetalleMovimientoEx")]
        public DetalleMovimientos ObtenerDetalleMovimientoEx(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.ObtenerDetalleMovimientoEx(detalle.movimiento_id, detalle.producto_id);
        }

        [HttpPost]
        [Route("ObtenerUltDetalleMovimiento")]
        public DetalleMovimientos ObtenerUltDetalleMovimiento(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.ObtenerUltDetalleMovimiento(detalle.movimiento_id);
        }

        [HttpPost]
        [Route("ObtenerDetalleMovimientoSociedad")]
        public DetalleMovimientos ObtenerDetalleMovimientoSociedad(SociedadesEntity sociedad)
        {
            return DALDetallesMovimiento.ObtenerDetalleMovimientoSociedad(sociedad.idSociedad);
        }

        [HttpPost]
        [Route("ObtenerDetalleMovimientoAlm")]
        public DetalleMovimientos ObtenerDetalleMovimientoAlm(AlmacenesEntity almacen)
        {
            return DALDetallesMovimiento.ObtenerDetalleMovimientoAlm(almacen.nombre_almacen);
        }

        [HttpGet]
        [Route("ObtenerDetalleMovimientoAlmF")]
        public DetalleMovimientos ObtenerDetalleMovimientoAlmF(string almacen, string fechadesde, string fechahasta)
        {
            return DALDetallesMovimiento.ObtenerDetalleMovimientoAlmF(almacen, fechadesde, fechahasta);
        }

        [HttpGet]
        [Route("ObtenerDetalleMovimientoAlmFTipo")]
        public DetalleMovimientos ObtenerDetalleMovimientoAlmFTipo(string almacen, string fechadesde, string fechahasta, string tipo)
        {
            return DALDetallesMovimiento.ObtenerDetalleMovimientoAlmFTipo(almacen, fechadesde, fechahasta, tipo);
        }

        [HttpGet]
        [Route("ObtenerDetalleMovimientoAlmTipo")]
        public DetalleMovimientos ObtenerDetalleMovimientoAlmTipo(string almacen, string tipo)
        {
            return DALDetallesMovimiento.ObtenerDetalleMovimientoAlmTipo(almacen, tipo);
        }

        [HttpPost]
        [Route("InsertarDetallePedido")]
        public DetalleMovimientos InsertarDetallePedido(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.InsertarDetallePedido(detalle);
        }

        [HttpPost]
        [Route("InsertarDetalleCompras")]
        public DetalleMovimientos InsertarDetalleCompras(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.InsertarDetalleCompras(detalle);
        }

        [HttpPost]
        [Route("InsertarDetalleCompra")]
        public DetalleMovimientos InsertarDetalleCompra(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.InsertarDetalleCompra(detalle);
        }

        [HttpPost]
        [Route("ModificarDetallePedido")]
        public DetalleMovimientos ModificarDetallePedido(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.ModificarDetallePedido(detalle);
        }

        [HttpPost]
        [Route("ModificarDetallePedidoVenta")]
        public DetalleMovimientos ModificarDetallePedidoVenta(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.ModificarDetallePedidoVenta(detalle);
        }

        [HttpPost]
        [Route("ModificarDetalleCompra")]
        public DetalleMovimientos ModificarDetalleCompra(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.ModificarDetalleCompra(detalle);
        }

        [HttpPost]
        [Route("EliminarDetalleMovimiento")]
        public DetalleMovimientos EliminarDetalleMovimiento(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.EliminarDetalleMovimiento(detalle);
        }

        [HttpPost]
        [Route("EliminarDetalleMovimientoVenta")]
        public DetalleMovimientos EliminarDetalleMovimientoVenta(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.EliminarDetalleMovimientoVenta(detalle);
        }

        [HttpPost]
        [Route("EliminarDetalleCompra")]
        public DetalleMovimientos EliminarDetalleCompra(DetalleMovimientosEntity detalle)
        {
            return DALDetallesMovimiento.EliminarDetalleCompra(detalle);
        }
    }
}