using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        bool AddToCart(CartModel cartModel);
        bool UpdateCart(int cartId, int Quantity);
        bool DeleteCart(int cartId);
        List<CartModel> GetCart(int userId);
    }
}
