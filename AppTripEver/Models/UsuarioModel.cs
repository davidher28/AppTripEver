using AppTripEver.Services.Propagation;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AppTripEver.Models
{
    public class UsuarioModel : BaseModel
    {
        #region Properties
        [JsonProperty ("IdUsuario")]
        public long IdUsuario { get; set; }

        [JsonProperty("Nombre")]
        private string nombre { get; set; }

        [JsonIgnore]
        private string email { get; set; }

        [JsonIgnore]
        public string FechaNacimiento { get; set; }

        [JsonIgnore]
        public string TipoIdentificacion { get; set; }

        [JsonIgnore]
        public string Identificacion { get; set; }
        
        [JsonIgnore]
        public CarteraModel Cartera { get; set; }

        [JsonIgnore]
        private int telefono { get; set; }

        [JsonIgnore]
        public string NombreUsuario { get; set; }

        [JsonIgnore]
        private string contraseña { get; set; }
        
        [JsonIgnore]
        private List<ReservasModel> reservas { get; set; }
        
        [JsonIgnore]
        private List<ServiciosModel> favoritos { get; set; }

        [JsonIgnore]
        private bool isHost { get; set; }
        
        [JsonIgnore]
        public TarjetasRegaloModel Comprador { get; set; }
        
        [JsonIgnore]
        public TarjetasRegaloModel Destinatario { get; set; }

        #endregion Properties

        #region Initialize
        public UsuarioModel(CarteraModel Cartera) 
        {
            this.Cartera = Cartera;
        }
        #endregion Initialize

        #region Getters & Setters

        [JsonIgnore]
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public int Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string Contraseña
        {
            get { return contraseña; }
            set
            {
                contraseña = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public List<ReservasModel> Reservas
        {
            get { return reservas; }
            set
            {
                reservas = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]     
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
 