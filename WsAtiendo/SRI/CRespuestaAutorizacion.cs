using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WsAtiendo.SRI
{
        [XmlRoot("RespuestaAutorizacionComprobante")]
        public class CRespuestaAutorizacion
        {
            [XmlElement("claveAccesoConsultada")]
            public string ClaveAcceso { get; set; }

            [XmlElement("estado")]
            public string Estado { get; set; }

            [XmlElement("NumeroComprobantes")]
            public string NumeroComprobantes { get; set; }

            [XmlArray(ElementName = "autorizaciones")]
            [XmlArrayItem(typeof(Autorizacion), ElementName = "autorizacion")]
            public List<Autorizacion> Comprobantes { get; set; }
        }

        public class Autorizacion
        {
            [XmlElement("estado")]
            public string Estado { get; set; }

            [XmlElement("numeroAutorizacion")]
            public string NumeroAutorizacion { get; set; }

            [XmlElement("fechaAutorizacion")]
            public string FechaAutorizacion { get; set; }

            [XmlElement("ambiente")]
            public string Ambiente { get; set; }

            [XmlElement("comprobante")]
            public string Comprobante { get; set; }

            [XmlElement("comprobanteRetencion")]
            public string comprobanteRetencion { get; set; }

            [XmlArray(ElementName = "mensajes")]
            [XmlArrayItem(typeof(Mensajes), ElementName = "mensaje")]
            public List<Mensajes> Mensajes { get; set; }
        }

        public class Mensajes
        {
            [XmlElement("identificador")]
            public string Identificador { get; set; }

            [XmlElement("mensaje")]
            public string mensaje { get; set; }

            [XmlElement("informacionAdicional")]
            public string InformacionAdicional { get; set; }

            [XmlElement("tipo")]
            public string Tipo { get; set; }
        }
    
}
