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
    public class DALProveedoresProductos
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";


        public static ProveedoresProductos AgregarProductosProv(ProveedoresProductosEntity proveedoresEntity)
        {
            ProveedoresProductos proveedoresProductosResponse = new ProveedoresProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO proveedores_productos
                                    (provedor_id,producto_id,nombre_producto,precio,created_at)
                                       VALUES(?pProvedor_id,?pProducto_id,?pNombre_producto,?pPrecio,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pProvedor_id", proveedoresEntity.provedor_id);
                                    myCmd.Parameters.AddWithValue("?pProducto_id", proveedoresEntity.producto_id);
                                    myCmd.Parameters.AddWithValue("?pNombre_producto", proveedoresEntity.nombre_producto);
                                    myCmd.Parameters.AddWithValue("?pPrecio", proveedoresEntity.precio);                                 
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        proveedoresProductosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        proveedoresProductosResponse.codigoError = "E";
                                        proveedoresProductosResponse.descripcionError = "3. No se pudo crear el Producto.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    proveedoresProductosResponse.codigoError = "E";
                                    proveedoresProductosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresProductosResponse.codigoError = "E";
                        proveedoresProductosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                proveedoresProductosResponse.codigoError = "E";
                proveedoresProductosResponse.descripcionError = "1. " + ex.Message;
            }
            return proveedoresProductosResponse;
        }

        public static ProveedoresProductos ObtenerProveedoresProductosEx(string producto)
        {
            ProveedoresProductos proveedoresProductosResponse = new ProveedoresProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT PP.*
                                        FROM proveedores_productos as PP
                                        Where PP.nombre_producto = ?pProducto";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pProducto", producto);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProveedoresProductosEntity proveedoresProductosEntity = new ProveedoresProductosEntity()
                                {                                    
                                    id = dataReader["id"].ToString(),
                                    nombre_producto = dataReader["nombre_producto"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                    created_at = dataReader["created_at"].ToString(),
                                    producto_id = dataReader["producto_id"].ToString(),
                                    provedor_id = dataReader["provedor_id"].ToString(),
                                    updated_at = dataReader["updated_at"].ToString()

                                };
                                (proveedoresProductosResponse.lstProveedoresProductos ?? new List<ProveedoresProductosEntity>()).Add(proveedoresProductosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresProductosResponse.codigoError = "E";
                        proveedoresProductosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(proveedoresProductosResponse.codigoError))
                    {
                        if (proveedoresProductosResponse.lstProveedoresProductos.Count == 0)
                        {
                            proveedoresProductosResponse.codigoError = "E";
                            proveedoresProductosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            proveedoresProductosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                proveedoresProductosResponse.codigoError = "E";
                proveedoresProductosResponse.descripcionError = "1." + ex.Message;
            }
            return proveedoresProductosResponse;
        }

        public static ProveedoresProductos ObtenerProveedoresProductosProv(string proveedor)
        {
            ProveedoresProductos proveedoresProductosResponse = new ProveedoresProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT PP.nombre_producto, P.etiquetas, PP.producto_id, P.tamanio, P.unidad_medida, T.descripcion, PP.precio, MA.marca, PP.costo
                                        FROM proveedores_productos as PP
                                        INNER JOIN productos as P on P.id = PP.producto_id
                                        INNER JOIN tarifa_ice_iva as T on T.id = P.tarifa_ice_iva_id
                                        INNER JOIN modelo_productos as MP on MP.id = P.modelo_producto_id
                                        INNER JOIN modelos as M on M.id = MP.modelo_id
                                        INNER JOIN marcas as MA on MA.id = M.marca_id
                                        WHERE PP.provedor_id = ?pProveedor
                                        GROUP BY PP.nombre_producto";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pProveedor", proveedor);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProveedoresProductosEntity proveedoresProductosEntity = new ProveedoresProductosEntity()
                                {
                                    producto_id = dataReader["producto_id"].ToString(),
                                    nombre_producto = dataReader["nombre_producto"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                    tamanio = dataReader["tamanio"].ToString(),
                                    unidad_medida = dataReader["unidad_medida"].ToString(),
                                    descripcion_uni = dataReader["descripcion"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    costo = dataReader["costo"].ToString()
                                };
                                (proveedoresProductosResponse.lstProveedoresProductos ?? new List<ProveedoresProductosEntity>()).Add(proveedoresProductosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresProductosResponse.codigoError = "E";
                        proveedoresProductosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(proveedoresProductosResponse.codigoError))
                    {
                        if (proveedoresProductosResponse.lstProveedoresProductos.Count == 0)
                        {
                            proveedoresProductosResponse.codigoError = "E";
                            proveedoresProductosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            proveedoresProductosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                proveedoresProductosResponse.codigoError = "E";
                proveedoresProductosResponse.descripcionError = "1." + ex.Message;
            }
            return proveedoresProductosResponse;
        }
        public static ProveedoresProductos ObtenerProductosProveedores(string proveedor,string producto)
        {
            ProveedoresProductos proveedoresProductosResponse = new ProveedoresProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select * 
                                    from proveedores_productos 
                                    where(producto_id = ?pProducto and provedor_id= ?pProveedor)";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pProveedor", proveedor);
                            myCmd.Parameters.AddWithValue("?pProducto", producto);


                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProveedoresProductosEntity proveedoresProductosEntity = new ProveedoresProductosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    producto_id = dataReader["producto_id"].ToString(),
                                    nombre_producto = dataReader["nombre_producto"].ToString(),
                                    provedor_id = dataReader["provedor_id"].ToString(),
                                    precio = dataReader["precio"].ToString(),

                                };
                                (proveedoresProductosResponse.lstProveedoresProductos ?? new List<ProveedoresProductosEntity>()).Add(proveedoresProductosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresProductosResponse.codigoError = "E";
                        proveedoresProductosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(proveedoresProductosResponse.codigoError))
                    {
                        if (proveedoresProductosResponse.lstProveedoresProductos.Count == 0)
                        {
                            proveedoresProductosResponse.codigoError = "E";
                            proveedoresProductosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            proveedoresProductosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                proveedoresProductosResponse.codigoError = "E";
                proveedoresProductosResponse.descripcionError = "1." + ex.Message;
            }
            return proveedoresProductosResponse;
        }

        public static ProveedoresProductos ActualizarProductosProveedores(ProveedoresProductosEntity proveedoresproducto)
        {
            ProveedoresProductos proveedoresproductosResponse = new ProveedoresProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE proveedores_productos 
                                      SET producto_id = ?pProducto_id,
                                          provedor_id = ?pProveedor_id,
                                          nombre_producto =?pNombre,
                                          precio = ?pPrecio,
                                          costo = ?pCosto
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
                                    myCmd.Parameters.AddWithValue("?pProducto_id", proveedoresproducto.producto_id);
                                    myCmd.Parameters.AddWithValue("?pProveedor_id", proveedoresproducto.provedor_id);
                                    myCmd.Parameters.AddWithValue("?pNombre", proveedoresproducto.nombre_producto);
                                    myCmd.Parameters.AddWithValue("?pPrecio", proveedoresproducto.precio);
                                    myCmd.Parameters.AddWithValue("?pCosto", proveedoresproducto.costo);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", proveedoresproducto.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        proveedoresproductosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        proveedoresproductosResponse.codigoError = "E";
                                        proveedoresproductosResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    proveedoresproductosResponse.codigoError = "E";
                                    proveedoresproductosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresproductosResponse.codigoError = "E";
                        proveedoresproductosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                proveedoresproductosResponse.codigoError = "E";
                proveedoresproductosResponse.descripcionError = "1. " + ex.Message;
            }
            return proveedoresproductosResponse;
        }
    }
}