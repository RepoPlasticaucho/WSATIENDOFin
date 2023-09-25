using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class SustentosTributarios
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<SustentosTributariosEntity> lstSustentos { get; set; }

        public SustentosTributarios(string codigoError, string descripcionError, List<MarcasEntity> lstColors)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstSustentos = lstSustentos;
        }

        public SustentosTributarios()
        {
            this.lstSustentos = new List<SustentosTributariosEntity>();
        }
    }

    public class SustentosTributariosEntity
    {
        public string id { set; get; }
        public string codigo { set; get; }
        public string etiquetas { set; get; }
        public string sustento { get; set; }
        public SustentosTributariosEntity()
        {
        }
        public SustentosTributariosEntity(string id, string codigo, string etiquetas, string sustento)
        {
            this.id = id;
            this.codigo = codigo;
            this.etiquetas = etiquetas;
            this.sustento = sustento;
        }
    }
}