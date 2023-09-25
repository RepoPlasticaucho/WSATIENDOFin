using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class DetalleMovimientos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<DetalleMovimientosEntity> lstDetalleMovimientos { get; set; }

        public DetalleMovimientos(string codigoError, string descripcionError, List<DetalleMovimientosEntity> lstDetalleMovimientos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstDetalleMovimientos = lstDetalleMovimientos;
        }
        public DetalleMovimientos()
        {
            this.lstDetalleMovimientos = new List<DetalleMovimientosEntity>();
        }
    }

    public class DetalleMovimientosEntity
    {
        public DetalleMovimientosEntity()
        {
        }

        public DetalleMovimientosEntity(string id, string producto_id, string producto_nombre, string modelo_producto_nombre, string inventario_id, string pto_emision, string movimiento_id, string tamanio, string color, string cantidad, string tarifa, string tipo_movimiento, string desc_add, string costo, string precio, string url_image, string created_at, string update_at, string unidad_medida, string cod_tarifa)
        {
            this.id = id;
            this.producto_id = producto_id;
            this.producto_nombre = producto_nombre;
            this.modelo_producto_nombre = modelo_producto_nombre;
            this.inventario_id = inventario_id;
            this.pto_emision = pto_emision;
            this.movimiento_id = movimiento_id;
            this.tamanio = tamanio;
            this.color = color;
            this.cantidad = cantidad;
            this.tarifa = tarifa;
            this.tipo_movimiento = tipo_movimiento;
            this.desc_add = desc_add;
            this.costo = costo;
            this.precio = precio;
            this.url_image = url_image;
            this.created_at = created_at;
            this.update_at = update_at;
            this.unidad_medida = unidad_medida;
            this.cod_tarifa = cod_tarifa;
        }

        public string id { get; set; }
        public string producto_id { get; set; }
        public string producto_nombre { get; set; }
        public string modelo_producto_nombre { get; set; }
        public string inventario_id { get; set; }
        public string pto_emision { get; set; }
        public string movimiento_id { get; set; }
        public string tamanio { get; set; }
        public string color { get; set; }
        public string cantidad { get; set; }
        public string tarifa { get; set; }
        public string tipo_movimiento { get; set; }
        public string desc_add { get; set; }
        public string costo { get; set; }
        public string precio { get; set; }
        public string url_image { get; set; }
        public string created_at { get; set; }
        public string update_at { get; set; }
        public string unidad_medida { get; set; }
        public string cod_tarifa { get; set; }
    }
}