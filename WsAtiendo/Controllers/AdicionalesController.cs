using System;
using System.Collections.Generic;
using System.IO;
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
    [RoutePrefix("api/adicionales")]
    public class AdicionalesController : ApiController
    {

        [HttpPost]
        [Route("ProcesarImagenMarca")]
        public ImageUpload procesarImagenMarca(ImageUpload imageUpload)
        {
            if (!imageUpload.nombreArchivo.ToUpper().Equals(imageUpload.nombreArchivoEliminar)&& !imageUpload.nombreArchivo.Equals("https://calidad.atiendo.ec/eojgprlg/Marcas/producto.png"))
            {
                ImageUpload imgResponse = DALImagenes.UploadImage(imageUpload, "Marcas/");
                if (imgResponse.codigoError.ToUpper().Equals("OK") && !String.IsNullOrEmpty(imageUpload.nombreArchivoEliminar))
                {
                    imgResponse = DALImagenes.DeleteImage(imageUpload, "Marcas/");
                }
                return imgResponse;
            }
            else
            {
                return new ImageUpload()
                {
                    codigoError = "OK"
                };
            }
        }

        [HttpPost]
        [Route("ProcesarImagenModeloProducto")]
        public ImageUpload procesarImagenModeloProducto(ImageUpload imageUpload)
        {
            if (!imageUpload.nombreArchivo.ToUpper().Equals(imageUpload.nombreArchivoEliminar) && !imageUpload.nombreArchivo.Equals("https://calidad.atiendo.ec/eojgprlg/ModeloProducto/producto.png"))
            {
                ImageUpload imgResponse = DALImagenes.UploadImage(imageUpload, "ModeloProducto/");
                if (imgResponse.codigoError.ToUpper().Equals("OK") && !String.IsNullOrEmpty(imageUpload.nombreArchivoEliminar))
                {
                    imgResponse = DALImagenes.DeleteImage(imageUpload, "ModeloProducto/");
                }
                return imgResponse;
            }
            else
            {
                return new ImageUpload()
                {
                    codigoError = "OK"
                };
            }
        }

        [HttpPost]
        [Route("ProcesarCertificado")]
        public ImageUpload procesarCertificado(ImageUpload imageUpload)
        {
            if (!imageUpload.nombreArchivo.ToUpper().Equals(imageUpload.nombreArchivoEliminar) && !imageUpload.nombreArchivo.Equals("https://calidad.atiendo.ec/eojgprlg/Certificados/certificate.pfx"))
            {
                ImageUpload imgResponse = DALImagenes.UploadImage(imageUpload, "Certificados/");
                if (imgResponse.codigoError.ToUpper().Equals("OK") && !String.IsNullOrEmpty(imageUpload.nombreArchivoEliminar))
                {
                    imgResponse = DALImagenes.DeleteImage(imageUpload, "Certificados/");
                }
                return imgResponse;
            }
            else
            {
                return new ImageUpload()
                {
                    codigoError = "OK"
                };
            }
        }

        [HttpPost]
        [Route("ProcesarFacturasPDF")]
        public ImageUpload procesarFacturasPDF(ImageUpload imageUpload)
        {
            if (!imageUpload.nombreArchivo.ToUpper().Equals(imageUpload.nombreArchivoEliminar) && !imageUpload.nombreArchivo.Equals("https://calidad.atiendo.ec/eojgprlg/FacturasPDF/ejemplo.pdf"))
            {
                ImageUpload imgResponse = DALImagenes.UploadImage(imageUpload, "FacturasPDF/");
                if (imgResponse.codigoError.ToUpper().Equals("OK") && !String.IsNullOrEmpty(imageUpload.nombreArchivoEliminar))
                {
                    imgResponse = DALImagenes.DeleteImage(imageUpload, "FacturasPDF/");
                }
                return imgResponse;
            }
            else
            {
                return new ImageUpload()
                {
                    codigoError = "OK"
                };
            }
        }

    }
}