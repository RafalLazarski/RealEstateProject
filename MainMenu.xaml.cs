using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
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
using static RealEstateProject.RealEstate;
using MessageBox = System.Windows.MessageBox;


namespace RealEstateProject
{
    public partial class MainMenu : Window
    {

        #region Fields & Constructors

        public List<RealEstate> RealEstatesList = new List<RealEstate>();
        public List<RealEstate> RealEstatesListTemp = new List<RealEstate>();
        public List<Cities> FavouriteCitiesListtemp = new List<Cities>();
        public List<Markets> FavouriteMarketsListTemp = new List<Markets>();
        public List<Types> FavouriteTypesListTemp = new List<Types>();

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
            this.ImportRealEstatesList();
            this.ChangeCheckboxesSelection();
        }

        #endregion

        #region Helper Methods

        private void ImportRealEstatesList()
        { 
            using (Stream stream = File.Open("RealEstatesList.txt", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                var realEstates = (List<RealEstate>)bin.Deserialize(stream);
                realEstates.ForEach(x => this.RealEstatesList.Add(x));
            }
            this.RefreshListView();
        }

        private void ChangeCheckboxesSelection()
        {
            var currentUserPreferences = this.CurrentUser.UserPreferences;
            if (!currentUserPreferences.FavouriteTypes.Contains(Types.Mieszkanie))
                this.CheckBoxTypeFlat.IsChecked = false;
            else
                this.CheckBoxTypeFlat.IsChecked = true;
            if (!currentUserPreferences.FavouriteTypes.Contains(Types.Dom))
                this.CheckBoxTypeHome.IsChecked = false;
            else
                this.CheckBoxTypeHome.IsChecked = true;
            if (!currentUserPreferences.FavouriteTypes.Contains(Types.Działka))
                this.CheckBoxTypePlot.IsChecked = false;
            else
                this.CheckBoxTypePlot.IsChecked = true;
            if (!currentUserPreferences.FavouriteCities.Contains(Cities.Białystok))
                this.CheckBoxCityBialystok.IsChecked = false;
            else
                this.CheckBoxCityBialystok.IsChecked = true;
            if (!currentUserPreferences.FavouriteCities.Contains(Cities.BuenosAires))
                this.CheckBoxCityBuenosAires.IsChecked = false;
            else
                this.CheckBoxCityBuenosAires.IsChecked = true;
            if (!currentUserPreferences.FavouriteCities.Contains(Cities.Moskwa))
                this.CheckBoxCityMoscow.IsChecked = false;
            else
                this.CheckBoxCityMoscow.IsChecked = true;
            if (!currentUserPreferences.FavouriteMarkets.Contains(Markets.Pierwotny))
                this.CheckBoxMarketPrimary.IsChecked = false;
            else
                this.CheckBoxMarketPrimary.IsChecked = true;
            if (!currentUserPreferences.FavouriteMarkets.Contains(Markets.Wtórny))
                this.CheckBoxMarketSecondary.IsChecked = false;
            else
                this.CheckBoxMarketSecondary.IsChecked = true;
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
        //private void ButtonUseFiltres_Click(object sender, RoutedEventArgs e)
        //{
        //    //this.ShowRealEstateList(this.CheckHome, this.CheckPlate, this.CheckFlat, this.CheckBialystok, this.CheckMoscow, this.CheckBuenosAires, this.CheckPrimary, this.CheckSecondary);
        //}

        private void ButtonShowDetails_Click(object sender, RoutedEventArgs e)
        {
            if (listviewOferts.SelectedItem == null)
                MessageBox.Show("Należy wybrać nieruchomość z listy.");

            else
            {
                foreach (RealEstate xRealEstate in RealEstatesList)
                {

                    if (xRealEstate.RealEstateID.ToString().Equals(listviewOferts.SelectedItem.ToString()))
                    {
                        RealEstate RealEstateSelectedOne = RealEstatesList.Where(x => x.RealEstateID == xRealEstate.RealEstateID).FirstOrDefault();
                        Specification specification = new Specification(false, RealEstateSelectedOne);
                        specification.Show();
                    }
                }
            }

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
            var tmp = this.RealEstatesList.Where(x => x.Type.Equals(Types.Dom.ToString())).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any() && !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteTypesListTemp.Remove(Types.Dom);
            this.RefreshListView();
        }
        private void CheckBoxTypeHome_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteTypesListTemp.Add(Types.Dom);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals(Types.Dom.ToString())));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteTypesListTemp.Add(Types.Mieszkanie);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals(Types.Mieszkanie.ToString())));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.Type.Equals(Types.Mieszkanie.ToString())).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any() && !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteTypesListTemp.Remove(Types.Mieszkanie);
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteTypesListTemp.Add(Types.Działka);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals(Types.Działka.ToString())));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.Type.Equals(Types.Działka.ToString())).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any() && !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteTypesListTemp.Remove(Types.Działka);
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCitiesListtemp.Add(Cities.Białystok);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == Cities.Białystok.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.City == Cities.Białystok.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteCitiesListtemp.Remove(Cities.Białystok);
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCitiesListtemp.Add(Cities.Moskwa);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == Cities.Moskwa.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.City == Cities.Moskwa.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteCitiesListtemp.Remove(Cities.Moskwa);
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCitiesListtemp.Add(Cities.BuenosAires);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == Cities.BuenosAires.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.City == Cities.BuenosAires.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteCitiesListtemp.Remove(Cities.BuenosAires);
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarketsListTemp.Add(Markets.Pierwotny);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == Markets.Pierwotny.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.Market == Markets.Pierwotny.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && !this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteMarketsListTemp.Remove(Markets.Pierwotny);
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarketsListTemp.Add(Markets.Wtórny);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == Markets.Wtórny.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.Market == Markets.Wtórny.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && !this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteMarketsListTemp.Remove(Markets.Wtórny);
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
                    MessageBox.Show("Pomyślnie zmieniono login.");
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
                MessageBox.Show("Pomyślnie zmieniono hasło.");
            }

            else
            {
                if (this.IsLoginChanging)
                    MessageBox.Show("Najpierw wprowadź nowy login");
                else if (this.IsPasswordChanging)
                    MessageBox.Show("Najpierw wprowadź nowe hasło");
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
                this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Surface >= Int32.Parse(TextBoxAreaFrom.Text)).ToList());
            else
                this.RefreshListView(this.RealEstatesListTemp);
        }

        private void TextBoxAreaTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxAreaTo.Text.Length > 0)
                this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Surface <= Int32.Parse(TextBoxAreaTo.Text)).ToList());
            else
                this.RefreshListView(this.RealEstatesListTemp);
        }

        private void TextBoxPriceFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxPriceFrom.Text.Length > 0)
                this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Price >= Int32.Parse(TextBoxPriceFrom.Text)).ToList());
            else
                this.RefreshListView(this.RealEstatesListTemp);
        }

        private void TextBoxPriceTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxPriceTo.Text.Length > 0)
                this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Price <= Int32.Parse(TextBoxPriceTo.Text)).ToList());
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

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            string msg = "Czy na pewno chcesz zamknąć aplikację?";
            MessageBoxResult result =
              MessageBox.Show(
                msg,
                "Potwierdzenie",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                using (Stream stream = File.Open("RealEstatesList.txt", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, this.RealEstatesList);
                }

                using (Stream stream = File.Open("UsersList.txt", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, this.UsersList);
                }
            }
        }


        //Trzeba sprawdzić
        
        //private void listviewOferts_Click(object sender, RoutedEventArgs e)
        //{
        //    Specification specification = new Specification(CurrentUserID, RealEstatesList);
        //    specification.Show();
        //}
    }

}

     