using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class HighSchoolEducation
    {
        [Key] 
        public int HighSchoolEducId { get; set; }
        public string HighSchoolName { get; set; }
        public int YearStarted { get; set; }
        public int YearEnded { get; set; }

        public int ApplicantId { get; set; } //Foreign key
        [ForeignKey("ApplicantId")]
        [JsonIgnore]
        public virtual Applicant Applicant { get; set; }
       // [JsonIgnore]
       // public virtual HighSchoolEducation HighSchool { get; set; }
    }
}
