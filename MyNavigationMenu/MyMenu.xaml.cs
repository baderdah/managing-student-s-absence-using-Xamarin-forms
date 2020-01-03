using System;
using System.Collections.Generic;
using MyNavigationMenu.Model;
using Xamarin.Forms;


namespace MyNavigationMenu
{
    public partial class MyMenu : MasterDetailPage
    {
        public MyMenu(User user)
        {
            InitializeComponent();

            Detail = new NavigationPage(new ClassesPage());
            IsPresented = false;
        }

        void handleSelectedItem(object sender, System.EventArgs e)
        {
            var selectedPage = sender as Button;

            if (selectedPage.Text.Equals("Classes"))
            {
                Detail = new NavigationPage(new ClassesPage());
            }
            else if (selectedPage.Text.Equals("Lessons"))
            {
                Detail = new NavigationPage(new LessonsPage());
            }
            else if (selectedPage.Text.Equals("Seances"))
            {
                Detail = new NavigationPage(new SeancePage());
            }
            else
            {
                Detail = new NavigationPage(new SelectedPage(selectedPage.Text));
            }

            IsPresented = false;

        }
        void handleLogOut(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync();

        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}
