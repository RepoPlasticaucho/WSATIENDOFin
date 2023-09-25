using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Lineas
    {

        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<LineasEntity> lstLineas { get; set; }

        public Lineas(string codigoError, string descripcionError, List<LineasEntity> lstLineas)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstLineas = lstLineas;
        }

        public Lineas()
        {
            this.lstLineas = new List<LineasEntity>();
        }
    }

    public class LineasEntity
    {
        public string id { get; set; }
        public string categoria_id { get; set; }
        public string categoria_nombre { get; set; }
        public string linea { get; set; }
        public string etiquetas { get; set; }
        public string cod_sap { get; set; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }
        public string almacen_id { set; get; }

      

        public LineasEntity()
        {
        }

        public LineasEntity(string id, string categoria_id, string categoria_nombre, string linea, string etiquetas, string cod_sap, string es_plasticaucho, string es_sincronizado, string almacen_id)
        {
            this.id = id;
            this.categoria_id = categoria_id;
            this.categoria_nombre = categoria_nombre;
            this.linea = linea;
            this.etiquetas = etiquetas;
            this.cod_sap = cod_sap;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
            this.almacen_id = almacen_id;
        }
    }

}