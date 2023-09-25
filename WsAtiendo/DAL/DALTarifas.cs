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
    public class DALTarifas
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000; Allow User Variables=true;";

        public static Tarifas ObtenerTarifas()
        {
            Tarifas tarifaResponse = new Tarifas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM tarifa_ice_iva";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                TarifasEntity tarifaEntity = new TarifasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    descripcion = dataReader["descripcion"].ToString(),
                                    impuesto_id = dataReader["impuesto_id"].ToString(),
                                    porcentaje = dataReader["porcentaje"].ToString(),
                                    tarifa_ad_valorem_e_d_2020 = dataReader["tarifa_ad_valorem_e_d_2020"].ToString(),
                                    tarifa_esp_9_mayo_diciembre_2020 = dataReader["tarifa_esp_9_mayo_diciembre_2020"].ToString(),
                                    tarifa_esp_e_d_2020 = dataReader["tarifa_esp_e_d_2020"].ToString(),
                                };
                                (tarifaResponse.lstTarifas ?? new List<TarifasEntity>()).Add(tarifaEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tarifaResponse.codigoError = "E";
                        tarifaResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tarifaResponse.codigoError))
                    {
                        if (tarifaResponse.lstTarifas.Count == 0)
                        {
                            tarifaResponse.codigoError = "E";
                            tarifaResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tarifaResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tarifaResponse.codigoError = "E";
                tarifaResponse.descripcionError = "1." + ex.Message;
            }
            return tarifaResponse;
        }

        public static Tarifas ObtenerTarifasN(string tarifa)
        {
            Tarifas tarifasResponse = new Tarifas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM tarifa_ice_iva	
                                        WHERE descripcion=?pDesc
                                      ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pDesc", tarifa);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                TarifasEntity tarifaEntity = new TarifasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    descripcion = dataReader["descripcion"].ToString(),
                                    impuesto_id = dataReader["impuesto_id"].ToString(),
                                    porcentaje = dataReader["porcentaje"].ToString(),
                                    tarifa_ad_valorem_e_d_2020 = dataReader["tarifa_ad_valorem_e_d_2020"].ToString(),
                                    tarifa_esp_9_mayo_diciembre_2020 = dataReader["tarifa_esp_9_mayo_diciembre_2020"].ToString(),
                                    tarifa_esp_e_d_2020 = dataReader["tarifa_esp_e_d_2020"].ToString(),
                                };
                                (tarifasResponse.lstTarifas ?? new List<TarifasEntity>()).Add(tarifaEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tarifasResponse.codigoError = "E";
                        tarifasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tarifasResponse.codigoError))
                    {
                        if (tarifasResponse.lstTarifas.Count == 0)
                        {
                            tarifasResponse.codigoError = "E";
                            tarifasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tarifasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tarifasResponse.codigoError = "E";
                tarifasResponse.descripcionError = "1." + ex.Message;
            }
            return tarifasResponse;
        }
    }
}