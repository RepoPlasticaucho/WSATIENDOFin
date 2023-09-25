using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class ModeloProductos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<ModeloProductosEntity> lstModelo_Productos { get; set; }

        public ModeloProductos(string codigoError, string descripcionError, List<ModeloProductosEntity> lstModelos_Productos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstModelo_Productos = lstModelo_Productos;
        }

        public ModeloProductos()
        {
            this.lstModelo_Productos = new List<ModeloProductosEntity>();
        }

    }

    public class ModeloProductosEntity
    {

        public string id { set; get; }
        public string categoria { set; get; }
        public string linea { set; get; }
        public string marca_id { set; get; }
        public string marca { set; get; }
        public string modelo_id { set; get; }
        public string modelo { set; get; }
        public string color_id { set; get; }
        public string color { set; get; }
        public string atributo_id { set; get; }
        public string atributo { set; get; }
        public string genero_id { set; get; }
        public string genero { set; get; }
        public string modelo_producto { set; get; }
        public string etiquetas { set; get; }
        public string cod_sap { set; get; }
        public string cod_familia { set; get; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }
        public string url_image { get; set; }
        public ModeloProductosEntity()
        {
        }

        public ModeloProductosEntity(string id, string categoria, string linea, string marca_id, string marca, string modelo_id, string modelo, string color_id, string color, string atributo_id, string atributo, string genero_id, string genero, string modelo_producto, string etiquetas, string cod_sap, string cod_familia, string es_plasticaucho, string es_sincronizado, string url_image)
        {
            this.id = id;
            this.categoria = categoria;
            this.linea = linea;
            this.marca_id = marca_id;
            this.marca = marca;
            this.modelo_id = modelo_id;
            this.modelo = modelo;
            this.color_id = color_id;
            this.color = color;
            this.atributo_id = atributo_id;
            this.atributo = atributo;
            this.genero_id = genero_id;
            this.genero = genero;
            this.modelo_producto = modelo_producto;
            this.etiquetas = etiquetas;
            this.cod_sap = cod_sap;
            this.cod_familia = cod_familia;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
            this.url_image = url_image;
        }
    }
}