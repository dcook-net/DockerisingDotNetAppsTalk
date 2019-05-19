using System.ComponentModel;
using Newtonsoft.Json;

namespace FrontEnd.Models
{
    public class Member
    {
        [JsonProperty("id")]
        public string Identifier { get; set; }
        
        [DisplayName("First Name")]
        [JsonProperty("firstname")]
        public string ForeName { get; set; }
        
        [DisplayName("Surname")]
        [JsonProperty("surname")]
        public string LastName { get; set; }
        
        [DisplayName("Biography")]
        [JsonProperty("bio")]
        public string Blurb { get; set;}

        public string FullName => $"{ForeName} {LastName}";
    }
}