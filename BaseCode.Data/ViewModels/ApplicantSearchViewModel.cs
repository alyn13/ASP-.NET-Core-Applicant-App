using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class ApplicantSearchViewModel
    {
        [JsonProperty("applicantId")]
        public string ApplicantId { get; set; }

        [JsonProperty("applicantFname")]
        public string ApplicantFName { get; set; }
        //
        [JsonProperty("applicantLname")]
        public string ApplicantLName { get; set; }
        //
        [JsonProperty("applicantSubmissiondate")]
        public DateTime ApplicantSubmissionDate { get; set; }
        //
        [JsonProperty("applicantPosition")]
        public string ApplicantPosition { get; set; }
        //
        [JsonProperty("applicantStatus")]
        public string ApplicantStatus { get; set; }

        

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("sortBy")]
        public string SortBy { get; set; }

        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }
        public string ApplicantCVFileName { get; internal set; }
    }
}
