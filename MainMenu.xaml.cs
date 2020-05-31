using KellermanSoftware.CompareNetObjects.TypeComparers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        private bool CheckHome { get; set; }
        private bool CheckPlate { get; set; }
        private bool CheckFlat { get; set; }
        private bool CheckBialystok { get; set; }
        private bool CheckMoscow { get; set; }
        private bool CheckBuenosAires { get; set; }
        private bool CheckPrimary { get; set; }
        private bool CheckSecondary { get; set; }

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
            this.CheckBoxCityBuenosAires.IsChecked = true;
            this.CheckBoxCityBialystok.IsChecked = true;
            this.CheckBoxCityMoscow.IsChecked = true;
            this.CheckBoxMarketSecondary.IsChecked = true;
            this.CheckBoxTypeFlat.IsChecked = true;
            this.CheckBoxTypeHome.IsChecked = true;
            this.CheckBoxTypePlot.IsChecked = true;
            this.CheckBoxMarketPrimary.IsChecked = true;
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
            //Do zrobienia
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

        private void RefreshListView()
        {
            this.listviewOferts.ItemsSource = null;
            this.listviewOferts.ItemsSource = this.RealEstatesListTemp;
        }

        //checkboxy
        private void CheckBoxTypeHome_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckHome = false;
            this.RealEstatesListTemp.RemoveAll(x => x.Type.Equals("Dom"));
            this.RefreshListView();
        }
        private void CheckBoxTypeHome_Checked(object sender, RoutedEventArgs e)
        {
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals("Dom")));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckFlat = true;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie")));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckFlat = false;
            this.RealEstatesListTemp.RemoveAll(x => x.Type.Equals("Mieszkanie"));
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckPlate = true;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals("Działka")));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckPlate = false;
            this.RealEstatesListTemp.RemoveAll(x => x.Type.Equals("Działka"));
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckBialystok = true;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == "Białystok"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckBialystok = false;
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Białystok");
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckMoscow = true;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == "Moskwa"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckMoscow = false;
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Moskwa");
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckBuenosAires = true;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == "Buenos Aires"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckBuenosAires = false;
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Buenos Aires");
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckPrimary = true;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == "Pierowtony"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckPrimary = false;
            this.RealEstatesListTemp.RemoveAll(x => x.City == "Pierowtny");
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckSecondary = true;
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == "Wtórny"));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckSecondary = false;
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

    }

}

     