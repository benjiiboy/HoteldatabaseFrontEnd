using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoteldatabaseFrontEnd.Model;
using System.Windows.Input;
using HoteldatabaseFrontEnd.Common;

namespace HoteldatabaseFrontEnd.Viewmodel
{
    public class GuestViewModel : INotifyPropertyChanged
    { // test

        public GuestCatalogSingleton GuestCatalogSingleton { get; set; }

        private int guest_no;

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
            set { selectedguest = value; OnPropertyChanged(nameof(SelectedGuest)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ICommand createGuestCommand;

        public ICommand CreateGuestCommand
        {
            get { return createGuestCommand; }
            set { createGuestCommand = value; }
        }

        private ICommand deleteGuestCommand;

        public ICommand DeleteGuestCommand
        {
            get { return deleteGuestCommand; }
            set { deleteGuestCommand = value; }
        }

        private ICommand updateGuestCommand;

        public ICommand UpdateGuestCommand
        {
            get { return updateGuestCommand; }
            set { updateGuestCommand = value; }
        }

        public Handler.GuestHandler GuestHandler {get; set;}

        public GuestViewModel()
        {
            GuestHandler = new HoteldatabaseFrontEnd.Handler.GuestHandler(this);
            GuestCatalogSingleton = Model.GuestCatalogSingleton.Instance;

            CreateGuestCommand = new RelayCommand(GuestHandler.CreateGuest);
            DeleteGuestCommand = new RelayCommand(GuestHandler.DeleteGuest);
            UpdateGuestCommand = new RelayCommand(GuestHandler.UpdateGuestRigtig);
        }

    }
}
