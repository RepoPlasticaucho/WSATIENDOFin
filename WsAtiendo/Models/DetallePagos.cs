using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class DetallePagos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<DetallePagosEntity> lstDetallePagos { get; set; }

        public DetallePagos(string codigoError, string descripcionError, List<DetallePagosEntity> lstDetallePagos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstDetallePagos = lstDetallePagos;
        }

        public DetallePagos()
        {
            this.lstDetallePagos = new List<DetallePagosEntity>();
        }
    }

    public class DetallePagosEntity
    {
        public DetallePagosEntity()
        {
        }

        public string id { get; set; }
        public string movimiento_id { get; set; }
        public string forma_pago_id { get; set; }
        public string descripcion { get; set; }
        public string valor { get; set; }
        public string valorE { get; set; }
        public string valorTD { get; set; }
        public string valorTC { get; set; }
        public string nombre { get; set; }
        public string fecha_recaudo { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public DetallePagosEntity(string id, string movimiento_id, string forma_pago_id, string descripcion, string valor, string valorE, string valorTD, string valorTC, string nombre, string fecha_recaudo, string created_at, string updated_at)
        {
            this.id = id;
            this.movimiento_id = movimiento_id;
            this.forma_pago_id = forma_pago_id;
            this.descripcion = descripcion;
            this.valor = valor;
            this.valorE = valorE;
            this.valorTD = valorTD;
            this.valorTC = valorTC;
            this.nombre = nombre;
            this.fecha_recaudo = fecha_recaudo;
            this.created_at = created_at;
            this.updated_at = updated_at;
        }
    }
}