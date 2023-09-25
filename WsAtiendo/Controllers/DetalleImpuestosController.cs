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
    [RoutePrefix("api/detalleimpuestos")]
    public class DetalleImpuestosController : ApiController
    {

        [HttpPost]
        [Route("ObtenerDetalleImpuesto")]
        public DetalleImpuestos ObtenerDetalleImpuesto(DetalleImpuestosEntity detalle)
        {
            return DALDetalleImpuestos.ObtenerDetalleImpuesto(detalle.detalle_movimiento_id);
        }


        [HttpPost]
        [Route("InsertarDetalleImpuestos")]
        public DetalleImpuestos InsertarDetalleImpuestos(DetalleImpuestosEntity detalle)
        {
            return DALDetalleImpuestos.InsertarDetalleImpuestos(detalle);
        }

        [HttpPost]
        [Route("ModificarMovimientoBP")]
        public DetalleImpuestos ModificarMovimientoBP(DetalleImpuestosEntity detalle)
        {
            return DALDetalleImpuestos.ModificarMovimientoBP(detalle);
        }

        [HttpPost]
        [Route("ModificarDetalleImpuestosBP")]
        public DetalleImpuestos ModificarDetalleImpuestosBP(DetalleImpuestosEntity detalle)
        {
            return DALDetalleImpuestos.ModificarDetalleImpuestosBP(detalle);
        }

        [HttpPost]
        [Route("ModificarDetalleImpuestosVal")]
        public DetalleImpuestos ModificarDetalleImpuestosVal(DetalleImpuestosEntity detalle)
        {
            return DALDetalleImpuestos.ModificarDetalleImpuestosVal(detalle);
        }
    }
}