using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_BSK.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(11, MinimumLength = 11)]
        [RegularExpression("[1-9][0-9]{10}")]
        [Display(Name = "PESEL")]
        public string Pesel { get; set; }

        [Required, StringLength(20, MinimumLength = 3)]
        [RegularExpression("[A-ZŚŻŹĆŁÓŃĘĄ][a-ząćęłśźńżó]+")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required, StringLength(30, MinimumLength = 3)]
        [RegularExpression("[A-ZŚŻŹĆŁŃÓĘĄ][a-ząćęłńśźżó-]+")] //TODO Big Letter
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required, StringLength(9, MinimumLength = 9)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[1-9][0-9]{8}")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50, MinimumLength = 8)]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"([\\w\.\-] +)@([\\w\-] +)((\.(\\w){2, 3})+)")]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        [Display(Name = "Dzieci")]
        public Responsibility Child { get; set; }
    }
}
