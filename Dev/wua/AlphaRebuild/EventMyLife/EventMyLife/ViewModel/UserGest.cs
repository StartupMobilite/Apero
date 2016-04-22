using EventMyLife.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace EventMyLife.ViewModel
{
    public class UserGest
    {
        private ObservableCollection<UserEML> myContactsEML;

        public ObservableCollection<UserEML> MyContactsEML
        {
            get
            {
                return myContactsEML ?? (myContactsEML = new ObservableCollection<UserEML>());
            }
        }

        public async void sendUserInf(UserInf nUserInf)
        {
            List<UserInf> listTest = await App.MobileService.GetTable<UserInf>().Where(toto => toto.IdProvider == nUserInf.IdProvider && toto.Idp == nUserInf.Idp).ToListAsync();
            Debug.WriteLine(listTest.Count);
            if (listTest.Count == 0) {
                await App.MobileService.GetTable<UserInf>().InsertAsync(nUserInf);
            }
            else
            {
//mis a jour si besoindes informations
            }
            
        }

    }
}
