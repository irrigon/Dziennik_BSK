using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_BSK.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        
        [Required, DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data wystwienia")]
        public DateTime AddDate { get; set; }

        [Required]
        [RegularExpression(@"[1-6][+\-!]?")]
        [Display(Name = "Ocena")]
        public string Rate { get; set; }

        [Required, Range(1, 10)]
        [Display(Name = "Waga")]
        public int Weight { get; set; }

        [MaxLength(128)]
        [Display(Name = "Komentarz")]
        public string Comment { get; set; }

        [Required, StringLength(20, MinimumLength = 2)]
        //[RegularExpression(@"\\w\\s-")]
        [Display(Name = "Przedmiot")]
        public string Subject { get; set; }

        [Display(Name = "Wystawiona przez")]
        public Teacher Teacher { get; set; }

        [Display(Name = "Otrzymana przez")]
        public Student Student { get; set; }

    }
}
