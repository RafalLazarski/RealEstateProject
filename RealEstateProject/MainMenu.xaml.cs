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
            this.SetComboboxesItemsSources();
            this.SetCurrentUserDetails();
            this.SetTextBlocks();
            this.ImportRealEstatesList();
            this.ChangeCheckboxesSelection();
            this.RefreshListViewUsers(false);
            if (currentUserID != 1)
                this.tabitemAdminPanel.Visibility = Visibility.Collapsed;
            else
                this.tabitemMailInbox.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Helper Methods

        private void SetComboboxesItemsSources()
        {
            this.ComboBoxCity.ItemsSource = Enum.GetValues(typeof(Cities)).Cast<Cities>();
            this.ComboBoxFlatStandards.ItemsSource = Enum.GetValues(typeof(Flat.FlatStandards)).Cast<Flat.FlatStandards>();
            this.ComboBoxMarket.ItemsSource = Enum.GetValues(typeof(Markets)).Cast<Markets>();
            this.ComboBoxPlotTypes.ItemsSource = Enum.GetValues(typeof(Plot.PlotTypes)).Cast<Plot.PlotTypes>();
            this.ComboBoxType.ItemsSource = Enum.GetValues(typeof(Types)).Cast<Types>();
            this.ComboBoxTypeOfOven.ItemsSource = Enum.GetValues(typeof(House.TypesOfStove)).Cast<House.TypesOfStove>();
        }

        private void ImportRealEstatesList()
        {

            using (Stream stream = File.Open("RealEstatesList.txt", FileMode.Open))
            {
                if (new FileInfo("RealEstatesList.txt").Length != 0)
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    var realEstates = (List<RealEstate>)bin.Deserialize(stream);
                    realEstates.ForEach(x => this.RealEstatesList.Add(x));
                    RealEstate.RealEstateCount = this.RealEstatesList.Count();
                }
            }

            this.RefreshListView();
            this.RefreshCustomerListView();

        }

        private void ChangeCheckboxesSelection()
        {
            var currentUserPreferences = this.CurrentUser.UserPreferences;
            if (!currentUserPreferences.FavouriteTypes.Contains(Types.Flat))
                this.CheckBoxTypeFlat.IsChecked = false;
            else
                this.CheckBoxTypeFlat.IsChecked = true;
            if (!currentUserPreferences.FavouriteTypes.Contains(Types.House))
                this.CheckBoxTypeHome.IsChecked = false;
            else
                this.CheckBoxTypeHome.IsChecked = true;
            if (!currentUserPreferences.FavouriteTypes.Contains(Types.Plot))
                this.CheckBoxTypePlot.IsChecked = false;
            else
                this.CheckBoxTypePlot.IsChecked = true;
            if (!currentUserPreferences.FavouriteCities.Contains(Cities.Bialystok))
                this.CheckBoxCityBialystok.IsChecked = false;
            else
                this.CheckBoxCityBialystok.IsChecked = true;
            if (!currentUserPreferences.FavouriteCities.Contains(Cities.Buenos_Aires))
                this.CheckBoxCityBuenosAires.IsChecked = false;
            else
                this.CheckBoxCityBuenosAires.IsChecked = true;
            if (!currentUserPreferences.FavouriteCities.Contains(Cities.Moscow))
                this.CheckBoxCityMoscow.IsChecked = false;
            else
                this.CheckBoxCityMoscow.IsChecked = true;
            if (!currentUserPreferences.FavouriteMarkets.Contains(Markets.Primary))
                this.CheckBoxMarketPrimary.IsChecked = false;
            else
                this.CheckBoxMarketPrimary.IsChecked = true;
            if (!currentUserPreferences.FavouriteMarkets.Contains(Markets.Secondary))
                this.CheckBoxMarketSecondary.IsChecked = false;
            else
                this.CheckBoxMarketSecondary.IsChecked = true;
        }

        private void SetCurrentUserDetails()
        {
            this.textblockCurrentUserLogin.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login;
            this.textblockCurrentUserName.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Name;
            this.textblockCurrentUserSurname.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Surname;
            this.CurrentUser = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault();
            this.listviewMailInbox.ItemsSource = CurrentUser.MailsList;
            if (CurrentUser.UserID == 1)
            {
                this.MaxWidth = 1100;
                this.MinWidth = 1100;
                TextBlockUserProducts.Visibility = Visibility.Collapsed;
                ButtonSeeMore.Visibility = Visibility.Collapsed;
                TextBlockAdminProducts.Visibility = Visibility.Visible;
                ButtonAccept.Visibility = Visibility.Visible;
                ButtonDecline.Visibility = Visibility.Visible;
            }
            else
            {
                TextBlockAdminProducts.Visibility = Visibility.Collapsed;
                ButtonAccept.Visibility = Visibility.Collapsed;
                ButtonDecline.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(listviewOfertsGrid, 6);
            }

            if (CurrentUser.NewMessage && CurrentUser.UserID != 1)
            {
                MessageBox.Show("You have a new message in your mailbox.");
                CurrentUser.NewMessage = false;
            }
            else if (CurrentUser.NewMessage && CurrentUser.UserID == 1)
            {
                MessageBox.Show("There are pending applications for consideration.");
                CurrentUser.NewMessage = false;
            }
        }


        private void SetTextBlocks()
        {
            if (this.ComboBoxType.SelectedValue == null)
            {
                TextBlockTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockNumberofFloorsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxNumberofFloorsSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockHouseAreaNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxHouseAreaSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockFlatStandardsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelFlatStandardsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockFloorNumberNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxFloorNumberSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxRoomsSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockRoomsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockPlotTypesNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelPlotTypesNewRealEstateWindow.Visibility = Visibility.Collapsed;
                buttonAddRealEstate.SetValue(Grid.RowProperty, 6);
                buttonAddRealEstate.SetValue(Grid.RowSpanProperty, 2);
            }
            else if ((Types)this.ComboBoxType.SelectedValue == Types.House)
            {
                TextBlockTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Visible;
                StackPanelTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBlockNumberofFloorsNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBoxNumberofFloorsSelectNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBlockHouseAreaNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBoxHouseAreaSelectNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBlockFlatStandardsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelFlatStandardsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockFloorNumberNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxFloorNumberSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxRoomsSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockRoomsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockPlotTypesNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelPlotTypesNewRealEstateWindow.Visibility = Visibility.Collapsed;
                buttonAddRealEstate.SetValue(Grid.RowProperty, 9);
                buttonAddRealEstate.SetValue(Grid.RowSpanProperty, 1);
                this.TextBlockNumberofFloorsNewRealEstateWindow.Text = "Number of floors:";
                this.TextBlockHouseAreaNewRealEstateWindow.Text = "Surface of the house:";
                this.TextBlockSurfaceNewRealEstateWindow.Text = "Surface of the plot:";
            }
            else if ((Types)this.ComboBoxType.SelectedValue == Types.Flat)
            {
                TextBlockTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockNumberofFloorsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxNumberofFloorsSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockHouseAreaNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxHouseAreaSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockFlatStandardsNewRealEstateWindow.Visibility = Visibility.Visible;
                StackPanelFlatStandardsNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBlockFloorNumberNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBoxFloorNumberSelectNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBoxRoomsSelectNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBlockRoomsNewRealEstateWindow.Visibility = Visibility.Visible;
                TextBlockPlotTypesNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelPlotTypesNewRealEstateWindow.Visibility = Visibility.Collapsed;
                this.TextBlockSurfaceNewRealEstateWindow.Text = "Surface of the flat:";
                buttonAddRealEstate.SetValue(Grid.RowProperty, 9);
                buttonAddRealEstate.SetValue(Grid.RowSpanProperty, 1);
            }
            else if ((Types)this.ComboBoxType.SelectedValue == Types.Plot)
            {
                TextBlockTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockNumberofFloorsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxNumberofFloorsSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockHouseAreaNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxHouseAreaSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockFlatStandardsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelFlatStandardsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockFloorNumberNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxFloorNumberSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxRoomsSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockRoomsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockPlotTypesNewRealEstateWindow.Visibility = Visibility.Visible;
                StackPanelPlotTypesNewRealEstateWindow.Visibility = Visibility.Visible;
                buttonAddRealEstate.SetValue(Grid.RowProperty, 7);
                buttonAddRealEstate.SetValue(Grid.RowSpanProperty, 2);
            }

            if (CurrentUserID != 1)
            {
                TextBlockTypeNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelTypeNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockCity.Visibility = Visibility.Collapsed;
                StackPanelCityNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockPriceNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxPriceSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockRentNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxRentSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockMarketNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelMarketNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockSurfaceNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxSurfaceSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelTypeOfOvenNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockNumberofFloorsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxNumberofFloorsSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockHouseAreaNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxHouseAreaSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelFlatStandardsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockFloorNumberNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxFloorNumberSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockRoomsNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBoxRoomsSelectNewRealEstateWindow.Visibility = Visibility.Collapsed;
                TextBlockPlotTypesNewRealEstateWindow.Visibility = Visibility.Collapsed;
                StackPanelPlotTypesNewRealEstateWindow.Visibility = Visibility.Collapsed;
                buttonAddRealEstate.Visibility = Visibility.Collapsed;
            }
        }

        private void RefreshListView(List<RealEstate> tempList)
        {
            this.listviewOferts.ItemsSource = null;
            this.listviewOferts.ItemsSource = tempList.Where(x => x.OwnerID == 1);
        }

        private void RefreshListView()
        {
            this.listviewOferts.ItemsSource = null;
            this.listviewOferts.ItemsSource = this.RealEstatesListTemp.Where(x => x.OwnerID == 1);
        }

        private void RefreshListView(RealEstate item)
        {
            this.RealEstatesListTemp.Remove(item);
            this.listviewOferts.ItemsSource = null;
            this.listviewOferts.ItemsSource = this.RealEstatesListTemp.Where(x => x.OwnerID == 1);
        }

        #endregion

        #region Filters Methods

        private void ButtonSaveUserFilteres_Click(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteCitiesListtemp.Any() ||
                this.FavouriteMarketsListTemp.Any() ||
                this.FavouriteTypesListTemp.Any())
            {
                this.CurrentUser.UserPreferences.FavouriteCities = this.FavouriteCitiesListtemp;
                this.CurrentUser.UserPreferences.FavouriteMarkets = this.FavouriteMarketsListTemp;
                this.CurrentUser.UserPreferences.FavouriteTypes = this.FavouriteTypesListTemp;
                MessageBox.Show("User preferences saved successfully.");
            }
            else
                MessageBox.Show("You must select at least one preference.");
        }

        //checkboxes
        private void CheckBoxTypeHome_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.Type.Equals(Types.House.ToString())).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any() 
                    && !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteTypesListTemp.Remove(Types.House);
            this.RefreshListView();
        }
        private void CheckBoxTypeHome_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteTypesListTemp.Add(Types.House);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals(Types.House.ToString())));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteTypesListTemp.Add(Types.Flat);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals(Types.Flat.ToString())));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypeFlat_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.Type.Equals(Types.Flat.ToString())).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any() && 
                    !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteTypesListTemp.Remove(Types.Flat);
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteTypesListTemp.Add(Types.Plot);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Type.Equals(Types.Plot.ToString())));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxTypePlot_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.Type.Equals(Types.Plot.ToString())).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any() && 
                    !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteTypesListTemp.Remove(Types.Plot);
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCitiesListtemp.Add(Cities.Bialystok);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == Cities.Bialystok.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBialystok_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.City == Cities.Bialystok.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && 
                    !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteCitiesListtemp.Remove(Cities.Bialystok);
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCitiesListtemp.Add(Cities.Moscow);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == Cities.Moscow.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityMoscow_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.City == Cities.Moscow.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && 
                    !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteCitiesListtemp.Remove(Cities.Moscow);
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteCitiesListtemp.Add(Cities.Buenos_Aires);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.City == Cities.Buenos_Aires.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxCityBuenosAires_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.City == Cities.Buenos_Aires.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && 
                    !this.FavouriteMarketsListTemp.Where(x => x.ToString() == item.Market).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteCitiesListtemp.Remove(Cities.Buenos_Aires);
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarketsListTemp.Add(Markets.Primary);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == Markets.Primary.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void ChceckBoxMarketPrimary_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.Market == Markets.Primary.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && 
                    !this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteMarketsListTemp.Remove(Markets.Primary);
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Checked(object sender, RoutedEventArgs e)
        {
            this.FavouriteMarketsListTemp.Add(Markets.Secondary);
            this.RealEstatesListTemp.AddRange(this.RealEstatesList.Where(x => x.Market == Markets.Secondary.ToString()));
            this.RealEstatesListTemp = new HashSet<RealEstate>(this.RealEstatesListTemp).ToList();
            this.RefreshListView();
        }
        private void CheckBoxMarketSecondary_Unchecked(object sender, RoutedEventArgs e)
        {
            var tmp = this.RealEstatesList.Where(x => x.Market == Markets.Secondary.ToString()).ToList();
            foreach (RealEstate item in tmp)
            {
                if (!this.FavouriteTypesListTemp.Where(x => x.ToString().Equals(item.Type)).Any() && 
                    !this.FavouriteCitiesListtemp.Where(x => x.ToString() == item.City).Any())
                    this.RealEstatesListTemp.Remove(item);
            }
            this.FavouriteMarketsListTemp.Remove(Markets.Secondary);
            this.RefreshListView();
        }

        private void TextBoxAreaFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (this.TextBoxAreaFrom.Text.Length > 0)
                    this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Surface >= Int32.Parse(TextBoxAreaFrom.Text)).ToList());
                else
                    this.RefreshListView(this.RealEstatesListTemp);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid character format.");
                this.TextBoxAreaFrom.Text = null;
            }
        }

        private void TextBoxAreaTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (this.TextBoxAreaTo.Text.Length > 0)
                    this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Surface <= Int32.Parse(TextBoxAreaTo.Text)).ToList());
                else
                    this.RefreshListView(this.RealEstatesListTemp);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid character format.");
                this.TextBoxAreaTo.Text = null;
            }
        }

        private void TextBoxPriceFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (this.TextBoxPriceFrom.Text.Length > 0)
                    this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Price >= Int32.Parse(TextBoxPriceFrom.Text)).ToList());
                else
                    this.RefreshListView(this.RealEstatesListTemp);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid character format.");
                this.TextBoxPriceFrom.Text = null;
            }
        }

        private void TextBoxPriceTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (this.TextBoxPriceTo.Text.Length > 0)
                    this.RefreshListView(this.RealEstatesListTemp.Where(x => x.Price <= Int32.Parse(TextBoxPriceTo.Text)).ToList());
                else
                    this.RefreshListView(this.RealEstatesListTemp);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid character format.");
                this.TextBoxPriceTo.Text = null;
            }
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


        #endregion

        #region Change Login/Password Events

        private void ButtonChangeUserLogin_Click(object sender, RoutedEventArgs e)
        {
            if (this.textboxChangeDataLogin.Text.Length > 2)
            {
                this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login = this.textboxChangeDataLogin.Text;
                this.textblockCurrentUserLogin.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login;
                MessageBox.Show("Login changed successfully.");
            }
            else
            {
                MessageBox.Show("The new login must be at least 3 characters long!");
            }
        }

        private void ButtonChangeUserPassword_Click(object sender, RoutedEventArgs e)
        {
            if (this.PasswordBoxChangeData.Password.Length > 2)
            {
                this.IsPasswordChanging = false;
                this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Password = this.PasswordBoxChangeData.Password;
                MessageBox.Show("Password changed successfully.");
            }
            else
            {
                MessageBox.Show("The new password must be at least 3 characters long!");
            }
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region CustomerProducts

        private void ButtonSeeMore_Click(object sender, RoutedEventArgs e)
        {
            if (listviewCustomer.SelectedItem == null)
            {
                MessageBox.Show("Select a property from the list.");
            }
            else
            {
                foreach (RealEstate estate in RealEstatesList)
                {

                    if (estate.RealEstateID.ToString().Equals(listviewCustomer.SelectedItem.ToString()))
                    {
                        RealEstate selectedEstate = RealEstatesList.Where(x => x.RealEstateID == estate.RealEstateID).FirstOrDefault();
                        Specification specification = new Specification(this.CurrentUserID, selectedEstate, RealEstatesList);
                        specification.Show();
                        specification.ButtonReserveItemClick += (_sender, _e) =>
                        {
                            this.RefreshListView();
                            this.RefreshCustomerListView();
                        };
                    }
                }
            }
        }

        private void RefreshCustomerListView()
        {
            listviewCustomer.ItemsSource = null;
            if (CurrentUser.UserID == 1)
            {
                listviewCustomer.ItemsSource = RealEstatesList.Where(x => x.OwnerID != 1 && x.SoldItem == false);
            }
            else
            {
                listviewCustomer.ItemsSource = RealEstatesList.Where(x => x.OwnerID == CurrentUser.UserID && x.SoldItem == true);
            }

        }





        #endregion

        #region Admin

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (listviewCustomer.SelectedItem == null)
            {
                MessageBox.Show("Select a property from the list..");
            }
            else
            {
                foreach (RealEstate estate in RealEstatesList)
                {

                    if (estate.RealEstateID.ToString().Equals(listviewCustomer.SelectedItem.ToString()))
                    {
                        var customer = this.UsersList.Where(x => x.UserID == estate.OwnerID).FirstOrDefault();
                        customer.MailsList.Add("Congratulations! You've successfully purchased: " + estate.Type + " in " + estate.City);
                        estate.SoldItem = true;
                        customer.NewMessage = true;
                    }
                }
                this.RefreshCustomerListView();
                this.RefreshListView();
            }
        }

        private void ButtonDecline_Click(object sender, RoutedEventArgs e)
        {
            if (listviewCustomer.SelectedItem == null)
            {
                MessageBox.Show("Select a property from the list.");
            }
            else
            {
                foreach (RealEstate estate in RealEstatesList)
                {

                    if (estate.RealEstateID.ToString().Equals(listviewCustomer.SelectedItem.ToString()))
                    {
                        var customer = this.UsersList.Where(x => x.UserID == estate.OwnerID).FirstOrDefault();
                        customer.MailsList.Add("Unfortunately, your application for " + estate.Type + " w " + estate.City + " has been rejected.");
                        estate.OwnerID = 1;
                        customer.NewMessage = true;
                    }
                }
                this.RefreshCustomerListView();
                this.RefreshListView();
            }
        }

        private void ButtonAddRealEstate_Click(object sender, RoutedEventArgs e)
        {
            if (this.ComboBoxCity.SelectedItem != null 
                && !String.IsNullOrEmpty(this.TextBoxPriceSelectNewRealEstateWindow.Text)
                && !String.IsNullOrEmpty(this.TextBoxSurfaceSelectNewRealEstateWindow.Text) 
                && this.ComboBoxMarket.SelectedItem != null
                && this.ComboBoxType.SelectedItem != null 
                && ((this.ComboBoxTypeOfOven.SelectedItem != null
                && !String.IsNullOrEmpty(this.TextBoxNumberofFloorsSelectNewRealEstateWindow.Text)  
                && !String.IsNullOrEmpty(this.TextBoxHouseAreaSelectNewRealEstateWindow.Text))
                || (this.ComboBoxFlatStandards.SelectedItem != null 
                    && !String.IsNullOrEmpty(this.TextBoxFloorNumberSelectNewRealEstateWindow.Text)
                    && !String.IsNullOrEmpty(this.TextBoxRoomsSelectNewRealEstateWindow.Text)) 
                || this.ComboBoxPlotTypes.SelectedItem != null))
            {
                try
                {
                    switch ((Types)this.ComboBoxType.SelectedValue)
                    {
                        case Types.Flat:
                            this.RealEstatesList.Add(
                                new Flat(
                                    Int32.Parse(this.TextBoxFloorNumberSelectNewRealEstateWindow.Text),
                            Int32.Parse(this.TextBoxRoomsSelectNewRealEstateWindow.Text), 
                            (Flat.FlatStandards)this.ComboBoxFlatStandards.SelectedItem,
                            Double.Parse(this.TextBoxPriceSelectNewRealEstateWindow.Text), 
                            Double.Parse(this.TextBoxSurfaceSelectNewRealEstateWindow.Text),
                            (Cities)this.ComboBoxCity.SelectedItem, 
                            Double.Parse(this.TextBoxRentSelectNewRealEstateWindow.Text), 
                            (Markets)this.ComboBoxMarket.SelectedItem));
                            if (this.CheckBoxTypeFlat.IsChecked == true)
                                this.RealEstatesListTemp.Add(this.RealEstatesList.Last());
                            break;
                        case Types.House:
                            if (Double.Parse(this.TextBoxSurfaceSelectNewRealEstateWindow.Text) <
                                Double.Parse(this.TextBoxHouseAreaSelectNewRealEstateWindow.Text))
                            {
                                MessageBox.Show("The built-up area cannot exceed the plot area.");
                                return;

                            }
                            else
                            {
                                this.RealEstatesList.Add(new House(
                                        new Plot(Plot.PlotTypes.Building, 
                                        Double.Parse(this.TextBoxPriceSelectNewRealEstateWindow.Text),
                                Double.Parse(this.TextBoxSurfaceSelectNewRealEstateWindow.Text), 
                                        (Cities)this.ComboBoxCity.SelectedItem, 
                                        (Markets)this.ComboBoxMarket.SelectedItem),
                                Int32.Parse(this.TextBoxNumberofFloorsSelectNewRealEstateWindow.Text), 
                                        Double.Parse(this.TextBoxHouseAreaSelectNewRealEstateWindow.Text), 
                                        (House.TypesOfStove)this.ComboBoxTypeOfOven.SelectedItem,
                                    Double.Parse(this.TextBoxRentSelectNewRealEstateWindow.Text)));
                                if (this.CheckBoxTypeHome.IsChecked == true)
                                    this.RealEstatesListTemp.Add(this.RealEstatesList.Last());
                            }
                            break;
                        case Types.Plot:
                            this.RealEstatesList.Add(
                                new Plot(Plot.PlotTypes.Building, 
                                Double.Parse(this.TextBoxPriceSelectNewRealEstateWindow.Text),
                                Double.Parse(this.TextBoxSurfaceSelectNewRealEstateWindow.Text), 
                                (Cities)this.ComboBoxCity.SelectedItem, 
                                (Markets)this.ComboBoxMarket.SelectedItem));
                            if (this.CheckBoxTypePlot.IsChecked == true)
                                this.RealEstatesListTemp.Add(this.RealEstatesList.Last());
                            break;
                    }
                    MessageBox.Show("Property added successfully.");
                    this.RefreshListView();
                    this.TextBoxFloorNumberSelectNewRealEstateWindow.Text = null;
                    this.TextBoxRoomsSelectNewRealEstateWindow.Text = null;
                    this.TextBoxPriceSelectNewRealEstateWindow.Text = null;
                    this.TextBoxSurfaceSelectNewRealEstateWindow.Text = null;
                    this.TextBoxRentSelectNewRealEstateWindow.Text = null;
                    this.TextBoxNumberofFloorsSelectNewRealEstateWindow.Text = null;
                    this.TextBoxHouseAreaSelectNewRealEstateWindow.Text = null;
                }

                catch (Exception)
                {
                    MessageBox.Show("Invalid character format.");
                    this.TextBoxFloorNumberSelectNewRealEstateWindow.Text = null;
                    this.TextBoxRoomsSelectNewRealEstateWindow.Text = null;
                    this.TextBoxPriceSelectNewRealEstateWindow.Text = null;
                    this.TextBoxSurfaceSelectNewRealEstateWindow.Text = null;
                    this.TextBoxRentSelectNewRealEstateWindow.Text = null;
                    this.TextBoxNumberofFloorsSelectNewRealEstateWindow.Text = null;
                    this.TextBoxHouseAreaSelectNewRealEstateWindow.Text = null;
                }

            }
            else
                MessageBox.Show("All fields must be completed.");
                           
        }       

        private void RefreshListViewUsers(bool isArchive)
        {
            this.listviewUsers.ItemsSource = null;
            if (!isArchive)
                this.listviewUsers.ItemsSource = this.UsersList.Where(x => x.Archive == false && x.UserID != 1).OrderBy(x => x.UserID);
            else
                this.listviewUsers.ItemsSource = this.UsersList.Where(x => x.UserID != 1).OrderBy(x => x.UserID);
        }

        private void ButtonBanUser_Click(object sender, RoutedEventArgs e)
        {
            if (this.listviewUsers.SelectedItem != null)
            {
                var selectedUser = this.UsersList.Where(x => x.Equals(this.listviewUsers.SelectedItem)).FirstOrDefault();
                if (!selectedUser.Archive)
                {
                    selectedUser.Archive = true;
                    if (this.checkboxShowArchive.IsChecked == true)
                        this.RefreshListViewUsers(true);
                    else
                        this.RefreshListViewUsers(false);
                    MessageBox.Show("The user has been successfully banned.");
                }
                else
                    MessageBox.Show("The user is already banned.");
            }
            else
                MessageBox.Show("Należy wybrać użytkownika z listy");
        }

        private void CheckboxShowArchive_Checked(object sender, RoutedEventArgs e)
        {
            this.RefreshListViewUsers(true);
        }

        private void CheckboxShowArchive_Unchecked(object sender, RoutedEventArgs e)
        {
            this.RefreshListViewUsers(false);
        }

        #endregion

        #region OtherEvents

        private void ButtonShowDetails_Click(object sender, RoutedEventArgs e)
        {
            if (listviewOferts.SelectedItem == null)
                MessageBox.Show("Select a property from the list.");

            else
            {
                foreach (RealEstate estate in RealEstatesList)
                {

                    if (estate.RealEstateID.ToString().Equals(listviewOferts.SelectedItem.ToString()))
                    {
                        RealEstate selectedEstate = RealEstatesList.Where(x => x.RealEstateID == estate.RealEstateID).FirstOrDefault();
                        Specification specification = new Specification(CurrentUserID, selectedEstate, RealEstatesList);
                        specification.Show();
                        if (CurrentUserID == 1)
                        {
                            specification.ButtonDeleteItemClick += (_sender, _e) =>
                            {
                                this.RefreshListView(selectedEstate);
                            };
                        }
                        else
                        {
                            specification.ButtonReserveItemClick += (_sender, _e) =>
                            {
                                this.RefreshListView();
                                this.RefreshCustomerListView();
                                this.UsersList.Where(x => x.UserID == 1).FirstOrDefault().NewMessage = true;
                                this.RealEstatesList.Where(x => x.OwnerID == this.CurrentUserID).FirstOrDefault().OwnerName =
                                this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login;
                            };
                        }
                    }
                }
            }
        }

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SetTextBlocks();
        }

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            string msg = "Are you sure you want to log out?";
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
                RoutedEventArgs routedEventArgs = new RoutedEventArgs();
                ButtonLogoutClick(this, routedEventArgs);

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

        private void TextBoxPriceSelectNewRealEstateWindow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxRentSelectNewRealEstateWindow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxSurfaceSelectNewRealEstateWindow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxNumberofFloorsSelectNewRealEstateWindow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxHouseAreaSelectNewRealEstateWindow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxFloorNumberSelectNewRealEstateWindow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxRoomsSelectNewRealEstateWindow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

    }

}

