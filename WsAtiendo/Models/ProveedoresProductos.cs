using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class ProveedoresProductos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<ProveedoresProductosEntity> lstProveedoresProductos { get; set; }

        public ProveedoresProductos(string codigoError, string descripcionError, List<ProveedoresProductosEntity> lstProveedoresProductos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstProveedoresProductos = lstProveedoresProductos;
        }

        public ProveedoresProductos()
        {
            this.lstProveedoresProductos = new List<ProveedoresProductosEntity>();
        }
    }

    public class ProveedoresProductosEntity
    {
        public ProveedoresProductosEntity()
        {
        }

        public string id { get; set; }
        public string provedor_id { get; set; }
        public string producto_id { get; set; }
        public string nombre_producto { get; set; }
        public string tamanio { get; set; }
        public string unidad_medida { get; set; }
        public string descripcion_uni { get; set; }
        public string precio { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string marca { get; set; }
        public string etiquetas { get; set; }
        public string costo { get; set; }

        public ProveedoresProductosEntity(string id, string provedor_id, string producto_id, string nombre_producto, string tamanio, string unidad_medida, string descripcion_uni, string precio, string created_at, string updated_at, string marca, string etiquetas)
        {
            this.id = id;
            this.provedor_id = provedor_id;
            this.producto_id = producto_id;
            this.nombre_producto = nombre_producto;
            this.tamanio = tamanio;
            this.unidad_medida = unidad_medida;
            this.descripcion_uni = descripcion_uni;
            this.precio = precio;
            this.costo = costo;
            this.created_at = created_at;
            this.updated_at = updated_at;
            this.marca = marca;
            this.etiquetas = etiquetas;
            this.costo = costo;
        }

       
    }
}