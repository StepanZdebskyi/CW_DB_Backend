using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_DB_Backend.Models
{
    public class SpecialityModel
    {
        [Required]
        public int? SpecID { get; set; }

        [Required]
        public string SpecName { get; set; }
    }
}
