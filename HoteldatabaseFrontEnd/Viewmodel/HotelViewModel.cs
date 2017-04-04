using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoteldatabaseFrontEnd.Model;
using System.Windows.Input;

namespace HoteldatabaseFrontEnd.Viewmodel
{
    public class HotelViewModel : INotifyPropertyChanged
    { 

        public HotelCatalogSingleton HotelCatalogSingleton { get; set; }
        private int guest_no;
        // 
        public int Guest_No
        {
            get { return guest_no; }
            set { guest_no = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        
        private Guest selectedguest;

        public Guest SelectedGuest
        {
            get { return selectedguest; }
            set { selectedguest = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
