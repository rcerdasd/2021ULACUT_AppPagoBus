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
    [RoutePrefix("api/tarjeta")]
    public class TarjetaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Tarjeta tarjeta = new Tarjeta();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Numero, CCV, FechaExpiracion, Nombre, Predeterminado
                                                             FROM   Tarjeta
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        tarjeta.Codigo = sqlDataReader.GetInt32(0);
                        tarjeta.Numero = sqlDataReader.GetDecimal(1);
                        tarjeta.CCV = sqlDataReader.GetDecimal(2);
                        tarjeta.FechaExpiracion = sqlDataReader.GetDateTime(3);
                        tarjeta.Nombre = sqlDataReader.GetString(4);
                        tarjeta.Predeterminado = sqlDataReader.GetString(5);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tarjeta);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Tarjeta> tarjetas = new List<Tarjeta>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Numero, CCV, FechaExpiracion, Nombre, Predeterminado FROM Tarjeta", sqlConnection);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Tarjeta tarjeta = new Tarjeta();
                        tarjeta.Codigo = sqlDataReader.GetInt32(0);
                        tarjeta.Numero = sqlDataReader.GetDecimal(1);
                        tarjeta.CCV = sqlDataReader.GetDecimal(2);
                        tarjeta.FechaExpiracion = sqlDataReader.GetDateTime(3);
                        tarjeta.Nombre = sqlDataReader.GetString(4);
                        tarjeta.Predeterminado = sqlDataReader.GetString(5);

                        tarjetas.Add(tarjeta);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(tarjetas);
        }


        [HttpPost]
        public IHttpActionResult Ingresar(Tarjeta tarjeta)
        {
            if (tarjeta == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"INSERT INTO Tarjeta (Numero, CCV, FechaExpiracion, Nombre, Predeterminado) 
                                         VALUES (@Numero, @CCV, @FechaExpiracion, @Nombre, @Predeterminado)",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Numero", tarjeta.Numero);
                    sqlCommand.Parameters.AddWithValue("@CCV", tarjeta.CCV);
                    sqlCommand.Parameters.AddWithValue("@FechaExpiracion", tarjeta.FechaExpiracion);
                    sqlCommand.Parameters.AddWithValue("@Nombre", tarjeta.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Predeterminado", tarjeta.Predeterminado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tarjeta);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Tarjeta tarjeta)
        {
            if (tarjeta == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@" UPDATE Tarjeta 
                                                        SET Numero = @Numero, 
                                                            CCV = @CCV,
                                                            FechaExpiracion = @FechaExpiracion,
                                                            Nombre = @Nombre,
                                                            Predeterminado = @Predeterminado,
                                          WHERE Codigo = @Codigo",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Numero", tarjeta.Numero);
                    sqlCommand.Parameters.AddWithValue("@CCV", tarjeta.CCV);
                    sqlCommand.Parameters.AddWithValue("@FechaExpiracion", tarjeta.FechaExpiracion);
                    sqlCommand.Parameters.AddWithValue("@Nombre", tarjeta.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Predeterminado", tarjeta.Predeterminado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tarjeta);
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
                        new SqlCommand(@"DELETE Tarjeta WHERE Codigo = @Codigo",
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
