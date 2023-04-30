using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Applicant
    {
        [Key] 
        public int ApplicantId { get; set; }
        //
        [Column("FirstName", TypeName = "varchar(100)")]
        [Required]
        public string FirstName { get; set; }
        //
        [Column("LastName", TypeName = "varchar(100)")]
        [Required]
        public string LastName { get; set; }
        //
        [Column("Street", TypeName = "varchar(300)")]
        [Required] 
        public string Street { get; set; }
        //
        [Column("Barangay", TypeName = "varchar(200)")]
        [Required]
        public string Barangay { get; set; }
        //
        [Column("City", TypeName = "varchar(200)")]
        [Required]
        public string City { get; set; }
        //
        [Column("Province", TypeName = "varchar(200)")]
        [Required]
        public string Province { get; set; }
        //
        [Column("ZipCode")]
        [Required]
        public int ZipCode { get; set; }
        //
        [Column("Country", TypeName = "varchar(100)")]
        [Required]
        public string Country { get; set; }
        //
        [Column("Email", TypeName = "varchar(100)")]
        [Required]
        public string Email { get; set; }
        //
        [Column("Phone", TypeName = "varchar(11)")]
        [Required]
        public string Phone { get; set; }
        //
        [Required]
        public CVFile CVFile { get; set; }
      //
        public ICollection<Website> Website { get; set; }
        //
        public ICollection<Skill> Skill { get; set; }
        //
        public ICollection<CollegeEducation> College { get; set; }
        //     
        public HighSchoolEducation HighSchool { get; set; }   
        //
        public ICollection<Experience> WorkExperience { get; set; }
        //
        [Column("SubmissionDate")]
        [Required]
        public DateTime SubmissionDate { get; set; }
        //
        [Column("Status", TypeName = "varchar(20)")]
        [Required]
        public string Status { get; set; } = "To View";
        public string Remarks { get; set; }
        [Required] 
        public string JobRole { get; set; }

    }
}
