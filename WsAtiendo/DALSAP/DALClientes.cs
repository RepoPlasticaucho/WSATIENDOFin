using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DALSAP
{
    public class DALClientes
    {

        public static SociedadesEntity ObtenerSociedadesSAP(SociedadesEntity sociedad)
        {
            Session session = ObtenerDatosConexion();
            try
            {
                SAPRfc SAPRfc = new SAPRfc();
                //APE: Toma los datos de conexión a SAP segun el ambiente de Trabajo
                RfcConfigParameters rfc = new RfcConfigParameters();
                rfc.Clear();
                rfc = SAPRfc.ObtenerConexionSAP(session);
                RfcDestination rfcDestination = RfcDestinationManager.GetDestination(rfc);
                RfcRepository rfcRepository = rfcDestination.Repository;
                //APE: Instancia la RFC con el nombre en SAP
                IRfcFunction function = rfcRepository.CreateFunction("ZMFFI_OBT_DAT_CLI");
                //APE: Carga los parametros de consulta
                function.SetValue("P_IDEN_FIS", sociedad.id_fiscal);
                //APE: Ejecuta la RFC en SAP
                function.Invoke(rfcDestination);
                //APE: Toma la información de retorno de la RFC
                var E_ERROR = function.GetValue("E_ERROR").ToString();
                if (E_ERROR.Equals("OK"))
                {
                    sociedad.cod_sap = function.GetValue("E_KUNNR").ToString();
                    sociedad.razon_social = function.GetValue("E_NAME1").ToString() + " " + function.GetValue("E_NAME2").ToString();
                    sociedad.gven = function.GetValue("E_VKGRP").ToString();
                    sociedad.tipologia = function.GetValue("E_KVGR1").ToString();
                }
            }
            catch (Exception ex) { }
            return sociedad;
        }

        public static Session ObtenerDatosConexion()
        {
            Session session = new Session();
            session.Name = "session_atiendo".ToUpper();
            session.AppServerHost = ConfigurationManager.AppSettings["AppServerHost"];
            session.Usuario = "SAPOPTMZ".ToUpper();
            session.Contraseña = "Opt1m1zA_2019!";
            session.SystemNumber = ConfigurationManager.AppSettings["SystemNumber"];
            session.Language = ConfigurationManager.AppSettings["Language"];
            session.PoolSize = ConfigurationManager.AppSettings["PoolSize"];
            session.MaxPoolSize = ConfigurationManager.AppSettings["MaxPoolSize"];
            session.IdleTimeout = ConfigurationManager.AppSettings["IdleTimeout"];
            session.SAPRouter = ConfigurationManager.AppSettings["SAPRouter"];
            session.Mandante = ConfigurationManager.AppSettings["Mandante"];
            return session;
        }

    }

}