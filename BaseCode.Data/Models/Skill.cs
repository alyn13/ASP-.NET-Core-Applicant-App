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
        public int ApplicantId { get; set; } //Foreign key
        [ForeignKey("ApplicantId")]
        [JsonIgnore]
        public virtual Applicant Applicant { get; set; }
    }
}
