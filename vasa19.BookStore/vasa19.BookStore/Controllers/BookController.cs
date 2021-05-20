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

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ViewResult> GetAllBooks()
        {
            var data =  await _bookRepository.GetAllBooks();
            return View(data);
        }

        [Route("book-details/{id}", Name = "bookDeatilsRoute")]
        public async Task<ViewResult> GetBook(int id)
        {
            var data =  await _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookmodel)
        {
            int id = await _bookRepository.AddNewBook(bookmodel);
            if(id > 0)
            {
                return RedirectToAction("AddNewBook", new { isSuccess = true, bookId = id });
            }
            return View();
        }
    }
}
