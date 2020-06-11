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
    public class UserBookingsViewModel : BaseViewModel
    {

        #region Request
        public ElegirRequest<BaseModel> GetReservas { get; set; }


        #endregion Request 

        #region Commands

        public ICommand SelectServiceCommand { get; set; }

        public ICommand CarteraCommand { get; set; }

        public ICommand UsuarioInfoCommand { get; set; }

        public ICommand SelectHospedajeServiceCommand { get; set; }

        #endregion Commands

        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;

        public NavigationService NavigationService { get; set; }

        private ObservableCollection<ReservasSimpleModel> reservas;

        private ReservasSimpleModel reservaActual;


        #endregion Properties

        #region Getters & Setters

        public ReservasSimpleModel ReservaActual
        {
            get { return reservaActual; }
            set
            {
                reservaActual = value;
                OnPropertyChanged();
            }
        }

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

        public ObservableCollection<ReservasSimpleModel> Reservas
        {
            get { return reservas; }
            set
            {
                reservas = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters/Setters

        #region Initialize
        public UserBookingsViewModel()
        {
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(Cartera);
            Reservas = new ObservableCollection<ReservasSimpleModel>();
            NavigationService = new NavigationService();
            InitializeCommands();
            InitializeRequest();
            ListaReserva();
        }

        public void InitializeRequest()
        {
            string urlReservasUser = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_RESERVAS_USER;
            GetReservas = new ElegirRequest<BaseModel>();
            GetReservas.ElegirEstrategia("GET", urlReservasUser);

        }

        public void InitializeCommands()
        {
            SelectServiceCommand = new Command(async () => await SelectService(), () => true);
            CarteraCommand = new Command(async () => await DisplayCartera(), () => true);
            SelectHospedajeServiceCommand = new Command(async () => await SelectHospedajeService(), () => true);
            UsuarioInfoCommand = new Command(async () => await DisplayUsuario(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            Usuario = usuario;
        }

        #endregion Initialize

        #region Methods

        public async Task ListaReserva()
        {
            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(Usuario.IdUsuario.ToString());
                APIResponse response = await GetReservas.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    List<ReservasSimpleModel> listaReservas = JsonConvert.DeserializeObject<List<ReservasSimpleModel>>
                        (response.Response);
                    Reservas = new ObservableCollection<ReservasSimpleModel>(listaReservas);
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
            await ((BaseViewModel)viewModel).ConstructorAsync2(Usuario, ReservaActual);
            await PopupNavigation.Instance.PushAsync(popUp);
        }


        public async Task SelectService()
        {
            ServiceInfoViewPop popUp = new ServiceInfoViewPop();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync2(Usuario, ReservaActual);
            await PopupNavigation.Instance.PushAsync(popUp);
        }


        public async Task DisplayCartera()
        {
            CarteraView popUp = new CarteraView();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync(Usuario);
            await PopupNavigation.Instance.PushAsync(popUp);
        }

        public async Task DisplayUsuario()
        {
            InfoUserView popUp = new InfoUserView();
            var viewModel = popUp.BindingContext;
            await ((BaseViewModel)viewModel).ConstructorAsync(Usuario);
            await PopupNavigation.Instance.PushAsync(popUp);
        }
        #endregion Methods
    }
}
