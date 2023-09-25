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
    [RoutePrefix("api/terceros")]
    public class TercerosController : ApiController
    {
        [HttpGet]
        [Route("ObtenerTercerosAll")]
        public Terceros ObtenerTercerosAll()
        {
            return DALTerceros.ObtenerTercerosAll();
        }

        [HttpPost]
        [Route("ObtenerTerceros")]
        public Terceros ObtenerTerceros(TercerosEntity terceros)
        {
            return DALTerceros.ObtenerTerceros(terceros.sociedad_id,terceros.almacen_id);
        }

        [HttpPost]
        [Route("ObtenerTerceroCedula")]
        public Terceros ObtenerTerceroCedula(TercerosEntity terceros)
        {
            return DALTerceros.ObtenerTerceroCedula(terceros.id_fiscal);
        }

        [HttpPost]
        [Route("InsertarTercero")]
        public Terceros InsertarTercero(TercerosEntity terceros)
        {
            return DALTerceros.InsertarTercero(terceros);
        }

        [HttpPost]
        [Route("ModificarTerceros")]
        public Terceros ModificarTerceros(TercerosEntity terceros)
        {

            return DALTerceros.ModificarTerceros(terceros);
        }

        [HttpPost]
        [Route("ModificarCliente")]
        public Terceros ModificarCliente(TercerosEntity terceros)
        {

            return DALTerceros.ModificarCliente(terceros);
        }

        [HttpPost]
        [Route("EliminarTerceros")]
        public Terceros EliminarTerceros(TercerosEntity terceros)
        {
            return DALTerceros.EliminarTerceros(terceros);
        }
    }
}
