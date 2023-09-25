using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Provincias
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<ProvinciasEntity> lstProvincias { get; set; }

        public Provincias(string codigoError, string descripcionError, List<ProvinciasEntity> lstProvincias)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstProvincias = lstProvincias;
        }

        public Provincias()
        {
            this.lstProvincias = new List<ProvinciasEntity>();
        }
    }
    public class ProvinciasEntity
    {
        public ProvinciasEntity()
        {
        }

        public ProvinciasEntity(string idProvincia, string provincia, string codigo, string created_at, string update_at)
        {
            this.idProvincia = idProvincia;
            this.provincia = provincia;
            this.codigo = codigo;
            this.created_at = created_at;
            this.update_at = update_at;
        }

        public string idProvincia { get; set; }
        public string provincia { get; set; }
        public string codigo { get; set; }
        public string created_at { get; set; }
        public string update_at { get; set; }

    }
}