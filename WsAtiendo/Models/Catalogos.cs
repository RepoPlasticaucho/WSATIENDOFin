using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Catalogos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<CatalogosEntity> lstCatalogos { get; set; }

        public Catalogos(string codigoError, string descripcionError, List<GruposEntity> lstGrupos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstCatalogos = lstCatalogos;
        }

        public Catalogos()
        {
            lstCatalogos = new List<CatalogosEntity>();
        }
    }

    public class CatalogosEntity
    {
        public CatalogosEntity()
        {

        }

        public CatalogosEntity(string id, string codigo, string material, string talla, string costo, string pvp, string subfamilia, string familia, string marca, string marcaid, string tipo, string tipoid, string producto, string productoid, string color, string colorid, string caracteristica, string caracteristicaid, string genero, string generoid, string categoria, string categoriaid, string moelo_producto, string modelo_producto_id, string linea_producto_id, string unidad_medidad, string tarifa_ice_iva, string tarifa_ice_iva_id)
        {
            this.id = id;
            this.codigo = codigo;
            this.material = material;
            this.talla = talla;
            this.costo = costo;
            this.pvp = pvp;
            this.subfamilia = subfamilia;
            this.familia = familia;
            this.marca = marca;
            this.marcaid = marcaid;
            this.tipo = tipo;
            this.tipoid = tipoid;
            this.producto = producto;
            this.productoid = productoid;
            this.color = color;
            this.colorid = colorid;
            this.caracteristica = caracteristica;
            this.caracteristicaid = caracteristicaid;
            this.genero = genero;
            this.generoid = generoid;
            this.categoria = categoria;
            this.categoriaid = categoriaid;
            this.moelo_producto = moelo_producto;
            this.modelo_producto_id = modelo_producto_id;
            this.linea_producto_id = linea_producto_id;
            this.unidad_medidad = unidad_medidad;
            this.tarifa_ice_iva = tarifa_ice_iva;
            this.tarifa_ice_iva_id = tarifa_ice_iva_id;
        }

        public string id { get; set; }
        public string codigo { get; set; }
        public string material { get; set; }
        public string talla { get; set; }
        public string costo { get; set; }
        public string pvp { get; set; }
        public string subfamilia { get; set; }
        public string familia { get; set; }
        public string marca { get; set; }
        public string marcaid { get; set; }
        public string tipo { get; set; }
        public string tipoid { get; set; }
        public string producto { get; set; }
        public string productoid { get; set; }
        public string color { get; set; }
        public string colorid { get; set; }
        public string caracteristica { get; set; }
        public string caracteristicaid { get; set; }
        public string genero { get; set; }
        public string generoid { get; set; }
        public string categoria { get; set; }
        public string categoriaid { get; set; }
        public string moelo_producto { get; set; }
        public string modelo_producto_id { get; set; }
        public string linea_producto_id { get; set; }
        public string unidad_medidad { get; set; }
        public string tarifa_ice_iva { get; set; }
        public string tarifa_ice_iva_id { get; set; }

    }
}
