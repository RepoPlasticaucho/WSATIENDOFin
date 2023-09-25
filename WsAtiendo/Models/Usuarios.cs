using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Usuarios
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<UsuariosEntity> lstUsuarios { get; set; }

        public Usuarios(string codigoError, string descripcionError, List<UsuariosEntity> lstUsuarios)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstUsuarios = lstUsuarios;
        }
        public Usuarios()
        {
            this.lstUsuarios = new List<UsuariosEntity>();

        }
    }

    public class UsuariosEntity
    {
        public UsuariosEntity()
        {
        }

        public UsuariosEntity(string id, string grupo, string grupoid, string nombre, string comercial, string idfiscal, string correo, string rol, string contraseña, string telefono, string direccion, string gven, string tipologia, string cod_sap)
        {
            this.id = id;
            this.grupo = grupo;
            this.grupoid = grupoid;
            this.nombre = nombre;
            this.comercial = comercial;
            this.idfiscal = idfiscal;
            this.correo = correo;
            this.rol = rol;
            this.contraseña = contraseña;
            this.telefono = telefono;
            this.direccion = direccion;
            this.gven = gven;
            this.tipologia = tipologia;
            this.cod_sap = cod_sap;
        }

        public string id { get; set; }
        public string grupo { get; set; }
        public string grupoid { get; set; }
        public string nombre { get; set; }
        public string comercial { get; set; }
        public string idfiscal { get; set; }
        public string correo { get; set; }
        public string rol { get; set; }
        public string contraseña { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string gven { get; set; }
        public string tipologia { get; set; }
        public string cod_sap { get; set; }
        
    }
}