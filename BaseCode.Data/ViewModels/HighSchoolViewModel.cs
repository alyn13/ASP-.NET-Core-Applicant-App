using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class HighSchoolViewModel
    {
        [Key]
        [JsonProperty("highschool_id")]
        public int HighSchoolEducId { get; set; }
        [JsonProperty("highschool_name")]
        public string HighSchoolName { get; set; }
        [JsonProperty("highschool_year_started")]
        public int YearStarted { get; set; }
        [JsonProperty("highschool_year_ended")]
        public int YearEnded { get; set; }

    }
}
