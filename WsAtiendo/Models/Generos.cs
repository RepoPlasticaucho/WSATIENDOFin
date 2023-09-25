using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Generos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<GenerosEntity> lstGeneros { get; set; }

        public Generos(string codigoError, string descripcionError, List<GenerosEntity> lstGeneros)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstGeneros = lstGeneros;
        }
        public Generos()
        {
            lstGeneros = new List<GenerosEntity>();
        }

    }

    public class GenerosEntity
    {
        public string id { set; get; }
        public string genero { set; get; }
        public string etiquetas { set; get; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }
        public GenerosEntity()
        {
        }

        public GenerosEntity(string id, string genero, string etiquetas, string es_plasticaucho, string es_sincronizado)
        {
            this.id = id;
            this.genero = genero;
            this.etiquetas = etiquetas;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
        }

        

    }
}