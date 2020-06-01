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
        public List<RealEstate.Cities> FavouriteCitiesListtemp = new List<RealEstate.Cities>();
        public List<RealEstate.Markets> FavouriteMarketsListTemp = new List<RealEstate.Markets>();
        public List<RealEstate.Types> FavouriteTypesListTemp = new List<RealEstate.Types>();

        //do uzytkownika
        public User CurrentUser { get; set; }
        public bool FirstLoad { get; set; }
        public int CurrentUserID { get; set; }
        public List<User> UsersList { get; set; }
        public bool IsLoginChanging = false;
        public bool IsPasswordChanging = false;
        public delegate void ButtonLogoutDelegate(object sender, RoutedEventArgs e);
        public event ButtonLogoutDelegate ButtonLogoutClick;


        public MainMenu(int currentUserID, List<User> usersList)
        {
            InitializeComponent();            
            this.CurrentUserID = currentUserID;
            this.UsersList = usersList;            
            this.SetCurrentUserDetails();
            this.SetRealEstatesList();
            this.SetOnFirstLoad();
        }

        #endregion

        #region Helper Methods

        private void SetOnFirstLoad()
        {
            var currentUserPreferences = this.CurrentUser.UserPreferences;
            if (!currentUserPreferences.FavouriteTypes.Contains(RealEstate.Types.Flat))
                this.CheckBoxTypeFlat.IsChecked = false;
            if (!currentUserPreferences.FavouriteTypes.Contains(RealEstate.Types.House))
                this.CheckBoxTypeHome.IsChecked = false;
            if (!currentUserPreferences.FavouriteTypes.Contains(RealEstate.Types.Plot))
                this.CheckBoxTypePlot.IsChecked = false;
            if (!currentUserPreferences.FavouriteCities.Contains(RealEstate.Cities.Białystok))
                this.CheckBoxCityBialystok.IsChecked = false;
            if (!currentUserPreferences.FavouriteCities.Contains(RealEstate.Cities.BuenosAires))
                this.CheckBoxCityBuenosAires.IsChecked = false;
            if (!currentUserPreferences.FavouriteCities.Contains(RealEstate.Cities.Moskwa))
                this.CheckBoxCityMoscow.IsChecked = false;
            if (!currentUserPreferences.FavouriteMarkets.Contains(RealEstate.Markets.Primary))
                this.CheckBoxMarketPrimary.IsChecked = false;
            if (!currentUserPreferences.FavouriteMarkets.Contains(RealEstate.Markets.Secondary))
                this.CheckBoxMarketSecondary.IsChecked = false;
        }

        private void SetRealEstatesList()
        {

            this.RealEstatesList.Add(new Plot(Plot.PlotTypes.BuildingPlot, 50000, 2323, RealEstate.Cities.BuenosAires));
            this.RealEstatesList.Add(new Flat(4, 4, Flat.FlatStandards.Apartment, 5, 4, RealEstate.Cities.BuenosAires, 4, RealEstate.Markets.Secondary));          
            this.RealEstatesListTemp.AddRange(this.RealEstatesList);
            this.RefreshListView();
        }

        //tab z szczegółami konta
        private void SetCurrentUserDetails()
        {
            this.textblockCurrentUserLogin.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login;
            this.textblockCurrentUserName.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Name;
            this.textblockCurrentUserSurname.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Surname;
            this.CurrentUser = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault();
        }

        #endregion

        #region Filters Methods


        //Do przycisków
        private void ButtonUseFiltres_Click(object sender, RoutedEventArgs e)
        {
            //this.ShowRealEstateList(this.CheckHome, this.CheckPlate, this.CheckFlat, this.CheckBialystok, this.CheckMoscow, this.CheckBuenosAires, this.CheckPrimary, this.CheckSecondary);
        }

        private void ButtonAddState_Click(object sender, RoutedEventArgs e)
        {
            //do zrobienia
        }
        private void ButtonSaveUserFilteres_Click(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteCitiesListtemp.Any() ||
                this.FavouriteMarketsListTemp.Any() ||
                this.FavouriteTypesListTemp.Any())
            {
                this.CurrentUser.UserPreferences.FavouriteCities = this.FavouriteCitiesListtemp;
                this.CurrentUser.UserPreferences.FavouriteMarkets = this.FavouriteMarketsListTemp;
                this.CurrentUser.UserPreferences.FavouriteTypes = this.FavouriteTypesListTemp;
                MessageBox.Show("Pomyślnie zapisano preferencje użytkownika");
            }
            else
                MessageBox.Show("Należy wybrać przynajmniej jedną preferencję.");
        }

        private void ButtonShowUserFilteres_Click(object sender, RoutedEventArgs e)
        {
            //do zrobienia
        }

        //filtry
        private double ReturnLowerPrice()
        {
            double lowerPrice = 0;
            try
            {
                lowerPrice = Convert.ToDouble(TextBoxPriceFrom.Text);
            }
            catch (FormatException)
            {
                TextBoxPriceFrom.Text = "0";
            }
            return lowerPrice;
        }
        private double ReturnHigherPrice()
        {
            double higherPrice = 10000000;
            try
            {
                higherPrice = Convert.ToDouble(TextBoxPriceTo.Text);
            }
            catch (FormatException)
            {
                TextBoxPriceTo.Text = "10000000";
            }

            return higherPrice;
        }
        private double ReturnSmallerArea()
        {
            double smallerArea = 0;
            try
            {
                smallerArea = Convert.ToDouble(TextBoxAreaFrom.Text);
            }
            catch (FormatException)
            {
                TextBoxAreaFrom.Text = "0";
            }
            return smallerArea;
        }
        private double ReturnBiggerArea()
        {
            double biggerArea = 10000;
            try
            {
                biggerArea = Convert.ToDouble(TextBoxAreaTo.Text);
            }
            catch (FormatException)
            {
                TextBoxAreaTo.Text = "10000";
            }
            return biggerArea;
        }

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
            if (this.FavouriteTypesListTemp.Contains(RealEstate.Types.House))
                this.FavouriteTypesListTemp.Remove(RealEstate.Types.House);
            this.RealEstatesListTemp.RemoveAll(x => x.Type.Equals("Dom"));
            this.RefreshListView();
        }
        private void CheckBoxTypeHome_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteTypesListTemp.Add(RealEstate.Types.House);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals("Dom")));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteTypesListTemp.Add(RealEstate.Types.Flat);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie")));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteTypesListTemp.Contains(RealEstate.Types.Flat))
                this.FavouriteTypesListTemp.Remove(RealEstate.Types.Flat);
            this.RealEstatesListTemp.RemoveAll(x => x.Type.Equals("Mieszkanie"));
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteTypesListTemp.Add(RealEstate.Types.Plot);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals("Działka")));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteTypesListTemp.Contains(RealEstate.Types.Plot))
                this.FavouriteTypesListTemp.Remove(RealEstate.Types.Plot);
            this.RealEstatesListTemp.RemoveAll(x => x.Type.Equals("Działka"));
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCitiesListtemp.Add(RealEstate.Cities.Białystok);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == "Białystok"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteCitiesListtemp.Contains(RealEstate.Cities.Białystok))
                this.FavouriteCitiesListtemp.Remove(RealEstate.Cities.Białystok);
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Białystok");
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCitiesListtemp.Add(RealEstate.Cities.Moskwa);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == "Moskwa"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteCitiesListtemp.Contains(RealEstate.Cities.Moskwa))
                this.FavouriteCitiesListtemp.Remove(RealEstate.Cities.Moskwa);
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Moskwa");
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCitiesListtemp.Add(RealEstate.Cities.BuenosAires);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == "Buenos Aires"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteCitiesListtemp.Contains(RealEstate.Cities.BuenosAires))
                this.FavouriteCitiesListtemp.Remove(RealEstate.Cities.BuenosAires);
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Buenos Aires");
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarketsListTemp.Add(RealEstate.Markets.Primary);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == "Pierowtony"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteMarketsListTemp.Contains(RealEstate.Markets.Primary))
                this.FavouriteMarketsListTemp.Remove(RealEstate.Markets.Primary);
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Pierowtny");
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarketsListTemp.Add(RealEstate.Markets.Secondary);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == "Wtórny"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteMarketsListTemp.Contains(RealEstate.Markets.Secondary))
                this.FavouriteMarketsListTemp.Remove(RealEstate.Markets.Secondary);
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

     