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
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Nombre, Apellido, Identificacion, FechaNacimiento, Usuario, Contrasena, Email, Tipo, Saldo, Estado FROM Persona Where Usuario = @Usuario and Contrasena = @Contrasena ", sqlConnection);
                    
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
                        persona.Estado = sqlDataReader.GetString(10);

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
                using(SqlConnection sqlConnection = new 
                    SqlConnection(ConfigurationManager.ConnectionStrings["ULACIT2021_PAGO_ELECTRONICO_BUSES"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Persona (Nombre, Apellido, Identificacion, FechaNacimiento, Usuario, Contrasena, Email, Tipo, Saldo, Estado) VALUES (@Nombre, @Apellido, @Identificacion, @FechaNacimiento, @Usuario, @Contrasena, @Email, @Tipo, @Saldo, @Estado)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Nombre",persona.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Apellido", persona.Apellido);
                    sqlCommand.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
                    sqlCommand.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);
                    sqlCommand.Parameters.AddWithValue("@Usuario", persona.Usuario);
                    sqlCommand.Parameters.AddWithValue("@Contrasena", persona.Contrasena);
                    sqlCommand.Parameters.AddWithValue("@Email", persona.Email);
                    sqlCommand.Parameters.AddWithValue("@Tipo", persona.Tipo);
                    sqlCommand.Parameters.AddWithValue("@Saldo", persona.Saldo);
                    sqlCommand.Parameters.AddWithValue("@Estado", persona.Estado);


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


    }
}
