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
    [RoutePrefix("api/marcas")]
    public class MarcasController : ApiController
    {
        [HttpGet]
        [Route("ObtenerMarcas")]
        public Marcas ObtenerMarcas()
        {
            return DALMarcas.ObtenerMarcas();
        }

        [HttpPost]
        [Route("ObtenerMarcasProveedor")]
        public Marcas ObtenerMarcasProveedor(ProveedoresEntity proveedor)
        {
            return DALMarcas.ObtenerMarcasProveedor(proveedor.nombre);
        }

        [HttpPost]
        [Route("InsertarMarcas")]
        public Marcas InsertarMarcas(MarcasEntity marca)
        {
            return DALMarcas.InsertarMarca(marca);
        }

        [HttpPost]
        [Route("ModificarMarcas")]
        public Marcas ModificarMarcas(MarcasEntity marca)
        {
            return DALMarcas.ModificarMarca(marca);
        }

        [HttpPost]
        [Route("EliminarMarcas")]
        public Marcas EliminarMarcas(MarcasEntity marca)
        {
            if (!marca.url_image.Equals("https://calidad.atiendo.ec/eojgprlg/Marcas/producto.png"))
            {
                ImageUpload imageDelete = new ImageUpload()
                {
                    nombreArchivoEliminar = marca.url_image.Split('/')[5]
                };
                imageDelete = DALImagenes.DeleteImage(imageDelete, "Marcas/");
            }
            return DALMarcas.EliminarMarca(marca);
        }

        [HttpPost]
        [Route("ObtenerMarcaNombre")]
        public Marcas ObtenerMarcaNombre(MarcasEntity marca)
        {
            return DALMarcas.ObtenerMarcaNombre(marca.marca);
        }

        [HttpPost]
        [Route("ObtenerMarcaCategoria")]
        public Marcas ObtenerMarcaCategoria(CategoriasEntity categoria)
        {
            return DALMarcas.ObtenerMarcaCategoria(categoria.categoria);
        }

        [HttpPost]
        [Route("ObtenerMarcaModelo")]
        public Marcas ObtenerMarcaModelo(ModelosEntity modelo)
        {
            return DALMarcas.ObtenerMarcaModelo(modelo.modelo);
        }

        [HttpPost]
        [Route("ObtenerMarcaLinea")]
        public Marcas ObtenerMarcaLinea(LineasEntity linea)
        {
            return DALMarcas.ObtenerMarcaLinea(linea.linea);
        }

        [HttpPost]
        [Route("ObtenerMarcaUno")]
        public Marcas ObtenerMarcaUno(MarcasEntity marca)
        {
            return DALMarcas.ObtenerMarcaUno(marca.marca);
        }
    }
}