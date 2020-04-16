using AppTripEver.Services.Propagation;

namespace AppTripEver.Models
{
    public class EstadoModel : NotificationObject
    {
        #region Properties
        public long ID { get; set; }
        public string Estado { get; set; }

        /* public ReservasModel Reserva { get; set; } Creo que esto no va, fue lo que no pregunté */
        #endregion Properties

        #region Initialize
        public EstadoModel() { }
        #endregion Initialize

        #region Getters & Setters 
        #endregion Getters & Setters 
    }
}