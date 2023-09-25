using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class DetalleImpuestos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<DetalleImpuestosEntity> lstDetalleImpuestos { get; set; }

        public DetalleImpuestos(string codigoError, string descripcionError, List<DetalleImpuestosEntity> lstDetalleImpuestos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstDetalleImpuestos = lstDetalleImpuestos;
        }

        public DetalleImpuestos()
        {
            this.lstDetalleImpuestos = new List<DetalleImpuestosEntity>();
        }
    }

    public class DetalleImpuestosEntity
    {
        public DetalleImpuestosEntity()
        {
        }

        public DetalleImpuestosEntity(string id, string detalle_movimiento_id, string cod_impuesto, string porcentaje, string base_imponible, string valor, string movimiento_id, string created_at, string updated_at)
        {
            this.id = id;
            this.detalle_movimiento_id = detalle_movimiento_id;
            this.cod_impuesto = cod_impuesto;
            this.porcentaje = porcentaje;
            this.base_imponible = base_imponible;
            this.valor = valor;
            this.movimiento_id = movimiento_id;
            this.created_at = created_at;
            this.updated_at = updated_at;
        }

        public string id { get; set; }
        public string detalle_movimiento_id { get; set; }
        public string cod_impuesto { get; set; }
        public string porcentaje { get; set; }
        public string base_imponible { get; set; }
        public string valor { get; set; }
        public string movimiento_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}