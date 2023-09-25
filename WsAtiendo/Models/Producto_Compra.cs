using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Producto_Compra
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<Producto_CompraEntity> lstProductos_Compra { get; set; }

        public Producto_Compra(string codigoError, string descripcionError, List<Producto_CompraEntity> lstProductos_Compra)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstProductos_Compra = lstProductos_Compra;
        }
        public Producto_Compra()
        {
            this.lstProductos_Compra = new List<Producto_CompraEntity>();

        }
    }
    public class Producto_CompraEntity
    {
        public Producto_CompraEntity()
        {
        }

        public Producto_CompraEntity(string id, string cliente, string clienteid, string producto, string productoid, string nombre_producto, string proveedorid, string proveedor, string nombre_prove, string precio, string created_at, string update_at)
        {
            this.id = id;
            this.cliente = cliente;
            this.clienteid = clienteid;
            this.producto = producto;
            this.productoid = productoid;
            this.nombre_producto = nombre_producto;
            this.proveedorid = proveedorid;
            this.proveedor = proveedor;
            this.nombre_prove = nombre_prove;
            this.precio = precio;
            this.created_at = created_at;
            this.update_at = update_at;
        }

        public string id { get; set; }
        public string cliente { get; set; }
        public string clienteid { get; set; }
        public string producto { get; set; }
        public string productoid { get; set; }
        public string nombre_producto { get; set; }
        public string proveedorid { get; set; }
        public string proveedor { get; set; }
        public string nombre_prove { get; set; }
        public string precio { get; set; }
        public string created_at { get; set; }
        public string update_at { get; set; }


    }
}