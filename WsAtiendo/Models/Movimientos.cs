using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Movimientos
    {
        public string codigoError { get; set; }
        public string descripcionError { get; set; }
        public List<MovimientosEntity> lstMovimientos { get; set; }

        public Movimientos(string codigoError, string descripcionError, List<MovimientosEntity> lstMovimientos)
        {
            this.codigoError = codigoError;
            this.descripcionError = descripcionError;
            this.lstMovimientos = lstMovimientos;
        }

        public Movimientos()
        {
            this.lstMovimientos = new List<MovimientosEntity>();
        }
    }

    public class MovimientosEntity
    {
        public MovimientosEntity()
        {

        }

        public MovimientosEntity(string id, string tercero_id, string tipo_id, string tipo_emision_cod, string estado_fact_id, string tipo_comprb_id, string id_fiscal_soc, string tipo_comprb, string almacen_id, string estab, string pto_emision, string cod_doc, string secuencial, string clave_acceso, string fecha_emision, string periodo_fiscal, string total_si, string total_desc, string total_imp, string propina, string importe_total, string proveedor_id, string tipo_ambiente, string tipo_comprb_cod, string url_factura, string proveedor, string sustento_id, string sustento, string comp_venta, string autorizacion_venta, string moneda, string valor_rete_iva, string valor_rete_renta, string camp_ad1, string camp_ad2, string tercero, string es_sincronizado, string created_at, string updated_at, string detalle_pago)
        {
            this.id = id;
            this.tercero_id = tercero_id;
            this.tipo_id = tipo_id;
            this.tipo_emision_cod = tipo_emision_cod;
            this.estado_fact_id = estado_fact_id;
            this.tipo_comprb_id = tipo_comprb_id;
            this.id_fiscal_soc = id_fiscal_soc;
            this.tipo_comprb = tipo_comprb;
            this.almacen_id = almacen_id;
            this.estab = estab;
            this.pto_emision = pto_emision;
            this.cod_doc = cod_doc;
            this.secuencial = secuencial;
            this.clave_acceso = clave_acceso;
            this.fecha_emision = fecha_emision;
            this.periodo_fiscal = periodo_fiscal;
            this.total_si = total_si;
            this.total_desc = total_desc;
            this.total_imp = total_imp;
            this.propina = propina;
            this.importe_total = importe_total;
            this.proveedor_id = proveedor_id;
            this.tipo_ambiente = tipo_ambiente;
            this.tipo_comprb_cod = tipo_comprb_cod;
            this.url_factura = url_factura;
            this.proveedor = proveedor;
            this.sustento_id = sustento_id;
            this.sustento = sustento;
            this.comp_venta = comp_venta;
            this.autorizacion_venta = autorizacion_venta;
            this.moneda = moneda;
            this.valor_rete_iva = valor_rete_iva;
            this.valor_rete_renta = valor_rete_renta;
            this.camp_ad1 = camp_ad1;
            this.camp_ad2 = camp_ad2;
            this.tercero = tercero;
            this.es_sincronizado = es_sincronizado;
            this.created_at = created_at;
            this.updated_at = updated_at;
            this.detalle_pago = detalle_pago;
        }

        public string id { get; set; }
        public string tercero_id { get; set; }
        public string tipo_id { get; set; }
        public string tipo_emision_cod { get; set; }
        public string estado_fact_id { get; set; }
        public string tipo_comprb_id { get; set; }
        public string id_fiscal_soc { get; set; }
        public string tipo_comprb { get; set; }
        public string almacen_id { get; set; }
        public string estab { get; set; }
        public string pto_emision { get; set; }
        public string cod_doc { get; set; }
        public string secuencial { get; set; }
        public string clave_acceso { get; set; }
        public string fecha_emision { get; set; }
        public string periodo_fiscal { get; set; }
        public string total_si { get; set; }
        public string total_desc { get; set; }
        public string total_imp { get; set; }
        public string propina { get; set; }
        public string importe_total { get; set; }
        public string proveedor_id { get; set; }
        public string tipo_ambiente { get; set; }
        public string tipo_comprb_cod { get; set; }
        public string url_factura { get; set; }
        public string proveedor { get; set; }
        public string sustento_id { get; set; }
        public string sustento { get; set; }
        public string comp_venta { get; set; }
        public string autorizacion_venta { get; set; }
        public string moneda { get; set; }
        public string valor_rete_iva { get; set; }
        public string valor_rete_renta { get; set; }
        public string camp_ad1 { get; set; }
        public string camp_ad2 { get; set; }
        public string tercero { get; set; }
        public string es_sincronizado { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string detalle_pago { get; set; }



    }
}