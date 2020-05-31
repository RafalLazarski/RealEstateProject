using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace RealEstateProject
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {       
        public List<RealEstate> RealEstatesList { get; set; }

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
        public int CurrentUserID { get; set; }
        public List<User> UsersList { get; set; }
        public bool IsLoginChanging = false;
        public bool IsPasswordChanging = false;
        public delegate void ButtonLogoutDelegate(object sender, RoutedEventArgs e);
        public event ButtonLogoutDelegate ButtonLogoutClick;
        

        public MainMenu(int currentUserID, List<User> usersList)
        {
            InitializeComponent();
            bool checkHome = this.CheckHome;
            bool checkPlate = this.CheckPlate;
            bool checkFlat = this.CheckFlat;
            bool checkBialystok = this.CheckBialystok;
            bool checkMoscow = this.CheckMoscow;
            bool checkBuenosAires = this.CheckBuenosAires;
            bool checkPrimary = this.CheckPrimary;
            bool checkSecondary = this.CheckSecondary;
            this.CurrentUserID = currentUserID;
            this.UsersList = usersList;
            this.RealEstatesList = new List<RealEstate>();
            this.SetCurrentUserDetails();
            this.SetRealEstatesList();
            this.ShowRealEstateList(checkHome, checkPlate, checkFlat, checkBialystok, checkMoscow, checkBuenosAires, checkPrimary, checkSecondary);
        }
        //tab z ofertą
        private void SetRealEstatesList()
        {

            this.RealEstatesList.Add(new Plot(Plot.PlotTypes.BuildingPlot, 50000, 2323, RealEstate.Cities.BuenosAires));
            this.RealEstatesList.Add(new Flat(4, 4, Flat.FlatStandards.Apartment, 5, 4, RealEstate.Cities.BuenosAires, 4, RealEstate.Markets.Secondary));
            this.RealEstatesList.Add(new House(new Plot(Plot.PlotTypes.BuildingPlot, 10000, 3000, RealEstate.Cities.Moskwa), 2, 300, House.TypesOfOven.CoalFurnace, 600, RealEstate.Markets.Primary));

        }

        private void ShowRealEstateList(bool checkHome, bool checkPlate, bool checkFlat, bool checkBialystok, bool checkMoscow, bool checkBuenosAires, bool checkPrimary, bool checkSecondary)
        {
            double biggerArea = ReturnBiggerArea();
            double smallerArea = ReturnSmallerArea();
            double lowerPrice = ReturnLowerPrice();
            double higherPrice = ReturnHigherPrice();
            //obojętny typ
            if (checkHome && checkPlate && checkFlat)
            {
                //obojętny rynek
                if(checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                }
                //używane
                else if(!checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                }
                //nowe
                else if (checkPrimary && !checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                }
                //pusta lista
                else if (!checkPrimary && !checkSecondary)
                {
                    this.listviewOferts.ItemsSource = null;
                }
            }
            //bez mieszkania
            else if(checkHome && checkPlate && !checkFlat)
            {
                //obojętny rynek
                if (checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Dom") || x.Type.Equals("Działka")) && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                }
                //używane
                else if (!checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Dom") || x.Type.Equals("Działka")) && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                }
                //nowe
                else if (checkPrimary && !checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Dom") || x.Type.Equals("Działka")) && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                }
                //pusta lista
                else if (!checkPrimary && !checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = null;
                }
            }
            //bez działki
            else if (checkHome && !checkPlate && checkFlat)
            {
                //obojętny rynek
                if (checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Dom") || x.Type.Equals("Mieszkanie")) && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                }
                //używane
                else if (!checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Dom") || x.Type.Equals("Mieszkanie")) && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                }
                //nowe
                else if (checkPrimary && !checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Dom") || x.Type.Equals("Mieszkanie")) && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                }
                //pusta lista
                else if (!checkPrimary && !checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires" || x.City == "Moskwa")
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Białystok")
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        && (x.City == "Buenos Aires")
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        && (x.City == "Moskwa")
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    this.listviewOferts.ItemsSource = null;
                }
            }
            //bez domu
            else if (!checkHome && checkPlate && checkFlat)
            {
                //obojętny rynek
                if (checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                        && (x.City == "Białystok" || x.City == "Moskwa")
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                        && (x.City == "Białystok" || x.City == "Buenos Aires")
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    
                }
                //używane
                else if (!checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));

                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    
                }
                //nowe
                else if (checkPrimary && !checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                       
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => (x.Type.Equals("Mieszkanie") || x.Type.Equals("Działka")) && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                }
                //pusta lista
                else if (!checkPrimary && !checkSecondary)
                {
                    this.listviewOferts.ItemsSource = null;
                }
            }
            //tylko dom
            else if (checkHome && !checkPlate && !checkFlat)
            {
                //obojętny rynek
                if (checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                       
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                       
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                       
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    
                }
                //używane
                else if (!checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    
                }
                //nowe
                else if (checkPrimary && !checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Dom") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    } 
                }
                //pusta lista
                else if (!checkPrimary && !checkSecondary)
                {
                    this.listviewOferts.ItemsSource = null;
                }
            }
            //tylko działka
            else if (!checkHome && checkPlate && !checkFlat)
            {
                //obojętny rynek
                if (checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    
                }
                //używane
                else if (!checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    
                }
                //nowe
                else if (checkPrimary && !checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Działka") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                }
                //pusta lista
                else if (!checkPrimary && !checkSecondary)
                {
                    this.listviewOferts.ItemsSource = null;
                }
            }
            //tylko mieszkanie
            else if (!checkHome && !checkPlate && checkFlat)
            {
                //obojętny rynek
                if (checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                    
                }
                //używane
                else if (!checkPrimary && checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Wtórny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                }
                //nowe
                else if (checkPrimary && !checkSecondary)
                {
                    //obojętne miasto
                    if (checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea));
                    }
                    //bez Buenos
                    else if (checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Moskwa"));
                        
                    }
                    //bez Moskwy
                    else if (checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok" || x.City == "Buenos Aires"));
                        
                    }
                    //bez Białegostoku
                    else if (!checkBialystok && checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires" || x.City == "Moskwa"));
                        
                    }
                    //tylko Białystok
                    else if (checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Białystok"));
                        
                    }
                    //tylko Buenos
                    else if (!checkBialystok && !checkMoscow && checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Buenos Aires"));
                        
                    }
                    //tylko Moskwa
                    else if (!checkBialystok && checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = this.RealEstatesList.Where(x => x.Type.Equals("Mieszkanie") && x.Market == "Pierwotny" && (x.Price >= lowerPrice && x.Price <= higherPrice) && (x.Surface >= smallerArea && x.Surface <= biggerArea) && (x.City == "Moskwa"));
                        
                    }
                    //pusta lista
                    else if (!checkBialystok && !checkMoscow && !checkBuenosAires)
                    {
                        this.listviewOferts.ItemsSource = null;
                    }
                }
                //pusta lista
                else if (!checkPrimary && !checkSecondary)
                {
                    this.listviewOferts.ItemsSource = null;
                }
            }
            //pusta lista
            else if (!checkHome && !checkPlate && !checkFlat)
            {
                this.listviewOferts.ItemsSource = null;
            }


        }

        //Do przycisków
        private void ButtonUseFiltres_Click(object sender, RoutedEventArgs e)
        {
            this.ShowRealEstateList(this.CheckHome, this.CheckPlate, this.CheckFlat, this.CheckBialystok, this.CheckMoscow, this.CheckBuenosAires, this.CheckPrimary, this.CheckSecondary);
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
            catch(FormatException)
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
            catch(FormatException)
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
            catch(FormatException)
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
            catch(FormatException)
            {
                TextBoxAreaTo.Text = "10000";
            }
            return biggerArea;
        }

        //checkboxy
        private void CheckBoxTypeHome_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckHome = false;
        }
        private void CheckBoxTypeHome_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckHome = true;
        }
        private void CheckBoxTypeFlat_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckFlat = true;
        }
        private void CheckBoxTypeFlat_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckFlat = false;
        }
        private void CheckBoxTypePlate_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckPlate = true;
        }
        private void CheckBoxTypePlate_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckPlate = false;
        }
        private void CheckBoxCityBialystok_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckBialystok = true;
        }
        private void CheckBoxCityBialystok_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckBialystok = false;
        }
        private void CheckBoxCityMoscow_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckMoscow = true;
        }
        private void CheckBoxCityMoscow_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckMoscow = false;
        }
        private void CheckBoxCityBuenosAires_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckBuenosAires = true;
        }
        private void CheckBoxCityBuenosAires_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckBuenosAires = false;
        }
        private void ChceckBoxMarketPrimary_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckPrimary = true;
        }
        private void ChceckBoxMarketPrimary_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckPrimary = false;
        }
        private void CheckBoxMarketSecondary_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckSecondary = true;
        }
        private void CheckBoxMarketSecondary_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CheckSecondary = false;
        }

        //tab z szczegółami konta
        private void SetCurrentUserDetails()
        {
            this.textblockCurrentUserLogin.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Login;
            this.textblockCurrentUserName.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Name;
            this.textblockCurrentUserSurname.Text = this.UsersList.Where(x => x.UserID == this.CurrentUserID).FirstOrDefault().Surname;
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

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            RoutedEventArgs routedEventArgs = new RoutedEventArgs();
            ButtonLogoutClick(this, routedEventArgs);
        }

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
    }
}
