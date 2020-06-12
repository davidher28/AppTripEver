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


        private string labelTipo;

        private string labelHoraI;

        private string labelHoraF;

        private string labelFechaI;

        private string labelFechaF;


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

        public string LabelTipo
        {
            get { return labelTipo; }
            set
            {
                labelTipo = value;
                OnPropertyChanged();
            }
        }

        public string LabelHoraI
        {
            get { return labelHoraI; }
            set
            {
                labelHoraI = value;
                OnPropertyChanged();
            }
        }

        public string LabelHoraF
        {
            get { return labelHoraF; }
            set
            {
                labelHoraF = value;
                OnPropertyChanged();
            }
        }

        public string LabelFechaI
        {
            get { return labelFechaI; }
            set
            {
                labelFechaI = value;
                OnPropertyChanged();
            }
        }

        public string LabelFechaF
        {
            get { return labelFechaF; }
            set
            {
                labelFechaF = value;
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
            if (Service.TipoServicio == 1)
            {
                LabelTipo = "Noche";
                LabelHoraI = "Check In:";
                LabelHoraF = "Check Out:";
                LabelFechaI = "Disponible Desde:";
                LabelFechaF = "Disponible Hasta:";
            }
            else
            {
                LabelTipo = "Persona:";
                LabelHoraI = "Hora Inicio:";
                LabelHoraF = "Hora Fin:";
                LabelFechaI = "Inicia:";
                LabelFechaF = "Finaliza:";
            }
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
                Message.Message = "Servicio editado correctamente";
                PopGeneralView view = new PopGeneralView();
                var context = view.BindingContext;
                await ((BaseViewModel)context).ConstructorAsync(Message);
                await PopupNavigation.Instance.PushAsync(view);
            }
            else
            {
                Message.Message = "No se realizó la actualización, intentalo de nuevo";
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
