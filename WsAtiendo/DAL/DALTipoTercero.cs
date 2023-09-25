using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALTipoTercero
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Tipo_Tercero ObtenerTipo_Tercero()
        {
            Tipo_Tercero tipo_terceroResponse = new Tipo_Tercero();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM tipo_tercero
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                               Tipo_TerceroEntity tipo_terceroEntity = new Tipo_TerceroEntity()
                                {
                                   idTipo_tercero = dataReader["id"].ToString(),
                                   codigo = dataReader["codigo"].ToString(),
                                   descripcion = dataReader["descripcion"].ToString(),
                                   created_at = dataReader["created_at"].ToString()
                                };
                                (tipo_terceroResponse.lstTipo_Tercero ?? new List<Tipo_TerceroEntity>()).Add(tipo_terceroEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tipo_terceroResponse.codigoError = "E";
                        tipo_terceroResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tipo_terceroResponse.codigoError))
                    {
                        if (tipo_terceroResponse.lstTipo_Tercero.Count == 0)
                        {
                            tipo_terceroResponse.codigoError = "E";
                            tipo_terceroResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tipo_terceroResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tipo_terceroResponse.codigoError = "E";
                tipo_terceroResponse.descripcionError = "1." + ex.Message;
            }
            return tipo_terceroResponse;
        }

        public static Tipo_Tercero ObtenerTipo_TerceroN(string tipo_tercero)
        {
            Tipo_Tercero tipo_terceroResponse = new Tipo_Tercero();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM tipo_tercero
                                        WHERE descripcion LIKE ?tDescripcion
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?tDescripcion", tipo_tercero);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                Tipo_TerceroEntity tipo_terceroEntity = new Tipo_TerceroEntity()
                                {
                                    idTipo_tercero = dataReader["id"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    descripcion = dataReader["descripcion"].ToString(),
                                    created_at = dataReader["created_at"].ToString()
                                };
                                (tipo_terceroResponse.lstTipo_Tercero ?? new List<Tipo_TerceroEntity>()).Add(tipo_terceroEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tipo_terceroResponse.codigoError = "E";
                        tipo_terceroResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tipo_terceroResponse.codigoError))
                    {
                        if (tipo_terceroResponse.lstTipo_Tercero.Count == 0)
                        {
                            tipo_terceroResponse.codigoError = "E";
                            tipo_terceroResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tipo_terceroResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tipo_terceroResponse.codigoError = "E";
                tipo_terceroResponse.descripcionError = "1." + ex.Message;
            }
            return tipo_terceroResponse;
        }

      }
}