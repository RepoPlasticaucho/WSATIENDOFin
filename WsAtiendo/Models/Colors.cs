using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Colors
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<ColorsEntity> lstColors { get; set; }

        public Colors(string codigoError, string descripcionError, List<ColorsEntity> lstColors)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstColors = lstColors;
        }

        public Colors()
        {
            this.lstColors = new List<ColorsEntity>();
        }
    }

    public class ColorsEntity
    {
        public string id { set; get; }
        public string color { set; get; }
        public string cod_sap { set; get; }
        public string etiquetas { set; get; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }

        public ColorsEntity(string id, string color, string cod_sap, string etiquetas, string es_plasticaucho, string es_sincronizado)
        {
            this.id = id;
            this.color = color;
            this.cod_sap = cod_sap;
            this.etiquetas = etiquetas;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
        }

        public ColorsEntity()
        {
        }
    }
}