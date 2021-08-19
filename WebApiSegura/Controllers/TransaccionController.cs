using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    
    [RoutePrefix("api/transaccion")]
    public class TransaccionController : ApiController
    {
        
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            List<Transaccion> transacciones = new List<Transaccion>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Transaccion.Codigo, ClienteId, RutaId, TarjetaClienteId, Fecha, Monto, Estado, Ruta.Descripcion
                                                                FROM    Transaccion                                                                
                                                                JOIN    Ruta ON Transaccion.RutaId = Ruta.Codigo
                                                                WHERE   ClienteId = @ClienteId", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ClienteId", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Transaccion transaccion = new Transaccion();
                        transaccion.Codigo = sqlDataReader.GetInt32(0);
                        transaccion.ClienteId = sqlDataReader.GetInt32(1);
                        transaccion.RutaId = sqlDataReader.GetInt32(2);
                        transaccion.TarjetaClienteId = sqlDataReader.GetInt32(3);
                        transaccion.Fecha = sqlDataReader.GetDateTime(4);
                        transaccion.Monto = sqlDataReader.GetDecimal(5);
                        transaccion.Estado = sqlDataReader.GetString(6);
                        transaccion.Descripcion = sqlDataReader.GetString(7);

                        transacciones.Add(transaccion);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(transacciones);
        }

        [HttpGet]
        public IHttpActionResult GetAll(int clienteId)
        {
            List<Transaccion> transacciones = new List<Transaccion>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, ClienteId, RutaId, TarjetaClienteId, Fecha, Monto, Estado
                                                                FROM    Transaccion
                                                                WHERE   ClienteId = @ClienteId", sqlConnection);
                    
                    sqlCommand.Parameters.AddWithValue("@ClienteId", clienteId);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Transaccion transaccion = new Transaccion();

                        transaccion.Codigo = sqlDataReader.GetInt32(0);
                        transaccion.ClienteId = sqlDataReader.GetInt32(1);
                        transaccion.RutaId = sqlDataReader.GetInt32(2);
                        transaccion.TarjetaClienteId = sqlDataReader.GetInt32(3);
                        transaccion.Fecha = sqlDataReader.GetDateTime(4);
                        transaccion.Monto = sqlDataReader.GetDecimal(5);
                        transaccion.Estado = sqlDataReader.GetString(6);
                        
                        transacciones.Add(transaccion);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(transacciones);
        }


        [HttpPost]
        public IHttpActionResult Ingresar(Transaccion transaccion)
        {
            if (transaccion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"INSERT INTO Transaccion (ClienteId, RutaId, TarjetaClienteId, Fecha, Monto, Estado) 
                                         VALUES (@ClienteId, @RutaId, @TarjetaClienteId, @Fecha, @Monto, @Estado)",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@ClienteId", transaccion.ClienteId);
                    sqlCommand.Parameters.AddWithValue("@RutaId", transaccion.RutaId);
                    sqlCommand.Parameters.AddWithValue("@TarjetaClienteId", transaccion.TarjetaClienteId);
                    sqlCommand.Parameters.AddWithValue("@Fecha", transaccion.Fecha);
                    sqlCommand.Parameters.AddWithValue("@Monto", transaccion.Monto);
                    sqlCommand.Parameters.AddWithValue("@Estado", transaccion.Estado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(transaccion);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Transaccion transaccion)
        {
            if (transaccion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"UPDATE Transaccion 
                                                        SET ClienteId = @ClienteId, RutaId = @RutaId, TarjetaClienteId = @TarjetaClienteId, Fecha = @Fecha, Monto = @Monto, Estado = @Estado
                                          WHERE Codigo = @Codigo",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", transaccion.Codigo);
                    sqlCommand.Parameters.AddWithValue("@ClienteId", transaccion.ClienteId);
                    sqlCommand.Parameters.AddWithValue("@RutaId", transaccion.RutaId);
                    sqlCommand.Parameters.AddWithValue("@TarjetaClienteId", transaccion.TarjetaClienteId);
                    sqlCommand.Parameters.AddWithValue("@Fecha", transaccion.Fecha);
                    sqlCommand.Parameters.AddWithValue("@Monto", transaccion.Monto);
                    sqlCommand.Parameters.AddWithValue("@Estado", transaccion.Estado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(transaccion);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"DELETE Transaccion WHERE Codigo = @Codigo",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(id);
        }
    }
}
