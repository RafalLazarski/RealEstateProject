using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace RealEstateProject
{
    /// <summary>
    /// Interaction logic for Specification.xaml
    /// </summary>
    public partial class Specification : Window
    {
        private RealEstate RealEstate { get; set; }
        private List<RealEstate> RealEstateList { get; set; }

        public delegate void ButtonDeleteItemDelegate(object sender, RoutedEventArgs e);
        public event ButtonDeleteItemDelegate ButtonDeleteItemClick;
        public Specification(int userID, RealEstate realEstate, List<RealEstate> realEstateList)
        {
            InitializeComponent();
            this.DataContext = realEstate;
            SetTextBlocks(realEstate, userID);
            this.RealEstateList = realEstateList;
            this.RealEstate = realEstate;
            
        }

        

        private void SetTextBlocks(RealEstate realEstate, int userID)
        {
            
            if(userID == 1)
            {
                ButtonReservation.Visibility = Visibility.Collapsed;
            }
            else
            {
                ButtonDeleteProduct.Visibility = Visibility.Collapsed;
            }

            string typeOfSelectedRealEstate = realEstate.Type.ToString();
            switch (typeOfSelectedRealEstate)
            {
                case "Dom":
                    TextBlockFlatStandards.Visibility = Visibility.Collapsed;
                    TextBlockFlatStandardsSpecification.Visibility = Visibility.Collapsed;
                    TextBlockFloorNumber.Visibility = Visibility.Collapsed;
                    TextBlockFloorNumberSpecification.Visibility = Visibility.Collapsed;
                    TextBlockRooms.Visibility = Visibility.Collapsed;
                    TextBlockRoomsSpecification.Visibility = Visibility.Collapsed;
                    TextBlockPlotTypes.Visibility = Visibility.Collapsed;
                    TextBlockPlotTypesSpecification.Visibility = Visibility.Collapsed;
                    break;
                case "Mieszkanie":
                    TextBlockTypeOfOven.Visibility = Visibility.Collapsed;
                    TextBlockTypeOfOvenSpecification.Visibility = Visibility.Collapsed;
                    TextBlockNumberofFloors.Visibility = Visibility.Collapsed;
                    TextBlockNumberofFloorsSpecification.Visibility = Visibility.Collapsed;
                    TextBlockHouseArea.Visibility = Visibility.Collapsed;
                    TextBlockHouseAreaSpecification.Visibility = Visibility.Collapsed;
                    TextBlockPlotTypes.Visibility = Visibility.Collapsed;
                    TextBlockPlotTypesSpecification.Visibility = Visibility.Collapsed;
                    break;
                case "Działka":
                    TextBlockTypeOfOven.Visibility = Visibility.Collapsed;
                    TextBlockTypeOfOvenSpecification.Visibility = Visibility.Collapsed;
                    TextBlockNumberofFloors.Visibility = Visibility.Collapsed;
                    TextBlockNumberofFloorsSpecification.Visibility = Visibility.Collapsed;
                    TextBlockHouseArea.Visibility = Visibility.Collapsed;
                    TextBlockHouseAreaSpecification.Visibility = Visibility.Collapsed;
                    TextBlockFlatStandards.Visibility = Visibility.Collapsed;
                    TextBlockFlatStandardsSpecification.Visibility = Visibility.Collapsed;
                    TextBlockFloorNumber.Visibility = Visibility.Collapsed;
                    TextBlockFloorNumberSpecification.Visibility = Visibility.Collapsed;
                    TextBlockRooms.Visibility = Visibility.Collapsed;
                    TextBlockRoomsSpecification.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void ButtonBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var removedItem = this.RealEstateList.Where(x => x.RealEstateID == RealEstate.RealEstateID).FirstOrDefault();
            this.RealEstateList.Remove(removedItem);
            using (Stream stream = File.Open("RealEstatesList.txt", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, this.RealEstateList);
            }

            this.Close();
            RoutedEventArgs routedEventArgs = new RoutedEventArgs();
            ButtonDeleteItemClick(this, routedEventArgs);           
        }

        private void ButtonReservation_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
