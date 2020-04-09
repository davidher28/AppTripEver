using AppTripEver.Services.Propagation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Models
{
    public class UsuarioModel : NotificationObject
    {
        #region Properties
        public long ID { get; set; }
        public string Nombre { get; set; }
        private string email { get; set; }
        public string FechaNacimiento { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public CarteraModel Cartera { get; set; }
        private int telefono { get; set; }
        public string NombreUsuario { get; set; }
        private string contraseña { get; set; }
        private List<ReservasModel> reservas { get; set; }
        private List<FavoritosModel> favoritos { get; set; }
        private bool isHost { get; set; }
        #endregion Properties

        #region Getters & Setters
        public string Email
        {
            get { return Email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public int Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                OnPropertyChanged();
            }
        }
        public string Contraseña
        {
            get { return contraseña; }
            set
            {
                contraseña = value;
                OnPropertyChanged();
            }
        }
        public List<ReservasModel> Reservas
        {
            get { return reservas; }
            set
            {
                reservas = value;
                OnPropertyChanged();
            }
        }
        public bool IsHost
        {
            get { return isHost; }
            set
            {
                isHost = value;
                OnPropertyChanged();
            }
        }
        #endregion 
    }
}
