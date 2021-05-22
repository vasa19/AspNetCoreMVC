﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using vasa19.BookStore.Helper;

namespace vasa19.BookStore.Models
{
    public class BookModel
    {
        public string MyField { get; set; }
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title of your book")]
        //[MyCustomValidation]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the author name")]
        public string Author { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please choose the language of your book")]
        [Display(Name = "Language of book")]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total pages of book")]
        public int? TotalPages { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
