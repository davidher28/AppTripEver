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
        [JsonIgnore]
        public long IdUsuario { get; set; }

        [JsonProperty("Nombre")]
        private string nombre { get; set; }

        [JsonProperty("Mail")]
        private string email { get; set; }

        [JsonProperty("FechaNacimiento")]
        private string fechaNacimiento { get; set; }

        [JsonProperty("TipoIdentificacion")]
        private string tipoIdentificacion { get; set; }

        [JsonProperty("NoIdentificacion")]
        private string identificacion { get; set; }
        
        [JsonIgnore]
        public CarteraModel Cartera { get; set; }

        [JsonProperty("Telefono")]
        private string telefono { get; set; }

        [JsonProperty("Usuario")]
        private string nombreUsuario { get; set; }

        [JsonProperty("Contrasena")]
        private string contrasena { get; set; }
        
        [JsonIgnore]
        private List<ReservasModel> reservas { get; set; }
        
        [JsonIgnore]
        private List<ServiciosModel> favoritos { get; set; }

        [JsonProperty("Tipo")]
        private int isHost { get; set; }
        
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
        public string FechaNacimiento
           {
            get { return fechaNacimiento; }
            set
            {
                fechaNacimiento = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string TipoIdentificacion
        {
            get { return tipoIdentificacion; }
            set
            {
                tipoIdentificacion = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string Contrasena
        {
            get { return contrasena; }
            set
            {
                contrasena = value;
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
        public int IsHost
        {
            get { return isHost; }
            set
            {
                isHost = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string Identificacion
        {
            get { return identificacion; }
            set
            {
                identificacion = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set
            {
                nombreUsuario = value;
                OnPropertyChanged();
            }
        }

        

        #endregion 
    }
}
 