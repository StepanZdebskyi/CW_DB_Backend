using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CW_DB_Backend.Models
{
    public class DoctorModel
    {
        [Required]
        public int DoctorID { get; set; }
        
        [Required]
        [MinLength(2)]
        public string DoctorName { get; set; }
        
        [Required]
        [MinLength(2)]
        public string DoctorSurname { get; set; }

        [Required]
        public int SpecID { get; set; }
    }
}
