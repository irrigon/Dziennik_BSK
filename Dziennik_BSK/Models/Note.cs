using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_BSK.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data wystawienia")]
        public DateTime AddDate { get; set; }

        [Required, StringLength(128, MinimumLength = 5)]
        [Display(Name = "Treść")]
        public string Content { get; set; }

        [Required]
        [RegularExpression("Tak|Nie")]
        [Display(Name = "Czy negatywna")]
        public string IsNegative { get; set; }

        [Display(Name = "Otrzymał")]
        public Student Student { get; set; }

        [Display(Name = "Wystawiona przez")]
        public Teacher Teacher { get; set; }
        
    }
}
