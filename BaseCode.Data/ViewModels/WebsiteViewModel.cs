using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class WebsiteViewModel
    {
       // [JsonProperty("website_id")]
       // public int WebsiteId { get; set; }
        [JsonProperty("website_name")]
        public string WebsiteUrl { get; set; }
    }
}
