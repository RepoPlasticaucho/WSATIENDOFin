using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Ciudades
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<CiudadesEntity> lstCiudades { get; set; }

        public Ciudades(string codigoError, string descripcionError, List<CiudadesEntity> lstCiudades)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstCiudades = lstCiudades;
        }
        public Ciudades()
        {
            this.lstCiudades = new List<CiudadesEntity>();
        }
    }
    public class CiudadesEntity
    {
        public CiudadesEntity()
        {
        }

        public CiudadesEntity(string idCiudad, string provinciaid, string provincia, string ciudad, string codigo, string created_at, string update_at)
        {
            this.idCiudad = idCiudad;
            this.provinciaid = provinciaid;
            this.provincia = provincia;
            this.ciudad = ciudad;
            this.codigo = codigo;
            this.created_at = created_at;
            this.update_at = update_at;
        }

        public string idCiudad { get; set; }
        public string provinciaid { get; set; }
        public string provincia { get; set; }
        public string ciudad { get; set; }
        public string codigo { get; set; }
        public string created_at { get; set; }
        public string update_at { get; set; }

    }
}