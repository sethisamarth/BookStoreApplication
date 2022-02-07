using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interfaces
{
    public interface IWishlistBL
    {
        bool AddToWishList(WishlistModel wishListmodel);
        List<WishlistModel> GetWishList(int userId);
        bool DeleteWishlist(int WishlistId);
    }
}
