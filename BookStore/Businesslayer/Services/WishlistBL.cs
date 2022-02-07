using Businesslayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Services
{
    public class WishlistBL:IWishlistBL
    {
        IWishlistRL wishlistRL;

        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }
        public bool AddToWishList(WishlistModel wishListmodel)
        {
            try
            {
                return this.wishlistRL.AddToWishList(wishListmodel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<WishlistModel> GetWishList(int userId)
        {
            try
            {
                return this.wishlistRL.GetWishList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteWishlist(int WishlistId)
        {
            try
            {
                return this.wishlistRL.DeleteWishlist(WishlistId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
