using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMyLife.Model
{
    class UserInf
    {
        [JsonProperty("email")]
        public String Email { get; set; }
        [JsonProperty("given_name")]
        public String Given_name { get; set; }
        [JsonProperty("family_name")]
        public String Family_name { get; set; }
        [JsonProperty("gender")]
        public String Gender { get; set; }
        [JsonProperty("birthdate")]
        public String Birthdate { get; set; }
        [JsonProperty("idp")]
        public String Idp { get; set; }

        public string Id { get; set; }
    }
}
