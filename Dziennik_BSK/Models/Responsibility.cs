using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dziennik_BSK.Models
{
    public class Responsibility
    {
        [Key]
        public int Id { get; set; }

        public int ParentId { get; set; }
        public Parent Parents { get; set; }

        public int StudentId { get; set; }
        public Student Students { get; set; }
    }
}
