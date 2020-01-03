
using System.Collections.ObjectModel;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class LessonsPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Lesson> lessonObsColl;

        public LessonsPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        async protected override void OnAppearing()
        {
            await _connection.CreateTableAsync<Lesson>();

            var lessons = await _connection.Table<Lesson>().ToListAsync();

            lessonObsColl = new ObservableCollection<Lesson>(lessons);
            lessonsListView.ItemsSource = lessonObsColl;

            base.OnAppearing();

        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NewLessonPage());
        }

        void MenuItem_Clicked(System.Object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            Lesson selectedLesson = menuItem.CommandParameter as Lesson;

            DbOperations.deleteLesson(selectedLesson);

            lessonObsColl.Remove(selectedLesson);
        }
    }
}
