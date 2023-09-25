using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Modelos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<ModelosEntity> lstModelos { get; set; }

        public Modelos(string codigoError, string descripcionError, List<ModelosEntity> lstColors)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstModelos = lstModelos;
        }

        public Modelos()
        {
            this.lstModelos = new List<ModelosEntity>();
        }
    }

    public class ModelosEntity
    {
        public string id { set; get; }
        public string almacen_id { set; get; }
        public string linea_id { set; get; }
        public string linea_nombre { set; get; }
        public string modelo { set; get; }
        public string marca_id { set; get; }
        public string marca_nombre { set; get; }
        public string etiquetas { set; get; }
        public string cod_sap { set; get; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }

        public ModelosEntity()
        {
        }

        public ModelosEntity(string id, string almacen_id, string linea_id, string linea_nombre, string modelo, string marca_id, string marca_nombre, string etiquetas, string cod_sap, string es_plasticaucho, string es_sincronizado)
        {
            this.id = id;
            this.almacen_id = almacen_id;
            this.linea_id = linea_id;
            this.linea_nombre = linea_nombre;
            this.modelo = modelo;
            this.marca_id = marca_id;
            this.marca_nombre = marca_nombre;
            this.etiquetas = etiquetas;
            this.cod_sap = cod_sap;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
        }
    }
}
