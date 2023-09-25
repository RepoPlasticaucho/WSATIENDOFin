using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WsAtiendo.SRI
{
    public class SendEmail
    {
        public String EnviarComprobante(string remitente, string contraseña, string destinatario, string rutaArchivoXML, string rutaArchivoPDF)
        {
            // Asunto del correo
            string asunto = "Factura";
            // Cuerpo del correo
            string cuerpo = "Factura con archivos correspondientes XML y PDF";

            // Crear un objeto MailMessage
            MailMessage correo = new MailMessage(remitente, destinatario, asunto, cuerpo);

            // Credenciales del servidor
            WebClient ftpClient = new WebClient();
            ftpClient.Credentials = new NetworkCredential("eojgprlg@calidad.atiendo.ec", "rs2NB4XN94we");

            // Adjuntar archivo XML
            byte[] archivoXMLBytes = ftpClient.DownloadData(rutaArchivoXML);
            MemoryStream archivoXMLStream = new MemoryStream(archivoXMLBytes);
            Attachment archivoAdjuntoXML = new Attachment(archivoXMLStream, Path.GetFileName(rutaArchivoXML));
            correo.Attachments.Add(archivoAdjuntoXML);

            // Adjuntar archivo PDF
            byte[] archivoPDFBytes = ftpClient.DownloadData(rutaArchivoPDF);
            MemoryStream archivoPDFStream = new MemoryStream(archivoPDFBytes);
            Attachment archivoAdjuntoPDF = new Attachment(archivoPDFStream, Path.GetFileName(rutaArchivoPDF));
            correo.Attachments.Add(archivoAdjuntoPDF);

            // Configurar el cliente SMTP
            SmtpClient clienteSmtp = new SmtpClient("smtp.office365.com", 587);
            clienteSmtp.EnableSsl = true;
            clienteSmtp.Credentials = new NetworkCredential(remitente, contraseña);

            try
            {
                // Enviar el correo electrónico
                clienteSmtp.Send(correo);
                return "Correo enviado correctamente";
                // Console.WriteLine("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                return "Error al enviar el correo " + ex.Message;
                // Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }

        public void EnviarC(string remitente, string contraseña, string destinatario)
        {
            // Asunto del correo
            string asunto = "Actualizacion de Credenciales Digitales";
            // Cuerpo del correo
            string cuerpo = "Estimado, mediante el presente correo damos a conocer que se actualizó sus credenciales.";

            // Crear un objeto MailMessage
            MailMessage correo = new MailMessage(remitente, destinatario, asunto, cuerpo);

            // Configurar el cliente SMTP
            SmtpClient clienteSmtp = new SmtpClient("smtp.office365.com", 587);
            clienteSmtp.EnableSsl = true;
            clienteSmtp.Credentials = new NetworkCredential(remitente, contraseña);

            try
            {
                // Enviar el correo electrónico
                clienteSmtp.Send(correo);
                Console.WriteLine("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}