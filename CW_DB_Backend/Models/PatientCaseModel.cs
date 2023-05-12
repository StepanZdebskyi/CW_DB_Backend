using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_DB_Backend.Models
{
    public class PatientCaseModel
    {
        [Required]
        public int PatientID { get; set; }

        [Required]
        public int CaseID { get; set; }

        [Required]
        public bool IsCaseClosed { get; set; }

        [Required]
        public DateTime CaseOpeningDate { get; set; }

        [Required]
        public string IllnessDescription { get; set; }

        public string ConclusionsNotes { get; set; }

        public string Remarks { get; set; }
    }
}
