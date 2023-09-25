using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALImagenes
    {

        public static ImageUpload UploadImage(ImageUpload imageUpload, string carpeta)
        {
            ImageUpload imageRequest = new ImageUpload();
            try
            {
                string imageBase64 = imageUpload.imageBase64;
                byte[] bytes = Convert.FromBase64String(imageBase64);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://calidad.atiendo.ec/" + carpeta + imageUpload.nombreArchivo);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("eojgprlg@calidad.atiendo.ec", "rs2NB4XN94we");
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                imageRequest.codigoError = "OK";
            }
            catch (Exception ex)
            {
                imageRequest.codigoError = "E";
                imageRequest.descripcionError = ex.Message;
            }
            return imageRequest;
        }

        public static ImageUpload DeleteImage(ImageUpload imageUpload, string carpeta)
        {
            ImageUpload imageRequest = new ImageUpload();
            try
            {
                if (!imageUpload.nombreArchivoEliminar.Equals("producto.png"))
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://calidad.atiendo.ec/" + carpeta + imageUpload.nombreArchivoEliminar);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;
                    request.Credentials = new NetworkCredential("eojgprlg@calidad.atiendo.ec", "rs2NB4XN94we");
                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        imageRequest.descripcionError = response.StatusDescription;
                    }
                }
                imageRequest.codigoError = "OK";
            }
            catch (Exception ex)
            {
                imageRequest.codigoError = "E";
                imageRequest.descripcionError = ex.Message;
            }
            return imageRequest;
        }

    }
}