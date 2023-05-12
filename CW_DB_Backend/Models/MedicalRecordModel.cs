using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_DB_Backend.Models
{
    public class MedicalRecordModel
    {
        [Required]
        public int RecordID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        [Required]
        public DateTime RecordTime { get; set; }

        [Required]
        public string RecordHeader { get; set; }

        [Required]
        public string RecordBody { get; set; }
    }
}
