using System;
using Newtonsoft.Json;

namespace AppTripEver.Models
{
    public class ReservasSimpleModel : BaseModel
    {
        #region Properties
        public long IdReserva { get; set; }

        [JsonProperty("numPersonas")]
        public int NumPersonas { get; set; }
        public long IdEstado { get; set; }
        private string fechaInicio { get; set; }
        private string fechaFin { get; set; }
        private string valor { get; set; }
        private string numNoches { get; set; }
        private string titulo { get; set; }
        #endregion Properties

        public ReservasSimpleModel()
        {
        }

        #region Getters & Setters

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

        public string NumNoches
        {
            get { return numNoches; }
            set
            {
                numNoches = value;
                OnPropertyChanged();
            }
        }

        public string Valor
        {
            get { return valor; }
            set
            {
                valor = value;
                OnPropertyChanged();
            }
        }

        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                OnPropertyChanged();
            }
        }


        #endregion Getters & Setters
    }
}
