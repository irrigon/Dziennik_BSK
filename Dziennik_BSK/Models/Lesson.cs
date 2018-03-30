using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_BSK.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data")]
        public DateTime LessonDate { get; set; }

        [Required, StringLength(20, MinimumLength = 2)]
        //[RegularExpression(@"\\w\\s-")]
        [Display(Name = "Przedmiot")]
        public string Subject { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        [Display(Name = "Temat")]
        public string Topic { get; set; }

        [Required, StringLength(3, MinimumLength = 1)]
        [RegularExpression("[1-9][0-9a-zA-Z]*")]
        [Display(Name = "Klasa")]
        public string Class { get; set; }

        [Display(Name = "Poprowadzona przez")]
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        [Display(Name = "Uczniowie")]
        public ICollection<Participation> Participations { get; set; }
        
    }
}
