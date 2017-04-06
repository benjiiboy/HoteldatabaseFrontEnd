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
using Windows.UI.Popups;

namespace HoteldatabaseFrontEnd.Persistency
{
   public class PersistencyService
    {
        const string serverUrl = "http://hotelws.azurewebsites.net";

        public ObservableCollection<Guest> Guests { get; set; }
        
        
        
        /*Opretter et objekt og lægger det i skyen + opdaterer tabel i view*/
        public static void SaveGuestAsJsonAsync(Guest g)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();

                try
                {
                    var response = client.PostAsJsonAsync<Guest>("api/guests", g).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("Du har oprettet en ny guest");
                    }
                    else
                    {
                        ShowMessage("FEJL, Guest blev ikke oprettet: " + response.StatusCode);
                    }
                }
                catch (Exception e)
                {

                    ShowMessage("Der er sket en fejl: " + e.Message);
                }
            }
        }
        
        
        /*Henter data fra sky og opdatere view*/
        public static ObservableCollection<Model.Guest> LoadGuestFromJsonAsync()
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

                    ShowMessage("Fejl under hetning af data");
                }
                return null;
            }
        }


        /*Sletning af et objekt og opdatere view*/
        public static void DeleteGuestFromJsonAsync(Guest guest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                string urlstring = "api/guests/" + guest.Guest_No;
                try
                {
                    HttpResponseMessage response = client.DeleteAsync(urlstring).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("Du har nu slettet den valgte gæst");
                    }
                    else
                    {
                        ShowMessage("Gæsten blev ikke slettet");
                    }
                }
                catch (Exception e)
                {

                    ShowMessage("Der er sket en fejl: " + e.Message);
                }
            }
        }
 



        /*kode til visning af fejl*/

            public static async void ShowMessage(string content)
        {
            MessageDialog messageDialog = new MessageDialog(content);
            await messageDialog.ShowAsync();
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
