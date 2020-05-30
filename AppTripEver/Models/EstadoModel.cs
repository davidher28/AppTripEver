using AppTripEver.Services.Propagation;

namespace AppTripEver.Models
{
    public class EstadoModel : BaseModel
    {
        #region Properties
        public long IdEstado { get; set; }

        public string Estado { get; set; }

        public ReservasModel Reserva { get; set; } 
        #endregion Properties

        #region Initialize
        public EstadoModel() { }
        #endregion Initialize

        #region Getters & Setters 
        #endregion Getters & Setters 
    } 
}