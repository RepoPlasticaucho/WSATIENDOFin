using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Tipo_Comprobante
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<Tipo_ComprobanteEntity> lstTipo_Comprobante { get; set; }

        public Tipo_Comprobante(string codigoError, string descripcionError, List<Tipo_ComprobanteEntity> lstTipo_Comprobante)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstTipo_Comprobante = lstTipo_Comprobante;
        }

        public Tipo_Comprobante()
        {
            this.lstTipo_Comprobante = new List<Tipo_ComprobanteEntity>();
        }
    }

    public class Tipo_ComprobanteEntity
    {
        public Tipo_ComprobanteEntity()
        {
        }

        public Tipo_ComprobanteEntity(string id, string nombre, string codigo, string created_at, string update_at)
        {
            this.id = id;
            this.nombre = nombre;
            this.codigo = codigo;
            this.created_at = created_at;
            this.update_at = update_at;
        }

        public string id { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public string created_at { get; set; }
        public string update_at { get; set; }

    }
}