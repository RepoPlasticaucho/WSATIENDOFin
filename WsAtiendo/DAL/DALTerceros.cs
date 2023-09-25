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
    public class DALTerceros
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;AllowZeroDateTime=True;";
        
        public static Terceros ObtenerTercerosAll()
        {
            Terceros tercerosResponse = new Terceros();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select S.nombre_comercial,Al.nombre_almacen,TU.usuario,TR.sociedad_id,TR.tipo_tercero,
		                                TT.descripcion, TR.nombre, TR.id_fiscal,TR.correo, TR.almacen_id,TR.tipo_usuario_id,
                                        TR.telefono, TR.direccioncl, TR.fecha_nac,TR.almacen_id,TR.id,C.ciudad,P.provincia
		                                from terceros as TR
		                                INNER
                                              JOIN sociedades AS S ON S.id = TR.sociedad_id
		                                      JOIN almacenes AS Al ON Al.id = TR.almacen_id
		                                      JOIN tipo_tercero AS TT ON TT.id = TR.tipo_tercero
		                                      JOIN tipo_usuario AS TU ON TU.id = TR.tipo_usuario_id
		                                      JOIN ciudad AS C ON C.id = TR.ciudadid
                                              JOIN provincia  AS P ON P.id = C.provinciaid";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                TercerosEntity tercerosEntity = new TercerosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    sociedad_id = dataReader["sociedad_id"].ToString(),
                                    nombresociedad = dataReader["nombre_comercial"].ToString(),
                                    almacen_id = dataReader["almacen_id"].ToString(),
                                    nombrealmacen = dataReader["nombre_almacen"].ToString(),
                                    tipousuario_id = dataReader["tipo_usuario_id"].ToString(),
                                    tipousuario = dataReader["usuario"].ToString(),
                                    tipotercero_id = dataReader["tipo_tercero"].ToString(),
                                    nombretercero = dataReader["descripcion"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    correo = dataReader["correo"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    direccion = dataReader["direccioncl"].ToString(),
                                    fecha_nac = dataReader["fecha_nac"].ToString(),
                                    provincia = dataReader["provincia"].ToString(),
                                    ciudad = dataReader ["ciudad"].ToString()
                                };
                                (tercerosResponse.lstTerceros ?? new List<TercerosEntity>()).Add(tercerosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tercerosResponse.codigoError = "E";
                        tercerosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tercerosResponse.codigoError))
                    {
                        if (tercerosResponse.lstTerceros.Count == 0)
                        {
                            tercerosResponse.codigoError = "E";
                            tercerosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tercerosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tercerosResponse.codigoError = "E";
                tercerosResponse.descripcionError = "1." + ex.Message;
            }
            return tercerosResponse;
        }

        public static Terceros ObtenerTerceros(string sociedad, string almacen)
        {
            Terceros tercerosResponse = new Terceros();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select S.nombre_comercial,Al.nombre_almacen,TU.usuario,TR.sociedad_id,TR.tipo_tercero,
		                                TT.descripcion, TR.nombre, TR.id_fiscal,TR.correo, TR.almacen_id,TR.tipo_usuario_id,
                                        TR.telefono, TR.direccioncl, TR.fecha_nac,TR.almacen_id,TR.id,C.ciudad,P.provincia
		                                from terceros as TR
		                                INNER
                                              JOIN sociedades AS S ON S.id = TR.sociedad_id
		                                      JOIN almacenes AS Al ON Al.id = TR.almacen_id
		                                      JOIN tipo_tercero AS TT ON TT.id = TR.tipo_tercero
		                                      JOIN tipo_usuario AS TU ON TU.id = TR.tipo_usuario_id
		                                      JOIN ciudad AS C ON C.id = TR.ciudadid
                                              JOIN provincia  AS P ON P.id = C.provinciaid
                                         where ( S.id = ?psociedadid AND Al.id = ?palmacenid)
                                     ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?psociedadid", sociedad);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                TercerosEntity tercerosEntity = new TercerosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    sociedad_id = dataReader["sociedad_id"].ToString(),
                                    nombresociedad = dataReader["nombre_comercial"].ToString(),
                                    almacen_id = dataReader["almacen_id"].ToString(),
                                    nombrealmacen = dataReader["nombre_almacen"].ToString(),
                                    tipousuario_id = dataReader["tipo_usuario_id"].ToString(),
                                    tipousuario = dataReader["usuario"].ToString(),
                                    tipotercero_id = dataReader["tipo_tercero"].ToString(),
                                    nombretercero = dataReader["descripcion"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    correo = dataReader["correo"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    direccion = dataReader["direccioncl"].ToString(),
                                    fecha_nac = dataReader["fecha_nac"].ToString(),
                                    provincia = dataReader["provincia"].ToString(),
                                    ciudad = dataReader["ciudad"].ToString()

                                };
                                (tercerosResponse.lstTerceros ?? new List<TercerosEntity>()).Add(tercerosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tercerosResponse.codigoError = "E";
                        tercerosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tercerosResponse.codigoError))
                    {
                        if (tercerosResponse.lstTerceros.Count == 0)
                        {
                            tercerosResponse.codigoError = "E";
                            tercerosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tercerosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tercerosResponse.codigoError = "E";
                tercerosResponse.descripcionError = "1." + ex.Message;
            }
            return tercerosResponse;
        }

        public static Terceros ObtenerTerceroCedula(string cedula)
        {
            Terceros tercerosResponse = new Terceros();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select *
		                                from terceros as T
                                        INNER JOIN ciudad as C on C.id = T.ciudadid
                                        INNER JOIN sociedades as S on S.id = T.sociedad_id
                                        where T.id_fiscal=?pCedula
                                     ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pCedula", cedula);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                TercerosEntity tercerosEntity = new TercerosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    almacen_id = dataReader["almacen_id"].ToString(),
                                    ciudadid = dataReader["ciudadid"].ToString(),
                                    ciudad = dataReader["ciudad"].ToString(),
                                    correo = dataReader["correo"].ToString(),
                                    direccion = dataReader["direccioncl"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    telefono = dataReader["telefono"].ToString()
                                };
                                (tercerosResponse.lstTerceros ?? new List<TercerosEntity>()).Add(tercerosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tercerosResponse.codigoError = "E";
                        tercerosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(tercerosResponse.codigoError))
                    {
                        if (tercerosResponse.lstTerceros.Count == 0)
                        {
                            tercerosResponse.codigoError = "NEXISTE";
                            tercerosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            tercerosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                tercerosResponse.codigoError = "E";
                tercerosResponse.descripcionError = "1." + ex.Message;
            }
            return tercerosResponse;
        }

        public static Terceros InsertarTercero(TercerosEntity tercero)
        {
            Terceros tercerosResponse = new Terceros();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO terceros(sociedad_id,almacen_id,tipo_tercero,nombre,id_fiscal, correo,
                                                           telefono, ciudadid, direccioncl, fecha_nac, tipo_usuario_id, created_at)
                                       VALUES(?pSociedad_id,?pAlmacen_id,?pTipo_tercero,?pNombre,?pId_fiscal,?pCorreo,
                                              ?pTelefono,?pCiudadid,?pDireccioncl,?pFecha_nac,?pTipo_usuario_id,?pCreated_At)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pSociedad_id", tercero.sociedad_id);
                                    myCmd.Parameters.AddWithValue("?pAlmacen_id", tercero.almacen_id);
                                    myCmd.Parameters.AddWithValue("?pTipo_tercero", tercero.tipotercero_id);
                                    myCmd.Parameters.AddWithValue("?pNombre", tercero.nombre);
                                    myCmd.Parameters.AddWithValue("?pId_fiscal", tercero.id_fiscal);
                                    myCmd.Parameters.AddWithValue("?pCorreo", tercero.correo);
                                    myCmd.Parameters.AddWithValue("?pTelefono", tercero.telefono);
                                    myCmd.Parameters.AddWithValue("?pCiudadid", tercero.ciudadid);
                                    myCmd.Parameters.AddWithValue("?pDireccioncl", tercero.direccion);
                                    myCmd.Parameters.AddWithValue("?pFecha_nac", System.DateTime.Parse(tercero.fecha_nac));
                                    myCmd.Parameters.AddWithValue("?pTipo_usuario_id", tercero.tipousuario_id);
                                    myCmd.Parameters.AddWithValue("?pCreated_At", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        tercerosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        tercerosResponse.codigoError = "E";
                                        tercerosResponse.descripcionError = "3. No se pudo crear el Usuario.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    tercerosResponse.codigoError = "E";
                                    tercerosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tercerosResponse.codigoError = "E";
                        tercerosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                tercerosResponse.codigoError = "E";
                tercerosResponse.descripcionError = "1. " + ex.Message;
            }
            return tercerosResponse;
        }

        public static Terceros ModificarTerceros(TercerosEntity terceros)
        {
            Terceros tercerosResponse = new Terceros();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE terceros 
                                      SET sociedad_id=?pSociedad_id,
                                            almacen_id=?pAlmacen_id,
                                            tipo_tercero=?pTipo_tercero,
                                            nombre=?pNombre,
                                            id_fiscal=?pId_fiscal,
                                            correo=?pCorreo,
                                            telefono=?pTelefono,
                                            ciudadid=?pCiudadid,
                                            direccioncl=?pDireccioncl,
                                            fecha_nac=?pFecha_nac,
                                            tipo_usuario_id=?pTipo_usuario_id,
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
                                    myCmd.Parameters.AddWithValue("?pSociedad_id", terceros.sociedad_id);
                                    myCmd.Parameters.AddWithValue("?pAlmacen_id", terceros.almacen_id);
                                    myCmd.Parameters.AddWithValue("?pTipo_tercero", terceros.tipotercero_id);
                                    myCmd.Parameters.AddWithValue("?pNombre", terceros.nombre);
                                    myCmd.Parameters.AddWithValue("?pId_fiscal", terceros.id_fiscal);
                                    myCmd.Parameters.AddWithValue("?pCorreo", terceros.correo);
                                    myCmd.Parameters.AddWithValue("?pTelefono", terceros.telefono);
                                    myCmd.Parameters.AddWithValue("?pCiudadid", terceros.ciudadid);
                                    myCmd.Parameters.AddWithValue("?pDireccioncl", terceros.direccion);
                                    myCmd.Parameters.AddWithValue("?pFecha_nac", terceros.fecha_nac);
                                    myCmd.Parameters.AddWithValue("?pTipo_usuario_id", terceros.tipousuario_id);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", terceros.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        tercerosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        tercerosResponse.codigoError = "E";
                                        tercerosResponse.descripcionError = "3. No se pudo actualizar el Tercero.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    tercerosResponse.codigoError = "E";
                                    tercerosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tercerosResponse.codigoError = "E";
                        tercerosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                tercerosResponse.codigoError = "E";
                tercerosResponse.descripcionError = "1. " + ex.Message;
            }
            return tercerosResponse;
        }

        public static Terceros ModificarCliente(TercerosEntity terceros)
        {
            Terceros tercerosResponse = new Terceros();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE terceros 
                                      SET nombre=?pNombre,
                                            correo=?pCorreo,
                                            telefono=?pTelefono,
                                            ciudadid=?pCiudadid,
                                            direccioncl=?pDireccioncl,
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
                                    myCmd.Parameters.AddWithValue("?pNombre", terceros.nombre);
                                    myCmd.Parameters.AddWithValue("?pCorreo", terceros.correo);
                                    myCmd.Parameters.AddWithValue("?pTelefono", terceros.telefono);
                                    myCmd.Parameters.AddWithValue("?pCiudadid", terceros.ciudadid);
                                    myCmd.Parameters.AddWithValue("?pDireccioncl", terceros.direccion);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", terceros.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        tercerosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        tercerosResponse.codigoError = "E";
                                        tercerosResponse.descripcionError = "3. No se pudo actualizar el Tercero.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    tercerosResponse.codigoError = "E";
                                    tercerosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tercerosResponse.codigoError = "E";
                        tercerosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                tercerosResponse.codigoError = "E";
                tercerosResponse.descripcionError = "1. " + ex.Message;
            }
            return tercerosResponse;
        }

        public static Terceros EliminarTerceros(TercerosEntity terceros)
        {
            Terceros tercerosResponse = new Terceros();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM terceros WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", terceros.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        tercerosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        tercerosResponse.codigoError = "E";
                                        tercerosResponse.descripcionError = "3. No se pudo eliminar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    tercerosResponse.codigoError = "E";
                                    tercerosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tercerosResponse.codigoError = "E";
                        tercerosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                tercerosResponse.codigoError = "E";
                tercerosResponse.descripcionError = "1. " + ex.Message;
            }
            return tercerosResponse;
        }
    }
    
}