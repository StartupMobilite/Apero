using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMyLife.Model
{
    public class ParticipTicket
    {
        public string id { get; set; }
        public string Role { get; set; }
        public string IdEvent { get; set; }
        public string IdUser { get; set; }
        public string Statut { get; set; }
        public int ParticipNumber { get; set; }
        public int NoteEvent { get; set; }

        public ParticipTicket(string idEvent,string idUser,int participNumber)
        {
            IdUser = idUser;
            IdEvent = idEvent;
            ParticipNumber = participNumber;
            Statut = "D";
            Role = "P";
            NoteEvent = 0;
        }

        public ParticipTicket(string idEvent, string idUser, string role)
        {
            IdUser = idUser;
            IdEvent = idEvent;
            ParticipNumber = 0;
            Statut = "A";
            Role = "O";
            NoteEvent = 0;
        }

    }


}
