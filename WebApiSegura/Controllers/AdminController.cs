using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Persona persona = new Persona();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Nombre, Apellido, Identificacion, 
                    FechaNacimiento, Usuario, Contrasena, Email, Tipo FROM Persona Where Tipo = 1 AND Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        persona.Codigo = sqlDataReader.GetInt32(0);
                        persona.Nombre = sqlDataReader.GetString(1);
                        persona.Apellido = sqlDataReader.GetString(2);
                        persona.Identificacion = sqlDataReader.GetString(3);
                        persona.FechaNacimiento = sqlDataReader.GetDateTime(3);
                        persona.Usuario = sqlDataReader.GetString(3);
                        persona.Contrasena = sqlDataReader.GetString(3);
                        persona.Email = sqlDataReader.GetString(3);
                        persona.Tipo = sqlDataReader.GetString(3);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(persona);
        }


        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Persona> admin = new List<Persona>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Nombre, Apellido, Identificacion, 
                    FechaNacimiento, Usuario, Contrasena, Email, Tipo FROM Persona Where Tipo = '1'", sqlConnection);
                    //sqlCommand.Parameters.AddWithValue("@Tipo", "1");
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Persona persona = new Persona();
                        persona.Codigo = sqlDataReader.GetInt32(0);
                        persona.Nombre = sqlDataReader.GetString(1);
                        persona.Apellido = sqlDataReader.GetString(2);
                        persona.Identificacion = sqlDataReader.GetString(3);
                        persona.FechaNacimiento = sqlDataReader.GetDateTime(4);
                        persona.Usuario = sqlDataReader.GetString(5);
                        persona.Contrasena = sqlDataReader.GetString(6);
                        persona.Email = sqlDataReader.GetString(7);
                        persona.Tipo = sqlDataReader.GetString(8);

                        admin.Add(persona);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(admin);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Persona persona)
        {
            if (persona == null)
                return BadRequest();
            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"INSERT INTO Persona (Nombre, Apellido, Identificacion, 
                    FechaNacimiento, Usuario, Contrasena, Email, Tipo, Saldo) 
                                         VALUES (@Nombre, @Apellido, @Identificacion, 
                    @FechaNacimiento, @Usuario, @Contrasena, @Email, @Tipo, @Saldo)",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Apellido", persona.Apellido);
                    sqlCommand.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
                    sqlCommand.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);
                    sqlCommand.Parameters.AddWithValue("@Usuario", persona.Usuario);
                    sqlCommand.Parameters.AddWithValue("@Contrasena", persona.Contrasena);
                    sqlCommand.Parameters.AddWithValue("@Email", persona.Email);
                    sqlCommand.Parameters.AddWithValue("@Tipo", persona.Tipo);
                    sqlCommand.Parameters.AddWithValue("@Saldo", 0);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(persona);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Persona persona)
        {
            if (persona == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@" UPDATE Persona 
                                                        SET Nombre = @Nombre, 
                                                            Apellido = @Apellido,
                                                            Identificacion = @Identificacion,
                                                            FechaNacimiento = @FechaNacimiento,
                                                            Usuario = @Usuario,
                                                            Email = @Email,
                                                            Tipo = @Tipo,
                                                            Saldo = @Saldo,
                                                            Contrasena = @Contrasena  
                                          WHERE Codigo = @Codigo",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", persona.Codigo);
                    sqlCommand.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Apellido", persona.Apellido);
                    sqlCommand.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
                    sqlCommand.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);
                    sqlCommand.Parameters.AddWithValue("@Usuario", persona.Usuario);
                    sqlCommand.Parameters.AddWithValue("@Email", persona.Email);
                    sqlCommand.Parameters.AddWithValue("@Tipo", persona.Tipo);
                    sqlCommand.Parameters.AddWithValue("@Saldo", persona.Saldo);
                    sqlCommand.Parameters.AddWithValue("@Contrasena", persona.Contrasena);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(persona);
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
                        new SqlCommand(@"DELETE Persona WHERE Codigo = @Codigo",
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