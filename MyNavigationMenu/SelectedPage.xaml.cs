using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class SelectedPage : ContentPage
    {
        public SelectedPage(String name)
        {
            InitializeComponent();
            label.Text = name;
        }
    }
}
