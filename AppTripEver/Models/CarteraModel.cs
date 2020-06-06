using AppTripEver.Services.Propagation;
using Newtonsoft.Json;

namespace AppTripEver.Models
{
    public class CarteraModel : BaseModel
    {
        #region Properties

        [JsonProperty("IdCartera")]
        private long idCartera { get; set; }

        [JsonIgnore]
        public UsuarioModel Usuario { get; set; }

        [JsonProperty("Monto")]
        private int montoTotal {get; set;}
        #endregion Properties 

        #region Initialize 
        public CarteraModel() { }
        #endregion Initialize 

        #region Getters & Setters


        [JsonIgnore]
        public int MontoTotal
        {
            get { return montoTotal; }
            set
            {
                montoTotal = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public long IdCartera
        {
            get { return idCartera; }
            set 
            {
                idCartera = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters
    }
}