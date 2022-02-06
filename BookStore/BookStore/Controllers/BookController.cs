﻿using Businesslayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
                this.bookBL = bookBL;
        }
        [HttpPost]
        [Route("api/AddBook")]
        public IActionResult AddBook([FromBody] BookModel bookmodel)
        {
            try
            {
                bool result = this.bookBL.AddBook(bookmodel);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New Book Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new book" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
        [HttpGet]
        [Route("api/GetBook")]
        public IActionResult GetBook(int bookId)
        {
            var result = this.bookBL.GetBook(bookId);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Book is retrived", data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        [Route("api/UpdateBook")]
        public IActionResult UpdateBook(BookModel Bookmodel)
        {
            try
            {
                var result = this.bookBL.UpdateBook(Bookmodel);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book updated Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to updated Book" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

        [HttpDelete]
        [Route("api/DeleteBook")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.bookBL.DeleteBook(BookId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Removed Book Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to Remove Book, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
