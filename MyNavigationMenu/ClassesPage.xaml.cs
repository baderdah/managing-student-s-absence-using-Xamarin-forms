using System.Collections.ObjectModel;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class ClassesPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Classe> classeObsColl;

        public ClassesPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        }

        async protected override void OnAppearing()
        {
            await _connection.CreateTableAsync<Classe>();

            var classes = await _connection.Table<Classe>().ToListAsync();

            classeObsColl = new ObservableCollection<Classe>(classes);
            classesListView.ItemsSource = classeObsColl;

            base.OnAppearing();

        }

        async void classesListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

            var selectedClasse = e.SelectedItem as Classe;
            if(selectedClasse != null)
            await Navigation.PushAsync(new ClasseDetailsPage(selectedClasse));
            classesListView.SelectedItem = null;

        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NewClassePage());
        }


        //delete classe 
        void MenuItem_Clicked(System.Object sender, System.EventArgs e)
        {

            var menuItem = sender as MenuItem;
            Classe selectedClasse = menuItem.CommandParameter as Classe;
            //operations op = new operations();
            DbOperations.deleteClasse(selectedClasse);

            classeObsColl.Remove(selectedClasse);
        }
    }
}
