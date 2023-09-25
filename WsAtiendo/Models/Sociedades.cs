using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Sociedades
    {

        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<SociedadesEntity> lstSociedades { get; set; }

        public Sociedades(string codigoError, string descripcionError, List<SociedadesEntity> lstSociedades)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstSociedades = lstSociedades;
        }

        public Sociedades()
        {
            this.lstSociedades = new List<SociedadesEntity>();
        }
    }

    public class SociedadesEntity
    {
        public SociedadesEntity()
        {
        }

        public SociedadesEntity(string idSociedad, string idGrupo, string nombreGrupo, string razon_social, string nombre_comercial, string id_fiscal_grupo, string id_fiscal, string password, string email, string telefono, string gven, string tipologia, string cod_sap, string funcion, string paginacion, string tipo_ambienteid, string dir1, string direccion, string ambiente, string url_certificado, string clave_certificado)
        {
            this.idSociedad = idSociedad;
            this.idGrupo = idGrupo;
            this.nombreGrupo = nombreGrupo;
            this.razon_social = razon_social;
            this.nombre_comercial = nombre_comercial;
            this.id_fiscal_grupo = id_fiscal_grupo;
            this.id_fiscal = id_fiscal;
            this.password = password;
            this.email = email;
            this.telefono = telefono;
            this.gven = gven;
            this.tipologia = tipologia;
            this.cod_sap = cod_sap;
            this.funcion = funcion;
            this.paginacion = paginacion;
            this.tipo_ambienteid = tipo_ambienteid;
            this.dir1 = dir1;
            this.direccion = direccion;
            this.ambiente = ambiente;
            this.url_certificado = url_certificado;
            this.clave_certificado = clave_certificado;
            this.email_certificado = email_certificado;
            this.pass_certificado = pass_certificado;
        }

        public string idSociedad { get; set; }
        public string idGrupo { get; set; }
        public string nombreGrupo { get; set; }
        public string razon_social { get; set; }
        public string nombre_comercial { get; set; }
        public string id_fiscal_grupo { get; set; }
        public string id_fiscal { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string gven { get; set; }
        public string tipologia { get; set; }
        public string cod_sap { get; set; }
        public string funcion { set; get; }
        public string paginacion { set; get; }
        public string tipo_ambienteid { set; get; }
        public string dir1 { set; get; }
        public string direccion { set; get; }
        public string ambiente { set; get; }
        public string url_certificado { set; get; }
        public string clave_certificado{ set; get; }
        public string email_certificado { set; get; }
        public string pass_certificado { set; get; }


    }

}