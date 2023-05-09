using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class CollegeEducation
    {
        [Key]
        public int CollegeEducId { get; set; }
        public string CollegeName { get; set; }
        public string Degree { get; set; }
        public int YearStarted { get; set; }
        public int YearEnded { get; set; }

        [ForeignKey("ApplicantId")]
        public int ApplicantId { get; set; } //Foreign key
        
        //[JsonIgnore]
        //public virtual Applicant Applicant { get; set; }
    }
}
