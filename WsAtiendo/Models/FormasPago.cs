using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class FormasPago
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<FormasPagoEntity> lstFormasPago { get; set; }

        public FormasPago(string codigoError, string descripcionError, List<FormasPagoEntity> lstFormasPago)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstFormasPago = lstFormasPago;
        }
        public FormasPago()
        {
            this.lstFormasPago = new List<FormasPagoEntity>();
        }
    }

    public class FormasPagoEntity
    {
        public FormasPagoEntity()
        {
        }

        public FormasPagoEntity(string id, string nombre, string codigo, string fecha_inicio, string fecha_fin, string created_at, string updated_at)
        {
            this.id = id;
            this.nombre = nombre;
            this.codigo = codigo;
            this.fecha_inicio = fecha_inicio;
            this.fecha_fin = fecha_fin;
            this.created_at = created_at;
            this.updated_at = updated_at;
        }

        public string id { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }


    }
}