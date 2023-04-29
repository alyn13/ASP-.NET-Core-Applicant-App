using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Experience
    {
        [Key] 
        public int ExperienceId { get; set; }
        //
        [Column("IsFirstJob")]
        [Required]
        public bool IsFirstJob { get; set; }
        //
        [Column("CompanyName", TypeName = "varchar(100)")]
        public string CompanyName { get; set; }
        //
        [Column("Position", TypeName = "varchar(100)")]
        public string Position { get; set; }
        //
        [Column("Street", TypeName = "varchar(200)")]
        public string Street { get; set; }
        //
        [Column("Barangay", TypeName = "varchar(200)")]
        public string Barangay { get; set; }
        //
        [Column("City", TypeName = "varchar(200)")]
        public string City { get; set; }
        //
        [Column("Province", TypeName = "varchar(200)")]
        public string Province { get; set; }
        //
        [Column("ZipCode")]
        public int ZipCode { get; set; }
        //
        [Column("Country", TypeName = "varchar(100)")]
        public string Country { get; set; }
        //
        [Column("CurrentlyWorking", TypeName = "varchar(100)")]
        public bool CurrentlyWorking { get; set; }
        //
        [Column("TimeStarted", TypeName = "varchar(100)")]
        public DateTime TimeStarted{ get; set; }
        //
        [Column("TimeEnded", TypeName = "varchar(100)")]
        public DateTime TimeEnded { get; set; }
        //
        public int ApplicantId { get; set; } //Foreign key
        [ForeignKey("ApplicantId")]
        [JsonIgnore]
        public Applicant Applicant { get; set; }
    }
}
