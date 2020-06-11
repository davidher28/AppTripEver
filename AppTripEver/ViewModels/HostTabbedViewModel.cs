using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;
using AppTripEver.Validation;
using AppTripEver.Validation.Rules;
using AppTripEver.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppTripEver.Behaviors;
using System.Threading;

namespace AppTripEver.ViewModels
{
    public class HostTabbedViewModel : BaseViewModel
    {
        #region Request


        #endregion Request 

        #region Commands

        #endregion Commands

        #region Properties

        private UsuarioHostModel host;

        private CarteraModel cartera;

        public HostViewModel HostViewModel { get; set; }

        public CrearServicioViewModel CrearServicioViewModel { get; set; }

        public HostBookingsViewModel HostBookingsViewModel { get; set; }

        public NavigationService NavigationService { get; set; }

        #endregion Properties

        #region Getters & Setters
        public UsuarioHostModel Host
        {
            get { return host; }
            set
            {
                host = value;
                OnPropertyChanged();
            }
        }

        public CarteraModel Cartera
        {
            get { return cartera; }
            set
            {
                cartera = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters/Setters

        #region Initialize
        public HostTabbedViewModel()
        {
            //InitializeCommands();
            NavigationService = new NavigationService();
            HostViewModel = new HostViewModel();
            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);
            CrearServicioViewModel = new CrearServicioViewModel();
            HostBookingsViewModel = new HostBookingsViewModel();
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioHostModel;
            Host = usuario;
            await HostViewModel.ConstructorAsync(Host);
            await CrearServicioViewModel.ConstructorAsync(Host);
            await HostBookingsViewModel.ConstructorAsync(Host);
        }

        #endregion Initialize

        #region Methods

        #endregion Methods

    }
}
