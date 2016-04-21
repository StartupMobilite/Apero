using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Newtonsoft.Json.Linq;
using EventMyLife.Model;

namespace EventMyLife.ViewModel
{
    class JsonClass
    {
        public UserInf userDeserialize(JObject juser)
        {
            var records = Newtonsoft.Json.JsonConvert.
                DeserializeObject<UserInf>(juser["records"].ToString());

            return records;
        }
    }
}
