using System;

using Xamarin.Forms;

namespace MyNavigationMenu
{
    public class NewStudentPage : ContentPage
    {
        public NewStudentPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

