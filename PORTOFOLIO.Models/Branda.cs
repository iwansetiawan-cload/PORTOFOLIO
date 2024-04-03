using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTOFOLIO.Models
{
    public class Branda
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public string? Position { get; set; }
        public string? Photo { get; set; }
        public int? Flag { get; set; }
        public string? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}
