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
    [RoutePrefix("api/ruta")]
    public class RutaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Ruta ruta = new Ruta();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Costo, Descripcion
                                                             FROM   Ruta
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        ruta.Codigo = sqlDataReader.GetInt32(0);
                        ruta.Costo = sqlDataReader.GetInt32(1);
                        ruta.Descripcion = sqlDataReader.GetString(2);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(ruta);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Ruta> cuentas = new List<Ruta>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Costo, Descripcion FROM Ruta", sqlConnection);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Ruta ruta = new Ruta();
                        ruta.Codigo = sqlDataReader.GetInt32(0);
                        ruta.Costo = sqlDataReader.GetInt32(1);
                        ruta.Descripcion = sqlDataReader.GetString(2);

                        cuentas.Add(ruta);
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
        public IHttpActionResult Ingresar(Ruta ruta)
        {
            if (ruta == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"INSERT INTO Ruta (Costo, Descripcion) 
                                         VALUES (@Costo, @Descripcion)",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Costo", ruta.Costo);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", ruta.Descripcion);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(ruta);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Ruta ruta)
        {
            if (ruta == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@" UPDATE Ruta 
                                                        SET Costo = @Costo, 
                                                            Descripcion = @Descripcion
                                          WHERE Codigo = @Codigo",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", ruta.Codigo);
                    sqlCommand.Parameters.AddWithValue("@Costo", ruta.Costo);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", ruta.Descripcion);
 
                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(ruta);
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
                        new SqlCommand(@"DELETE Ruta WHERE Codigo = @Codigo",
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
