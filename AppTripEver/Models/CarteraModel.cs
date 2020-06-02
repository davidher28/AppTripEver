using AppTripEver.Services.Propagation;

namespace AppTripEver.Models
{
    public class CarteraModel : BaseModel
    {
        #region Properties
        public long IdCartera { get; set; }

        public UsuarioModel Usuario { get; set; }
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