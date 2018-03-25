﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_BSK.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(11, MinimumLength = 11)]
        [RegularExpression("[1-9][0-9]{10}")]
        [Display(Name = "PESEL")]
        public string Pesel { get; set; }

        [Required, StringLength(20, MinimumLength = 3)]
        [RegularExpression("[A-Z][a-ząćęłśźżó]+")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required, StringLength(20, MinimumLength = 3)]
        [RegularExpression("[A-Z][a-ząćęłśźżó-]+")]
        [Display(Name = "Drugie imię")]
        public string SecendName { get; set; }

        [Required, StringLength(30, MinimumLength = 3)]
        [RegularExpression("[A-Z][a-ząćęłśźżó-]+")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data urodzenia")]
        public DateTime BirthDate { get; set; }

        [Range(2010, 2100)]
        [Display(Name = "Rocznik")]
        public int Year { get; set; }

        [Required, StringLength(3, MinimumLength = 1)]
        [RegularExpression("[1-9][0-9a-zA-Z]*")]
        public string Class { get; set; }
        
    }
}
