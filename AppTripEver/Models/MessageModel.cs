using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripEver.Models
{
    public class MessageModel : BaseModel
    {
        #region Properties
        private string message;
        #endregion Properties

        #region Getters & Setters
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public MessageModel()
        {

        }
        #endregion Getters & Setters
    }
}
