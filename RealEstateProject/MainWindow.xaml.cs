using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    public partial class MainWindow : Window
    {
        public List<User> UsersList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            SetVisibilityOnFirstLoad();
            this.UsersList = new List<User>();
            this.ImportUsersList();
        }

        private void ImportUsersList()
        {
            
            using (Stream stream = File.Open("UsersList.txt", FileMode.Open))
            {
                if (new FileInfo("UsersList.txt").Length != 0)
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    var users = (List<User>)bin.Deserialize(stream);
                    users.ForEach(x => this.UsersList.Add(x));
                    User.UsersCount = this.UsersList.Count();
                }
            }
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
            if (this.textBoxName.IsVisible)
            {
                this.ButtonLogin.Content = "Sign In";
                this.ButtonRegister.Content = "I don't have an account yet, register me!";
                SetVisibilityOnFirstLoad();
            }
            else
            {
                var currentUser = this.UsersList.Where(x => x.Login == this.textBoxLogin.Text && x.Password == this.passwordBox.Password);
                if (currentUser.Any())
                {
                    if (!currentUser.FirstOrDefault().Archive)
                    {
                        this.Hide();
                        MainMenu menu = new MainMenu(currentUser.FirstOrDefault().UserID, this.UsersList);
                        menu.ButtonLogoutClick += (_sender, _e) =>
                        {
                            this.Show();
                        };
                        menu.Show();
                    }
                    else
                        MessageBox.Show("Your account has been banned!");
                }
                else
                    MessageBox.Show("The login details are incorrect!");
            }
            this.passwordBox.Password = null;
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!this.textBoxName.IsVisible)
            {
                this.ButtonLogin.Content = "I already have an account, log me in!";
                this.ButtonRegister.Content = "Register";
                this.textBlockName.Visibility = Visibility.Visible;
                this.textBlockSurname.Visibility = Visibility.Visible;
                this.textBoxName.Visibility = Visibility.Visible;
                this.textBoxSurname.Visibility = Visibility.Visible;
            }
            else
            {

                if (this.Validate())
                {
                    this.UsersList.Add(new User(this.textBoxLogin.Text, this.passwordBox.Password, this.textBoxName.Text, this.textBoxSurname.Text, new UserPreferences(
                        new List<RealEstate.Types>(), new List<RealEstate.Cities>(), new List<RealEstate.Markets>())));
                    this.textBlockName.Visibility = Visibility.Collapsed;
                    this.textBlockSurname.Visibility = Visibility.Collapsed;
                    this.textBoxName.Visibility = Visibility.Collapsed;
                    this.textBoxSurname.Visibility = Visibility.Collapsed;
                    this.ButtonLogin.Content = "Sign In";
                    this.ButtonRegister.Content = "I do not have an account yet, register me!";
                    MessageBox.Show("New user successfully registered.");
                    this.textBoxName.Text = null;
                    this.textBoxSurname.Text = null;
                }
            }
            this.passwordBox.Password = null;
        }

        private bool Validate()
        {
            if (this.UsersList.Where(x => x.Login == this.textBoxLogin.Text).Any())
            {
                MessageBox.Show("There is already a user with the given login.");
                return false;
            }
            if (this.textBoxLogin.Text.Length < 3 || this.passwordBox.Password.Length < 3)
            {
                MessageBox.Show("Login and password must be at least 3 characters long!");
                return false;
            }
            if(String.IsNullOrEmpty(this.textBoxName.Text) || String.IsNullOrEmpty(this.textBoxSurname.Text))
            {
                MessageBox.Show("All fields must be completed!");
                return false;
            }
            return true;
        }

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            string msg = "Are you sure you want to close the application?";
            MessageBoxResult result =
              MessageBox.Show(
                msg,
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                using (Stream stream = File.Open("UsersList.txt", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, this.UsersList);
                }
            }
        }
    }
}
