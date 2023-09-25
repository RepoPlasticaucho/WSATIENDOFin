using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
     public class Almacenes
    {

        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<AlmacenesEntity> lstAlmacenes { get; set; }

        public Almacenes(string codigoError, string descripcionError, List<AlmacenesEntity> lstAlmacenes)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstAlmacenes = lstAlmacenes;
        }

        public Almacenes()
        {
            this.lstAlmacenes = new List<AlmacenesEntity>();
        }

    }
    public class AlmacenesEntity
    {
        public string idAlmacen { get; set; }
        public string sociedad_id { get; set; }
        public string nombre_almacen { get; set; }
        public string nombresociedad { get; set; }
        public string idfiscal_sociedad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string codigo { get; set; }
        public string pto_emision { get; set; }
        public string es_plasticaucho { get; set; }
        public string es_sincronizado { get; set; }

        public AlmacenesEntity(string idAlmacen, string sociedad_id, string nombre_almacen, string nombresociedad, string idfiscal_sociedad, string direccion, string telefono, string codigo, string pto_emision, string es_plasticaucho, string es_sincronizado)
        {
            this.idAlmacen = idAlmacen;
            this.sociedad_id = sociedad_id;
            this.nombre_almacen = nombre_almacen;
            this.nombresociedad = nombresociedad;
            this.idfiscal_sociedad = idfiscal_sociedad;
            this.direccion = direccion;
            this.telefono = telefono;
            this.codigo = codigo;
            this.pto_emision = pto_emision;
            this.es_plasticaucho = es_plasticaucho;
            this.es_sincronizado = es_sincronizado;
        }

        public AlmacenesEntity()
        {
        }
    }
}