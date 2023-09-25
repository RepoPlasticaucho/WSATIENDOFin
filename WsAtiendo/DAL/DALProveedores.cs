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
    public class DALProveedores
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000; ConvertZeroDateTime=True;";


        public static Proveedores ObtenerProveedores()
        {
            Proveedores proveedoresResponse = new Proveedores();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM proveedores";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProveedoresEntity proveedoresEntity = new ProveedoresEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    ciudadid = dataReader["ciudadid"].ToString(),
                                    correo = dataReader["correo"].ToString(),
                                    created_at = dataReader["created_at"].ToString(),
                                    direccionprov = dataReader["direccionprov"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    updated_at = dataReader["updated_at"].ToString()
                                };
                                (proveedoresResponse.lstProveedores ?? new List<ProveedoresEntity>()).Add(proveedoresEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresResponse.codigoError = "E";
                        proveedoresResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(proveedoresResponse.codigoError))
                    {
                        if (proveedoresResponse.lstProveedores.Count == 0)
                        {
                            proveedoresResponse.codigoError = "E";
                            proveedoresResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            proveedoresResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                proveedoresResponse.codigoError = "E";
                proveedoresResponse.descripcionError = "1." + ex.Message;
            }
            return proveedoresResponse;
        }
        public static Proveedores ObtenerProveedoresAll(string sociedad)
        {
            Proveedores proveedoresResponse = new Proveedores();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT  distinct P.*,C.ciudad
		                            FROM proveedores_productos as PR
                                    INNER JOIN inventarios as I on I.producto_id = PR.producto_id
		                            INNER JOIN proveedores as P on P.id = PR.provedor_id
                                    INNER JOIN almacenes as A on A.id = I.almacen_id
                                    INNER JOIN ciudad as C on C.id = P.ciudadid
                                    WHERE A.sociedad_id = ?pSociedad";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pSociedad", sociedad);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProveedoresEntity proveedoresEntity = new ProveedoresEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    ciudadid = dataReader["ciudadid"].ToString(),
                                    ciudad = dataReader["ciudad"].ToString(),
                                    correo = dataReader["correo"].ToString(),
                                    created_at = dataReader["created_at"].ToString(),
                                    direccionprov = dataReader["direccionprov"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    updated_at = dataReader["updated_at"].ToString()
                                };
                                (proveedoresResponse.lstProveedores ?? new List<ProveedoresEntity>()).Add(proveedoresEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresResponse.codigoError = "E";
                        proveedoresResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(proveedoresResponse.codigoError))
                    {
                        if (proveedoresResponse.lstProveedores.Count == 0)
                        {
                            proveedoresResponse.codigoError = "E";
                            proveedoresResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            proveedoresResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                proveedoresResponse.codigoError = "E";
                proveedoresResponse.descripcionError = "1." + ex.Message;
            }
            return proveedoresResponse;
        }

        
        public static Proveedores ObtenerProveedoresS(string sociedad)
        {
            Proveedores proveedoresResponse = new Proveedores();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT  P.*,C.ciudad
									FROM proveedores as P
                                    INNER JOIN ciudad as C on C.id = P.ciudadid
                                    WHERE P.sociedad_id = ?pSociedad";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pSociedad", sociedad);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProveedoresEntity proveedoresEntity = new ProveedoresEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    ciudadid = dataReader["ciudadid"].ToString(),
                                    ciudad = dataReader["ciudad"].ToString(),
                                    correo = dataReader["correo"].ToString(),
                                    created_at = dataReader["created_at"].ToString(),
                                    direccionprov = dataReader["direccionprov"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    updated_at = dataReader["updated_at"].ToString()
                                };
                                (proveedoresResponse.lstProveedores ?? new List<ProveedoresEntity>()).Add(proveedoresEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresResponse.codigoError = "E";
                        proveedoresResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(proveedoresResponse.codigoError))
                    {
                        if (proveedoresResponse.lstProveedores.Count == 0)
                        {
                            proveedoresResponse.codigoError = "E";
                            proveedoresResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            proveedoresResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                proveedoresResponse.codigoError = "E";
                proveedoresResponse.descripcionError = "1." + ex.Message;
            }
            return proveedoresResponse;
        }
        public static Proveedores ObtenerProveedoresN(string name)
        {
            Proveedores proveedoresResponse = new Proveedores();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM proveedores
                                        WHERE nombre= ?pNombre
                                      ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pNombre", name);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProveedoresEntity proveedoresEntity = new ProveedoresEntity()
                                {
                                    id = dataReader["id"].ToString()
                                };
                                (proveedoresResponse.lstProveedores ?? new List<ProveedoresEntity>()).Add(proveedoresEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresResponse.codigoError = "E";
                        proveedoresResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(proveedoresResponse.codigoError))
                    {
                        if (proveedoresResponse.lstProveedores.Count == 0)
                        {
                            proveedoresResponse.codigoError = "E";
                            proveedoresResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            proveedoresResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                proveedoresResponse.codigoError = "E";
                proveedoresResponse.descripcionError = "1." + ex.Message;
            }
            return proveedoresResponse;
        }

        public static Proveedores ObtenerProveedoresID(string id, string sociedad)
        {
            Proveedores proveedoresResponse = new Proveedores();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT P.*, S.nombre_comercial
                                        FROM proveedores as P
                                        INNER JOIN sociedades as S on S.id = P.sociedad_id
                                        WHERE P.id=?pID AND P.sociedad_id=?pSociedad
                                      ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pID", id);
                            myCmd.Parameters.AddWithValue("?pSociedad", sociedad);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProveedoresEntity proveedoresEntity = new ProveedoresEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    ciudadid = dataReader["ciudadid"].ToString(),
                                    direccionprov = dataReader["direccionprov"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    sociedad_id = dataReader["sociedad_id"].ToString(),
                                    nombre_sociedad = dataReader["nombre_comercial"].ToString(),
                                    telefono = dataReader["telefono"].ToString()
                                };
                                (proveedoresResponse.lstProveedores ?? new List<ProveedoresEntity>()).Add(proveedoresEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedoresResponse.codigoError = "E";
                        proveedoresResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(proveedoresResponse.codigoError))
                    {
                        if (proveedoresResponse.lstProveedores.Count == 0)
                        {
                            proveedoresResponse.codigoError = "E";
                            proveedoresResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            proveedoresResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                proveedoresResponse.codigoError = "E";
                proveedoresResponse.descripcionError = "1." + ex.Message;
            }
            return proveedoresResponse;
        }

        public static Proveedores InsertarProveedores(ProveedoresEntity proveedor)
        {
            Proveedores proveedorResponse = new Proveedores();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO proveedores(sociedad_id,nombre,id_fiscal,correo,telefono,ciudadid,direccionprov,created_at)
                                       VALUES(?pSociedad,?pNombre,?pId_fiscal,?pCorreo,?pTelefono,?pCiudadid,?pDireccionprov,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pSociedad", proveedor.sociedad_id);
                                    myCmd.Parameters.AddWithValue("?pNombre", proveedor.nombre);
                                    myCmd.Parameters.AddWithValue("?pId_fiscal", proveedor.id_fiscal);
                                    myCmd.Parameters.AddWithValue("?pCorreo", proveedor.correo);
                                    myCmd.Parameters.AddWithValue("?pTelefono", proveedor.telefono);
                                    myCmd.Parameters.AddWithValue("?pCiudadid", proveedor.ciudadid);
                                    myCmd.Parameters.AddWithValue("?pDireccionprov", proveedor.direccionprov);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        proveedorResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        proveedorResponse.codigoError = "E";
                                        proveedorResponse.descripcionError = "3. No se pudo crear el proveedor.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    proveedorResponse.codigoError = "E";
                                    proveedorResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedorResponse.codigoError = "E";
                        proveedorResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                proveedorResponse.codigoError = "E";
                proveedorResponse.descripcionError = "1. " + ex.Message;
            }
            return proveedorResponse;
        }

        public static Proveedores ModificarProveedores(ProveedoresEntity proveedores)
        {
            Proveedores proveedorResponse = new Proveedores();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE proveedores 
                                      SET nombre=?pNombre,
                                          id_fiscal=?pId_fiscal,
                                          correo=?pCorreo,
                                          telefono=?pTelefono,
                                          ciudadid=?pCiudadid,
                                          direccionprov=?pDireccionprov,
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
                                    myCmd.Parameters.AddWithValue("?pNombre", proveedores.nombre);
                                    myCmd.Parameters.AddWithValue("?pId_fiscal", proveedores.id_fiscal);
                                    myCmd.Parameters.AddWithValue("?pCorreo", proveedores.correo);
                                    myCmd.Parameters.AddWithValue("?pTelefono", proveedores.telefono);
                                    myCmd.Parameters.AddWithValue("?pCiudadid", proveedores.ciudadid);
                                    myCmd.Parameters.AddWithValue("?pDireccionprov", proveedores.direccionprov);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", proveedores.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        proveedorResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        proveedorResponse.codigoError = "E";
                                        proveedorResponse.descripcionError = "3. No se pudo actualizar el proveedor.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    proveedorResponse.codigoError = "E";
                                    proveedorResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedorResponse.codigoError = "E";
                        proveedorResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                proveedorResponse.codigoError = "E";
                proveedorResponse.descripcionError = "1. " + ex.Message;
            }
            return proveedorResponse;
        }

        public static Proveedores EliminarProveedor(ProveedoresEntity proveedor)
        {
            Proveedores proveedorResponse = new Proveedores();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM proveedores WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", proveedor.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        proveedorResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        proveedorResponse.codigoError = "E";
                                        proveedorResponse.descripcionError = "3. No se pudo eliminar el proveedor.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    proveedorResponse.codigoError = "E";
                                    proveedorResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        proveedorResponse.codigoError = "E";
                        proveedorResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                proveedorResponse.codigoError = "E";
                proveedorResponse.descripcionError = "1. " + ex.Message;
            }
            return proveedorResponse;
        }
    }
}