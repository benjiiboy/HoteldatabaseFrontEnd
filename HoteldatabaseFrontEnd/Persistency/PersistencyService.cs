using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using HoteldatabaseFrontEnd.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HoteldatabaseFrontEnd.Persistency
{
   public class PersistencyService
    {
        const string serverUrl = "http://hotelws.azurewebsites.net";

        public ObservableCollection<Guest> Guests { get; set; }

        public static async void SaveGuestAsJsonAsync()
        {

        }

        public static ObservableCollection<Model.Guest> LoadEventsFromJsonAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Clear();

                string urlstring = "api/guests";

                try
                {
                    HttpResponseMessage response = client.GetAsync(urlstring).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var guestsList =  response.Content.ReadAsAsync<ObservableCollection<Guest>>().Result;

                         return guestsList;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return null;
            }
        }







        #region gammel kode for gem og hent lokalt
        private readonly Task<StorageFile> Localfolder;
        private object filnavn;


        const string fileName = "hoteldbGuest.json";

        public static async void SaveGuestAsJsonAsyncgammel()
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            string JsonData = JsonConvert.SerializeObject(Model.GuestCatalogSingleton.Instance.Guests);
            await FileIO.WriteTextAsync(localFile, JsonData);
        }


        public static async Task<ObservableCollection<Model.Guest>> LoadEventsFromJsonAsyncgammel()
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
        #endregion



    }
}
