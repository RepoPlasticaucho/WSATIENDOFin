using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALCatalogos
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Catalogos ObtenerCatologos()
        {
            Catalogos catalogoResponse = new Catalogos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM catalogos;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CatalogosEntity catalogosEntity = new CatalogosEntity()
                                {
                                    marca = dataReader["marca"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    genero = dataReader["genero"].ToString(),
                                    caracteristica = dataReader["caracteristica"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    tipo = dataReader["tipo"].ToString(),
                                    producto = dataReader ["producto"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    material = dataReader["material"].ToString(),
                                    modelo_producto_id = dataReader["modelo_producto"].ToString()
                                };
                                (catalogoResponse.lstCatalogos ?? new List<CatalogosEntity>()).Add(catalogosEntity);
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
                        if (catalogoResponse.lstCatalogos.Count == 0)
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

        public static Catalogos ObtenerCatologosLineas()
        {
            Catalogos catalogoResponse = new Catalogos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select distinct categoria,tipo from catalogos;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CatalogosEntity catalogosEntity = new CatalogosEntity()
                                {
                                    categoria = dataReader["categoria"].ToString(),
                                    tipo = dataReader["tipo"].ToString()

                                };
                                (catalogoResponse.lstCatalogos ?? new List<CatalogosEntity>()).Add(catalogosEntity);
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
                        if (catalogoResponse.lstCatalogos.Count == 0)
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

        public static Catalogos ObtenerCatalogosModelos()
        {
            Catalogos catalogoResponse = new Catalogos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT 
                                        CAT.marca,CAT.tipo,CAT.producto,CAT.linea_producto_id,CAT.categoria,M.id as marcaid,C.id as categoriaid,L.id as lineaid  
                                        from catalogos AS CAT 
                                        INNER JOIN marcas as M on M.marca = CAT.marca
                                        INNER JOIN categorias as C on C.categoria = CAT.categoria
                                        INNER JOIN lineas as L on L.linea = CAT.tipo
                                        group by producto
                                        order by tipo";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CatalogosEntity catalogosEntity = new CatalogosEntity()
                                {
                                    tipo = dataReader["tipo"].ToString(),
                                    producto = dataReader["producto"].ToString(),
                                    linea_producto_id = dataReader["linea_producto_id"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    categoriaid = dataReader ["categoriaid"].ToString(),
                                    tipoid = dataReader["lineaid"].ToString(),
                                    marcaid = dataReader["marcaid"].ToString()

                                };
                                (catalogoResponse.lstCatalogos ?? new List<CatalogosEntity>()).Add(catalogosEntity);
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
                        if (catalogoResponse.lstCatalogos.Count == 0)
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

        public static Catalogos ObtenerCatalogosModelosProductos()
        {
            Catalogos catalogoResponse = new Catalogos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT 
                                        distinct CAT.marca,CAT.producto,CAT.color,CAT.caracteristica,CAT.genero,CAT.subfamilia,CAT.modelo_producto,CAT.familia,
				                        C.id as color_id , Car.id as caracteristica_id, G.id as genero_id, MO.id as modelo_id, M.id as marca_id
                                        from catalogos as CAT
                                        INNER JOIN marcas as M on M.marca = CAT.marca
                                        INNER JOIN modelos as MO on MO.modelo = CAT.producto and M.id = MO.marca_id
                                        INNER JOIN colors as C on C.color = CAT.color
                                        INNER JOIN atributos as Car on Car.atributo = CAT.caracteristica
                                        INNER JOIN generos as G on G.genero = CAT.genero
                                        order by producto";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CatalogosEntity catalogosEntity = new CatalogosEntity()
                                {
                                    marca = dataReader["marca"].ToString(),
                                    marcaid = dataReader["marca_id"].ToString(),
                                    producto = dataReader["producto"].ToString(),
                                    productoid = dataReader["modelo_id"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    colorid = dataReader["color_id"].ToString(),
                                    caracteristica = dataReader["caracteristica"].ToString(),
                                    caracteristicaid = dataReader["caracteristica_id"].ToString(),
                                    genero = dataReader["genero"].ToString(),
                                    generoid = dataReader["genero_id"].ToString(),
                                    subfamilia = dataReader ["subfamilia"].ToString(),
                                    moelo_producto = dataReader["modelo_producto"].ToString(),
                                    familia = dataReader["familia"].ToString()

                                };
                                (catalogoResponse.lstCatalogos ?? new List<CatalogosEntity>()).Add(catalogosEntity);
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
                        if (catalogoResponse.lstCatalogos.Count == 0)
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

        public static Catalogos ObtenerCatalogosProductos()
        {
            Catalogos catalogoResponse = new Catalogos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct 
                                       CAT.codigo,CAT.talla, CAT.costo, CAT.pvp ,CAT.subfamilia, CAT.modelo_producto,CAT.familia,CAT.material,CAT.impuesto, CAT.unidad_medida,
                                        MO.id as modelo_productoid,TI.id as tarifa_id
                                        from catalogos as CAT
                                        INNER JOIN modelo_productos as MO ON MO.modelo_producto = CAT.modelo_producto
                                        INNER JOIN tarifa_ice_iva as TI ON TI.descripcion = CAT.impuesto
                                     order by codigo";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CatalogosEntity catalogosEntity = new CatalogosEntity()
                                {
                                    codigo = dataReader["codigo"].ToString(),
                                    talla = dataReader["talla"].ToString(),
                                    costo = dataReader["costo"].ToString(),
                                    pvp = dataReader["pvp"].ToString(),
                                    subfamilia = dataReader["subfamilia"].ToString(),
                                    moelo_producto = dataReader["modelo_producto"].ToString(),
                                    familia = dataReader["familia"].ToString(),
                                    material = dataReader["material"].ToString(),
                                    modelo_producto_id = dataReader["modelo_productoid"].ToString(),
                                    tarifa_ice_iva_id = dataReader["tarifa_id"].ToString(),
                                    unidad_medidad = dataReader["unidad_medida"].ToString()
                                };
                                (catalogoResponse.lstCatalogos ?? new List<CatalogosEntity>()).Add(catalogosEntity);
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
                        if (catalogoResponse.lstCatalogos.Count == 0)
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

    
