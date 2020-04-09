using AppTripEver.Services.Propagation;

namespace AppTripEver.Models
{
    public class CarteraModel : NotificationObject
    {
        #region Properties
        public long ID { get; set; }
        public UsuarioModel Usuario { get; set; }
        public UsuarioHostModel Usuario1 { get; set; }
        private int montoTotal {get; set;}
        #endregion Properties 

        #region Initialize 
        public CarteraModel() { }
        #endregion Initialize 

        #region Getters & Setters
        public int MontoTotal
        {
            get { return montoTotal; }
            set 
            {
                montoTotal = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters
    }
}