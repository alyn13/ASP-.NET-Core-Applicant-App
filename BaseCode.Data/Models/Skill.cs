using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCode.Data.Models
{
    public class Skill
    {
        [Key] 
        public int SkillId { get; set; }
        public string SkillName { get; set; }

        [ForeignKey("ApplicantId")]

        public int ApplicantId { get; set; } //Foreign key
        
        //[JsonIgnore]
        //public virtual Applicant Applicant { get; set; }
    }
}
