using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALTipo_Comprobante
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Tipo_Comprobante ObtenerTipos()
        {
            Tipo_Comprobante tipoResponse = new Tipo_Comprobante();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM tipo_comprobante WHERE id BETWEEN 3 AND 5";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                Tipo_ComprobanteEntity tipoEntity = new Tipo_ComprobanteEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    nombre = dataReader["nombre"].ToString()
                                };
                                (tipoResponse.lstTipo_Comprobante ?? new List<Tipo_ComprobanteEntity>()).Add(tipoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tipoResponse.codigoError = "E";
                        tipoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tipoResponse.codigoError))
                    {
                        if (tipoResponse.lstTipo_Comprobante.Count == 0)
                        {
                            tipoResponse.codigoError = "E";
                            tipoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tipoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tipoResponse.codigoError = "E";
                tipoResponse.descripcionError = "1." + ex.Message;
            }
            return tipoResponse;
        }

        public static Tipo_Comprobante ObtenerTipoN(string nombre)
        {
            Tipo_Comprobante tipoResponse = new Tipo_Comprobante();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM tipo_comprobante
                                        WHERE nombre LIKE ?tNombre
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?tNombre", nombre);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                Tipo_ComprobanteEntity tipoEntity = new Tipo_ComprobanteEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    nombre = dataReader["nombre"].ToString()
                                };
                                (tipoResponse.lstTipo_Comprobante ?? new List<Tipo_ComprobanteEntity>()).Add(tipoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tipoResponse.codigoError = "E";
                        tipoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tipoResponse.codigoError))
                    {
                        if (tipoResponse.lstTipo_Comprobante.Count == 0)
                        {
                            tipoResponse.codigoError = "E";
                            tipoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tipoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tipoResponse.codigoError = "E";
                tipoResponse.descripcionError = "1." + ex.Message;
            }
            return tipoResponse;

    }
}
}