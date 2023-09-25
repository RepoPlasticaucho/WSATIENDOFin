using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Grupos
    {

        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<GruposEntity> lstGrupos { get; set; }

        public Grupos(string codigoError, string descripcionError, List<GruposEntity> lstGrupos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstGrupos = lstGrupos;
        }

        public Grupos()
        {
            lstGrupos = new List<GruposEntity>();
        }
    }

    public class GruposEntity
    {
        public string id { get; set; }
        public string grupo { get; set; }
        public string idFiscal { get; set; }

        public GruposEntity(string id, string grupo, string idFiscal)
        {
            this.id = id;
            this.grupo = grupo;
            this.idFiscal = idFiscal;
        }

        public GruposEntity()
        {
        }
    }
}