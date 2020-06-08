using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;
using AppTripEver.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class ServicesViewModel : BaseViewModel
    {

        #region Request
        public ElegirRequest<BaseModel> GetServicios { get; set; }

        #endregion Request 

        #region Commands
        public ICommand CrearHostCommand { get; set; }

        #endregion Commands

        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;

        public NavigationService NavigationService { get; set; }

        private ObservableCollection<ServiciosModel> servicios;

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

        public ObservableCollection<ServiciosModel> Servicios
        {
            get { return servicios; }
            set
            {
                servicios = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters/Setters

        #region Initialize
        public ServicesViewModel()
        {
            Servicios = new ObservableCollection<ServiciosModel>();
            NavigationService = new NavigationService();
            InitializeCommands();
            InitializeRequest();
            ListaServicios();
        }

        public void InitializeRequest()
        {
            string urlServicios = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_ALL_SERVICIOS;

            GetServicios = new ElegirRequest<BaseModel>();
            GetServicios.ElegirEstrategia("GET", urlServicios);
        }

        public void InitializeCommands()
        {
            CrearHostCommand = new Command(async () => await CrearUsuario(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            Usuario = usuario;
        }

        #endregion Initialize

        #region

        public async Task ListaServicios()
        {
            try
            {
                APIResponse response = await GetServicios.EjecutarEstrategia(null);
                if (response.IsSuccess)
                {
                    List<ServiciosModel> listaServicios = JsonConvert.DeserializeObject<List<ServiciosModel>>
                        (response.Response);
                    Servicios = new ObservableCollection<ServiciosModel>(listaServicios);
                }
                else
                {

                }
            }
            catch (Exception)
            {

            }
        }


        public async Task CrearUsuario()
        {
            await NavigationService.PushPage(new RegistroHostView(), Usuario);
        }
        #endregion Methods
    }
}
