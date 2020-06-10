using System;
using System.Threading.Tasks;
using AppTripEver.Models;

namespace AppTripEver.ViewModels
{
    public class CheckOutViewModel : BaseViewModel
    {
        #region Properties

        private UsuarioModel usuario;

        private CarteraModel cartera;

        private ReservasModel booking;

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

        public ReservasModel Booking
        {
            get { return booking; }
            set
            {
                booking = value;
                OnPropertyChanged();
            }
        }


        #endregion Getters/Setters

        public CheckOutViewModel()
        {
        }

        public override async Task ConstructorAsync2(object parameters, object parameters2)
        {
            var usuario = parameters as UsuarioModel;
            var booking = parameters2 as ReservasModel;
            Usuario = usuario;
            Booking = booking;
        }
    }
}
