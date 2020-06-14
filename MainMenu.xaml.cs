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
            this.SetComboboxesItemsSources();
            this.SetCurrentUserDetails();
            this.SetTextBlocks();
            this.ImportRealEstatesList();
            this.ChangeCheckboxesSelection();
            this.RefreshListViewUsers(false);
            if (currentUserID != 1)
                this.tabitemAdminPanel.Visibility = Visibility.Collapsed;
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
            this.ComboBoxTypeOfOven.ItemsSource = Enum.GetValues(typeof(House.TypesOfOven)).Cast<House.TypesOfOven>();
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
                }
            }
                
            this.RefreshListView();
            this.RefreshCustomerListView();

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
            if(CurrentUser.UserID == 1)
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
            else if ((Types)this.ComboBoxType.SelectedValue == Types.Dom)
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
            }
            else if ((Types)this.ComboBoxType.SelectedValue == Types.Mieszkanie)
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
                buttonAddRealEstate.SetValue(Grid.RowProperty, 9);
                buttonAddRealEstate.SetValue(Grid.RowSpanProperty, 1);
            }
            else if ((Types)this.ComboBoxType.SelectedValue == Types.Działka)
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

            if(CurrentUserID != 1)
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

        #endregion

        #region Filters Methods



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
                        Specification specification = new Specification(CurrentUserID, RealEstateSelectedOne, RealEstatesList);
                        specification.Show();
                        if(CurrentUserID == 1)
                        {
                            specification.ButtonDeleteItemClick += (_sender, _e) =>
                            {
                                this.RefreshListView(RealEstateSelectedOne);
                            };
                        }
                        else
                        {
                            specification.ButtonReserveItemClick += (_sender, _e) =>
                            {
                                this.RefreshListView();
                                this.RefreshCustomerListView();
                            };
                        }
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


        #endregion

        #region Change Login/Password Events

        private void ButtonChangeUserLogin_Click(object sender, RoutedEventArgs e)
        {
            if (this.textboxChangeDataLogin.Text.Length > 2)
            {
                this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login = this.textboxChangeDataLogin.Text;
                this.textblockCurrentUserLogin.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login;
                MessageBox.Show("Pomyślnie zmieniono login");
            }
            else
            {
                MessageBox.Show("Nowy login musi mieć przynajmniej 3 znaki!");
            }
        }

        private void ButtonChangeUserPassword_Click(object sender, RoutedEventArgs e)
        {
            if (this.PasswordBoxChangeData.Password.Length > 2)
            {
                this.IsPasswordChanging = false;
                this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Password = this.PasswordBoxChangeData.Password;
                MessageBox.Show("Pomyślnie zmieniono hasło");
            }
            else
            {
                MessageBox.Show("Nowe hasło musi mieć przynajmniej 3 znaki!");
            }
        }



        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            string msg = "Czy na pewno chcesz się wylogować?";
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


        #endregion

        #region CustomerProducts

        private void ButtonSeeMore_Click(object sender, RoutedEventArgs e)
        {
            if (listviewCustomer.SelectedItem == null)
            {
                MessageBox.Show("Należy wybrać nieruchomość z listy.");
            }
            else
            {
                foreach (RealEstate xRealEstate in RealEstatesList)
                {

                    if (xRealEstate.RealEstateID.ToString().Equals(listviewCustomer.SelectedItem.ToString()))
                    {
                        RealEstate RealEstateSelectedOne = RealEstatesList.Where(x => x.RealEstateID == xRealEstate.RealEstateID).FirstOrDefault();
                        Specification specification = new Specification(this.CurrentUserID, RealEstateSelectedOne, RealEstatesList);
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

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (listviewCustomer.SelectedItem == null)
            {
                MessageBox.Show("Należy wybrać nieruchomość z listy.");
            }
            else
            {
                foreach (RealEstate xRealEstate in RealEstatesList)
                {

                    if (xRealEstate.RealEstateID.ToString().Equals(listviewCustomer.SelectedItem.ToString()))
                    {
                        xRealEstate.SoldItem = true;
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
                MessageBox.Show("Należy wybrać nieruchomość z listy.");
            }
            else
            {
                foreach (RealEstate xRealEstate in RealEstatesList)
                {

                    if (xRealEstate.RealEstateID.ToString().Equals(listviewCustomer.SelectedItem.ToString()))
                    {
                        xRealEstate.OwnerID = 1;
                    }
                }
                this.RefreshCustomerListView();
                this.RefreshListView();
            }
        }

        #endregion

        #region Admin

        private void buttonAddRealEstate_Click(object sender, RoutedEventArgs e)
        {
            if (this.ComboBoxCity.SelectedItem != null && this.TextBoxPriceSelectNewRealEstateWindow.Text != null
                && this.TextBoxSurfaceSelectNewRealEstateWindow.Text != null && this.ComboBoxMarket.SelectedItem != null
                && this.ComboBoxType.SelectedItem != null && ((this.ComboBoxTypeOfOven.SelectedItem != null
                && this.TextBoxNumberofFloorsSelectNewRealEstateWindow.Text != null && this.TextBoxHouseAreaSelectNewRealEstateWindow.Text != null)
                || (this.ComboBoxFlatStandards.SelectedItem != null && this.TextBoxFloorNumberSelectNewRealEstateWindow.Text != null
                && this.TextBoxRoomsSelectNewRealEstateWindow.Text != null) || this.ComboBoxPlotTypes.SelectedItem != null))
            {
                switch ((Types)this.ComboBoxType.SelectedValue)
                {
                    case Types.Mieszkanie:
                        this.RealEstatesList.Add(new Flat(Int32.Parse(this.TextBoxFloorNumberSelectNewRealEstateWindow.Text),
                        Int32.Parse(this.TextBoxRoomsSelectNewRealEstateWindow.Text), (Flat.FlatStandards)this.ComboBoxFlatStandards.SelectedItem,
                        Double.Parse(this.TextBoxPriceSelectNewRealEstateWindow.Text), Double.Parse(this.TextBoxSurfaceSelectNewRealEstateWindow.Text),
                        (Cities)this.ComboBoxCity.SelectedItem, Double.Parse(this.TextBoxRentSelectNewRealEstateWindow.Text), (Markets)this.ComboBoxMarket.SelectedItem));
                        if (this.CheckBoxTypeFlat.IsChecked == true)
                            this.RealEstatesListTemp.Add(this.RealEstatesList.Last());
                        break;
                    case Types.Dom:
                        this.RealEstatesList.Add(new House(new Plot(Plot.PlotTypes.Budowlana, Double.Parse(this.TextBoxPriceSelectNewRealEstateWindow.Text),
                            Double.Parse(this.TextBoxSurfaceSelectNewRealEstateWindow.Text), (Cities)this.ComboBoxCity.SelectedItem, (Markets)this.ComboBoxMarket.SelectedItem),
                            Int32.Parse(this.TextBoxNumberofFloorsSelectNewRealEstateWindow.Text), Double.Parse(this.TextBoxHouseAreaSelectNewRealEstateWindow.Text), (House.TypesOfOven)this.ComboBoxTypeOfOven.SelectedItem,
                            Double.Parse(this.TextBoxRentSelectNewRealEstateWindow.Text)));
                        if (this.CheckBoxTypeHome.IsChecked == true)
                            this.RealEstatesListTemp.Add(this.RealEstatesList.Last());
                        break;
                    case Types.Działka:
                        this.RealEstatesList.Add(new Plot(Plot.PlotTypes.Budowlana, Double.Parse(this.TextBoxPriceSelectNewRealEstateWindow.Text),
                            Double.Parse(this.TextBoxSurfaceSelectNewRealEstateWindow.Text), (Cities)this.ComboBoxCity.SelectedItem, (Markets)this.ComboBoxMarket.SelectedItem));
                        if (this.CheckBoxTypePlot.IsChecked == true)
                            this.RealEstatesListTemp.Add(this.RealEstatesList.Last());
                        break;

                }
                MessageBox.Show("Pomyślnie dodano nieruchomość");
                this.RefreshListView();
            }
            else
                MessageBox.Show("Należy uzupełnić wszystkie pola.");
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

        private void RefreshListViewUsers(bool isArchive)
        {
            this.listviewUsers.ItemsSource = null;
            if (!isArchive)
                this.listviewUsers.ItemsSource = this.UsersList.Where(x => x.Archive == false && x.UserID != 1).OrderBy(x => x.UserID);
            else
                this.listviewUsers.ItemsSource = this.UsersList.Where(x => x.UserID != 1).OrderBy(x => x.UserID);
        }


        private void RefreshListViewCustomer(int adminStatus)
        {
            this.listviewOferts.ItemsSource = null;
            if (adminStatus == 1)
            {
                this.listviewOferts.ItemsSource = this.RealEstatesList; //powinna być oddzielna lista ze sprzedanymi już produktami - pełen widok dla administratora
            }
            else
            {
                this.listviewOferts.ItemsSource = this.RealEstatesList; // - kupione nieruchomości przez użytkownika
            }
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
                    MessageBox.Show("Użytkownik został pomyślnie zbanowany");
                }
                else
                    MessageBox.Show("Uzytkownik jest już zbanowany.");
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

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SetTextBlocks();
        }




    #endregion


    }

}

