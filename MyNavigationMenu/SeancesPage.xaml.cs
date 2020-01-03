using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class SeancesPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Seance> seanceObsColl;

        public SeancesPage()
        { 
            InitializeComponent();
        }
    }
}
