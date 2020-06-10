using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;
using AppTripEver.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
        public ElegirRequest<BaseModel> GetServiciosHospedaje { get; set; }
        public ElegirRequest<BaseModel> GetServiciosExperiencia { get; set; }

        #endregion Request 

        #region Commands
        public ICommand CrearHostCommand { get; set; }

        public ICommand SelectServiceCommand { get; set; }

        public ICommand SelectHospedajeServiceCommand { get; set; }

        #endregion Commands

        #region Properties

        private UsuarioModel usuario;

        private UsuarioHostModel usuarioHost;

        private CarteraModel cartera;

        private HorarioModel horario;

        public NavigationService NavigationService { get; set; }

        private ObservableCollection<ServiciosModel> serviciosHospedaje;

        private ObservableCollection<ServiciosModel> serviciosExperiencia;

        private ServiciosModel servicioActual;

        private ServiciosModel servicioExperienciaActual;

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

        public UsuarioHostModel UsuarioHost
        {
            get { return usuarioHost; }
            set
            {
                usuarioHost = value;
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

        public HorarioModel Horario
        {
            get { return horario; }
            set
            {
                horario = value;
                OnPropertyChanged();
            }
        }

        public ServiciosModel ServicioActual
        {
            get { return servicioActual; }
            set
            {
                servicioActual = value;
                OnPropertyChanged();
            }
        }
        public ServiciosModel ServicioExperienciaActual
        {
            get { return servicioExperienciaActual; }
            set
            {
                servicioExperienciaActual = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ServiciosModel> ServiciosHospedaje
        {
            get { return serviciosHospedaje; }
            set
            {
                serviciosHospedaje = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ServiciosModel> ServiciosExperiencia
        {
            get { return serviciosExperiencia; }
            set
            {
                serviciosExperiencia = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters/Setters

        #region Initialize
        public ServicesViewModel()
        {
            Cartera = new CarteraModel();
            Horario = new HorarioModel();
            UsuarioHost = new UsuarioHostModel(Cartera);
            Usuario = new UsuarioModel(Cartera);
            ServiciosHospedaje = new ObservableCollection<ServiciosModel>();
            ServiciosExperiencia = new ObservableCollection<ServiciosModel>();
            NavigationService = new NavigationService();
            InitializeCommands();
            InitializeRequest();
            ListaServiciosHospedaje();
            ListaServiciosExperiencia();
        }

        public void InitializeRequest()
        {
            string urlServiciosId = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_ALL_SERVICIOS_ID;
            GetServiciosHospedaje = new ElegirRequest<BaseModel>();
            GetServiciosHospedaje.ElegirEstrategia("GET", urlServiciosId);

            GetServiciosExperiencia = new ElegirRequest<BaseModel>();
            GetServiciosExperiencia.ElegirEstrategia("GET", urlServiciosId);
        }

        public void InitializeCommands()
        {
            CrearHostCommand = new Command(async () => await CrearUsuario(), () => true);
            SelectServiceCommand = new Command(async () => await SelectHospedajeService(), () => true);
            SelectHospedajeServiceCommand = new Command(async () => await SelectExperienciaService(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            Usuario = usuario;
        }

        #endregion Initialize

        #region

        public async Task ListaServiciosHospedaje()
        {
            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add("1");
                APIResponse response = await GetServiciosHospedaje.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    List<ServiciosModel> listaServicios = JsonConvert.DeserializeObject<List<ServiciosModel>>
                        (response.Response);
                    ServiciosHospedaje = new ObservableCollection<ServiciosModel>(listaServicios);
                }
                else
                {

                }
            }
            catch (Exception)
            {

            }
        }

        public async Task ListaServiciosExperiencia()
        {
            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add("2");
                APIResponse response = await GetServiciosExperiencia.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    List<ServiciosModel> listaServicios = JsonConvert.DeserializeObject<List<ServiciosModel>>
                        (response.Response);
                    ServiciosExperiencia = new ObservableCollection<ServiciosModel>(listaServicios);
                }
                else
                {

                }
            }
            catch (Exception)
            {

            }
        }

        public async Task SelectHospedajeService()
        {
            ServiceInfoViewPop popUp = new ServiceInfoViewPop();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync2(Usuario, ServicioActual);
            await PopupNavigation.Instance.PushAsync(popUp);
        }

        public async Task SelectExperienciaService()
        {
            ServiceInfoViewPop popUp = new ServiceInfoViewPop();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync2(Usuario, ServicioExperienciaActual);
            await PopupNavigation.Instance.PushAsync(popUp);
        }

        public async Task CrearUsuario()
        {
            await NavigationService.PushPage(new RegistroHostView(), Usuario);
        }
        #endregion Methods
    }
}
