using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WsAtiendo.SRI
{
    public class CComprobanteElectronico
    {
        public CRespuestaRecepcion RecepcionComprobantePrueba(String path)
        {
            var mensaje = "";
            byte[] xmlBytes;

            string usuario = "eojgprlg@calidad.atiendo.ec";
            string contraseña = "rs2NB4XN94we";
            
            using (WebClient certClient = new WebClient())
            {
                certClient.Credentials = new NetworkCredential(usuario, contraseña);
                xmlBytes = certClient.DownloadData(path);
            }
            var xmlByte = xmlBytes;
            CSRIws sri = new CSRIws();

            CRespuestaRecepcion resRecepcion = sri.RecepcionComprobantesOnlinePrueba(Convert.ToBase64String(xmlByte));

            if (resRecepcion.Estado.Equals("DEVUELTA"))
            {
                foreach(var ComprobanteRecepcion in resRecepcion.Comprobantes)
                {
                    foreach(var Mensaje in ComprobanteRecepcion.Mensajes)
                    {
                        mensaje = Mensaje.mensaje;
                        mensaje = Mensaje.InformacionAdicional;
                        mensaje = Mensaje.Identificador;
                        mensaje = Mensaje.Tipo;
                    }
                }
            }
            return resRecepcion;
        }

        public CRespuestaAutorizacion AutorizacionComprobantePrueba(String claveAcceso)
        {
            CSRIws sri = new CSRIws();
            CRespuestaAutorizacion resAutorizacion = sri.AutorizacionComprobanteOnlinePrueba(claveAcceso);
            return resAutorizacion;
        }
    }
}
