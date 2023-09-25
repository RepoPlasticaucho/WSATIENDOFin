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
    public class DALModelos
    {

        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Modelos ObtenerModelos()
        {
            Modelos modelosResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT modelos.*,lineas.linea, marcas.marca
                                        FROM modelos
                                        INNER JOIN lineas ON modelos.linea_id=lineas.id
                                        INNER JOIN marcas ON modelos.marca_id=marcas.id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModelosEntity modeloEntity = new ModelosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    linea_id = dataReader["linea_id"].ToString(),
                                    linea_nombre = dataReader["linea"].ToString(),
                                    modelo = dataReader["modelo"].ToString(),
                                    marca_id = dataReader["marca_id"].ToString(),
                                    marca_nombre = dataReader["marca"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString()
                                };
                                (modelosResponse.lstModelos ?? new List<ModelosEntity>()).Add(modeloEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modelosResponse.codigoError = "E";
                        modelosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modelosResponse.codigoError))
                    {
                        if (modelosResponse.lstModelos.Count == 0)
                        {
                            modelosResponse.codigoError = "E";
                            modelosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modelosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modelosResponse.codigoError = "E";
                modelosResponse.descripcionError = "1." + ex.Message;
            }
            return modelosResponse;
        }

        public static Modelos ObtenerModelosLineas(string linea, string almacen)
        {
            Modelos modelosResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct modelo
                                        FROM modelos as Mo 
                                        INNER JOIN lineas as L on L.id = Mo.linea_id
										INNER JOIN modelo_productos as MP ON MP.modelo_id = Mo.id
										INNER JOIN productos as PR on PR.modelo_producto_id = MP.id
										INNER JOIN inventarios as I on I.producto_id = PR.id
                                        WHERE (linea like ?plineaid and I.almacen_id = ?palmacenid)
                                        order by modelo
                                           ;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?plineaid", linea);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModelosEntity modeloEntity = new ModelosEntity()
                                {
                                  
                                    modelo = dataReader["modelo"].ToString(),
                                   
                                };
                                (modelosResponse.lstModelos ?? new List<ModelosEntity>()).Add(modeloEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modelosResponse.codigoError = "E";
                        modelosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modelosResponse.codigoError))
                    {
                        if (modelosResponse.lstModelos.Count == 0)
                        {
                            modelosResponse.codigoError = "E";
                            modelosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modelosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modelosResponse.codigoError = "E";
                modelosResponse.descripcionError = "1." + ex.Message;
            }
            return modelosResponse;
        }

        public static Modelos ObtenerModelosLineasAdm(string linea)
        {
            Modelos modelosResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct modelo
                                        FROM modelos as Mo 
                                        INNER JOIN lineas as L on L.id = Mo.linea_id
										INNER JOIN modelo_productos as MP ON MP.modelo_id = Mo.id
										INNER JOIN productos as PR on PR.modelo_producto_id = MP.id
										INNER JOIN inventarios as I on I.producto_id = PR.id
                                        WHERE linea like ?plinea
                                        order by modelo
                                           ;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?plinea", linea);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModelosEntity modeloEntity = new ModelosEntity()
                                {

                                    modelo = dataReader["modelo"].ToString(),

                                };
                                (modelosResponse.lstModelos ?? new List<ModelosEntity>()).Add(modeloEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modelosResponse.codigoError = "E";
                        modelosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modelosResponse.codigoError))
                    {
                        if (modelosResponse.lstModelos.Count == 0)
                        {
                            modelosResponse.codigoError = "E";
                            modelosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modelosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modelosResponse.codigoError = "E";
                modelosResponse.descripcionError = "1." + ex.Message;
            }
            return modelosResponse;
        }

        public static Modelos ObtenerModelosLineasMarcas(string linea, string marca)
        {
            Modelos modelosResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct modelo
                                        FROM modelos as Mo
                                        INNER JOIN marcas as M on M.id = Mo.marca_id
                                        INNER JOIN lineas as L on L.id = Mo.linea_id
										INNER JOIN modelo_productos as MP ON MP.modelo_id = Mo.id
										INNER JOIN productos as PR on PR.modelo_producto_id = MP.id
										INNER JOIN inventarios as I on I.producto_id = PR.id
                                        WHERE linea like ?plinea AND marca like ?pmarca
                                        order by modelo
                                           ;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?plinea", linea);
                            myCmd.Parameters.AddWithValue("?pmarca", marca);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModelosEntity modeloEntity = new ModelosEntity()
                                {

                                    modelo = dataReader["modelo"].ToString(),

                                };
                                (modelosResponse.lstModelos ?? new List<ModelosEntity>()).Add(modeloEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modelosResponse.codigoError = "E";
                        modelosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modelosResponse.codigoError))
                    {
                        if (modelosResponse.lstModelos.Count == 0)
                        {
                            modelosResponse.codigoError = "E";
                            modelosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modelosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modelosResponse.codigoError = "E";
                modelosResponse.descripcionError = "1." + ex.Message;
            }
            return modelosResponse;
        }
        public static Modelos InsertarModelo(ModelosEntity modelo)
        {

            Modelos modeloResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO modelos(linea_id,modelo,marca_id,etiquetas,cod_sap,es_plasticaucho,es_sincronizado,created_at)
                                       VALUES(?pLineaId,?pModelo, ?pMarcaId,?pEtiqueta,?pCodSAP,?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pLineaId", modelo.linea_id);
                                    myCmd.Parameters.AddWithValue("?pModelo", modelo.modelo);
                                    myCmd.Parameters.AddWithValue("?pMarcaId", modelo.marca_id);
                                    myCmd.Parameters.AddWithValue("?pEtiqueta", modelo.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", modelo.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", "1");
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        modeloResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        modeloResponse.codigoError = "E";
                                        modeloResponse.descripcionError = "3. No se pudo crear el Modelo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    modeloResponse.codigoError = "E";
                                    modeloResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloResponse.codigoError = "E";
                        modeloResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                modeloResponse.codigoError = "E";
                modeloResponse.descripcionError = "1. " + ex.Message;
            }
            return modeloResponse;
        }

        public static Modelos ModificarModelo(ModelosEntity modelo)
        {
            Modelos modeloResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE modelos
                                      SET linea_id=?pLineaId,
                                          modelo=?pModelo,
                                          marca_id=?pMarcaId,
                                          etiquetas=?pEtiquetas,
                                          cod_sap=?pCodSAP,
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
                                    myCmd.Parameters.AddWithValue("?pLineaId", modelo.linea_id);
                                    myCmd.Parameters.AddWithValue("?pModelo", modelo.modelo);
                                    myCmd.Parameters.AddWithValue("?pMarcaId", modelo.marca_id);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", modelo.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", modelo.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", modelo.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        modeloResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        modeloResponse.codigoError = "E";
                                        modeloResponse.descripcionError = "3. No se pudo actualizar el Modelo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    modeloResponse.codigoError = "E";
                                    modeloResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloResponse.codigoError = "E";
                        modeloResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                modeloResponse.codigoError = "E";
                modeloResponse.descripcionError = "1. " + ex.Message;
            }
            return modeloResponse;
        }

        public static Modelos EliminarModelo(ModelosEntity modelo)
        {
            Modelos modeloResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM modelos WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", modelo.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        modeloResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        modeloResponse.codigoError = "E";
                                        modeloResponse.descripcionError = "3. No se pudo eliminar el Modelo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    modeloResponse.codigoError = "E";
                                    modeloResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloResponse.codigoError = "E";
                        modeloResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                modeloResponse.codigoError = "E";
                modeloResponse.descripcionError = "1. " + ex.Message;
            }
            return modeloResponse;
        }
        public static Modelos ObtenerCatalogoModelo(string linea_id, string modelo, string marca_id)
        {
            Modelos modelosResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT modelo,linea_id,Mo.id,M.id
                                        FROM modelos as Mo 
                                        INNER JOIN lineas as L on L.id = Mo.linea_id
                                        INNER JOIN marcas as M on M.id = Mo.marca_id
										WHERE (L.id = ?plinea and modelo = ?pmodelo and M.id = ?pmarca)
                                        order by modelo
                                           ;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?plinea", linea_id);
                            myCmd.Parameters.AddWithValue("?pmarca", marca_id);
                            myCmd.Parameters.AddWithValue("?pmodelo", modelo);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModelosEntity modeloEntity = new ModelosEntity()
                                {

                                    modelo = dataReader["modelo"].ToString(),
                                    id = dataReader["id"].ToString(),
                                    linea_id = dataReader["linea_id"].ToString()
                                };
                                (modelosResponse.lstModelos ?? new List<ModelosEntity>()).Add(modeloEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modelosResponse.codigoError = "E";
                        modelosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modelosResponse.codigoError))
                    {
                        if (modelosResponse.lstModelos.Count == 0)
                        {
                            modelosResponse.codigoError = "E";
                            modelosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modelosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modelosResponse.codigoError = "E";
                modelosResponse.descripcionError = "1." + ex.Message;
            }
            return modelosResponse;
        }

        public static Modelos ObtenerLineaModelo(string modelo)
        {
            Modelos modelosResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT Mo.linea_id, L.linea
                                        FROM modelos as Mo
                                        INNER JOIN lineas as L on L.id = Mo.linea_id
										WHERE modelo = ?pmodelo
                                           ;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmodelo", modelo);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModelosEntity modeloEntity = new ModelosEntity()
                                {
                                    linea_nombre = dataReader["linea"].ToString()
                                };
                                (modelosResponse.lstModelos ?? new List<ModelosEntity>()).Add(modeloEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modelosResponse.codigoError = "E";
                        modelosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modelosResponse.codigoError))
                    {
                        if (modelosResponse.lstModelos.Count == 0)
                        {
                            modelosResponse.codigoError = "E";
                            modelosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modelosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modelosResponse.codigoError = "E";
                modelosResponse.descripcionError = "1." + ex.Message;
            }
            return modelosResponse;
        }

        public static Modelos ObtenerModelosNombre(string modelo)
        {
            Modelos modelosResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM modelos
										WHERE (modelo = ?pmodelo)
                                        order by modelo
                                           ;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmodelo", modelo);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModelosEntity modeloEntity = new ModelosEntity()
                                {

                                    modelo = dataReader["modelo"].ToString(),
                                    id = dataReader["id"].ToString(),
                                    linea_id = dataReader["linea_id"].ToString()
                                };
                                (modelosResponse.lstModelos ?? new List<ModelosEntity>()).Add(modeloEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modelosResponse.codigoError = "E";
                        modelosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modelosResponse.codigoError))
                    {
                        if (modelosResponse.lstModelos.Count == 0)
                        {
                            modelosResponse.codigoError = "E";
                            modelosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modelosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modelosResponse.codigoError = "E";
                modelosResponse.descripcionError = "1." + ex.Message;
            }
            return modelosResponse;
        }
        public static Modelos ActualizarModelos(ModelosEntity modelo)
        {
            Modelos modeloResponse = new Modelos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE modelos
                                      SET 
                                          cod_sap=?pCodSAP,
                                          updated_at=?pUpdatedAt
                                      WHERE id=?pmodeloid";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pCodSAP", modelo.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pmodeloid", modelo.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        modeloResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        modeloResponse.codigoError = "E";
                                        modeloResponse.descripcionError = "3. No se pudo actualizar el Modelo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    modeloResponse.codigoError = "E";
                                    modeloResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloResponse.codigoError = "E";
                        modeloResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                modeloResponse.codigoError = "E";
                modeloResponse.descripcionError = "1. " + ex.Message;
            }
            return modeloResponse;
        }
    }
}