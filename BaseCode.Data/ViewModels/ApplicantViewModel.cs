using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BaseCode.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.ViewModels
{
    public class ApplicantViewModel
    {
        [JsonProperty("applicant_id")] 
        public int ApplicantId { get; set; }

        [JsonProperty("applicant_fname")]
        [Required]
        public string FirstName { get; set; }
        //
        [JsonProperty("applicant_lname")]
        [Required]
        public string LastName { get; set; }
        //
        [JsonProperty("applicant_street")]
        [Required]
        public string Street { get; set; }
        //
        [JsonProperty("applicant_barangay")]
        [Required]
        public string Barangay { get; set; }
        //
        [JsonProperty("applicant_city")]
        [Required]
        public string City { get; set; }
        //
        [JsonProperty("applicant_province")]
        [Required]
        public string Province { get; set; }
        //
        [JsonProperty("applicant_zipcode")]
        [Required]
        public int ZipCode { get; set; }
        //
        [JsonProperty("applicant_country")]
        [Required]
        public string Country { get; set; }
        //
        [JsonProperty("applicant_email")]
        [Required]
        public string Email { get; set; }
        //
        [JsonProperty("applicant_phone")]
        [Required]
        public string Phone { get; set; }
        //
        [JsonProperty("applicant_CVfile")]
          [Required]
        public CVFile CVFile { get; set; }

        [JsonProperty("applicant_website")]
        public ICollection<Website> Website { get; set; }

        [JsonProperty("applicant_skill")] 
        public ICollection<Skill> Skill { get; set; }

        [JsonProperty("applicant_college")] 
        public ICollection<CollegeEducation> College { get; set; }

        [JsonProperty("applicant_highschool")] 
        public HighSchoolEducation HighSchool { get; set; }

        [JsonProperty("applicant_experience")] 
        public ICollection<Experience> Experience { get; set; }

        [JsonProperty("applicant_submissiondate")]
        [Required]
        public DateTime SubmissionDate { get; set; }
        //
        [JsonProperty("applicant_status")]
        [Required]
        public string Status { get; set; }
    }
}
