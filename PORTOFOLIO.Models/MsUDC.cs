using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTOFOLIO.Models
{
    public class MsUDC
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string EntryKey { get; set; }
        [MaxLength(255)]
        public string? Text1 { get; set; }
        [MaxLength(255)]
        public string? Text2 { get; set; }
        public string? Text3 { get; set; }
        public int? Inum1 { get; set; }
        public int? Inum2 { get; set; }
        public double? Mnum1 { get; set; }
        public double? Mnum2 { get; set; }
        [MaxLength(100)]
        public string? Creator { get; set; }
        [MaxLength(100)]
        public string? LastModifyUser { get; set; }
        public Nullable<System.DateTime> LastModifyDate { get; set; }
    }
}
