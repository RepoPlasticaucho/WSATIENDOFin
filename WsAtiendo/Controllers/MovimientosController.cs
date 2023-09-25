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
    [RoutePrefix("api/movimientos")]
    public class MovimientosController : ApiController
    {

        [HttpPost]
        [Route("ObtenerMovimientosAlmacen")]
        public Movimientos ObtenerMovimientosAlmacen(MovimientosEntity movimiento)
        {
            return DALMovimientos.ObtenerMovimientosAlmacen(movimiento.almacen_id);
        }

        [HttpPost]
        [Route("ObtenerMovimientosAlmacenV")]
        public Movimientos ObtenerMovimientosAlmacenV(MovimientosEntity movimiento)
        {
            return DALMovimientos.ObtenerMovimientosAlmacenV(movimiento.almacen_id);
        }

        [HttpGet]
        [Route("ObtenerMovimientosAlmacenFecha")]
        public Movimientos ObtenerMovimientosAlmacenFecha(string almacen, string fechadesde, string fechahasta)
        {
            return DALMovimientos.ObtenerMovimientosAlmacenFecha(almacen, fechadesde, fechahasta);
        }

        [HttpGet]
        [Route("ObtenerMovimientosAlmacenNombre")]
        public Movimientos ObtenerMovimientosAlmacenNombre(string almacen)
        {
            return DALMovimientos.ObtenerMovimientosAlmacenNombre(almacen);
        }

        [HttpGet]
        [Route("ObtenerMovimientosFechas")]
        public Movimientos ObtenerMovimientosFechas(string sociedad, string fechadesde, string fechahasta)
        {
            return DALMovimientos.ObtenerMovimientosFechas(sociedad, fechadesde, fechahasta);
        }

        [HttpGet]
        [Route("ObtenerMovimientosTodos")]
        public Movimientos ObtenerMovimientosTodos(string sociedad)
        {
            return DALMovimientos.ObtenerMovimientosTodos(sociedad);
        }

        [HttpGet]
        [Route("ObtenerMovimientosAlmacenFechaPAGO")]
        public Movimientos ObtenerMovimientosAlmacenFechaPAGO(string almacen, string fechadesde, string fechahasta, string pago)
        {
            return DALMovimientos.ObtenerMovimientosAlmacenFechaPAGO(almacen, fechadesde, fechahasta, pago);
        }

        [HttpGet]
        [Route("ObtenerMovimientosAlmacenNombrePAGO")]
        public Movimientos ObtenerMovimientosAlmacenNombrePAGO(string almacen, string pago)
        {
            return DALMovimientos.ObtenerMovimientosAlmacenNombrePAGO(almacen, pago);
        }

        [HttpGet]
        [Route("ObtenerMovimientosFechasPAGO")]
        public Movimientos ObtenerMovimientosFechasPAGO(string sociedad, string fechadesde, string fechahasta, string pago)
        {
            return DALMovimientos.ObtenerMovimientosFechasPAGO(sociedad, fechadesde, fechahasta, pago);
        }

        [HttpGet]
        [Route("ObtenerMovimientosTodosPAGO")]
        public Movimientos ObtenerMovimientosTodosPAGO(string sociedad, string pago)
        {
            return DALMovimientos.ObtenerMovimientosTodosPAGO(sociedad, pago);
        }

        [HttpPost]
        [Route("ObtenerMovimientosAlmacenC")]
        public Movimientos ObtenerMovimientosAlmacenC(MovimientosEntity movimiento)
        {
            return DALMovimientos.ObtenerMovimientosAlmacenC(movimiento.almacen_id);
        }

        [HttpGet]
        [Route("ObtenerUltimoSecuencial")]
        public Movimientos ObtenerUltimoSecuencial(string almacen_id)
        {
            return DALMovimientos.ObtenerUltimoSecuencial(almacen_id);
        }

        [HttpPost]
        [Route("ObtenerMovimientoUno")]
        public Movimientos ObtenerMovimientoUno(MovimientosEntity movimiento)
        {
            return DALMovimientos.ObtenerMovimientoUno(movimiento.almacen_id);
        }

        [HttpPost]
        [Route("ObtenerMovimientoCLAVEACCESO")]
        public Movimientos ObtenerMovimientoCLAVEACCESO(MovimientosEntity movimiento)
        {
            return DALMovimientos.ObtenerMovimientoCLAVEACCESO(movimiento.id);
        }

        [HttpPost]
        [Route("ObtenerMovimientoID")]
        public Movimientos ObtenerMovimientoID(MovimientosEntity movimiento)
        {
            return DALMovimientos.ObtenerMovimientoID(movimiento.id);
        }

        [HttpGet]
        [Route("ObtenerMovimientosCompF")]
        public Movimientos ObtenerMovimientosCompF(string almacen, string fechadesde, string fechahasta)
        {
            return DALMovimientos.ObtenerMovimientosCompF(almacen, fechadesde, fechahasta);
        }

        [HttpGet]
        [Route("ObtenerMovimientosVentF")]
        public Movimientos ObtenerMovimientosVentF(string almacen, string fechadesde, string fechahasta)
        {
            return DALMovimientos.ObtenerMovimientosVentF(almacen, fechadesde, fechahasta);
        }

        [HttpGet]
        [Route("CrearXML")]
        public String CrearXML(string movimiento)
        {
            return DALMovimientos.CrearXML(movimiento);
        }

        [HttpPost]
        [Route("InsertarMovimiento")]
        public Movimientos InsertarMovimiento(MovimientosEntity movimiento)
        {
            return DALMovimientos.InsertarMovimiento(movimiento);
        }

        [HttpPost]
        [Route("ActualizarCLAVEACCESO")]
        public Movimientos ActualizarCLAVEACCESO(MovimientosEntity movimiento)
        {
            return DALMovimientos.ActualizarCLAVEACCESO(movimiento);
        }

        [HttpPost]
        [Route("FinalizarPedido")]
        public Movimientos FinalizarPedido(MovimientosEntity movimiento)
        {
            return DALMovimientos.FinalizarPedido(movimiento);
        }

        [HttpPost]
        [Route("FinalizarCompra")]
        public Movimientos FinalizarCompra(MovimientosEntity movimiento)
        {
            return DALMovimientos.FinalizarCompra(movimiento);
        }

        [HttpPost]
        [Route("ActualizarTerceroPedido")]
        public Movimientos ActualizarTerceroPedido(MovimientosEntity movimiento)
        {
            return DALMovimientos.ActualizarTerceroPedido(movimiento);
        }

        [HttpPost]
        [Route("ActualizarClientePedido")]
        public Movimientos ActualizarClientePedido(MovimientosEntity movimiento)
        {
            return DALMovimientos.ActualizarClientePedido(movimiento);
        }
    }
}