using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interfaces
{
    public interface IUserBL
    {
        bool Register(RegisterModel userData);
    }
}
