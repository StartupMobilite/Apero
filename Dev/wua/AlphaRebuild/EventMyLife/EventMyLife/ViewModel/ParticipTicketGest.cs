using EventMyLife.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMyLife.ViewModel
{
    public class ParticipTicketGest
    {
        private ObservableCollection<ParticipTicket> myTickets;

        public ObservableCollection<ParticipTicket> MyTickets
        {
            get
            {
                return myTickets ?? (myTickets = new ObservableCollection<ParticipTicket>());
            }
        }


        public async void recupEvent()
        {

            try
            {
                List<ParticipTicket> listevents = await App.MobileService.GetTable<ParticipTicket>().Where(eventitems => eventitems.IdUser != App.MyProfile.IdProvider).ToListAsync();
                if (MyTickets != null)
                {
                    MyTickets.Clear();
                }
                if (listevents != null)
                    foreach (var item in listevents)
                    {
                        if (item.IdUser != App.MobileService.CurrentUser.UserId.ToString())
                        {
                            MyTickets.Add(item);
                        }
                    }
            }
            catch
            {

            }
        }


        public async void sendParticipTicket(ParticipTicket sParTicket)
        {
            await App.MobileService.GetTable<ParticipTicket>().InsertAsync(sParTicket);
        }

    }
}
