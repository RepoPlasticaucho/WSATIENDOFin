using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Atributos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<AtributosEntity> lstAtributos { get; set; }

        public Atributos(string codigoError, string descripcionError, List<AtributosEntity> lstAtributos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstAtributos = lstAtributos;
        }

        public Atributos()
        {
            this.lstAtributos = new List<AtributosEntity>();
        }

    }

    public class AtributosEntity
    {
        public string id { set; get; }
        public string atributo { set; get; }
        public string etiquetas { set; get; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }

        public AtributosEntity()
        {
        }

        public AtributosEntity(string id, string atributo, string etiquetas, string es_plasticaucho, string es_sincronizado)
        {
            this.id = id;
            this.atributo = atributo;
            this.etiquetas = etiquetas;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
        }

       
    }
}