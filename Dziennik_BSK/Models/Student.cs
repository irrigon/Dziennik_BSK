using System;
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
        [RegularExpression("[A-ZŚŻŹĆŃŁÓĘĄ][a-ząńćęłśźżó]+")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required, StringLength(20, MinimumLength = 3)]
        [RegularExpression("[A-ZŚŻŹĆŃŁÓĘĄ][a-zŃąćęłśźżó-]+")]
        [Display(Name = "Drugie imię")]
        public string SecendName { get; set; }

        [Required, StringLength(30, MinimumLength = 3)]
        [RegularExpression("[A-ZŚŻŹĆŁÓĘŃĄ][a-ząćńęłśźżó-]+")]//TODO big letter
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required, DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data urodzenia")]
        public DateTime BirthDate { get; set; }

        [Range(2010, 2100)]
        [Display(Name = "Rocznik")]
        public int Year { get; set; }

        [Required, StringLength(3, MinimumLength = 1)]
        [RegularExpression("[1-9][0-9a-zA-Z]*")]
        [Display(Name = "Klasa")]
        public string Class { get; set; }
        
        [Required, StringLength(30, MinimumLength = 3)]
        [RegularExpression("[A-ZŚŃŻŹĆŁÓĘĄ][a-ząćęłśźżóń-]*")]//TODO Big letter
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required, DataType(DataType.PostalCode)]
        [RegularExpression("[0-9]{2}-[0-9]{3}")]
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }

        [Required, StringLength(30, MinimumLength = 3)]
        [RegularExpression("[A-ZŚŻŹĆŁÓŃĘĄ][a-ząćęłśźżóń-]*")]// TODO space char
        [Display(Name = "Ulica")] 
        public string Street { get; set; }

        [Required, StringLength(4, MinimumLength = 1)]
        //[RegularExpression("\\w")]
        [Display(Name = "Numer domu")]
        public string BuildingNumber { get; set; }

        [StringLength(4, MinimumLength = 1)]
        //[RegularExpression("\\w")]
        [Display(Name = "Numer mieszkania")]
        public string FlatNumber { get; set; }

        [Display(Name = "Rodzice")]
        public Responsibility Parent { get; set; }

        [Display(Name = "Oceny")]
        public ICollection<Grade> Grades { get; set; }

        [Display(Name = "Obecności")]
        public ICollection<Participation> Participations { get; set; }

        [Display(Name = "Uwagi")]
        public ICollection<Note> Notes { get; set; }
    }
}
