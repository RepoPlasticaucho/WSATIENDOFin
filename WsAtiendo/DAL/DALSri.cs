using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WsAtiendo.Models;
using WsAtiendo.SRI;

namespace WsAtiendo.DAL
{
    public class DALSri
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static String RecibirXMLSri(string movimiento_id)
        {
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM movimientos
                                        WHERE id=?pMovimiento_id;";
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pMovimiento_id", movimiento_id);

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(myCmd))
                            {
                                DataSet dataSet = new DataSet();
                                adapter.Fill(dataSet);

                                DataTable dataTable = dataSet.Tables[0];
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    String url_factura = row["url_factura"].ToString();

                                    CComprobanteElectronico cComprobante = new CComprobanteElectronico();


                                    var recepcion = cComprobante.RecepcionComprobantePrueba(url_factura);
                                    var est = recepcion.Estado;

                                    return est;
                                }
                            }
                        }
                    
                }

            }
            catch (Exception ex)
            {
                return ex.Message + ex.StackTrace + ex.Source + ex.Data;
            }
            return "";
        }

        public static String AutorizarXMLSri(string movimiento_id)
        {
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM movimientos
                                        WHERE id=?pMovimiento_id;";
                    using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                    {
                        myCmd.Parameters.AddWithValue("?pMovimiento_id", movimiento_id);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(myCmd))
                        {
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet);

                            DataTable dataTable = dataSet.Tables[0];
                            foreach (DataRow row in dataTable.Rows)
                            {
                                String clave_acceso = row["clave_acceso"].ToString();

                                CComprobanteElectronico cComprobante = new CComprobanteElectronico();


                                var respuestaComprobanteProduccion = cComprobante.AutorizacionComprobantePrueba(clave_acceso);
                                var est = respuestaComprobanteProduccion.Comprobantes[0].Estado;
                                var hora = respuestaComprobanteProduccion.Comprobantes[0].FechaAutorizacion;

                                return est + hora;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                return ex.Message + ex.StackTrace + ex.Source + ex.Data;
            }
            return "";
        }
    }
}