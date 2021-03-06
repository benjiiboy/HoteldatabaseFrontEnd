﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoteldatabaseFrontEnd.Persistency;

namespace HoteldatabaseFrontEnd.Model
{
  public  class GuestCatalogSingleton
    {
        private ObservableCollection<Guest> guests;



        public ObservableCollection<Guest> Guests
        {
            get { return guests; }
            set { guests = value; }
        }

        private static GuestCatalogSingleton instance;


        public static GuestCatalogSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GuestCatalogSingleton();
                }
                return instance;
            }
        }
        public GuestCatalogSingleton()
        {
            Guests = new ObservableCollection<Guest>();
            HentJson();
            
        }

        public void AddGuest(Guest g)
        {
            Guests.Add(g);

        }

        public void RemoveGuest(Guest g)
        {
            Guests.Remove(g);
            PersistencyService.DeleteGuestFromJsonAsync(g);
        }

        public void UpdateGuest(Guest g)
        {
            //Magnler at lave en update
            Guests.Clear();
            PersistencyService.UpdateGuestListJsonAsync(g);
        }

        public void HentJson()
        {
            guests = Persistency.PersistencyService.LoadGuestFromJsonAsync();
        }



    }
}
