using System.Web.Http;
using System.Web.Http.Cors;
using WsAtiendo.DAL;
using WsAtiendo.Models;

namespace WsAtiendo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/almacenes")]
    public class AlmacenesController : ApiController
    {
        [HttpGet]
        [Route("ObtenerAlmacenes")]
        public Almacenes ObtenerAlmacenes()
        {
            return DALAlmacenes.ObtenerAlmacenes();
        }

        [HttpPost]
        [Route("ObtenerAlmacenesSociedad")]
        public Almacenes ObtenerAlmacenesSociedad(SociedadesEntity sociedad)
        {
            return DALAlmacenes.ObtenerAlmacenesSociedad(sociedad.idSociedad);
        }

        [HttpPost]
        [Route("ObtenerAlmacenesEsp")]
        public Almacenes ObtenerAlmacenesEsp(AlmacenesEntity almacen)
        {
            return DALAlmacenes.ObtenerAlmacenesEsp(almacen.sociedad_id, almacen.idAlmacen);
        }

        [HttpPost]
        [Route("ObtenerAlmacenID")]
        public Almacenes ObtenerAlmacenID(AlmacenesEntity almacen)
        {
            return DALAlmacenes.ObtenerAlmacenID(almacen.idAlmacen);
        }

        [HttpPost]
        [Route("InsertarAlmacen")]
        public Almacenes InsertarAlmacen(AlmacenesEntity almacen)
        {
            return DALAlmacenes.InsertarAlmacen(almacen);
        }

        [HttpPost]
        [Route("ModificarAlmacen")]
        public Almacenes ModificarAlmacen(AlmacenesEntity almacen)
        {
            return DALAlmacenes.ModificarAlmacen(almacen);
        }

        [HttpPost]
        [Route("EliminarAlmacen")]
        public Almacenes EliminarAlmacen(AlmacenesEntity almacen)
        {
            return DALAlmacenes.EliminarAlmacen(almacen);
        }

        [HttpPost]
        [Route("ObtenerAlmacenesS")]
        public Almacenes ObtenerAlmacenesS(SociedadesEntity almacen)
        {
            return DALAlmacenes.ObtenerAlmacenesS(almacen.nombre_comercial);
        }

        [HttpPost]
        [Route("ObtenerAlmacenN")]
        public Almacenes ObtenerAlmacenN(AlmacenesEntity nombre)
        {
            return DALAlmacenes.ObtenerAlmacenN(nombre.nombre_almacen);
        }
    }
}
