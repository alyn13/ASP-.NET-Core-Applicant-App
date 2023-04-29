using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.Models
{
    public class Website
    {
        [Key] 
        public int WebsiteId { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
