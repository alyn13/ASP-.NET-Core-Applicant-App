using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels.Common
{
    public class JobSearchViewModel
    {
        [JsonProperty("jobID")]
        public string jobId { get; set; }

        [JsonProperty("jobName")]
        public string jobName { get; set; }

        [JsonProperty("jobShortDescription")]
        public string jobShortDescription { get; set; }

        [JsonProperty("jobDescription")]
        public string jobDescription { get; set; }

        [JsonProperty("jobResponsibilities")]
        public string jobResponsibilities { get; set; }

        [JsonProperty("jobQualifications")]
        public string jobQualifications { get; set; }

        [JsonProperty("jobPage")]
        public int JobPage { get; set; }

        [JsonProperty("jobPageSize")]
        public int JobPageSize { get; set; }

        [JsonProperty("jobSortBy")]
        public string JobSortBy { get; set; }

        [JsonProperty("jobSortOrder")]
        public string JobSortOrder { get; set; }
    }
}
