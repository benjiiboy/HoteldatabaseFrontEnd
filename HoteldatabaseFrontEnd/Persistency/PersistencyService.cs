using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using HoteldatabaseFrontEnd.Model;
using Newtonsoft.Json;


namespace HoteldatabaseFrontEnd.Persistency
{
   public class PersistencyService
    {
        private readonly Task<StorageFile> Localfolder;
        private object filnavn;

        public ObservableCollection<Guest> Guests { get; set; }

        const string fileName = "hoteldbGuest.json";

        public static async void SaveGuestAsJsonAsync()
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            string JsonData = JsonConvert.SerializeObject(Model.GuestCatalogSingleton.Instance.Guests);
            await FileIO.WriteTextAsync(localFile, JsonData);
        }


        public static async Task<ObservableCollection<Model.Guest>> LoadEventsFromJsonAsync()
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            String jsonData = await FileIO.ReadTextAsync(localFile);
            return JsonConvert.DeserializeObject<ObservableCollection<Model.Guest>>(jsonData);
        }

        public void IndsætJson(string filnavn)
        {
            List<Model.Guest> nyListe = JsonConvert.DeserializeObject<List<Model.Guest>>(filnavn);
            foreach (var i in nyListe)
            {
                Model.GuestCatalogSingleton.Instance.Guests.Add(i);
            }
        }




    }
}
