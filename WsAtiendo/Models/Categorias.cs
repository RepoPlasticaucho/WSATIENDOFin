using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Categorias
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<CategoriasEntity> lstCategorias { get; set; }

        public Categorias(string codigoError, string descripcionError, List<CategoriasEntity> lstCategorias)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstCategorias = lstCategorias;
        }

        public Categorias()
        {
            this.lstCategorias = new List<CategoriasEntity>();
        }
    }

    public class CategoriasEntity
    {
        public string id { get; set; }
        public string categoria { get; set; }
        public string cod_sap { get; set; }
        public string etiquetas { get; set; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }
        public string almacen_id { set; get; }

       

        public CategoriasEntity()
        {
        }

        public CategoriasEntity(string id, string categoria, string cod_sap, string etiquetas, string es_plasticaucho, string es_sincronizado, string almacen_id)
        {
            this.id = id;
            this.categoria = categoria;
            this.cod_sap = cod_sap;
            this.etiquetas = etiquetas;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
            this.almacen_id = almacen_id;
        }
    }

}