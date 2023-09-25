using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Tarifas
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<TarifasEntity> lstTarifas { get; set; }

        public Tarifas(string codigoError, string descripcionError, List<TarifasEntity> lstTarifas)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstTarifas = lstTarifas;
        }

        public Tarifas()
        {
            this.lstTarifas = new List<TarifasEntity>();
        }
    }

    public class TarifasEntity
    {
        public string id { set; get; }
        public string impuesto_id { set; get; }
        public string codigo { set; get; }
        public string porcentaje { set; get; }
        public string descripcion { set; get; }
        public string tarifa_ad_valorem_e_d_2020 { set; get; }
        public string tarifa_esp_e_d_2020 { set; get; }
        public string tarifa_esp_9_mayo_diciembre_2020 { set; get; }
        public string created_at { set; get; }
        public string updated_at { set; get; }

        public TarifasEntity()
        {

        }
        public TarifasEntity(string id, string impuesto_id, string codigo, string porcentaje, string descripcion, string tarifa_ad_valorem_e_d_2020, string tarifa_esp_e_d_2020, string tarifa_esp_9_mayo_diciembre_2020, string created_at, string updated_at)
        {
            this.id = id;
            this.impuesto_id = impuesto_id;
            this.codigo = codigo;
            this.porcentaje = porcentaje;
            this.descripcion = descripcion;
            this.tarifa_ad_valorem_e_d_2020 = tarifa_ad_valorem_e_d_2020;
            this.tarifa_esp_e_d_2020 = tarifa_esp_e_d_2020;
            this.tarifa_esp_9_mayo_diciembre_2020 = tarifa_esp_9_mayo_diciembre_2020;
            this.created_at = created_at;
            this.updated_at = updated_at;
        }
    }
}