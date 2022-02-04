using Businesslayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public bool Register(RegisterModel userData)
        {
            try
            {
                return this.userRL.Register(userData);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
