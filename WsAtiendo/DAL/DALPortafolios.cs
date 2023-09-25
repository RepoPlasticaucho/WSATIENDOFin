using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALPortafolios
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";
        public static Portafolios ObtenerPortafolios()
        {
            Portafolios catalogoResponse = new Portafolios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT
	                                    Pt.*,
	                                    S.id as sociedadid,Al.id as almacenid,Pr.id as materialid, Pr.nombre
                                        FROM portafolios AS Pt
                                        INNER JOIN sociedades as S on S.id_fiscal = Pt.identificacion
                                        INNER JOIN almacenes as Al on Al.codigo = Pt.pto_emision and Al.nombre_almacen = Pt.almacen and S.id_fiscal = Pt.identificacion
                                        INNER JOIN productos as Pr on Pr.cod_sap = Pt.material_cod";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                PortafoliosEntity portafoliosEntity = new PortafoliosEntity()
                                {
                                    identificacion = dataReader["identificacion"].ToString(),
                                    sociedadid = dataReader["sociedadid"].ToString(),
                                    cliente = dataReader["cliente"].ToString(),
                                    almacen = dataReader["almacen"].ToString(),
                                    almacenid = dataReader["almacenid"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString(),
                                    material_cod = dataReader["material_cod"].ToString(),
                                    materialid = dataReader["materialid"].ToString(),
                                    costo = dataReader["costo"].ToString(),
                                    pvp1 = dataReader["pvp1"].ToString(),
                                    pvp_sugerido = dataReader["pvp_sugerido"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    materialnombre = dataReader["nombre"].ToString()
                                };
                                (catalogoResponse.lstPortafolios ?? new List<PortafoliosEntity>()).Add(portafoliosEntity);
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
                        if (catalogoResponse.lstPortafolios.Count == 0)
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