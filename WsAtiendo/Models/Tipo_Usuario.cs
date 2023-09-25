using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Tipo_Usuario
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<Tipo_UsuarioEntity> lstTipo_Usuario { get; set; }

        public Tipo_Usuario(string codigoError, string descripcionError, List<Tipo_UsuarioEntity> lstTipo_Usuario)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstTipo_Usuario = lstTipo_Usuario;
        }
        public Tipo_Usuario()
        {
            this.lstTipo_Usuario = new List<Tipo_UsuarioEntity>();
        }

    }
    public class Tipo_UsuarioEntity
    {
        public Tipo_UsuarioEntity()
        {
        }

        public Tipo_UsuarioEntity(string idTipo_Usuario, string usuario, string created_at, string update_at)
        {
            this.idTipo_Usuario = idTipo_Usuario;
            this.usuario = usuario;
            this.created_at = created_at;
            this.update_at = update_at;
        }

        public string idTipo_Usuario { get; set; }
        public string usuario { get; set; }
        public string created_at { get; set; }
        public string update_at { get; set; }

    }
}