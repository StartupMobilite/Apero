using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMyLife.Model
{
    class ParticipTicket
    {
        public string id { get; set; }
        public string Role { get; set; }
        public string IdEvent { get; set; }
        public string IdUser { get; set; }
        public string Statut { get; set; }
        public int NoteEvent { get; set; }
    }
}
