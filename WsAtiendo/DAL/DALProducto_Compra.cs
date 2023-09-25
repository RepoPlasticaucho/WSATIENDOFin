using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALProducto_Compra
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Producto_Compra ObtenerProducto_Compra()
        {
            Producto_Compra catalogoResponse = new Producto_Compra();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT PC.* , PR.id as productoid, PE.id as proveedorid, PE.sociedad_id
                                        FROM productos_compra as PC 
                                        INNER JOIN productos as PR on PR.cod_sap = PC.producto and PR.nombre = PC.nombre_producto
                                        INNER JOIN sociedades as S on S.id_fiscal = PC.cliente
                                        INNER JOIN proveedores as PE on PE.id_fiscal = PC.proveedor and PE.nombre = PC.nombre_prove and PE.sociedad_id = S.id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                Producto_CompraEntity producto_compraEntity = new Producto_CompraEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    cliente = dataReader["cliente"].ToString(),
                                    clienteid = dataReader["sociedad_id"].ToString(),
                                    productoid = dataReader["productoid"].ToString(),
                                    producto = dataReader["producto"].ToString(),
                                    nombre_producto = dataReader["nombre_producto"].ToString(),
                                    proveedorid = dataReader["proveedorid"].ToString(),
                                    proveedor = dataReader["proveedor"].ToString(),
                                    nombre_prove = dataReader["nombre_prove"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                   
                                };
                                (catalogoResponse.lstProductos_Compra ?? new List<Producto_CompraEntity>()).Add(producto_compraEntity);
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
                        if (catalogoResponse.lstProductos_Compra.Count == 0)
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