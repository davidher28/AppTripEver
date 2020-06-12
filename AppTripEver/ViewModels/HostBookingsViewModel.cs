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
    public class HostBookingsViewModel : BaseViewModel
    {

        #region Request
        public ElegirRequest<BaseModel> GetReservas { get; set; }


        #endregion Request 

        #region Commands

        public ICommand SelectServiceCommand { get; set; }

        public ICommand CarteraCommand { get; set; }

        public ICommand UsuarioInfoCommand { get; set; }

        public ICommand SelectReservaCommand { get; set; }

        #endregion Commands

        #region Properties

        private UsuarioHostModel usuario;

        private CarteraModel cartera;

        public NavigationService NavigationService { get; set; }

        private ObservableCollection<ReservasModel> reservas;

        private ReservasModel reservaActual;

        private string imagen;

        private int listReservas;


        #endregion Properties

        #region Getters & Setters

        public ReservasModel ReservaActual
        {
            get { return reservaActual; }
            set
            {
                reservaActual = value;
                OnPropertyChanged();
            }
        }

        public string Imagen
        {
            get { return imagen; }
            set
            {
                imagen = value;
                OnPropertyChanged();
            }
        }

        public int ListReservas
        {
            get { return listReservas; }
            set
            {
                listReservas = value;
                OnPropertyChanged();
            }
        }

        public UsuarioHostModel Usuario
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

        public ObservableCollection<ReservasModel> Reservas
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
        public HostBookingsViewModel()
        {
            Cartera = new CarteraModel();
            Usuario = new UsuarioHostModel(Cartera);
            Reservas = new ObservableCollection<ReservasModel>();
            NavigationService = new NavigationService();
            InitializeCommands();
            InitializeRequest();

        }

        public void InitializeRequest()
        {
            string urlReservasUser = Endpoints.URL_SERVIDOR + Endpoints.CONSULTAR_RESERVAS_HOST;
            GetReservas = new ElegirRequest<BaseModel>();
            GetReservas.ElegirEstrategia("GET", urlReservasUser);

        }

        public void InitializeCommands()
        {
            CarteraCommand = new Command(async () => await DisplayCartera(), () => true);
            SelectReservaCommand = new Command(async () => await SelectReserva(), () => true);
            UsuarioInfoCommand = new Command(async () => await DisplayUsuario(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioHostModel;
            Usuario = usuario;
            ListaReserva();
        }

        #endregion Initialize

        #region Methods

        public async Task ListaReserva()
        {
            try
            {
                ParametersRequest parametros = new ParametersRequest();
                parametros.Parametros.Add(Usuario.IdHost.ToString());
                APIResponse response = await GetReservas.EjecutarEstrategia(null, parametros);
                if (response.IsSuccess)
                {
                    List<ReservasModel> listaReservas = JsonConvert.DeserializeObject<List<ReservasModel>>(response.Response);
                    Reservas = new ObservableCollection<ReservasModel>(listaReservas);
                    if (Reservas.Count == 0)
                    {
                        Imagen = "True";
                        ListReservas = 0;
                    }
                    else
                    {
                        Imagen = "False";
                        ListReservas = Reservas.Count * 200;
                    }
                }
                else
                {
                    if (Reservas.Count == 0)
                    {
                        Imagen = "True";
                        ListReservas = 0;
                    }
                    else
                    {
                        Imagen = "False";
                        ListReservas = Reservas.Count * 200;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task SelectReserva()
        {
            BookingInfoViewPop popUp = new BookingInfoViewPop();
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