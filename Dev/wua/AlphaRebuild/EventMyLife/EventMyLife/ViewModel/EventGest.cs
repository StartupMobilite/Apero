using EventMyLife.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMyLife.ViewModel
{
    public class EventGest
    {
        private ObservableCollection<Event> allEvents;
        public ObservableCollection<Event> AllEvents
        {
            get
            {
                return allEvents ?? (allEvents = new ObservableCollection<Event>());
            }
        }

        public async void sendEvent(Event sEvent)
        {
            await App.MobileService.GetTable<Event>().InsertAsync(sEvent);
        }

        public async void recupEvent()
        {

            List<Event> listevents = await App.MobileService.GetTable<Event>().Where(eventitems => eventitems.NbParticipEvent != 0).ToListAsync();
            if (AllEvents != null)
            {
                AllEvents.Clear();
            }
                if (listevents != null)
                    foreach (var item in listevents)
                    {
                        AllEvents.Add(item);
                    }
        }

    }
}
