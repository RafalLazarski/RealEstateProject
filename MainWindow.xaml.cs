using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RealEstateProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<User> UsersList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            SetVisibilityOnFirstLoad();
            this.UsersList = new List<User>();
            this.UsersList.Add(new User("admin", "admin", "Administrator", "", new UserPreferences(RealEstate.Types.Flat, RealEstate.Cities.BuenosAires, RealEstate.Markets.Secondary)));
        }

        private void SetVisibilityOnFirstLoad()
        {
            this.textBlockName.Visibility = Visibility.Collapsed;
            this.textBlockSurname.Visibility = Visibility.Collapsed;
            this.textBoxName.Visibility = Visibility.Collapsed;
            this.textBoxSurname.Visibility = Visibility.Collapsed;
        }


        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            this.textBlockNotifications.Text = null;
            if (this.textBoxName.IsVisible)
            {
                this.ButtonLogin.Content = "Zaloguj się";
                this.ButtonRegister.Content = "Nie mam jeszcze konta, zarejestruj mnie!";
                SetVisibilityOnFirstLoad();
            }
            else
            {
                var currentUser = this.UsersList.Where(x => x.Login == this.textBoxLogin.Text && x.Password == this.textBoxPassword.Text);
                if (currentUser.Any())
                {
                    this.Hide();
                    MainMenu menu = new MainMenu(currentUser.FirstOrDefault().UserID, this.UsersList);
                    menu.ButtonLogoutClick += (_sender, _e) =>
                    {
                        this.textBlockNotifications.Text = "Pomyślnie wylogowano.";
                        this.Show();
                    };
                    menu.Show();
                }

                else
                    this.textBlockNotifications.Text = "Podano błędne dane logowania.";
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            this.textBlockNotifications.Text = null;
            if (!this.textBoxName.IsVisible)
            {
                this.ButtonLogin.Content = "Mam już konto, zaloguj mnie!";
                this.ButtonRegister.Content = "Zarejestruj się";
                this.textBlockName.Visibility = Visibility.Visible;
                this.textBlockSurname.Visibility = Visibility.Visible;
                this.textBoxName.Visibility = Visibility.Visible;
                this.textBoxSurname.Visibility = Visibility.Visible;
            }
            else
            {

                if (this.Validate())
                {
                    this.UsersList.Add(new User(this.textBoxLogin.Text, this.textBoxPassword.Text, this.textBoxName.Text, this.textBoxSurname.Text, null));
                    this.textBlockName.Visibility = Visibility.Collapsed;
                    this.textBlockSurname.Visibility = Visibility.Collapsed;
                    this.textBoxName.Visibility = Visibility.Collapsed;
                    this.textBoxSurname.Visibility = Visibility.Collapsed;
                    this.ButtonLogin.Content = "Zaloguj się";
                    this.ButtonRegister.Content = "Nie mam jeszcze konta, zarejestruj mnie!";
                    this.textBlockNotifications.Text = "Pomyślnie zarejestrowano nowego użytkownika.";
                }
            }
        }

        private bool Validate()
        {
            if (this.UsersList.Where(x => x.Login == this.textBoxLogin.Text).Any())
            {
                this.textBlockNotifications.Text = "Istnieje już użytkownik o podanym loginie";
                return false;
            }

            if (String.IsNullOrEmpty(this.textBlockLogin.Text) || String.IsNullOrEmpty(this.textBoxPassword.Text) ||
                    String.IsNullOrEmpty(this.textBoxName.Text) || String.IsNullOrEmpty(this.textBoxSurname.Text))
            {
                this.textBlockNotifications.Text = "Nalezy uzupełnić wszystkie pola.";
                return false;
            }
            return true;
        }
    }
}
