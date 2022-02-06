using Businesslayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBL;

        public UserController(IUserBL userBL)
        {
              this.userBL = userBL;
        }

        /// <summary>
        /// Registering User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UserRegistration(RegisterModel userData)
        {
            try
            {
                if (this.userBL.Register(userData))
                {
                    return this.Ok(new { Success = true, message = "Registration Successful", data = userData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [HttpPost("Login")]
        public IActionResult UserLogin(string EmailId, string Password)
        {
            try
            {
                var loginCrendential = this.userBL.Login(EmailId, Password);
                if (loginCrendential != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successful", token = loginCrendential });
                }
                else
                {
                    return this.Ok(new { Success = false, message = "Invalid User...Please enter valid email and password." });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });

            }
        }

        [HttpPost("forgotPassword")]
        public IActionResult ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email should not be null or empty");
            }

            try
            {
                if (this.userBL.ForgetPassword(email))
                {
                    return Ok(new { Success = true, message = "Password reset link sent on mail successfully" });
                }
                else
                {
                    return Ok(new { Success = false, message = "Password reset link not sent" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message, msg = ex.InnerException });
            }
        }


        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword newPassword)
        {
            try
            {
                var userEmail = User.FindFirst("Email").Value.ToString(); // taking the user's email form token
                if (this.userBL.ResetPassword(newPassword, userEmail))
                {
                    return Ok(new { Success = true, message = "Password reseted successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Password reset Unsuccesfull!" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message, msg = ex.InnerException });
            }
        }

    }
}
