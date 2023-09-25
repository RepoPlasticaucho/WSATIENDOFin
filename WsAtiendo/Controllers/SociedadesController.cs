using System.Web.Http;
using System.Web.Http.Cors;
using WsAtiendo.DAL;
using WsAtiendo.DALSAP;
using WsAtiendo.Models;

namespace WsAtiendo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/sociedades")]
    public class SociedadesController : ApiController
    {
        [HttpGet]
        [Route("ObtenerSociedades")]
        public Sociedades ObtenerGrupos()
        {
            return DALSociedades.ObtenerSociedades();
        }

        [HttpPost]
        [Route("EliminarSociedad")]
        public Sociedades EliminarSociedad(SociedadesEntity sociedad)
        {
            return DALSociedades.EliminarSociedad(sociedad);
        }

        [HttpPost]
        [Route("InsertarSociedad")]
        public Sociedades InsertarSociedad(SociedadesEntity sociedad)
        {
            if (sociedad.idGrupo.Equals("1"))
            {
                sociedad = DALClientes.ObtenerSociedadesSAP(sociedad);
            }
            return DALSociedades.InsertarSociedad(sociedad);
        }

        [HttpPost]
        [Route("ModificarSociedad")]
        public Sociedades ModificarSociedad(SociedadesEntity sociedad)
        {
            if (sociedad.idGrupo.Equals("1"))
            {
                sociedad = DALClientes.ObtenerSociedadesSAP(sociedad);
            }
            return DALSociedades.ModificarSociedad(sociedad);
        }

        [HttpPost]
        [Route("ObtenerSociedad")]
        public Sociedades ObtenerSociedad(SociedadesEntity sociedad)
        {
            return DALSociedades.ObtenerSociedad(sociedad);
        }

        [HttpPost]
        [Route("ObtenerUsuario")]
        public Sociedades ObtenerObtenerUsuario(SociedadesEntity sociedad)
        {
            return DALSociedades.ObtenerUsuario(sociedad);
        }

        [HttpPost]
        [Route("ObtenerUser")]
        public Sociedades ObtenerObtenerUs(SociedadesEntity sociedad)
        {
            return DALSociedades.ObtenerUser(sociedad);
        }

        [HttpPost]
        [Route("ObtenerSociedadDatos")]
        public Sociedades ObtenerSociedadDatos(SociedadesEntity sociedad)
        {
            return DALSociedades.ObtenerSociedadDatos(sociedad);
        }

        [HttpPost]
        [Route("ActualizarSociedad")]
        public Sociedades ActualizarSociedad(SociedadesEntity sociedad)
        {
            return DALSociedades.ActualizarSociedad(sociedad);
        }

        [HttpPost]
        [Route("ActualizarPass")]
        public Sociedades ActualizarPass(SociedadesEntity sociedad)
        {
            return DALSociedades.ActualizarPass(sociedad);
        }

        [HttpPost]
        [Route("ActualizarCerificado")]
        public Sociedades ActualizarCertificado(SociedadesEntity sociedad)
        {
            return DALSociedades.ActualizarCertificado(sociedad);
        }

        [HttpPost]
        [Route("ActualizarClaveCorreo")]
        public Sociedades ActualizarClaveCorreo(SociedadesEntity sociedad)
        {
            return DALSociedades.ActualizarClaveCorreo(sociedad);
        }

        [HttpPost]
        [Route("ObtenerSociedadesN")]
        public Sociedades ObtenerSociedadesN(SociedadesEntity name)
        {
            return DALSociedades.ObtenerSociedadesN(name.nombre_comercial);
        }
    }
}