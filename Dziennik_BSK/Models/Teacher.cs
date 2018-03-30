﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_BSK.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(11, MinimumLength = 11)]
        [RegularExpression("[1-9][0-9]{10}")]
        [Display(Name = "PESEL")]
        public string Pesel { get; set; }

        [Required, StringLength(20, MinimumLength = 3)]
        [RegularExpression("[A-ZŚŻŹŃĆŁÓĘĄ][a-ząćęłńśźżó]+")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required, StringLength(30, MinimumLength = 3)]
        [RegularExpression("[A-ZŚŻŹĆŁÓŃĘĄ][A-Za-zŚŻŹĆŁÓŃĘĄąćęłśńźżó-]+")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required, StringLength(9, MinimumLength = 9)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[1-9][0-9]{8}")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50, MinimumLength = 8)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        [Display(Name = "Wpisane uwagi")]
        public ICollection<Note> Notes { get; set; }

        [Display(Name = "Poprowadzone lekcje")]
        public ICollection<Lesson> Lessons { get; set; }

        [Display(Name = "Wystawione oceny")]
        public ICollection<Grade> Grades { get; set; }

        [Display(Name = "Imię i nazwisko nauczyciela")]
        public string FullName
        {
            get { return FirstName + " " + Surname; }
        }
    }
}
