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
    [RoutePrefix("api/detallespago")]
    public class DetallesPagoController : ApiController
    {
        [HttpPost]
        [Route("InsertarDetallePago")]
        public DetallePagos InsertarDetallePago(DetallePagosEntity detalle)
        {
            return DALDetallesPagos.InsertarDetallePago(detalle);
        }

        [HttpPost]
        [Route("ObtenerDetallePagoMov")]
        public DetallePagos ObtenerDetallePagoMov(MovimientosEntity movimiento)
        {
            return DALDetallesPagos.ObtenerDetallePagoMov(movimiento.id);
        }

        [HttpPost]
        [Route("ObtenerDetallePagoMovimiento")]
        public DetallePagos ObtenerDetallePagoMovimiento(MovimientosEntity movimiento)
        {
            return DALDetallesPagos.ObtenerDetallePagoMovimiento(movimiento.id);
        }

        [HttpPost]
        [Route("ObtenerDetallePagoE")]
        public DetallePagos ObtenerDetallePagoE(AlmacenesEntity almacen)
        {
            return DALDetallesPagos.ObtenerDetallePagoE(almacen.sociedad_id);
        }

        [HttpPost]
        [Route("ObtenerDetallePagoTD")]
        public DetallePagos ObtenerDetallePagoTD(AlmacenesEntity almacen)
        {
            return DALDetallesPagos.ObtenerDetallePagoTD(almacen.sociedad_id);
        }

        [HttpPost]
        [Route("ObtenerDetallePagoTC")]
        public DetallePagos ObtenerDetallePagoTC(AlmacenesEntity almacen)
        {
            return DALDetallesPagos.ObtenerDetallePagoTC(almacen.sociedad_id);
        }

        [HttpGet]
        [Route("ObtenerDetallePagoAlm")]
        public DetallePagos ObtenerDetallePagoAlm(string almacen, string forma)
        {
            return DALDetallesPagos.ObtenerDetallePagoAlm(almacen, forma);
        }

        [HttpGet]
        [Route("ObtenerDetallePagoAlmF")]
        public DetallePagos ObtenerDetallePagoAlmF(string almacen, string forma, string fechadesde, string fechahasta)
        {
            return DALDetallesPagos.ObtenerDetallePagoAlmF(almacen, forma, fechadesde, fechahasta);
        }

        [HttpGet]
        [Route("ObtenerDetallePagoF")]
        public DetallePagos ObtenerDetallePagoF(string sociedad, string forma, string fechadesde, string fechahasta)
        {
            return DALDetallesPagos.ObtenerDetallePagoF(sociedad, forma, fechadesde, fechahasta);
        }

        [HttpPost]
        [Route("ModificarDetallePago")]
        public DetallePagos ModificarDetallePago(DetallePagosEntity detalle)
        {
            return DALDetallesPagos.ModificarDetallePago(detalle);
        }

        [HttpPost]
        [Route("EliminarDetallePago")]
        public DetallePagos EliminarDetallePago(DetallePagosEntity detalle)
        {
            return DALDetallesPagos.EliminarDetallePago(detalle);
        }
    }
}