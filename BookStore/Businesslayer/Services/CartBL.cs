using Businesslayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Services
{
    public class CartBL:ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;   
        }
        public bool AddToCart(CartModel cartModel)
        {
            try
            {
                return this.cartRL.AddToCart(cartModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateCart(int cartId, int Quantity)
        {
            try
            {
                return this.cartRL.UpdateCart(cartId, Quantity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteCart(int cartId)
        {
            try
            {
                return this.cartRL.DeleteCart(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CartModel> GetCart(int userId)
        {
            try
            {
                return this.cartRL.GetCart(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
