namespace AppTripEver.Models
{
    public class ReseñasModel
    {
        #region Properties
        public long ID { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public UsuarioModel Usuario { get; set; }
        public ServiciosModel Servicio { get; set; }
        #endregion Properties 

        #region Initialize 
        #endregion Initialize

        #region Getters & Setters
        #endregion Getters & Setters 
    }
}