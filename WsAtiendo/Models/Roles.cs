using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Roles
    {

        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<RolesEntity> lstRoles { get; set; }

        public Roles(string codigoError, string descripcionError, List<RolesEntity> lstRoles)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstRoles = lstRoles;
        }

        public Roles()
        {
            this.lstRoles = new List<RolesEntity>();
        }
    }

    public class RolesEntity
    {
        public string id { get; set; }
        public string name { get; set; }

        public RolesEntity(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public RolesEntity()
        {
        }
    }

}