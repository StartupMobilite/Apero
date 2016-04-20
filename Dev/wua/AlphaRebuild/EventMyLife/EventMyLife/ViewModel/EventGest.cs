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

        private ObservableCollection<Event> myEvents;

        private ObservableCollection<Event> eventIGo;

        public ObservableCollection<Event> AllEvents
        {
            get
            {
                return allEvents ?? (allEvents = new ObservableCollection<Event>());
            }
        }

        public ObservableCollection<Event> EventIGo
        {
            get
            {
                return eventIGo ?? (eventIGo = new ObservableCollection<Event>());
            }
        }

        public ObservableCollection<Event> MyEvent
        {
            get
            {
                return myEvents ?? (myEvents = new ObservableCollection<Event>());
            }
        }

        public async void sendEvent(Event sEvent)
        {
            await App.MobileService.GetTable<Event>().InsertAsync(sEvent);
        }

        public async void supprEvent(Event sEvent)
        {
            await App.MobileService.GetTable<Event>().DeleteAsync(sEvent);
        }

        public async void updateEvent(Event sEvent)
        {
            await App.MobileService.GetTable<Event>().RefreshAsync(sEvent);
        }

        public async void recupEvent()
        {
            
            try
            {
                List<Event> listevents = await App.MobileService.GetTable<Event>().Where(eventitems => eventitems.NbParticipEvent != 0).ToListAsync();
                if (AllEvents != null)
                {
                    AllEvents.Clear();
                }
                if (listevents != null)
                    foreach (var item in listevents)
                    {
                        if (item.IdUser != App.MobileService.CurrentUser.UserId.ToString())
                        {
                            AllEvents.Add(item);
                        }
                    }
            }
            catch
            {

            }
        }

        public List<string> recupListId(string parseEventsId)
        {
            List<string> list = parseEventsId.Split('/').ToList<string>();
            return list;
        }


        public async void recupEventIGo()
        {
            List<string> listId;
            try
            {
                List<Event> listevents = await App.MobileService.GetTable<Event>().Where(eventitems => eventitems.NbParticipEvent != 0).ToListAsync();
                if (AllEvents != null)
                {
                    AllEvents.Clear();
                }
                if (listevents != null)
                    foreach (var item in listevents)
                    {
                        listId = recupListId(item.ParticipateUserIds);
                        if (listId.Contains(App.MobileService.CurrentUser.UserId.ToString()))
                        {
                            AllEvents.Add(item);
                        }
                    }
            }
            catch
            {

            }
        }


        public async void recupMyEvent()
        {


            try
            {
                List<Event> listevents = await App.MobileService.GetTable<Event>().Where(eventitems => eventitems.NbParticipEvent != 0).ToListAsync();
                if (MyEvent != null)
                {
                    MyEvent.Clear();
                }
                if (listevents != null)
                    foreach (var item in listevents)
                    {
                        if (item.IdUser == App.MobileService.CurrentUser.UserId.ToString())
                        {
                            MyEvent.Add(item);
                        }
                    }
            }
            catch
            {

            }
        }



        }
}
