using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class CollegeViewModel
    {
        [JsonProperty("college_id")]
        public int CollegeEducId { get; set; }
        [JsonProperty("college_name")]
        public string CollegeName { get; set; }
        [JsonProperty("college_degree")]
        public string Degree { get; set; }
        [JsonProperty("college_year_started")]
        public int YearStarted { get; set; }
        [JsonProperty("college_year_ended")]
        public int YearEnded { get; set; }
    }
}
