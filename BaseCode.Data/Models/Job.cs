using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        [Column("JobName", TypeName = "varchar(100)")]
        public string JobName { get; set; }
        public string JobShortDescription { get; set; }
        public string JobDescription { get; set; }
        public string JobResponsibilities { get; set; }
        public string JobQualifications { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        

    }

}
