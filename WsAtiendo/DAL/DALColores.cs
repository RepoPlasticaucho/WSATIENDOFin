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
    public class DALColores
    {

        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Colors ObtenerColores()
        {
            Colors colorsResponse = new Colors();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM colors";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ColorsEntity colorEntity = new ColorsEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                };
                                (colorsResponse.lstColors ?? new List<ColorsEntity>()).Add(colorEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        colorsResponse.codigoError = "E";
                        colorsResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(colorsResponse.codigoError))
                    {
                        if (colorsResponse.lstColors.Count == 0)
                        {
                            colorsResponse.codigoError = "E";
                            colorsResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            colorsResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                colorsResponse.codigoError = "E";
                colorsResponse.descripcionError = "1." + ex.Message;
            }
            return colorsResponse;
        }

        public static Colors ObtenerColoresNombre(string color)
        {
            Colors colorsResponse = new Colors();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM colors where color = ?pcolor";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcolor", color);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ColorsEntity colorEntity = new ColorsEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                };
                                (colorsResponse.lstColors ?? new List<ColorsEntity>()).Add(colorEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        colorsResponse.codigoError = "E";
                        colorsResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(colorsResponse.codigoError))
                    {
                        if (colorsResponse.lstColors.Count == 0)
                        {
                            colorsResponse.codigoError = "E";
                            colorsResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            colorsResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                colorsResponse.codigoError = "E";
                colorsResponse.descripcionError = "1." + ex.Message;
            }
            return colorsResponse;
        }

        public static Colors ObtenerColoresUno(string color)
        {
            Colors colorsResponse = new Colors();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM colors where color = ?pcolor LIMIT 1";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcolor", color);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ColorsEntity colorEntity = new ColorsEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                };
                                (colorsResponse.lstColors ?? new List<ColorsEntity>()).Add(colorEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        colorsResponse.codigoError = "E";
                        colorsResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(colorsResponse.codigoError))
                    {
                        if (colorsResponse.lstColors.Count == 0)
                        {
                            colorsResponse.codigoError = "E";
                            colorsResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            colorsResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                colorsResponse.codigoError = "E";
                colorsResponse.descripcionError = "1." + ex.Message;
            }
            return colorsResponse;
        }
        public static Colors InsertarColor(ColorsEntity color)
        {
            Colors colorResponse = new Colors();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO colors(color,cod_sap,etiquetas,es_plasticaucho,es_sincronizado,created_at)
                                       VALUES(?pColor,?pCodSAP,?pEtiquetas,?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pColor", color.color);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", color.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", color.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", "1");
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        colorResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        colorResponse.codigoError = "E";
                                        colorResponse.descripcionError = "3. No se pudo crear el Color.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    colorResponse.codigoError = "E";
                                    colorResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        colorResponse.codigoError = "E";
                        colorResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                colorResponse.codigoError = "E";
                colorResponse.descripcionError = "1. " + ex.Message;
            }
            return colorResponse;
        }

        public static Colors ModificarColor(ColorsEntity color)
        {
            Colors colorResponse = new Colors();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE colors 
                                      SET color=?pColor,
                                          cod_sap=?pCodSAP,
                                          etiquetas=?pEtiquetas,
                                          updated_at=?pUpdatedAt
                                      WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pColor", color.color);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", color.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", color.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", color.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        colorResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        colorResponse.codigoError = "E";
                                        colorResponse.descripcionError = "3. No se pudo actualizar el Color.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    colorResponse.codigoError = "E";
                                    colorResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        colorResponse.codigoError = "E";
                        colorResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                colorResponse.codigoError = "E";
                colorResponse.descripcionError = "1. " + ex.Message;
            }
            return colorResponse;
        }

        public static Colors EliminarColor(ColorsEntity color)
        {
            Colors colorResponse = new Colors();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM colors WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", color.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        colorResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        colorResponse.codigoError = "E";
                                        colorResponse.descripcionError = "3. No se pudo eliminar el Color.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    colorResponse.codigoError = "E";
                                    colorResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        colorResponse.codigoError = "E";
                        colorResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                colorResponse.codigoError = "E";
                colorResponse.descripcionError = "1. " + ex.Message;
            }
            return colorResponse;
        }

    }
}