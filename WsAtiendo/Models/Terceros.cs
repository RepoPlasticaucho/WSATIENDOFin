using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Terceros
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<TercerosEntity> lstTerceros { get; set; }

        public Terceros(string codigoError, string descripcionError, List<TercerosEntity> lstTerceros)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstTerceros = lstTerceros;
        }

        public Terceros()
        {
            this.lstTerceros = new List<TercerosEntity>();

        }
    }
    public class TercerosEntity
    {
        public TercerosEntity()
        {
        }

        public TercerosEntity(string id, string almacen_id, string sociedad_id, string tipotercero_id, string tipousuario_id, string nombresociedad, string nombretercero, string tipousuario, string nombre, string nombrealmacen, string id_fiscal, string provincia, string ciudadid, string ciudad, string direccion, string telefono, string correo, string fecha_nac, string created_at, string update_at)
        {
            this.id = id;
            this.almacen_id = almacen_id;
            this.sociedad_id = sociedad_id;
            this.tipotercero_id = tipotercero_id;
            this.tipousuario_id = tipousuario_id;
            this.nombresociedad = nombresociedad;
            this.nombretercero = nombretercero;
            this.tipousuario = tipousuario;
            this.nombre = nombre;
            this.nombrealmacen = nombrealmacen;
            this.id_fiscal = id_fiscal;
            this.provincia = provincia;
            this.ciudadid = ciudadid;
            this.ciudad = ciudad;
            this.direccion = direccion;
            this.telefono = telefono;
            this.correo = correo;
            this.fecha_nac = fecha_nac;
            this.created_at = created_at;
            this.update_at = update_at;
        }

        public string id { get; set; }
        public string almacen_id { get; set; }
        public string sociedad_id { get; set; }
        public string tipotercero_id { get; set; }
        public string tipousuario_id { get; set; }
        public string nombresociedad { get; set; }
        public string nombretercero { get; set; }
        public string tipousuario { get; set; }
        public string nombre { get; set; }
        public string nombrealmacen { get; set; }
        public string id_fiscal { get; set; }
        public string provincia { get; set; }
        public string ciudadid { get; set; }
        public string ciudad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string fecha_nac { get; set; }
        public string created_at { get; set; }
        public string update_at { get; set; }

    }
}
