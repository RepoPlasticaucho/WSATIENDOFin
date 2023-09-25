using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Proveedores
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<ProveedoresEntity> lstProveedores { get; set; }

        public Proveedores(string codigoError, string descripcionError, List<ProveedoresEntity> lstProveedores)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstProveedores = lstProveedores;
        }
        public Proveedores()
        {
            lstProveedores = new List<ProveedoresEntity>();
        }
    }

    public class ProveedoresEntity
    {
        public string id { set; get; }
        public string sociedad_id { set; get; }
        public string nombre_sociedad { set; get; }
        public string nombre { set; get; }
        public string id_fiscal { set; get; }
        public string correo { get; set; }
        public string ciudad { get; set; }
        public string telefono { get; set; }
        public string ciudadid { get; set; }
        public string direccionprov { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
    
        public ProveedoresEntity()
        {
        }

        public ProveedoresEntity(string id, string sociedad_id, string nombre_sociedad, string nombre, string id_fiscal, string correo, string ciudad, string telefono, string ciudadid, string direccionprov, string updated_at, string created_at)
        {
            this.id = id;
            this.sociedad_id = sociedad_id;
            this.nombre_sociedad = nombre_sociedad;
            this.nombre = nombre;
            this.id_fiscal = id_fiscal;
            this.correo = correo;
            this.ciudad = ciudad;
            this.telefono = telefono;
            this.ciudadid = ciudadid;
            this.direccionprov = direccionprov;
            this.updated_at = updated_at;
            this.created_at = created_at;
        }
    }
}