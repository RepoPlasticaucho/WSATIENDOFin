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
    [RoutePrefix("api/proveedores")]
    public class ProveedoresController : ApiController
    {
        [HttpGet]
        [Route("ObtenerProveedores")]
        public Proveedores ObtenerProveedores()
        {
            return DALProveedores.ObtenerProveedores();
        }

        [HttpPost]
        [Route("ObtenerProveedoresAll")]
        public Proveedores ObtenerProveedoresALL(SociedadesEntity sociedad)
        {
            return DALProveedores.ObtenerProveedoresAll(sociedad.idSociedad);
        }

        [HttpPost]
        [Route("ObtenerProveedoresS")]
        public Proveedores ObtenerProveedoresS(SociedadesEntity sociedad)
        {
            return DALProveedores.ObtenerProveedoresS(sociedad.idSociedad);
        }
        [HttpPost]
        [Route("ObtenerProveedoresN")]
        public Proveedores ObtenerProveedoresN(ProveedoresEntity name)
        {
            return DALProveedores.ObtenerProveedoresN(name.nombre);
        }

        [HttpPost]
        [Route("ObtenerProveedoresID")]
        public Proveedores ObtenerProveedoresID(ProveedoresEntity name)
        {
            return DALProveedores.ObtenerProveedoresID(name.id, name.sociedad_id);
        }

        [HttpPost]
        [Route("InsertarProveedores")]
        public Proveedores InsertarProveedores(ProveedoresEntity proveedor)
        {
            return DALProveedores.InsertarProveedores(proveedor);
        }

        [HttpPost]
        [Route("ModificarProveedores")]
        public Proveedores ModificarProveedores(ProveedoresEntity proveedor)
        {
            return DALProveedores.ModificarProveedores(proveedor);
        }

        [HttpPost]
        [Route("EliminarProveedor")]
        public Proveedores EliminarProveedor(ProveedoresEntity proveedor)
        {
            return DALProveedores.EliminarProveedor(proveedor);
        }
    }
}