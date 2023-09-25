using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Inventarios
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<InventariosEntity> lstInventarios { get; set; }

        public Inventarios(string codigoError, string descripcionError, List<InventariosEntity> lstInventarios)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstInventarios = lstInventarios;
        }

        public Inventarios()
        {
            this.lstInventarios = new List<InventariosEntity>();
        }
    }

    public class InventariosEntity
    {
        public string categoria_id { set; get; }
        public string categoria { set; get; }
        public string linea_id { set; get; }
        public string linea { set; get; }
        public string modelo_id { set; get; }
        public string marca_id { set; get; }
        public string marca { set; get; }
        public string atributo { set; get; }
        public string atributo_id { set; get; }
        public string genero { set; get; }
        public string genero_id { set; get; }
        public string modelo_producto_id { set; get; }
        public string idProducto { set; get; }
        public string Producto { set; get; }
        public string id { set; get; }
        public string idInventario { set; get; }
        public string producto_id { set; get; }
        public string almacen_id { set; get; }
        public string tarifa_ice_iva { set; get; }
        public string tarifa_ice_iva_id { set; get; }
        public string tarifa_ice_iva_id1 { set; get; }
        public string tarifa_ice_iva1 { set; get; }
        public string almacen { set; get; }
        public string stock { set; get; }
        public string stock_optimo { set; get; }
        public string es_sincronizado { set; get; }
        public string fav { set; get; }
        public string color_id { set; get; }
        public string color { set; get; }
        public string modelo { set; get; }
        public string etiquetas { set; get; }
        public string modelo_producto { set; get; }
        public string producto_nombre { set; get; }
        public string costo { set; get; }
        public string pvp1 { set; get; }
        public string pvp2 { set; get; }
        public string pvp_sugerido { set; get; }
        public string cod_principal { set; get; }
        public string cod_secundario { set; get; }
        public string talla { set; get; }
        public string unidad_medidad { set; get; }
        public string url_image { set; get; }

        public InventariosEntity()
        {
        }

        public InventariosEntity(string categoria_id, string categoria, string linea_id, string linea, string modelo_id, string marca_id, string marca, string atributo, string atributo_id, string genero, string genero_id, string modelo_producto_id, string idProducto, string producto, string id, string idInventario, string producto_id, string almacen_id, string tarifa_ice_iva, string tarifa_ice_iva_id, string tarifa_ice_iva_id1, string tarifa_ice_iva1, string almacen, string stock, string stock_optimo, string es_sincronizado, string fav, string color_id, string color, string modelo, string etiquetas, string modelo_producto, string producto_nombre, string costo, string pvp1, string pvp2, string pvp_sugerido, string cod_principal, string cod_secundario, string talla, string unidad_medidad, string url_image)
        {
            this.categoria_id = categoria_id;
            this.categoria = categoria;
            this.linea_id = linea_id;
            this.linea = linea;
            this.modelo_id = modelo_id;
            this.marca_id = marca_id;
            this.marca = marca;
            this.atributo = atributo;
            this.atributo_id = atributo_id;
            this.genero = genero;
            this.genero_id = genero_id;
            this.modelo_producto_id = modelo_producto_id;
            this.idProducto = idProducto;
            Producto = producto;
            this.id = id;
            this.idInventario = idInventario;
            this.producto_id = producto_id;
            this.almacen_id = almacen_id;
            this.tarifa_ice_iva = tarifa_ice_iva;
            this.tarifa_ice_iva_id = tarifa_ice_iva_id;
            this.tarifa_ice_iva_id1 = tarifa_ice_iva_id1;
            this.tarifa_ice_iva1 = tarifa_ice_iva1;
            this.almacen = almacen;
            this.stock = stock;
            this.stock_optimo = stock_optimo;
            this.es_sincronizado = es_sincronizado;
            this.fav = fav;
            this.color_id = color_id;
            this.color = color;
            this.modelo = modelo;
            this.etiquetas = etiquetas;
            this.modelo_producto = modelo_producto;
            this.producto_nombre = producto_nombre;
            this.costo = costo;
            this.pvp1 = pvp1;
            this.pvp2 = pvp2;
            this.pvp_sugerido = pvp_sugerido;
            this.cod_principal = cod_principal;
            this.cod_secundario = cod_secundario;
            this.talla = talla;
            this.unidad_medidad = unidad_medidad;
            this.url_image = url_image;
        }
    }
}