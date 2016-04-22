using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Newtonsoft.Json.Linq;
using EventMyLife.Model;
using System.Diagnostics;

namespace EventMyLife.ViewModel
{
    public class JsonClass
    {
        public UserInf userDeserialize(string juser)
        {
            var records = Newtonsoft.Json.JsonConvert.
                DeserializeObject<UserInf>(juser);
            Debug.WriteLine(records.ToString());
            return records;
        }
    }
}
