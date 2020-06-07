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

namespace AppTripEver.ViewModels
{
    public class ChooseViewModel : BaseViewModel
    {
        #region Request
        public ElegirRequest<BaseModel> GetUsuarioHost { get; set; }

        #endregion Request 

        #region Commands
        public ICommand UsuarioHostCommand { get; set; }
        public ICommand UsuarioCommand { get; set; }


        #endregion Commands

        #region Properties


        private UsuarioModel usuario;

        private CarteraModel cartera;

        private UsuarioHostModel host;

        public NavigationService NavigationService { get; set; }


        #endregion Properties

        #region Getters & Setters

        public UsuarioModel Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
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

        public UsuarioHostModel Host
        {
            get { return host; }
            set
            {
                host = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters/Setters

        #region Initialize
        public ChooseViewModel()
        {
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(Cartera);
            Host = new UsuarioHostModel(Cartera);
            NavigationService = new NavigationService();
            InitializeRequest();
            InitializeCommands();

        }

        public void InitializeRequest()
        {
            string urlUsuarioHost = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_USUARIO_HOST;

            GetUsuarioHost = new ElegirRequest<BaseModel>();
            GetUsuarioHost.ElegirEstrategia("GET", urlUsuarioHost);

        }
        public void InitializeCommands()
        {
            UsuarioHostCommand = new Command(async () => await GetHost(), () => true);
            UsuarioCommand = new Command(async () => await OpenServices(), () => true);

        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            var host = parameters as UsuarioHostModel;
            Usuario = usuario;
            Host = host;
        }

        #endregion Initialize

        #region Methods
        public async Task OpenServices()
        {
            await NavigationService.PushPage(new UsuarioTabbedView(), Usuario);
        }

        public async Task GetHost()
        {
            await NavigationService.PushPage(new HostTabbedView(), Host);

        }
    }

    #endregion Methods
}
