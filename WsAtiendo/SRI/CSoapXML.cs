using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WsAtiendo.SRI
{
    public class CSoapXML
    {

        public String RecepcionComprobanteSoap(String xml)
        {
            return @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ec=""http://ec.gob.sri.ws.recepcion"">
        <soapenv:Header />
        <soapenv:Body>
            <ec:validarComprobante>
                <!--Optional:-->
                <xml>" + xml + @"</xml>
            </ec:validarComprobante>
        </soapenv:Body>
    </soapenv:Envelope>";
        }

        public String AutorizacionComprobanteSoap(String claveAcceso)
        {
            return @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ec=""http://ec.gob.sri.ws.autorizacion"">
    <soapenv:Header/>
    <soapenv:Body>
        <ec:autorizacionComprobante>
            <!--Optional:-->
             <claveAccesoComprobante>" + claveAcceso + @"</claveAccesoComprobante>
          </ec:autorizacionComprobante>
       </soapenv:Body>
     </soapenv:Envelope>";
        }
    }
}
