using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        bool Register(RegisterModel userData);
        public string Login(string emailId, string password);
        public bool ForgetPassword(string email);
        public bool ResetPassword(ResetPassword reset, string email);
    }
}
