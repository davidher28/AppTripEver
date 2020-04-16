using AppTripEver.Services.Propagation;
using System.Collections.Generic;

namespace AppTripEver.Models
{
    public class ReservasModel : NotificationObject
    {
        #region Properties
        public long ID { get; set; }    
        public int NumPersonas { get; set; }
        private EstadoModel estado { get; set; }
        public UsuarioClienteModel Cliente { get; set; }
        public ServiciosModel Servicio { get; set; }
        #endregion Properties

        #region Initialize
        public ReservasModel(EstadoModel estado, ServiciosModel Servicio, UsuarioClienteModel Cliente)
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
        #endregion Getters & Setters
    }
}