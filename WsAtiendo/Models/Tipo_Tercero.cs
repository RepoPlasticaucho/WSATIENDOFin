using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Tipo_Tercero
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<Tipo_TerceroEntity> lstTipo_Tercero { get; set; }

        public Tipo_Tercero(string codigoError, string descripcionError, List<Tipo_TerceroEntity> lstTipo_Tercero)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstTipo_Tercero = lstTipo_Tercero;
        }
        public Tipo_Tercero()
        {
            this.lstTipo_Tercero = new List<Tipo_TerceroEntity>();
        }
    }

    public class Tipo_TerceroEntity
    {
        public Tipo_TerceroEntity()
        {
        }

        public Tipo_TerceroEntity(string idTipo_tercero, string descripcion, string codigo, string created_at, string update_at)
        {
            this.idTipo_tercero = idTipo_tercero;
            this.descripcion = descripcion;
            this.codigo = codigo;
            this.created_at = created_at;
            this.update_at = update_at;
        }

        public string idTipo_tercero { get; set; }
        public string descripcion { get; set; }
        public string codigo { get; set; }
        public string created_at { get; set; }
        public string update_at { get; set; }

    }
}