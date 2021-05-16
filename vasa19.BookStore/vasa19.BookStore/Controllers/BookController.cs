using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vasa19.BookStore.Controllers
{
    public class BookController : Controller
    {
        public string GetAllBooks()
        {
            return "All Books";
        }

        public string GetBook(int id)
        {
            return $"Book with Id = {id}";
        }

        public string SearchBooks(string bookName, string authorName)
        {
            return $"Book with name = {bookName} & Author = {authorName}";
        }
    }
}
