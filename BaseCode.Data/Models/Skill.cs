using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.Models
{
    public class Skill
    {
        [Key] 
        public int SkillId { get; set; }
        public string SkillName { get; set; }
    }
}
