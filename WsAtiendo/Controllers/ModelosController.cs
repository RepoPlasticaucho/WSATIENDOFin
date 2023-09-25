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
    [RoutePrefix("api/modelos")]
    public class ModelosController : ApiController
    {


        [HttpGet]
        [Route("ObtenerModelos")]
        public Modelos ObtenerModelos()
        {
            return DALModelos.ObtenerModelos();
        }

        [HttpPost]
        [Route("InsertarModelos")]
        public Modelos InsertarModelos(ModelosEntity categoria)
        {
            return DALModelos.InsertarModelo(categoria);
        }

        [HttpPost]
        [Route("ModificarModelos")]
        public Modelos ModificarModelos(ModelosEntity categoria)
        {
            return DALModelos.ModificarModelo(categoria);
        }

        [HttpPost]
        [Route("ActualizarModelos")]
        public Modelos ActualizarModelos(ModelosEntity categoria)
        {
            return DALModelos.ActualizarModelos(categoria);
        }

        [HttpPost]
        [Route("EliminarModelos")]
        public Modelos EliminarModelos(ModelosEntity categoria)
        {
            return DALModelos.EliminarModelo(categoria);
        }

        [HttpPost]
        [Route("ObtenerModelosLineas")]
        public Modelos ObtenerModelosLineas(LineasEntity linea)
        {
            return DALModelos.ObtenerModelosLineas(linea.linea, linea.almacen_id);
        }
        [HttpPost]
        [Route("ObtenerModelosLineasAdm")]
        public Modelos ObtenerModelosLineasAdm(LineasEntity linea)
        {
            return DALModelos.ObtenerModelosLineasAdm(linea.linea);
        }

        [HttpGet]
        [Route("ObtenerModelosLineasMarcas")]
        public Modelos ObtenerModelosLineasMarcas(string linea, string marca)
        {
            return DALModelos.ObtenerModelosLineasMarcas(linea, marca);
        }

        [HttpPost]
        [Route("ObtenerCatalogoModelo")]
        public Modelos ObtenerCatalogoModelo(ModelosEntity modelo)
        {
            return DALModelos.ObtenerCatalogoModelo(modelo.linea_id,modelo.modelo,modelo.marca_id);
        }

        [HttpPost]
        [Route("ObtenerLineaModelo")]
        public Modelos ObtenerLineaModelo(ModelosEntity modelo)
        {
            return DALModelos.ObtenerLineaModelo(modelo.modelo);
        }

        [HttpPost]
        [Route("ObtenerModelosNombre")]
        public Modelos ObtenerModelosNombre(ModelosEntity modelo)
        {
            return DALModelos.ObtenerModelosNombre(modelo.modelo);
        }
    }
}