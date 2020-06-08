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
using System.Windows.Shapes;

namespace RealEstateProject
{
    /// <summary>
    /// Interaction logic for Specification.xaml
    /// </summary>
    public partial class Specification : Window
    {
        public Specification(bool ifAdminCheck, RealEstate realEstate)
        {
            InitializeComponent();
            this.DataContext = realEstate;
            SetTextBlocks(realEstate, ifAdminCheck);
            
        }

        private void SetTextBlocks(RealEstate realEstate, bool adminStatus)
        {
            
            if(adminStatus)
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

        }

        private void ButtonReservation_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
