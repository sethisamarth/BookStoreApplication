using Businesslayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost]
        [Route("api/AddToCart")]
        public IActionResult AddToCart([FromBody] CartModel cartModel)
        {
            try
            {
                var result = this.cartBL.AddToCart(cartModel);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book is added to cart" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Adding to bag failed ! try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/UpadetCart")]
        public IActionResult UpdateCart(int cartId, int Quantity)
        {
            try
            {
                var result = this.cartBL.UpdateCart(cartId, Quantity);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "cart updated" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed TryAgain" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/DeleteCart")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                var result = this.cartBL.DeleteCart(cartId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Removed from cart" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = " failed ! try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetCart")]
        public IActionResult GetCart(int userId)
        {
            try
            {
                var result = this.cartBL.GetCart(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart item is present", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No Item in cart" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
