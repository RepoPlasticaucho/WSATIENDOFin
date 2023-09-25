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
    [RoutePrefix("api/sriws")]
    public class SriController : ApiController
    {
        [HttpGet]
        [Route("RecibirXMLSri")]
        public string RecibirXMLSri(string movimiento_id)
        {
            return DALSri.RecibirXMLSri(movimiento_id);
        }

        [HttpGet]
        [Route("AutorizarXMLSri")]
        public string AutorizarXMLSri(string movimiento_id)
        {
            return DALSri.AutorizarXMLSri(movimiento_id);
        }

        [HttpGet]
        [Route("EnviarComprobanteCorreo")]
        public string EnviarComprobanteCorreo(string movimiento_id)
        {
            return DALEnviarCom.EnviarComprobanteCorreo(movimiento_id);
        }
    }
}