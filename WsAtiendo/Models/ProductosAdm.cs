using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class ProductosAdm
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<ProductosAdmEntity> lstProductos { get; set; }

        public ProductosAdm(string codigoError, string descripcionError, List<ProductosAdmEntity> lstProductos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstProductos = lstProductos;
        }

        public ProductosAdm()
        {
            this.lstProductos = new List<ProductosAdmEntity>();
        }
    }

    public class ProductosAdmEntity
    {

        public string id { set; get; }
        public string tamanio { set; get; }
        public string nombre { set; get; }
        public string cod_sap { set; get; }
        public string etiquetas { set; get; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }
        public string modelo_producto_id { set; get; }
        public string modelo_producto { set; get; }
        public string impuesto_id { set; get; }
        public string impuesto_nombre { set; get; }
        public string marca_nombre { set; get; }
        public string color_nombre { set; get; }
        public string atributo_nombre { set; get; }
        public string genero_nombre { set; get; }
        public string modelo_nombre { set; get; }
        public string unidad_medida { set; get; }
        public string categoria { set; get; }
        public string url_image { set; get; }
        public string linea { set; get; }
        public string pvp { set; get; }
        public string tarifa_ice_iva_id { set; get; }
        public string precio_prom { set; get; }
        public string costo { set; get; }


        public ProductosAdmEntity()
        {

        }

    }
}