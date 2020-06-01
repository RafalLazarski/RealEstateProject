using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace RealEstateProject
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {

        #region Fields & Constructors

        public List<RealEstate> RealEstatesList = new List<RealEstate>();
        public List<RealEstate> RealEstatesListTemp = new List<RealEstate>();

        //do produktów
        private RealEstate.Types[] FavouriteType { get; set; }
        private RealEstate.Markets[] FavouriteMarket { get; set; }
        private RealEstate.Cities[] FavouriteCity { get; set; }

        //do uzytkownika
        public bool FirstLoad { get; set; }
        public int CurrentUserID { get; set; }
        public List<User> UsersList { get; set; }
        public bool IsLoginChanging = false;
        public bool IsPasswordChanging = false;
        public delegate void ButtonLogoutDelegate(object sender, RoutedEventArgs e);
        public event ButtonLogoutDelegate ButtonLogoutClick;


        public MainMenu(int currentUserID, List<User> usersList)
        {
            this.SetOnFirstLoad();
            InitializeComponent();

            
            this.CurrentUserID = currentUserID;
            this.UsersList = usersList;            
            this.SetCurrentUserDetails();
            this.SetRealEstatesList();
            CheckUserPreferences();
            
        }

        #endregion

        #region Helper Methods

        private void SetOnFirstLoad()
        {
            //Kolejność sprawdzania:
            //Buenos, Moskwa, Białystok
            //Primary, Secondary
            //Plot, House, Flat

            var currentUser = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault();
            currentUser.UserPreferences.FavouriteCity[0] = RealEstate.Cities.BuenosAires;
            currentUser.UserPreferences.FavouriteCity[1] = RealEstate.Cities.Moskwa;
            currentUser.UserPreferences.FavouriteCity[2] = default;

            currentUser.UserPreferences.FavouriteMarket[0] = RealEstate.Markets.Primary;
            currentUser.UserPreferences.FavouriteMarket[1] = default;

            currentUser.UserPreferences.FavouriteType[0] = default;
            currentUser.UserPreferences.FavouriteType[1] = RealEstate.Types.House;
            currentUser.UserPreferences.FavouriteType[2] = RealEstate.Types.Flat;


            
                               
        }

        private void CheckUserPreferences()
        {
            var currentUser = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault();
            if (currentUser.UserPreferences.FavouriteCity[0] == RealEstate.Cities.Białystok)
            {
                this.CheckBoxCityBialystok.IsChecked = true;
            }
            else
            {
                this.CheckBoxCityBialystok.IsChecked = false;
            }
            if (currentUser.UserPreferences.FavouriteCity[1] == RealEstate.Cities.Moskwa)
            {
                this.CheckBoxCityMoscow.IsChecked = true;
            }
            else
            {
                this.CheckBoxCityMoscow.IsChecked = false;
            }
            if (currentUser.UserPreferences.FavouriteCity[2] == RealEstate.Cities.BuenosAires)
            {
                this.CheckBoxCityBuenosAires.IsChecked = true;
            }
            else
            {
                this.CheckBoxCityBuenosAires.IsChecked = false;
            }

            if (currentUser.UserPreferences.FavouriteMarket[0] == RealEstate.Markets.Primary)
            {
                this.CheckBoxMarketPrimary.IsChecked = true;
            }
            else
            {
                this.CheckBoxMarketPrimary.IsChecked = false;
            }
            if (currentUser.UserPreferences.FavouriteMarket[1] == RealEstate.Markets.Secondary)
            {
                this.CheckBoxMarketSecondary.IsChecked = true;
            }
            else
            {
                this.CheckBoxMarketSecondary.IsChecked = false;
            }

            if (currentUser.UserPreferences.FavouriteType[0] == RealEstate.Types.Plot)
            {
                this.CheckBoxTypePlot.IsChecked = true;
            }
            else
            {
                this.CheckBoxTypePlot.IsChecked = false;
            }
            if (currentUser.UserPreferences.FavouriteType[0] == RealEstate.Types.House)
            {
                this.CheckBoxTypeHome.IsChecked = true;
            }
            else
            {
                this.CheckBoxTypeHome.IsChecked = false;
            }
            if (currentUser.UserPreferences.FavouriteType[0] == RealEstate.Types.Flat)
            {
                this.CheckBoxTypeFlat.IsChecked = true;
            }
            else
            {
                this.CheckBoxTypeFlat.IsChecked = false;
            }
        }

        private void SetRealEstatesList()
        {

            this.RealEstatesList.Add(new Plot(Plot.PlotTypes.BuildingPlot, 50000, 2323, RealEstate.Cities.BuenosAires));
            this.RealEstatesList.Add(new Flat(4, 4, Flat.FlatStandards.Apartment, 5, 4, RealEstate.Cities.BuenosAires, 4, RealEstate.Markets.Secondary));
            this.RealEstatesList.Add(new House(new Plot(Plot.PlotTypes.SummerPlot, 20000, 1000, RealEstate.Cities.Moskwa), 2, 300, House.TypesOfOven.ElectricStove, RealEstate.Cities.Moskwa, 400, RealEstate.Markets.Primary));
            this.RealEstatesListTemp.AddRange(this.RealEstatesList);
            this.RefreshListView();
        }

        //tab z szczegółami konta
        private void SetCurrentUserDetails()
        {
            this.textblockCurrentUserLogin.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login;
            this.textblockCurrentUserName.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Name;
            this.textblockCurrentUserSurname.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Surname;
        }

        #endregion

        #region Filters Methods


        //Do przycisków

        private void ButtonAddState_Click(object sender, RoutedEventArgs e)
        {
            //do zrobienia
        }
        private void ButtonSaveUserFilteres_Click(object sender, RoutedEventArgs e)
        {
            ///if (this.FavouriteCity != default || this.FavouriteMarket != null || this.FavouriteType != null)
            //{
                this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().UserPreferences = new UserPreferences(this.FavouriteType, this.FavouriteCity, this.FavouriteMarket);
                MessageBox.Show("Pomyślnie zapisano preferencje użytkownika");
            //}
            //else
                //MessageBox.Show("Należy wybrać przynajmniej jedną prefernecję");
        }

        //filtry
        //private double ReturnLowerPrice()
        //{
        //    double lowerPrice = 0;
        //    try
        //    {
        //        lowerPrice = Convert.ToDouble(TextBoxPriceFrom.Text);
        //    }
        //    catch (FormatException)
        //    {
        //        TextBoxPriceFrom.Text = "0";
        //    }
        //    return lowerPrice;
        //}
        //private double ReturnHigherPrice()
        //{
        //    double higherPrice = 10000000;
        //    try
        //    {
        //        higherPrice = Convert.ToDouble(TextBoxPriceTo.Text);
        //    }
        //    catch (FormatException)
        //    {
        //        TextBoxPriceTo.Text = "10000000";
        //    }

        //    return higherPrice;
        //}
        //private double ReturnSmallerArea()
        //{
        //    double smallerArea = 0;
        //    try
        //    {
        //        smallerArea = Convert.ToDouble(TextBoxAreaFrom.Text);
        //    }
        //    catch (FormatException)
        //    {
        //        TextBoxAreaFrom.Text = "0";
        //    }
        //    return smallerArea;
        //}
        //private double ReturnBiggerArea()
        //{
        //    double biggerArea = 10000;
        //    try
        //    {
        //        biggerArea = Convert.ToDouble(TextBoxAreaTo.Text);
        //    }
        //    catch (FormatException)
        //    {
        //        TextBoxAreaTo.Text = "10000";
        //    }
        //    return biggerArea;
        //}

        private void RefreshListView(List<RealEstate> tempList)
        {
            this.listviewOferts.ItemsSource = null;
            this.listviewOferts.ItemsSource = tempList;
        }

        private void RefreshListView()
        {
            this.listviewOferts.ItemsSource = null;
            this.listviewOferts.ItemsSource = this.RealEstatesListTemp;
        }

        //checkboxy
        private void CheckBoxTypeHome_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FavouriteType[0] = default;
            this.RealEstatesListTemp.RemoveAll(x => x.Type.Equals("Dom"));
            this.RefreshListView();
        }
        private void CheckBoxTypeHome_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteType[0] = RealEstate.Types.House;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals("Dom")));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteType[1] = RealEstate.Types.Flat;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie")));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FavouriteType[1] = default;
            this.RealEstatesListTemp.RemoveAll(x => x.Type.Equals("Mieszkanie"));
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteType[2] = RealEstate.Types.Plot;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals("Działka")));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FavouriteType[2] = default;
            this.RealEstatesListTemp.RemoveAll(x => x.Type.Equals("Działka"));
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCity[0] = RealEstate.Cities.Białystok;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == "Białystok"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCity[0] = default;
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Białystok");
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCity[1] = RealEstate.Cities.Moskwa;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == "Moskwa"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCity[1] = default;
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Moskwa");
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCity[2] = RealEstate.Cities.BuenosAires;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == "Buenos Aires"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCity[2] = default;
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Buenos Aires");
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarket[0] = RealEstate.Markets.Primary;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == "Pierowtony"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarket[0] = default;
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Pierowtny");
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarket[1] = RealEstate.Markets.Secondary;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == "Wtórny"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarket[1] = default;
            this.RealEstatesListTemp.RemoveAll(x => x.Market == "Wtórny");
            this.RefreshListView();
        }


        #endregion

        #region Change Login/Password Events

        private void ButtonChangeData_Click(object sender, RoutedEventArgs e)
        {
            if (this.textboxChangeData.Text != null)
            {
                if (this.IsLoginChanging)
                {
                    this.IsLoginChanging = false;
                    this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login = this.textboxChangeData.Text;
                    this.textblockCurrentUserLogin.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login;
                }

                else if (this.IsPasswordChanging)
                {
                    this.IsPasswordChanging = false;
                    this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Password = this.textboxChangeData.Text;
                }

                this.textblockChangeData.Visibility = Visibility.Collapsed;
                this.textboxChangeData.Visibility = Visibility.Collapsed;
                this.buttonChangeData.Visibility = Visibility.Collapsed;
                this.buttonChangeUserLogin.Visibility = Visibility.Visible;
                this.buttonChangeUserPassword.Visibility = Visibility.Visible;
            }

            else
            {
                if (this.IsLoginChanging)
                    this.textboxChangeData.Text = "Najpierw wprowadź nowy login";
                else if (this.IsPasswordChanging)
                    this.textboxChangeData.Text = "Najpierw wprowadź nowe hasło";
            }
        }


        private void ButtonChangeUserLogin_Click(object sender, RoutedEventArgs e)
        {
            this.textblockChangeData.Visibility = Visibility.Visible;
            this.textboxChangeData.Visibility = Visibility.Visible;
            this.buttonChangeData.Visibility = Visibility.Visible;
            this.buttonChangeUserLogin.Visibility = Visibility.Collapsed;
            this.buttonChangeUserPassword.Visibility = Visibility.Collapsed;
            this.buttonChangeData.Content = "Zmień login.";
            this.textblockChangeData.Text = "Wprowadź nowy login";
            this.IsLoginChanging = true;
        }

        private void ButtonChangeUserPassword_Click(object sender, RoutedEventArgs e)
        {
            this.textblockChangeData.Visibility = Visibility.Visible;
            this.textboxChangeData.Visibility = Visibility.Visible;
            this.buttonChangeData.Visibility = Visibility.Visible;
            this.buttonChangeUserLogin.Visibility = Visibility.Collapsed;
            this.buttonChangeUserPassword.Visibility = Visibility.Collapsed;
            this.buttonChangeData.Content = "Zmień hasło.";
            this.textblockChangeData.Text = "Wprowadź nowe hasło";
            this.IsPasswordChanging = true;
        }

        #endregion



        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            RoutedEventArgs routedEventArgs = new RoutedEventArgs();
            ButtonLogoutClick(this, routedEventArgs);
        }

        private void TextBoxAreaFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxAreaFrom.Text.Length > 0)
                this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Surface >= Int64.Parse(TextBoxAreaFrom.Text)).ToList());
            else
                this.RefreshListView(this.RealEstatesListTemp);
        }

        private void TextBoxAreaTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxAreaTo.Text.Length > 0)
                this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Surface <= Int64.Parse(TextBoxAreaTo.Text)).ToList());
            else
                this.RefreshListView(this.RealEstatesListTemp);
        }

        private void TextBoxPriceFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxPriceFrom.Text.Length > 0)
                this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Price >= Int64.Parse(TextBoxPriceFrom.Text)).ToList());
            else
                this.RefreshListView(this.RealEstatesListTemp);
        }

        private void TextBoxPriceTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxPriceTo.Text.Length > 0)
                this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Price <= Int64.Parse(TextBoxPriceTo.Text)).ToList());
            else
                this.RefreshListView(this.RealEstatesListTemp);
        }

        private void TextBoxPriceTo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxPriceFrom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxAreaFrom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxAreaTo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }

}

     