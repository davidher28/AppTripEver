using AppTripEver.Services.Propagation;

namespace AppTripEver.Models
{
    public class HorarioModel : NotificationObject
    {
        #region Properties
        public long ID { get; set; }
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
        #endregion Getters & Setters 
    }
}