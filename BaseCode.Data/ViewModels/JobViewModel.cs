using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class JobViewModel
    {
        [JsonProperty("job_id")]
        public int JobId { get; set; }

        [JsonProperty("job_name")]
        [Required(ErrorMessage = "Job Name is Required.")]
        public string JobName { get; set; }

        [JsonProperty("job_short_description")]
        [Required(ErrorMessage = "Job Short Description is Required.")]
        public string JobShortDescription { get; set; }

        [JsonProperty("job_description")]
        [Required(ErrorMessage = "Job Description is Required.")]
        public string JobDescription { get; set; }

        [JsonProperty("job_responsibilities")]
        [Required(ErrorMessage = "Job Responsibilities are Required.")]
        public string JobResponsibilities { get; set; }

        [JsonProperty("job_qualifications")]
        [Required(ErrorMessage = "Job Qualifications are Required.")]
        public string JobQualifications { get; set; }

        [JsonProperty("job_createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("job_createdDate")]
        public DateTime? CreatedDate { get; set; }

        [JsonProperty("job_modifiedBy")]
        public string ModifiedBy { get; set; }

        [JsonProperty("job_modifiedDate")]
        public DateTime? ModifiedDate { get; set; }

    }
}
