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
    public class DALAlmacenes
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Almacenes ObtenerAlmacenes()
        {
            Almacenes almacenesResponse = new Almacenes();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT S.id as idSociedad,S.nombre_comercial,A.id as idAlmacen,A.direccion,A.telefono,A.nombre_almacen,A.codigo,A.pto_emision
                                        FROM almacenes as A
                                        INNER JOIN sociedades as S ON S.id=A.sociedad_id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                AlmacenesEntity almacenesEntity = new AlmacenesEntity()
                                {
                                    idAlmacen = dataReader["idAlmacen"].ToString(),
                                    nombresociedad = dataReader["nombre_comercial"].ToString(),
                                    nombre_almacen = dataReader["nombre_almacen"].ToString(),
                                    sociedad_id = dataReader["idSociedad"].ToString(),
                                    direccion = dataReader["direccion"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString()
                                };
                                (almacenesResponse.lstAlmacenes ?? new List<AlmacenesEntity>()).Add(almacenesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        almacenesResponse.codigoError = "E";
                        almacenesResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(almacenesResponse.codigoError))
                    {
                        if (almacenesResponse.lstAlmacenes.Count == 0)
                        {
                            almacenesResponse.codigoError = "E";
                            almacenesResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            almacenesResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                almacenesResponse.codigoError = "E";
                almacenesResponse.descripcionError = "1." + ex.Message;
            }
            return almacenesResponse;
        }

        public static Almacenes ObtenerAlmacenesSociedad(string sociedad)
        {
            Almacenes almacenesResponse = new Almacenes();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM almacenes where sociedad_id= ?psociedadid";
                    ;
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?psociedadid", sociedad);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                AlmacenesEntity almacenesEntity = new AlmacenesEntity()
                                {
                                    idAlmacen = dataReader["id"].ToString(),
                                    sociedad_id = dataReader["sociedad_id"].ToString(),
                                    nombre_almacen = dataReader["nombre_almacen"].ToString(),
                                    direccion = dataReader["direccion"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString()

                                };
                                (almacenesResponse.lstAlmacenes ?? new List<AlmacenesEntity>()).Add(almacenesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        almacenesResponse.codigoError = "E";
                        almacenesResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(almacenesResponse.codigoError))
                    {
                        if (almacenesResponse.lstAlmacenes.Count == 0)
                        {
                            almacenesResponse.codigoError = "E";
                            almacenesResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            almacenesResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                almacenesResponse.codigoError = "E";
                almacenesResponse.descripcionError = "1." + ex.Message;
            }
            return almacenesResponse;
        }

        public static Almacenes ObtenerAlmacenesEsp(string sociedad, string almacen)
        {
            Almacenes almacenesResponse = new Almacenes();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM almacenes where sociedad_id = ?psociedadid and id != ?pAlmacen_id";
                    ;
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?psociedadid", sociedad);
                            myCmd.Parameters.AddWithValue("?pAlmacen_id", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                AlmacenesEntity almacenesEntity = new AlmacenesEntity()
                                {
                                    idAlmacen = dataReader["id"].ToString(),
                                    sociedad_id = dataReader["sociedad_id"].ToString(),
                                    nombre_almacen = dataReader["nombre_almacen"].ToString(),
                                    direccion = dataReader["direccion"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString()

                                };
                                (almacenesResponse.lstAlmacenes ?? new List<AlmacenesEntity>()).Add(almacenesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        almacenesResponse.codigoError = "E";
                        almacenesResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(almacenesResponse.codigoError))
                    {
                        if (almacenesResponse.lstAlmacenes.Count == 0)
                        {
                            almacenesResponse.codigoError = "E";
                            almacenesResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            almacenesResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                almacenesResponse.codigoError = "E";
                almacenesResponse.descripcionError = "1." + ex.Message;
            }
            return almacenesResponse;
        }

        public static Almacenes ObtenerAlmacenID(string id)
        {
            Almacenes almacenesResponse = new Almacenes();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT A.*, S.nombre_comercial, S.id_fiscal
                                        FROM almacenes as A
                                        INNER JOIN sociedades as S on S.id = A.sociedad_id
                                        where A.id= ?pID";
                    ;
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pID", id);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                AlmacenesEntity almacenesEntity = new AlmacenesEntity()
                                {
                                    idAlmacen = dataReader["id"].ToString(),
                                    sociedad_id = dataReader["sociedad_id"].ToString(),
                                    nombresociedad = dataReader["nombre_comercial"].ToString(),
                                    nombre_almacen = dataReader["nombre_almacen"].ToString(),
                                    direccion = dataReader["direccion"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    idfiscal_sociedad = dataReader["id_fiscal"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString()

                                };
                                (almacenesResponse.lstAlmacenes ?? new List<AlmacenesEntity>()).Add(almacenesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        almacenesResponse.codigoError = "E";
                        almacenesResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(almacenesResponse.codigoError))
                    {
                        if (almacenesResponse.lstAlmacenes.Count == 0)
                        {
                            almacenesResponse.codigoError = "E";
                            almacenesResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            almacenesResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                almacenesResponse.codigoError = "E";
                almacenesResponse.descripcionError = "1." + ex.Message;
            }
            return almacenesResponse;
        }

        public static Almacenes InsertarAlmacen(AlmacenesEntity almacen)
        {
            Almacenes almacenesResponse = new Almacenes();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO almacenes(sociedad_id,nombre_almacen,direccion,telefono,codigo,pto_emision,created_at)
                                       VALUES(?pSociedad_id,?pNombre_almacen,?pDireccion,?pTelefono,?pCodigo,?pPto_Emision,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pSociedad_id", almacen.sociedad_id);
                                    myCmd.Parameters.AddWithValue("?pDireccion", almacen.direccion);
                                    myCmd.Parameters.AddWithValue("?pNombre_almacen", almacen.nombre_almacen);
                                    myCmd.Parameters.AddWithValue("?pTelefono", almacen.telefono);
                                    myCmd.Parameters.AddWithValue("?pCodigo", almacen.codigo);
                                    myCmd.Parameters.AddWithValue("?pPto_Emision", almacen.pto_emision);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        almacenesResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        almacenesResponse.codigoError = "E";
                                        almacenesResponse.descripcionError = "3. No se pudo crear el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    almacenesResponse.codigoError = "E";
                                    almacenesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        almacenesResponse.codigoError = "E";
                        almacenesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                almacenesResponse.codigoError = "E";
                almacenesResponse.descripcionError = "1. " + ex.Message;
            }
            return almacenesResponse;
        }

        //comentari
        public static Almacenes ModificarAlmacen(AlmacenesEntity almacen)
        {
            Almacenes almacenesResponse = new Almacenes();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE almacenes 
                                      SET sociedad_id=?pSociedad_id,
                                          nombre_almacen=?pNombre_almacen,
                                          direccion=?pDireccion,
                                          telefono=?pTelefono,
                                          codigo=?pCodigo,
                                          pto_emision=?pPto_emision,
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
                                    myCmd.Parameters.AddWithValue("?pSociedad_id", almacen.sociedad_id);
                                    myCmd.Parameters.AddWithValue("?pDireccion", almacen.direccion);
                                    myCmd.Parameters.AddWithValue("?pNombre_almacen", almacen.nombre_almacen);
                                    myCmd.Parameters.AddWithValue("?pTelefono", almacen.telefono);
                                    myCmd.Parameters.AddWithValue("?pCodigo", almacen.codigo);
                                    myCmd.Parameters.AddWithValue("?pPto_emision", almacen.pto_emision);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", almacen.idAlmacen);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        almacenesResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        almacenesResponse.codigoError = "E";
                                        almacenesResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    almacenesResponse.codigoError = "E";
                                    almacenesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        almacenesResponse.codigoError = "E";
                        almacenesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                almacenesResponse.codigoError = "E";
                almacenesResponse.descripcionError = "1. " + ex.Message;
            }
            return almacenesResponse;
        }

        public static Almacenes EliminarAlmacen(AlmacenesEntity almacen)
        {
            Almacenes almacenesResponse = new Almacenes();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM almacenes WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", almacen.idAlmacen);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        almacenesResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        almacenesResponse.codigoError = "E";
                                        almacenesResponse.descripcionError = "3. No se pudo eliminar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    almacenesResponse.codigoError = "E";
                                    almacenesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        almacenesResponse.codigoError = "E";
                        almacenesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                almacenesResponse.codigoError = "E";
                almacenesResponse.descripcionError = "1. " + ex.Message;
            }
            return almacenesResponse;
        }

        public static Almacenes ObtenerAlmacenesS(string sociedad)
        {
            Almacenes almacenesResponse = new Almacenes();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM almacenes
										INNER JOIN sociedades as S ON S.id = sociedad_id	
                                        WHERE S.nombre_comercial= ?pNombre_comercial";
                    ;
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pNombre_comercial", sociedad);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                AlmacenesEntity almacenesEntity = new AlmacenesEntity()
                                {
                                    idAlmacen = dataReader["id"].ToString(),
                                    nombre_almacen = dataReader["nombre_almacen"].ToString(),
                                    sociedad_id = dataReader["sociedad_id"].ToString(),
                                    direccion = dataReader["direccion"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString()

                                };
                                (almacenesResponse.lstAlmacenes ?? new List<AlmacenesEntity>()).Add(almacenesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        almacenesResponse.codigoError = "E";
                        almacenesResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(almacenesResponse.codigoError))
                    {
                        if (almacenesResponse.lstAlmacenes.Count == 0)
                        {
                            almacenesResponse.codigoError = "E";
                            almacenesResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            almacenesResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                almacenesResponse.codigoError = "E";
                almacenesResponse.descripcionError = "1." + ex.Message;
            }
            return almacenesResponse;
        }

        public static Almacenes ObtenerAlmacenN(string almacen)
        {
            Almacenes almacenes_response = new Almacenes();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM almacenes
                                        WHERE nombre_almacen LIKE ?tNombre_almacen
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?tNombre_almacen", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                AlmacenesEntity almacenEntity = new AlmacenesEntity()
                                {
                                    idAlmacen = dataReader["id"].ToString(),
                                    nombre_almacen = dataReader["nombre_almacen"].ToString(),
                                    sociedad_id = dataReader["sociedad_id"].ToString(),
                                    direccion = dataReader["direccion"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString()
                                };
                                (almacenes_response.lstAlmacenes ?? new List<AlmacenesEntity>()).Add(almacenEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        almacenes_response.codigoError = "E";
                        almacenes_response.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(almacenes_response.codigoError))
                    {
                        if (almacenes_response.lstAlmacenes.Count == 0)
                        {
                            almacenes_response.codigoError = "E";
                            almacenes_response.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            almacenes_response.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                almacenes_response.codigoError = "E";
                almacenes_response.descripcionError = "1." + ex.Message;
            }
            return almacenes_response;
        }
    }
}