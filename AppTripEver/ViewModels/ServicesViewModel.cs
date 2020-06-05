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
    public class ServicesViewModel : BaseViewModel
    {

        #region Request


        #endregion Request 

        #region Commands
        public ICommand CrearHostCommand { get; set; }

        #endregion Commands

        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;

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

        #endregion Getters/Setters

        #region Initialize
        public ServicesViewModel()
        {
            InitializeCommands();
            NavigationService = new NavigationService();
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

        #region Methods
        public async Task CrearUsuario()
        {
            await NavigationService.PushPage(new RegistroHostView(), Usuario);
        }
        #endregion Methods
    }
}
