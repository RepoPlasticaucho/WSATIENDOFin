using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WsAtiendo.SRI
{
    public class CSRIws
    {
        public CRespuestaRecepcion RecepcionComprobantesOnlinePrueba(string xml)
        {
            String respuesta = "";
            CRespuestaRecepcion RespuestaRecepcionPrueba = new CRespuestaRecepcion();
            var soapEnvelopeXml = new XmlDocument();

            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create("https://celcer.sri.gob.ec/comprobantes-electronicos-ws/RecepcionComprobantesOffline?wsdl");

            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            soapEnvelopeXml.LoadXml(new CSoapXML().RecepcionComprobanteSoap(xml));

            using (Stream requestStream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(requestStream);
            }

            using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    respuesta = rd.ReadToEnd();

                    var soapResult = XDocument.Parse(respuesta);

                    var responseXml = soapResult.Descendants("RespuestaRecepcionComprobante").ToList();
                    foreach(var xmlDoc in responseXml)
                    {
                        RespuestaRecepcionPrueba = (CRespuestaRecepcion)DeserializeFromXElement(xmlDoc, typeof(CRespuestaRecepcion));
                    }
                }
            }
            return RespuestaRecepcionPrueba;
        }

        public CRespuestaAutorizacion AutorizacionComprobanteOnlinePrueba(string claveAcceso)
        {
            CRespuestaAutorizacion RespuestaAutorizacion = new CRespuestaAutorizacion();
            try
            {
                String respuesta = "";

                var soapEnvelopeXml = new XmlDocument();

                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create("https://celcer.sri.gob.ec/comprobantes-electronicos-ws/AutorizacionComprobantesOffline?wsdl");

                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";

                soapEnvelopeXml.LoadXml(new CSoapXML().AutorizacionComprobanteSoap(claveAcceso));

                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(requestStream);
                }

                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        respuesta = rd.ReadToEnd();
                        var soapResult = XDocument.Parse(respuesta);
                        var responseXml = soapResult.Descendants("RespuestaAutorizacionComprobante").ToList();

                        foreach(var xmlDoc in responseXml)
                        {
                            RespuestaAutorizacion = (CRespuestaAutorizacion)DeserializeFromXElement(xmlDoc, typeof(CRespuestaAutorizacion));
                        }
                    }
                }
                return RespuestaAutorizacion;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object DeserializeFromXElement(XElement element, Type t)
        {
            try
            {
                using (XmlReader reader1 = element.CreateReader())
                {
                    XmlSerializer serializer = new XmlSerializer(t);
                    return serializer.Deserialize(reader1);
                }
            }
            catch(Exception ex)
            {
                string g = ex.Message;
                string k = ex.ToString();
                return null;
            }
        }
    }
}
