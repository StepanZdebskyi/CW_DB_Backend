using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_DB_Backend.Models
{
    public class DoctorsInfoModel
    {
        [Required]
        public int DoctorID { get; set; }

        [Required]
        public string DoctorExperience { get; set; }

        [Required]
        public string DoctorGraduation { get; set; }

        [Required]
        public float DoctorSalary { get; set; }

        public string DoctorRemarks { get; set; }
    }
}
