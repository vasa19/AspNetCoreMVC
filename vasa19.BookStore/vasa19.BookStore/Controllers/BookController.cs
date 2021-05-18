using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vasa19.BookStore.Models;
using vasa19.BookStore.Repository;

namespace vasa19.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;

        public BookController()
        {
            _bookRepository = new BookRepository();
        }

        public ViewResult GetAllBooks()
        {
            var data =  _bookRepository.GetAllBooks();
            return View();
        }

        public BookModel GetBook(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }
    }
}
