using AppTripEver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTripEver.ViewModels
{
    public class ServicesViewModel : BaseViewModel
    {

        #region Request


        #endregion Request 

        #region Commands
        //public ICommand IniciarSesionCommand { get; set; }

        #endregion Commands

        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;


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

        }

        public override async Task ConstructorAsync(object parameters)
        {
            var usuario = parameters as UsuarioModel;
            Usuario = usuario;
        }

        #endregion Initialize

        #region Methods

        #endregion Methods
    }
}
