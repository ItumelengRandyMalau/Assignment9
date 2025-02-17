using Assignment9.DB_Services;
using Assignment9.Views;
namespace Assignment9
{
    public partial class App : Application
    {
        public static DatabaseServices Database { get; private set; }

        public App(string dbPath)
        {
            InitializeComponent();
            Database = new DatabaseServices(dbPath);
            MainPage = new NavigationPage(new ShoppingListPage()); // Landing Page
        }
    }
}
