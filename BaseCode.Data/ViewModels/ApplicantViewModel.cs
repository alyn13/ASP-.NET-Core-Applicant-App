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
        [JsonProperty("applicant_CVfile_name")]
        [Required]
        public string CVFileName { get; set; }

        [JsonProperty("applicant_website")]
        public ICollection<WebsiteViewModel> Website { get; set; } = new List<WebsiteViewModel>();

        [JsonProperty("applicant_skill")]
        public ICollection<SkillViewModel> Skill { get; set; } = new List<SkillViewModel>();

        [JsonProperty("applicant_college")] 
        public ICollection<CollegeViewModel> College { get; set; } = new List<CollegeViewModel>();

        [JsonProperty("applicant_highschool")] 
        public HighSchoolViewModel HighSchool { get; set; } = new HighSchoolViewModel();

        [JsonProperty("applicant_experience")] 
        public ICollection<ExperienceViewModel> WorkExperience { get; set; } = new List<ExperienceViewModel>();

        [JsonProperty("applicant_submissiondate")]
        [Required]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        //
        [JsonProperty("applicant_status")]
        [Required]
        public string Status { get; set; } = "To View";
        //
        [JsonProperty("applicant_remarks")]
        public string Remarks { get; set; }
        //
        [JsonProperty("applicant_job_applied")]
        [Required]
        public string JobApplied { get; set; }
    }
}
