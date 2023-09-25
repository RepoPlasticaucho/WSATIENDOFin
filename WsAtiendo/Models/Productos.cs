using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Productos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<ProductosEntity> lstProductos { get; set; }

        public Productos(string codigoError, string descripcionError, List<ProductosEntity> lstProductos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstProductos = lstProductos;
        }

        public Productos()
        {
            this.lstProductos = new List<ProductosEntity>();
        }
    }

    public class ProductosEntity {

        public string id { set; get; }
        public string modelo_producto_id { set; get; }
        public string tamanio { set; get; }
        public string nombre { set; get; }
        public string cod_sap { set; get; }
        public string pvp { set; get; }
        public string precio_prom { set; get; }
        public string costo { set; get; }
        public string tarifa_ice_iva_id { set; get; }
        public string etiquetas { set; get; }
        public string unidad_medida { set; get; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }

        public ProductosEntity()
        {
        }

        public ProductosEntity(string id, string modelo_producto_id, string tamanio, string nombre, string cod_sap, string pvp, string precio_prom, string costo, string tarifa_ice_iva_id, string etiquetas, string unidad_medida, string es_plasticaucho, string es_sincronizado)
        {
            this.id = id;
            this.modelo_producto_id = modelo_producto_id;
            this.tamanio = tamanio;
            this.nombre = nombre;
            this.cod_sap = cod_sap;
            this.pvp = pvp;
            this.precio_prom = precio_prom;
            this.costo = costo;
            this.tarifa_ice_iva_id = tarifa_ice_iva_id;
            this.etiquetas = etiquetas;
            this.unidad_medida = unidad_medida;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
        }
    }
}