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
    [RoutePrefix("api/lineas")]
    public class LineasController : ApiController
    {

        [HttpGet]
        [Route("ObtenerLineas")]
        public Lineas ObtenerLineas()
        {
            return DALLineas.ObtenerLineas();
        }

        [HttpPost]
        [Route("InsertarLineas")]
        public Lineas InsertarLineas(LineasEntity linea)
        {
            return DALLineas.InsertarLineas(linea);
        }

        [HttpPost]
        [Route("ModificarLineas")]
        public Lineas ModificarLineas(LineasEntity linea)
        {
            return DALLineas.ModificarLinea(linea);
        }

        [HttpPost]
        [Route("EliminarLineas")]
        public Lineas EliminarLineas(LineasEntity linea)
        {
            return DALLineas.EliminarLinea(linea);
        }

        [HttpPost]
        [Route("ObtenerLineasCategoria")]
        public Lineas ObtenerLineasCategoria(CategoriasEntity categoria)
        {
            return DALLineas.ObtenerLineasCategoria(categoria.id, categoria.almacen_id);
        }

        [HttpPost]
        [Route("ObtenerLineasCategoriaAdm")]
        public Lineas ObtenerLineasCategoriaAdm(CategoriasEntity categoria)
        {
            return DALLineas.ObtenerLineasCategoriaAdm(categoria.categoria);
        }

        [HttpGet]
        [Route("ObtenerLineasCategoriaMarca")]
        public Lineas ObtenerLineasCategoriaMarca(string categoria, string marca)
        {
            return DALLineas.ObtenerLineasCategoriaMarca(categoria, marca);
        }

        [HttpPost]
        [Route("ObtenerCatalogoLineas")]
        public Lineas ObtenerCatalogoLineas(LineasEntity linea)
        {
            return DALLineas.ObtenerCatalogoLineas(linea.categoria_nombre, linea.linea);
        }

        [HttpPost]
        [Route("ObtenerCategoriaLinea")]
        public Lineas ObtenerCategoriaLinea(LineasEntity linea)
        {
            return DALLineas.ObtenerCategoriaLinea(linea.linea);
        }

        [HttpPost]
        [Route("ObtenerLineasNombre")]
        public Lineas ObtenerLineasNombre(LineasEntity linea)
        {
            return DALLineas.ObtenerLineasNombre(linea.categoria_nombre, linea.linea);
        }
    }
}