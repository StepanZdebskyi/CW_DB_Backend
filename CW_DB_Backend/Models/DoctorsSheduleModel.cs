using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CW_DB_Backend.Models
{
    public class DoctorsSheduleModel
    {
        [Required]
        public int SheduleItemID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        public DateTime WorkDate { get; set; }

        [Required]
        public DateTime StartWorkTime { get; set; }

        [Required]
        public DateTime FinishWorkTime { get; set; }
    }
}
