using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_DB_Backend.Models
{
    public class HospitalizationRequestModel
    {
        [Required]
        public int RequestID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public string HealthComplaints { get; set; }

        [Required]
        public string ClinicName { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public bool IsRequestProcessed { get; set; }
    }
}
