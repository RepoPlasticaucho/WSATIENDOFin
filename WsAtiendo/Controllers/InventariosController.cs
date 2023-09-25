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
    [RoutePrefix("api/inventarios")]
    public class InventariosController : ApiController
    {

        [HttpGet]
        [Route("ObtenerInventarios")]
        public Inventarios ObtenerInventarios()
        {
            return DALInventarios.ObtenerInventarios();
        }

        [HttpPost]
        [Route("ObtenerPortafolios")]
        public Inventarios ObtenerPortafolios(AlmacenesEntity almacen)
        {
            return DALInventarios.ObtenerPortafolios(almacen.idAlmacen);
        }

        [HttpPost]
        [Route("ObtenerInventarioExiste")]
        public Inventarios ObtenerInventarioExiste(InventariosEntity inventario)
        {
            return DALInventarios.ObtenerInventarioExiste(inventario.producto_id, inventario.almacen_id);
        }

        [HttpPost]
        [Route("ObtenerInventarioAlm")]
        public Inventarios ObtenerInventarioAlm(InventariosEntity inventario)
        {
            return DALInventarios.ObtenerInventarioAlm(inventario.producto_id, inventario.almacen_id);
        }

        [HttpPost]
        [Route("ObtenerPortafoliosCategoria")]
        public Inventarios ObtenerPortafoliosCategoria(InventariosEntity inventario)
        {
            return DALInventarios.ObtenerPortafoliosCategoria(inventario.categoria_id, inventario.almacen_id);
        }

        [HttpPost]
        [Route("ObtenerPortafoliosLineas")]
        public Inventarios ObtenerPortafoliosLineas(InventariosEntity inventario)
        {
            return DALInventarios.ObtenerPortafoliosLineas(inventario.almacen_id, inventario.linea, inventario.categoria_id);
        }

        [HttpPost]
        [Route("ObtenerPortafoliosModelos")]
        public Inventarios ObtenerPortafoliosModelos(InventariosEntity inventario)
        {
            return DALInventarios.ObtenerPortafoliosModelos(inventario.almacen_id, inventario.modelo, inventario.linea, inventario.categoria_id);
        }

        [HttpPost]
        [Route("ObtenerPortafoliosModelosColores")]
        public Inventarios ObtenerPortafoliosModelosColores(InventariosEntity inventario)
        {
            return DALInventarios.ObtenerPortafoliosModelosColores(inventario.almacen_id, inventario.modelo, inventario.color,inventario.categoria_id,inventario.linea);
        }

        [HttpPost]
        [Route("ObtenerPortafoliosLineasSugerido")]
        public Inventarios ObtenerPortafoliosLineasSugerido(InventariosEntity inventario)
        {
            return DALInventarios.ObtenerPortafoliosLineasSugerido(inventario.almacen_id, inventario.linea, inventario.categoria_id);
        }

        [HttpPost]
        [Route("ObtenerPortafoliosCategoriaSugerido")]
        public Inventarios ObtenerPortafoliosCategoriaSugerido(InventariosEntity inventario)
        {
            return DALInventarios.ObtenerPortafoliosCategoriaSugerido(inventario.categoria_id, inventario.almacen_id);
        }

        [HttpPost]
        [Route("InsertarInventarios")]
        public Inventarios InsertarInventarios(InventariosEntity inventario)
        {
            return DALInventarios.InsertarInventarios(inventario);
        }

        [HttpPost]
        [Route("InsertarInventarioUltimo")]
        public Inventarios InsertarInventarioUltimo(InventariosEntity inventario)
        {
            return DALInventarios.InsertarInventarioUltimo(inventario);
        }

        [HttpPost]
        [Route("ModificarInventarios")]
        public Inventarios ModificarInventarios(InventariosEntity inventario)
        {

            return DALInventarios.ModificarInventarios(inventario);
        }

        [HttpPost]
        [Route("ActualizarInventarios")]
        public Inventarios ActualizarInventarios(InventariosEntity inventario)
        {

            return DALInventarios.ActualizarInventarios(inventario);
        }

        [HttpPost]
        [Route("ActualizarInventarioEx")]
        public Inventarios ActualizarInventarioEx(InventariosEntity inventario)
        {
            return DALInventarios.ActualizarInventarioEx(inventario);
        }

        [HttpPost]
        [Route("ActualizarSinc")]
        public Inventarios ActualizarSinc(InventariosEntity inventario)
        {
            return DALInventarios.ActualizarSinc(inventario);
        }

        [HttpPost]
        [Route("ActualizarCosto")]
        public Inventarios ActualizarCosto(InventariosEntity inventario)
        {

            return DALInventarios.ActualizarCosto(inventario);
        }

        [HttpPost]
        [Route("EliminarInventarios")]
        public Inventarios EliminarInventarios(InventariosEntity inventarios)
        {
            return DALInventarios.EliminarInventarios(inventarios);
        }

        [HttpPost]
        [Route("DeshabilitarInventarios")]
        public Inventarios DeshabilitarInventarios(InventariosEntity inventarios)
        {
            return DALInventarios.DeshabilitarInventarios(inventarios);
        }

        [HttpPost]
        [Route("ObtenerPortafoliosInventarios")]
        public Inventarios ObtenerPortafoliosInventarios(InventariosEntity inventario)
        {
            return DALInventarios.ObtenerPortafoliosInventarios(inventario.producto_id, inventario.almacen_id);
        }
    }
}
