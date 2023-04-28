using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Column("Name", TypeName = "varchar(500)")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public string EnrollYear { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Column("CreatedBy")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Column("StudentName", TypeName = "varchar(100)")]
        public string StudentName { get; set; }
        public string StudentName2 { get; set; }
        public string StudentName3 { get; set; }
    }
}
