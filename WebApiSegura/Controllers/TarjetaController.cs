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
    [Authorize]
    [RoutePrefix("api/tarjeta")]
    public class TarjetaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            List<Tarjeta> cuentas = new List<Tarjeta>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Numero, CCV, MesExpiracion, AnioExpiracion, Nombre, 
                    Predeterminado, CodigoCliente FROM Tarjeta WHERE CodigoCliente = @CodigoCliente", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoCliente", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Tarjeta tarjeta = new Tarjeta();
                        tarjeta.Codigo = sqlDataReader.GetInt32(0);
                        tarjeta.Numero = sqlDataReader.GetString(1);
                        tarjeta.CCV = sqlDataReader.GetString(2);
                        tarjeta.MesExpiracion = sqlDataReader.GetInt32(3);
                        tarjeta.AnioExpiracion = sqlDataReader.GetInt32(4);
                        tarjeta.Nombre = sqlDataReader.GetString(5);
                        tarjeta.Predeterminado = sqlDataReader.GetString(6);

                        cuentas.Add(tarjeta);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(cuentas);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Tarjeta> cuentas = new List<Tarjeta>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Numero, CCV, MesExpiracion, AnioExpiracion, Nombre, 
                    Predeterminado, CodigoCliente FROM Tarjeta", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Tarjeta tarjeta = new Tarjeta();
                        tarjeta.Codigo = sqlDataReader.GetInt32(0);
                        tarjeta.Numero = sqlDataReader.GetString(1);
                        tarjeta.CCV = sqlDataReader.GetString(2);
                        tarjeta.MesExpiracion = sqlDataReader.GetInt32(3);
                        tarjeta.AnioExpiracion = sqlDataReader.GetInt32(4);
                        tarjeta.Nombre = sqlDataReader.GetString(5);
                        tarjeta.Predeterminado = sqlDataReader.GetString(6);

                        cuentas.Add(tarjeta);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(cuentas);
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
                        new SqlCommand(@"INSERT INTO Tarjeta (Numero, CCV, MesExpiracion, AnioExpiracion, Nombre, Predeterminado, CodigoCliente) 
                                         VALUES (@Numero, @CCV, @MesExpiracion, @AnioExpiracion, @Nombre, @Predeterminado, @CodigoCliente)",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Numero", tarjeta.Numero);
                    sqlCommand.Parameters.AddWithValue("@CCV", tarjeta.CCV);
                    sqlCommand.Parameters.AddWithValue("@MesExpiracion", tarjeta.MesExpiracion);
                    sqlCommand.Parameters.AddWithValue("@AnioExpiracion", tarjeta.AnioExpiracion);
                    sqlCommand.Parameters.AddWithValue("@Nombre", tarjeta.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Predeterminado", tarjeta.Predeterminado);
                    sqlCommand.Parameters.AddWithValue("@CodigoCliente", tarjeta.CodigoCliente);


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
                        new SqlCommand(@"UPDATE Tarjeta 
                                                        SET Numero = @Numero, 
                                                            CCV = @CCV,
                                                            MesExpiracion = @MesExpiracion,
                                                            AnioExpiracion = @AnioExpiracion,
                                                            Nombre = @Nombre,
                                                            Predeterminado = @Predeterminado,
                                                            CodigoCliente = @CodigoCliente
                                          WHERE Codigo = @Codigo",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Numero", tarjeta.Numero);
                    sqlCommand.Parameters.AddWithValue("@CCV", tarjeta.CCV);
                    sqlCommand.Parameters.AddWithValue("@MesExpiracion", tarjeta.MesExpiracion);
                    sqlCommand.Parameters.AddWithValue("@AnioExpiracion", tarjeta.AnioExpiracion);
                    sqlCommand.Parameters.AddWithValue("@Nombre", tarjeta.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Predeterminado", tarjeta.Predeterminado);
                    sqlCommand.Parameters.AddWithValue("@CodigoCliente", tarjeta.CodigoCliente);
                    sqlCommand.Parameters.AddWithValue("@Codigo", tarjeta.Codigo);

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
