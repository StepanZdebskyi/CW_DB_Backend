using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_DB_Backend.Models
{
    public class PatientModel
    {
        [Required]
        public int PatientID { get; set; }

        [Required]
        [MinLength(2)]
        public string PatientName { get; set; }

        [Required]
        [MinLength(2)]
        public string PatientSurname { get; set; }

        [Required]
        public int PatientAge { get; set; }

        [Required]
        public int PatientGender { get; set; }

        [Required]
        public long PatientPassportNumber { get; set; }
    }
}
