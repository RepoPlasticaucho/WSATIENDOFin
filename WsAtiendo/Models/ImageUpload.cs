using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class ImageUpload
    {
        public string imageBase64 { get; set; }
        public string nombreArchivo { get; set; }
        public string nombreArchivoEliminar { get; set; }
        public string codigoError { get; set; }
        public string descripcionError { get; set; }

        public ImageUpload(string imageBase64, string nombreArchivo, string nombreArchivoEliminar, string codigoError, string descripcionError)
        {
            this.imageBase64 = imageBase64;
            this.nombreArchivo = nombreArchivo;
            this.nombreArchivoEliminar = nombreArchivoEliminar;
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
        }

        public ImageUpload()
        {
        }
    }
}