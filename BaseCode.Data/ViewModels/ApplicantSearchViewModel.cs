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
        [JsonProperty("applicantStreet")]
        public string ApplicantStreet { get; set; }
        //
        [JsonProperty("applicantBarangay")]
        public string ApplicantBarangay { get; set; }
        //
        [JsonProperty("applicantCity")]
        public string ApplicantCity { get; set; }
        //
        [JsonProperty("applicantProvince")]
        public string ApplicantProvince { get; set; }
        //
        [JsonProperty("applicantZipcode")]
        public string ApplicantZipCode { get; set; }
        //
        [JsonProperty("applicantCountry")]
        public string ApplicantCountry { get; set; }
        //
        [JsonProperty("applicantEmail")]
        public string ApplicantEmail { get; set; }
        //
        [JsonProperty("applicantPhone")]
        public string ApplicantPhone { get; set; }
        //
        [JsonProperty("applicantCVFileLocation")]
        public string ApplicantCVFileLocation { get; set; }

        [JsonProperty("applicantWebsite")]
        public string ApplicantWebsite { get; set; }

        [JsonProperty("applicantSkill")]
        public string ApplicantSkill { get; set; }

        [JsonProperty("applicantCollege")]
        public string ApplicantCollege { get; set; }

        [JsonProperty("applicantHighschool")]
        public string ApplicantHighSchool { get; set; }

        [JsonProperty("applicantExperience")]
        public string ApplicantExperience { get; set; }

        [JsonProperty("applicantSubmissiondate")]
        public DateTime ApplicantSubmissionDate { get; set; }
        //
        [JsonProperty("applicantStatus")]
        public string ApplicantStatus { get; set; }

        [JsonProperty("applicantPosition")]
        public string ApplicantPosition { get; set; }

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
