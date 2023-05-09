using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class SkillViewModel
    {
        [Key]
        public int SkillId { get; set; }
        
        [JsonProperty("skill_name")]
        public string SkillName { get; set; }
    }
}
