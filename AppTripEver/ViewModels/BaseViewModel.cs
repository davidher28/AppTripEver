using AppTripEver.Services.Propagation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTripEver.ViewModels
{
    public class BaseViewModel : NotificationObject
    {
        public virtual async Task ConstructorAsync(object parameters)
        {
            await Task.FromResult(true);
        }
    }
}
