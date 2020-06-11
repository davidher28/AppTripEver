using System;
namespace AppTripEver.Models
{
    public class ReservasSimpleModel : BaseModel
    {
        #region Properties
        public long IdReserva { get; set; }
        public int NumPersonas { get; set; }
        public long Idestado { get; set; }
        private string fechaInicio { get; set; }
        private string fechaFin { get; set; }
        private int valor { get; set; }
        private int numNoches { get; set; }
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
