using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALRoles
    {

        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Roles ObtenerRoles()
        {
            Roles rolesResponse = new Roles();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM roles";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                RolesEntity rolesEntity = new RolesEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    name = dataReader["name"].ToString(),
                                };
                                (rolesResponse.lstRoles ?? new List<RolesEntity>()).Add(rolesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        rolesResponse.codigoError = "E";
                        rolesResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(rolesResponse.codigoError))
                    {
                        if (rolesResponse.lstRoles.Count == 0)
                        {
                            rolesResponse.codigoError = "E";
                            rolesResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            rolesResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                rolesResponse.codigoError = "E";
                rolesResponse.descripcionError = "1." + ex.Message;
            }
            return rolesResponse;
        }

    }
}