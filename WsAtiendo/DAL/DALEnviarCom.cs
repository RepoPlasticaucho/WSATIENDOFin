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
    public class DALEnviarCom
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static String EnviarComprobanteCorreo(string movimiento_id)
        {
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT S.email_certificado, S.pass_certificado, T.correo, M.url_factura, M.clave_acceso
                                        FROM movimientos as M
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        INNER JOIN sociedades as S on S.id = A.sociedad_id
                                        INNER JOIN terceros as T on T.id = M.tercero_id
                                        WHERE M.id=?pMovimiento_id;";
                    using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                    {
                        myCmd.Parameters.AddWithValue("?pMovimiento_id", movimiento_id);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(myCmd))
                        {
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet);
                            string key = "Venus2022@!";
                            DataTable dataTable = dataSet.Tables[0];
                            foreach (DataRow row in dataTable.Rows)
                            {
                                String correoRem = row["email_certificado"].ToString();
                                String claveRem = EncryptionAlgorithm.Decrypt(row["pass_certificado"].ToString(),key);
                                String correoDes = row["correo"].ToString();
                                String url_facturaXML = row["url_factura"].ToString();
                                String clave_acceso = row["clave_acceso"].ToString();
                                String url_facturaPDF = "ftp://calidad.atiendo.ec/FacturasPDF/factura_" + clave_acceso + ".pdf";

                                SendEmail enviarEmail = new SendEmail();

                                var est = enviarEmail.EnviarComprobante(correoRem, claveRem, correoDes,url_facturaXML, url_facturaPDF);

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
    }
}