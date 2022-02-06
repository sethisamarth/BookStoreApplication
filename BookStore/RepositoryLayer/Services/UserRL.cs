using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;

        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public  IConfiguration Configuration { get;  }
       // SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));


        public bool Register(RegisterModel userData)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_AddUsers", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserName", userData.UserName);
                    sqlCommand.Parameters.AddWithValue("@Email", userData.Email);
                    sqlCommand.Parameters.AddWithValue("@PhoneNo", userData.PhoneNo);
                    sqlCommand.Parameters.AddWithValue("@Password", userData.Password);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public string Login(string emailId, string password)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    RegisterModel newUser = new RegisterModel();
                    // SqlCommand command = new SqlCommand(@"SELECT EmailId, Password FROM UserTable WHERE @EmailId = EmailId AND @Password = Password", this.sqlConnection);
                    SqlCommand command = new SqlCommand("SP_Login",sqlConnection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", emailId);
                    command.Parameters.AddWithValue("@Password", password);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newUser.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                            newUser.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                        }
                        string token = GenerateToken(emailId);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public string GenerateToken(string Email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //claims are the ways to store the data about the user
            var claims = new[]
            {
                new Claim("Email", Email),
                //new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Issuer"],
                  claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ForgetPassword(string EmailId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    RegisterModel newUser = new RegisterModel();
                    SqlCommand command = new SqlCommand("SP_Forget", sqlConnection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", EmailId);
                   
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newUser.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                        }
                        string token = GenerateToken(EmailId);
                        new MSMQOperations().Sender(token);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool ResetPassword(ResetPassword newPassword, string EmailId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
                using (this.sqlConnection)
                {
                    RegisterModel model = new RegisterModel();
                    SqlCommand command = new SqlCommand("SP_ResetPassword1", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", EmailId);
                    command.Parameters.AddWithValue("@Password", newPassword.ConfirmPassword);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                            model.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);

                        }
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }
    }
}
