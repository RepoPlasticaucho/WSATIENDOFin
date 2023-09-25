using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Portafolios
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<PortafoliosEntity> lstPortafolios { get; set; }

        public Portafolios(string codigoError, string descripcionError, List<PortafoliosEntity> lstPortafolios)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstPortafolios = lstPortafolios;
        }
        public Portafolios()
        {
            this.lstPortafolios = new List<PortafoliosEntity>();

        }
    }

    public class PortafoliosEntity
    {
        public PortafoliosEntity()
        {
        }

        public PortafoliosEntity(string id, string identificacion, string sociedadid, string cliente, string almacen, string almacenid, string pto_emision, string material_cod, string materialid, string costo, string pvp1, string pvp_sugerido, string stock, string materialnombre)
        {
            this.id = id;
            this.identificacion = identificacion;
            this.sociedadid = sociedadid;
            this.cliente = cliente;
            this.almacen = almacen;
            this.almacenid = almacenid;
            this.pto_emision = pto_emision;
            this.material_cod = material_cod;
            this.materialid = materialid;
            this.costo = costo;
            this.pvp1 = pvp1;
            this.pvp_sugerido = pvp_sugerido;
            this.stock = stock;
            this.materialnombre = materialnombre;
        }

        public string id { get; set; }
        public string identificacion { get; set; }
        public string sociedadid { get; set; }
        public string cliente { get; set; }
        public string almacen { get; set; }
        public string almacenid { get; set; }
        public string pto_emision { get; set; }
        public string material_cod { get; set; }
        public string materialid { get; set; }
        public string costo { get; set; }
        public string pvp1 { get; set; }
        public string pvp_sugerido { get; set; }
        public string stock { get; set; }
        public string materialnombre { get; set; }
    }
}