using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels
{
    public class ExperienceViewModel
    {
        [Key]
        [JsonProperty("experience_id")]
        public int ExperienceId { get; set; }
        //
        
        //
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        //
        [JsonProperty("position")] 
        public string Position { get; set; }
        //
        [JsonProperty("company_street")]
        public string Street { get; set; }
        //
        [JsonProperty("company_barangay")]
        public string Barangay { get; set; }
        //
        [JsonProperty("company_city")]
        public string City { get; set; }
        //
        [JsonProperty("company_province")]
        public string Province { get; set; }
        //
        [JsonProperty("company_zipcode")]
        public int ZipCode { get; set; }
        //
        [JsonProperty("company_country")]
        public string Country { get; set; }
        //
        [JsonProperty("currently_working")]
        public bool CurrentlyWorking { get; set; }
        //
        [JsonProperty("time_started")]
        public string TimeStarted { get; set; }
        //
        [JsonProperty("time_ended")]
        public string TimeEnded { get; set; }
    }
}
