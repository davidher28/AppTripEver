﻿using AppTripEver.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTripEver.Services.Navigation
{
    public class NavigationService
    {
        public async Task PushPage(Page page, object parameter = null)
        {
            if (page != null)
            {
                var navigationPage = Application.Current.MainPage;
                var viewModel = page.BindingContext;
                if (parameter != null)
                {
                    await ((BaseViewModel)viewModel).ConstructorAsync(parameter);
                }
                NavigationPage wrapper = new NavigationPage(page);
                await ((NavigationPage)navigationPage).PushAsync(wrapper);
            }
        }
    }
}
