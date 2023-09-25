using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALCiudades
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Ciudades ObtenerCiudadesAll()
        {
            Ciudades ciudadesResponse = new Ciudades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM ciudad
                                      ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                 CiudadesEntity ciudadesEntity = new CiudadesEntity()
                                {
                                    idCiudad = dataReader["id"].ToString(),
                                    ciudad = dataReader["ciudad"].ToString(),
                                    provinciaid = dataReader ["provinciaid"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    created_at = dataReader["created_at"].ToString()
                                };
                                (ciudadesResponse.lstCiudades ?? new List<CiudadesEntity>()).Add(ciudadesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ciudadesResponse.codigoError = "E";
                        ciudadesResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(ciudadesResponse.codigoError))
                    {
                        if (ciudadesResponse.lstCiudades.Count == 0)
                        {
                            ciudadesResponse.codigoError = "E";
                            ciudadesResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            ciudadesResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ciudadesResponse.codigoError = "E";
                ciudadesResponse.descripcionError = "1." + ex.Message;
            }
            return ciudadesResponse;
        }

        public static Ciudades ObtenerCiudades(string provincia)
        {
            Ciudades ciudadesResponse = new Ciudades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM ciudad
										INNER JOIN provincia as P ON P.id = provinciaid	
                                        WHERE P.provincia= ?pProvincia
                                      ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pProvincia", provincia);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CiudadesEntity ciudadesEntity = new CiudadesEntity()
                                {
                                    idCiudad = dataReader["id"].ToString(),
                                    ciudad = dataReader["ciudad"].ToString(),
                                    provinciaid = dataReader["provinciaid"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    created_at = dataReader["created_at"].ToString()
                                };
                                (ciudadesResponse.lstCiudades ?? new List<CiudadesEntity>()).Add(ciudadesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ciudadesResponse.codigoError = "E";
                        ciudadesResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(ciudadesResponse.codigoError))
                    {
                        if (ciudadesResponse.lstCiudades.Count == 0)
                        {
                            ciudadesResponse.codigoError = "E";
                            ciudadesResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            ciudadesResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ciudadesResponse.codigoError = "E";
                ciudadesResponse.descripcionError = "1." + ex.Message;
            }
            return ciudadesResponse;
        }
        public static Ciudades ObtenerCiudadesN(string name)
        {
            Ciudades ciudadesResponse = new Ciudades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM ciudad
										INNER JOIN provincia as P ON P.id = provinciaid	
                                        WHERE ciudad= ?pCiudad
                                      ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pCiudad", name);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CiudadesEntity ciudadesEntity = new CiudadesEntity()
                                {
                                    idCiudad = dataReader["id"].ToString(),
                                    ciudad = dataReader["ciudad"].ToString(),
                                    provinciaid = dataReader["provinciaid"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    created_at = dataReader["created_at"].ToString()
                                };
                                (ciudadesResponse.lstCiudades ?? new List<CiudadesEntity>()).Add(ciudadesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ciudadesResponse.codigoError = "E";
                        ciudadesResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(ciudadesResponse.codigoError))
                    {
                        if (ciudadesResponse.lstCiudades.Count == 0)
                        {
                            ciudadesResponse.codigoError = "E";
                            ciudadesResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            ciudadesResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ciudadesResponse.codigoError = "E";
                ciudadesResponse.descripcionError = "1." + ex.Message;
            }
            return ciudadesResponse;
        }
    }
}
