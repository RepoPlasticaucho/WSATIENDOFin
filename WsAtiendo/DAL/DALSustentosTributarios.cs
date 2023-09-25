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
    public class DALSustentosTributarios
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static SustentosTributarios ObtenerSustentos()
        {
            SustentosTributarios sustentosResponse = new SustentosTributarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM sustentos_tributarios";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                SustentosTributariosEntity sustentoEntity = new SustentosTributariosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    sustento = dataReader["sustento"].ToString(),
                                };
                                (sustentosResponse.lstSustentos ?? new List<SustentosTributariosEntity>()).Add(sustentoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sustentosResponse.codigoError = "E";
                        sustentosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(sustentosResponse.codigoError))
                    {
                        if (sustentosResponse.lstSustentos.Count == 0)
                        {
                            sustentosResponse.codigoError = "E";
                            sustentosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            sustentosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sustentosResponse.codigoError = "E";
                sustentosResponse.descripcionError = "1." + ex.Message;
            }
            return sustentosResponse;
        }

        public static SustentosTributarios ObtenerSustentosN(string sustento)
        {
            SustentosTributarios sustentosResponse = new SustentosTributarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT id
		                                FROM sustentos_tributarios
                                         WHERE sustento=?pSustento
                                     ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pSustento", sustento);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                SustentosTributariosEntity sustentoEntity = new SustentosTributariosEntity()
                                {
                                    id = dataReader["id"].ToString()

                                };
                                (sustentosResponse.lstSustentos ?? new List<SustentosTributariosEntity>()).Add(sustentoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sustentosResponse.codigoError = "E";
                        sustentosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(sustentosResponse.codigoError))
                    {
                        if (sustentosResponse.lstSustentos.Count == 0)
                        {
                            sustentosResponse.codigoError = "E";
                            sustentosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            sustentosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sustentosResponse.codigoError = "E";
                sustentosResponse.descripcionError = "1." + ex.Message;
            }
            return sustentosResponse;
        }
    }
}