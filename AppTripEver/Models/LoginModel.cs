using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Models
{
    public class LoginModel : BaseModel
    {
        #region Properties
        private string nombre;

        private string contra;

        #endregion Properties

        #region Initialize

        public LoginModel()
        { }

        #endregion Initialize

        #region Getters & Setters

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }

        public string Contra
        {
            get { return contra; }
            set
            {
                contra = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters


    }
}
