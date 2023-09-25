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
    [RoutePrefix("api/categorias")]
    public class CategoriasController : ApiController
    {

        [HttpGet]
        [Route("ObtenerCategorias")]
        public Categorias ObtenerGrupos()
        {
            return DALCategorias.ObtenerCategorias();
        }

        [HttpPost]
        [Route("InsertarCategorias")]
        public Categorias InsertarCategorias(CategoriasEntity categoria)
        {
            return DALCategorias.InsertarCategorias(categoria);
        }

        [HttpPost]
        [Route("ModificarCategorias")]
        public Categorias ModificarCategorias(CategoriasEntity categoria)
        {
            return DALCategorias.ModificarCategoria(categoria);
        }

        [HttpPost]
        [Route("EliminarCategorias")]
        public Categorias EliminarCategorias(CategoriasEntity categoria)
        {
            return DALCategorias.EliminarCategoria(categoria);
        }

        [HttpPost]
        [Route("ObtenerCategoriasid")]
        public Categorias ObtenerCategoriasid(CategoriasEntity categoria)
        {
            return DALCategorias.ObtenerCategoriasid(categoria.id);
        }

        [HttpPost]
        [Route("ObtenerCategoriaNombre")]
        public Categorias ObtenerCategoriaNombre(CategoriasEntity categoria)
        {
            return DALCategorias.ObtenerCategoriaNombre(categoria.categoria);
        }

        [HttpPost]
        [Route("ObtenerCategoriaMarca")]
        public Categorias ObtenerCategoriaMarca(MarcasEntity marca)
        {
            return DALCategorias.ObtenerCategoriaMarca(marca.marca);
        }

        [HttpPost]
        [Route("ObtenerCategoriasPro")]
        public Categorias ObtenerCategoriasPro(ProveedoresEntity proveedor)
        {
            return DALCategorias.ObtenerCategoriasPro(proveedor.nombre);
        }

    }
}
