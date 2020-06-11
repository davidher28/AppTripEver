using AppTripEver.Services.Propagation;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppTripEver.Models
{
    public class ReservasModel : BaseModel
    {
        #region Properties
        [JsonProperty("IdReserva")]
        public long IdReservas { get; set; }

        [JsonProperty("numPersonas")]
        public int NumPersonas { get; set; }

        [JsonIgnore]
        private EstadoModel estado { get; set; }

        [JsonIgnore]
        public UsuarioModel Cliente { get; set; }

        [JsonIgnore]
        public ServiciosModel Servicio { get; set; }

        [JsonProperty("fechaInicio")]
        private string fechaInicio { get; set; }

        [JsonProperty("fechaFin")]
        private string fechaFin { get; set; }

        [JsonProperty("valor")]
        private int valor { get; set; }

        [JsonProperty("numNoches")]
        private int numNoches { get; set; }

        [JsonProperty("titulo")]
        public string Titulo { get; set; }

        #endregion Properties

        #region Initialize
        public ReservasModel(EstadoModel estado, ServiciosModel Servicio, UsuarioModel Cliente)
        {
            this.estado = estado;
            this.Servicio = Servicio;
            this.Cliente = Cliente;
        }
        #endregion Initialize

        #region Getters & Setters
        public EstadoModel Estado
        {
            get { return estado; }
            set
            {
                estado = value;
                OnPropertyChanged();
            }
        }

        public string FechaInicio
        {
            get { return fechaInicio; }
            set
            {
                fechaInicio = value;
                OnPropertyChanged();
            }
        }

        public string FechaFin
        {
            get { return fechaFin; }
            set
            {
                fechaFin = value;
                OnPropertyChanged();
            }
        }

        public int NumNoches
        {
            get { return numNoches; }
            set
            {
                numNoches = value;
                OnPropertyChanged();
            }
        }

        public int Valor
        {
            get { return valor; }
            set
            {
                valor = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters
    }
}