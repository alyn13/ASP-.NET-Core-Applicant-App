using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCode.Data.Models
{
    public class CVFile
    {

        [Key]
        public int CVFileID { get; set; }

        [Column("CVFileName", TypeName = "varchar(100)")]
        public string CVFileName { get; set; }

        public string CVFileLocation { get; set; }
        public string CVFileType { get; set; }

        public int ApplicantId { get; set; } //Foreign key
        [ForeignKey("ApplicantId")]
        [JsonIgnore]
        public Applicant Applicant { get; set; }

    }
}
