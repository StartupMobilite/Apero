using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMyLife.Model
{
    class UserInf
    {
        public String UserId { get; set; }
        public String displayName { get; set; }
        public String streetAddress { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String postalCode { get; set; }
        public String mail { get; set; }
        public String[] otherMails { get; set; }

        public override string ToString()
        {
            return "displayName : " + displayName + "\n" +
                   "streetAddress : " + streetAddress + "\n" +
                   "city : " + city + "\n" +
                   "state : " + state + "\n" +
                   "postalCode : " + postalCode + "\n" +
                   "mail : " + mail + "\n" +
                   "otherMails : " + string.Join(", ", otherMails);
        }

    }
}
