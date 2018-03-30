using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_BSK.Models
{
    public class Participation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression("Tak|Nie")]
        [Display(Name = "Obecny")]
        public string IsPresent { get; set; }

        [Display(Name = "Uczeń")]
        public Student Student { get; set; }
        public int StudentId { get; set; }

        [Display(Name = "Lekcja")]
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }

    }
}
