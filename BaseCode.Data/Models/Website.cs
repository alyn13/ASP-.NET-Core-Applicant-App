using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCode.Data.Models
{
    public class Website
    {
        [Key] 
        public int WebsiteId { get; set; }
        public string WebsiteUrl { get; set; }
        public int ApplicantId { get; set; } //Foreign key
        [ForeignKey("ApplicantId")]
        [JsonIgnore]
        public virtual Applicant Applicant { get; set; }
       // [JsonIgnore]
       // public virtual ICollection<Website> Websites { get; set; } //is redundant and unnecessary. It can be removed since it's not used in the context of the foreign key relationship between Applicant and Website.
    }
}
