using AppTripEver.Models;
using AppTripEver.Services.Navigation;
using AppTripEver.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripEver.ViewModels
{
    public class HostViewModel : BaseViewModel
    {

        #region Request


        #endregion Request 

        #region Commands

        public ICommand CrearServicioCommand { get; set; }
        #endregion Commands


        #region Properties

        private CarteraModel cartera;

        private UsuarioHostModel host;

        public NavigationService NavigationService { get; set; }

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

        #endregion Getters/Setters

        #region Initialize
        public HostViewModel()
        {

            Cartera = new CarteraModel();
            Host = new UsuarioHostModel(Cartera);
            InitializeCommands();
            NavigationService = new NavigationService(); 
        }

        public void InitializeCommands()
        {
            CrearServicioCommand = new Command(async () => await CrearServicio(), () => true);
        }

        public override async Task ConstructorAsync(object parameters)
        {
            var host = parameters as UsuarioHostModel;
            Host = host;
            Console.WriteLine(Host.Nombre);
        }

        #endregion Initialize

        #region Methods
        public async Task CrearServicio()
        {
            await NavigationService.PushPage(new CrearServicioView(),Host);
        }

        #endregion Methods
    }
}
