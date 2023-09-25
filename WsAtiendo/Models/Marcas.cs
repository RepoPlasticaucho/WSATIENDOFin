using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Marcas
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<MarcasEntity> lstMarcas { get; set; }

        public Marcas(string codigoError, string descripcionError, List<MarcasEntity> lstColors)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstMarcas = lstMarcas;
        }

        public Marcas()
        {
            this.lstMarcas = new List<MarcasEntity>();
        }
    }

    public class MarcasEntity
    {

        public string id { set; get; }
        public string marca { set; get; }
        public string etiquetas { set; get; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }
        public string url_image { get; set; }
        public MarcasEntity()
        {
        }

        public MarcasEntity(string id, string marca, string etiquetas, string es_plasticaucho, string es_sincronizado, string url_image)
        {
            this.id = id;
            this.marca = marca;
            this.etiquetas = etiquetas;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
            this.url_image = url_image;
        }
    }


}