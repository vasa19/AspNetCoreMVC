﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using vasa19.BookStore.Models;
using vasa19.BookStore.Repository;

namespace vasa19.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
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

        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel();

            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if(ModelState.IsValid)
            {
                if(bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    await UploadImage(folder, bookModel.CoverPhoto);
                }

                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();
                    foreach(var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction("AddNewBook", new { isSuccess = true, bookId = id });
                }
            }
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}
