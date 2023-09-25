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
    public class DALFormasPago
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;AllowZeroDateTime=True;";

        public static FormasPago ObtenerFormasPago()
        {
            FormasPago formasResponse = new FormasPago();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM
                                        formas_pago";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                FormasPagoEntity formasEntity = new FormasPagoEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    fecha_fin = dataReader["fecha_fin"].ToString(),
                                    fecha_inicio = dataReader["fecha_inicio"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                };
                                (formasResponse.lstFormasPago ?? new List<FormasPagoEntity>()).Add(formasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        formasResponse.codigoError = "E";
                        formasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(formasResponse.codigoError))
                    {
                        if (formasResponse.lstFormasPago.Count == 0)
                        {
                            formasResponse.codigoError = "E";
                            formasResponse.descripcionError = "3. No se encontró datos";
                        }
                        else
                        {
                            formasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                formasResponse.codigoError = "E";
                formasResponse.descripcionError = "1." + ex.Message;
            }
            return formasResponse;
        }

        public static FormasPago ObtenerFormasPagoN(string name)
        {
            FormasPago formaResponse = new FormasPago();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT id
                                        FROM formas_pago
                                        WHERE nombre= ?pNombre
                                      ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pNombre", name);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                FormasPagoEntity formasEntity = new FormasPagoEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                };
                                (formaResponse.lstFormasPago ?? new List<FormasPagoEntity>()).Add(formasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        formaResponse.codigoError = "E";
                        formaResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(formaResponse.codigoError))
                    {
                        if (formaResponse.lstFormasPago.Count == 0)
                        {
                            formaResponse.codigoError = "E";
                            formaResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            formaResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                formaResponse.codigoError = "E";
                formaResponse.descripcionError = "1." + ex.Message;
            }
            return formaResponse;
        }

        
    }
}