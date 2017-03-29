using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteldatabaseFrontEnd.Model
{
  public  class HotelCatalogSingleton
    {
        private ObservableCollection<Guest> guests;



        public ObservableCollection<Guest> Guests
        {
            get { return guests; }
            set { guests = value; }
        }

        private static HotelCatalogSingleton instance;


        public static HotelCatalogSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HotelCatalogSingleton();
                }
                return instance;
            }
        }
        public HotelCatalogSingleton()
        {
            Guests = new ObservableCollection<Guest>();
            
        }

        public void AddGuest(Guest g)
        {

        }

        public void RemoveGuest(Guest g)
        {

        }

        public void UpdateGuest(Guest g)
        {

        }



    }
}
