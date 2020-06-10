using AppTripEver.Services.Propagation;
using System.Collections.Generic;

namespace AppTripEver.Models
{
    public class ReservasModel : BaseModel
    {
        #region Properties
        public long IdReservas { get; set; }
        public int NumPersonas { get; set; }
        private EstadoModel estado { get; set; }
        public UsuarioModel Cliente { get; set; }
        public ServiciosModel Servicio { get; set; }
        private string fechaInicio { get; set; }
        private string fechaFin { get; set; }
        public int valor { get; set; }
        public int numNoches { get; set; }
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
        #endregion Getters & Setters
    }
}