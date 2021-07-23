﻿using System;
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
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest loginRequest)
        {
            if (loginRequest == null)
                return BadRequest();

            Persona persona = new Persona();


            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Nombre, Apellido, Identificacion, FechaNacimiento, Usuario, Contrasena, Email, Tipo, Saldo FROM Persona Where Usuario = @Usuario and Contrasena = @Contrasena ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Usuario", loginRequest.Username);
                    sqlCommand.Parameters.AddWithValue("@Contrasena", loginRequest.Password);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        persona.Codigo = sqlDataReader.GetInt32(0);
                        persona.Nombre = sqlDataReader.GetString(1);
                        persona.Apellido = sqlDataReader.GetString(2);
                        persona.Identificacion = sqlDataReader.GetString(3);
                        persona.FechaNacimiento = sqlDataReader.GetDateTime(4);
                        persona.Usuario = sqlDataReader.GetString(5);
                        persona.Contrasena = sqlDataReader.GetString(6);
                        persona.Email = sqlDataReader.GetString(7);
                        persona.Tipo = sqlDataReader.GetString(8);
                        persona.Saldo = sqlDataReader.GetDecimal(9);

                        var token =
                            TokenGenerator.GenerateTokenJwt(persona.Identificacion);
                        persona.Token = token;
                    }

                    sqlConnection.Close();

                    if (!string.IsNullOrEmpty(persona.Token))
                        return Ok(persona);
                    else
                        return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(Persona persona)
        {
            if (persona == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Persona (Nombre, Apellido, Identificacion, FechaNacimiento, Usuario, Contrasena, Email, Tipo, Saldo) VALUES (@Nombre, @Apellido, @Identificacion, @FechaNacimiento, @Usuario, @Contrasena, @Email, @Tipo, @Saldo)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Apellido", persona.Apellido);
                    sqlCommand.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
                    sqlCommand.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);
                    sqlCommand.Parameters.AddWithValue("@Usuario", persona.Usuario);
                    sqlCommand.Parameters.AddWithValue("@Contrasena", persona.Contrasena);
                    sqlCommand.Parameters.AddWithValue("@Email", persona.Email);
                    sqlCommand.Parameters.AddWithValue("@Tipo", persona.Tipo);
                    sqlCommand.Parameters.AddWithValue("@Saldo", persona.Saldo);

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

        [HttpGet]
        [Route("chofer")]
        public IHttpActionResult GetAll()
        {
            List<Persona> chofer = new List<Persona>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Nombre, Apellido, Identificacion, 
                    FechaNacimiento, Usuario, Contrasena, Email, Tipo FROM Persona Where Tipo = 3", sqlConnection);
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

                        chofer.Add(persona);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(chofer);
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
                                                            Contrasena = @Contrasena,  
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
