using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALTipoUsuario
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Tipo_Usuario ObtenerTipo_Usuario()
        {
            Tipo_Usuario tipo_usuarioResponse = new Tipo_Usuario();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM tipo_usuario
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                Tipo_UsuarioEntity tipo_usuarioEntity = new Tipo_UsuarioEntity()
                                {
                                    idTipo_Usuario = dataReader["id"].ToString(),
                                    usuario = dataReader["usuario"].ToString(),
                                    created_at = dataReader["created_at"].ToString()
                                };
                                (tipo_usuarioResponse.lstTipo_Usuario ?? new List<Tipo_UsuarioEntity>()).Add(tipo_usuarioEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tipo_usuarioResponse.codigoError = "E";
                        tipo_usuarioResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tipo_usuarioResponse.codigoError))
                    {
                        if (tipo_usuarioResponse.lstTipo_Usuario.Count == 0)
                        {
                            tipo_usuarioResponse.codigoError = "E";
                            tipo_usuarioResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tipo_usuarioResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tipo_usuarioResponse.codigoError = "E";
                tipo_usuarioResponse.descripcionError = "1." + ex.Message;
            }
            return tipo_usuarioResponse;
        }

        public static Tipo_Usuario ObtenerTipo_UsuarioN(string tipo_usuario)
        {
            Tipo_Usuario tipo_usuarioResponse = new Tipo_Usuario();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM tipo_usuario
                                        WHERE usuario LIKE ?tUsuario
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?tUsuario", tipo_usuario);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                Tipo_UsuarioEntity tipo_usuarioEntity = new Tipo_UsuarioEntity()
                                {
                                    idTipo_Usuario = dataReader["id"].ToString(),
                                    usuario = dataReader["usuario"].ToString(),
                                    created_at = dataReader["created_at"].ToString()
                                };
                                (tipo_usuarioResponse.lstTipo_Usuario ?? new List<Tipo_UsuarioEntity>()).Add(tipo_usuarioEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tipo_usuarioResponse.codigoError = "E";
                        tipo_usuarioResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tipo_usuarioResponse.codigoError))
                    {
                        if (tipo_usuarioResponse.lstTipo_Usuario.Count == 0)
                        {
                            tipo_usuarioResponse.codigoError = "E";
                            tipo_usuarioResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tipo_usuarioResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tipo_usuarioResponse.codigoError = "E";
                tipo_usuarioResponse.descripcionError = "1." + ex.Message;
            }
            return tipo_usuarioResponse;
        }
    }
}
