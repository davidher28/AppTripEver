using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTripEver.Configuration;
using AppTripEver.Models;
using AppTripEver.Models.AuxiliarModels;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;
using AppTripEver.Validation;
using AppTripEver.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class ServiceEditViewModel : BaseViewModel
    {

        #region Request
        public ElegirRequest<BaseModel> EditService { get; set; }

        #endregion Request 

        #region Commands
        public ICommand EditCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        #endregion Commands

        #region Properties

        private CarteraModel cartera;

        private UsuarioHostModel host;

        private HorarioModel horario;

        private ServiciosModel service;

        private ReservasModel booking;

        private EstadoModel bookingState;

        private MessageModel message;

        public ValidatableObject<string> TituloServicio { get; set; }

        public ValidatableObject<Nullable<int>> NumMaxPersonas { get; set; }

        public ValidatableObject<string> Descripcion { get; set; }

        public ValidatableObject<Nullable<int>> Precio { get; set; }


        #endregion Properties

        #region Getters & Setters

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


        public HorarioModel Horario
        {
            get { return horario; }
            set
            {
                horario = value;
                OnPropertyChanged();
            }
        }

        public ServiciosModel Service
        {
            get { return service; }
            set
            {
                service = value;
                OnPropertyChanged();
            }
        }

        public MessageModel Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters/Setters

        #region Initialize
        public ServiceEditViewModel()
        {
            Horario = new HorarioModel();
            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);
            Service = new ServiciosModel(Horario, Host);
            Message = new MessageModel()
            {
                Message = "Servicio editado correctamente"
            };
            InitializeCommands();
            InitializeRequest();
            InitializeFields();
        }

        public void InitializeRequest()
        {
            string urlEditService = Endpoints.URL_SERVIDOR + Endpoints.EDITAR_SERVICIO;


            EditService = new ElegirRequest<BaseModel>();
            EditService.ElegirEstrategia("PUT", urlEditService);

        }

        public void InitializeFields()
        {
            TituloServicio = new ValidatableObject<string>();
            NumMaxPersonas = new ValidatableObject<Nullable<int>>();
            Descripcion = new ValidatableObject<string>();
            Precio = new ValidatableObject<Nullable<int>>();

    }

        public void InitializeCommands()
        {
            EditCommand = new Command(async () => await Editar(), () => true);
            CloseCommand = new Command(async () => await Close(), () => true);
        }

        public override async Task ConstructorAsync2(object parameters, object parameters2)
        {
            var host = parameters as UsuarioHostModel;
            var service = parameters2 as ServiciosModel;
            Host = host;
            Service = service;
        }

        #endregion Initialize

        #region Methods

        public async Task Editar()
        {
            JObject vals2 =
                new JObject(
                new JProperty("Titulo",  TituloServicio.Value ?? Service.Titulo),
                new JProperty("Pais", Service.Pais),
                new JProperty("Ciudad", Service.Ciudad),
                new JProperty("MaxPersonas", NumMaxPersonas.Value ?? Service.NumMaxPersonas),
                new JProperty("Descripcion", Descripcion.Value ?? Service.Descripcion),
                new JProperty("Precio", Precio.Value ?? Service.Precio),
                new JProperty("IdHost", Host.IdHost),
                new JProperty("IdTipoServicio", Service.TipoServicio)
                );
            string Json2 = vals2.ToString();
            ParametersRequest parametros = new ParametersRequest();
            parametros.Parametros.Add(Service.IdServicio.ToString());
            APIResponse response1 = await EditService.EjecutarEstrategia(Cartera, parametros, Json2);
            if (response1.IsSuccess)
            {
                PopGeneralView view = new PopGeneralView();
                var context = view.BindingContext;
                await ((BaseViewModel)context).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(view);
            }
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }
        #endregion Methods
    }
}
