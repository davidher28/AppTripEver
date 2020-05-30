using AppTripEver.Services.Propagation;

namespace AppTripEver.Models
{
    public class HorarioModel : BaseModel
    {
        #region Properties
        public long IdHorario { get; set; }
        public ServiciosModel Servicio { get; set; } 
        private string fechaInicio { get; set; }
        private string fechaFinal { get; set; }
        private string horaInicio { get; set; }
        private string horaFinal { get; set; }

        #endregion Properties

        #region Initialize 
        public HorarioModel() { }
        #endregion Initialize

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
        public string FechaFinal 
        {
            get { return fechaFinal; }
            set
            {
                fechaFinal = value;
                OnPropertyChanged();
            }
        }
        public string HoraInicio
        {
            get { return horaInicio; }
            set
            {
                horaInicio = value;
                OnPropertyChanged();
            }
        }
        public string HoraFinal
        {
            get { return horaFinal; }
            set
            {
                horaFinal = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters 
    }
}