using BaseCode.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class CollegeViewModel
    {
        [Key]
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
