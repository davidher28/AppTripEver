using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTripEver.Models;
using AppTripEver.Services.APIRest;
using AppTripEver.Services.Navigation;

namespace AppTripEver.ViewModels
{
    public class ServiceInformationViewModel : BaseViewModel
    {
        #region Request
        public ElegirRequest<BaseModel> PostReserva { get; set; }

        #endregion Request 

        #region Commands
        public ICommand BookServiceCommand { get; set; }
        public ICommand ReturnCommand { get; set; }

        #endregion Commands

        #region Properties


        private UsuarioModel usuario;

        private CarteraModel cartera;

        private ServiciosModel service;

        private HorarioModel horario;

        private MessageModel message;

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

        public ServiciosModel Service
        {
            get { return service; }
            set
            {
                service = value;
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

        #endregion Getters/Setters

        #region Initialize
        public ServiceInformationViewModel()
        {
            Cartera = new CarteraModel();
            Usuario = new UsuarioModel(Cartera);
            Service = new ServiciosModel(Horario, Host);
            NavigationService = new NavigationService();
            InitializeRequest();
            InitializeCommands();

        }

        public void InitializeRequest()
        {


        }
        public void InitializeCommands()
        {


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

        #endregion
    }
}
