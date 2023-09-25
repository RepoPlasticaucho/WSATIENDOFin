using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALUsuarios
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Usuarios ObtenerUsuarios()
        {
            Usuarios catalogoResponse = new Usuarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM usuarios;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                UsuariosEntity usuariosEntity = new UsuariosEntity()
                                {
                                    grupo = dataReader["grupo"].ToString(),
                                    grupoid = dataReader["grupo"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    comercial = dataReader["comercial"].ToString(),
                                    idfiscal = dataReader["idfiscal"].ToString(),
                                    correo = dataReader["correo"].ToString(),
                                    rol = dataReader["rol"].ToString(),
                                    contraseña = dataReader["contraseña"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    direccion = dataReader["direccion"].ToString(),
                                    gven = dataReader["gven"].ToString(),
                                    tipologia = dataReader["tipologia"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString()
                                };
                                (catalogoResponse.lstUsuarios ?? new List<UsuariosEntity>()).Add(usuariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        catalogoResponse.codigoError = "E";
                        catalogoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(catalogoResponse.codigoError))
                    {
                        if (catalogoResponse.lstUsuarios.Count == 0)
                        {
                            catalogoResponse.codigoError = "E";
                            catalogoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            catalogoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                catalogoResponse.codigoError = "E";
                catalogoResponse.descripcionError = "1." + ex.Message;
            }
            return catalogoResponse;
        }
    }
}