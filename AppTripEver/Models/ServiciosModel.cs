using AppTripEver.Services.Propagation;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppTripEver.Models
{
    public class ServiciosModel : BaseModel
    {
        #region Properties
        
        //[JsonProperty("IdServicio")]
        public long IdServicio { get; set; }

        //[JsonProperty("Titulo")]
        private string titulo { get; set; }

        //[JsonProperty("Pais")]
        public string Pais { get; set; }

        //[JsonProperty("Ciudad")]
        public string Ciudad { get; set; }

        [JsonProperty ("MaxPersonas")]
        private int numMaxPersonas { get; set; }

        //[JsonIgnore]
        public string Ubicacion { get; set; }

        //[JsonProperty("Descripcion")]
        private string descripcion { get; set; }

        //[JsonIgnore]
        private HorarioModel fecha { get; set; }

        //[JsonProperty("Precio")]
        private int precio { get; set; }

        //[JsonIgnore]
        private List<ResenasModel> reseñas { get; set; }

        //[JsonIgnore]
        public UsuarioHostModel Creador { get; set; }

        //[JsonIgnore]
        public ReservasModel Reserva { get; set; } 
        #endregion Properties

        #region Initialize
        public ServiciosModel(HorarioModel fecha, UsuarioHostModel Creador)
        {
            this.fecha = fecha;
            this.Creador = Creador;
        }
        #endregion Initialize

        #region Getters & Setters
        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                OnPropertyChanged();
            }
        }
        public int NumMaxPersonas
        {
            get { return numMaxPersonas; }
            set
            {
                numMaxPersonas = value;
                OnPropertyChanged();
            }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
                OnPropertyChanged();
            }
        }
        public HorarioModel Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
                OnPropertyChanged();
            }
        }
        public int Precio
        {
            get { return precio; }
            set
            {
                precio = value;
                OnPropertyChanged();
            }
        }
        public List<ResenasModel> Reseñas
        {
            get { return reseñas; }
            set
            {
                reseñas = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters 
    }
} 