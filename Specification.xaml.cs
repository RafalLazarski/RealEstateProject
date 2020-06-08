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
                TextBlockTypeSpecification.Visibility = Visibility.Collapsed;
                TextBlockCitySpecification.Visibility = Visibility.Collapsed;
                TextBlockPriceSpecification.Visibility = Visibility.Collapsed;
                TextBlockRentSpecification.Visibility = Visibility.Collapsed;
                TextBlockMarketSpecification.Visibility = Visibility.Collapsed;
                TextBlockSurfaceSpecification.Visibility = Visibility.Collapsed;
                TextBlockTypeOfOvenSpecification.Visibility = Visibility.Collapsed;
                TextBlockNumberofFloorsSpecification.Visibility = Visibility.Collapsed;
                TextBlockHouseAreaSpecification.Visibility = Visibility.Collapsed;
                TextBlockFlatStandardsSpecification.Visibility = Visibility.Collapsed;
                TextBlockFloorNumberSpecification.Visibility = Visibility.Collapsed;
                TextBlockRoomsSpecification.Visibility = Visibility.Collapsed;
                TextBlockPlotTypesSpecification.Visibility = Visibility.Collapsed;
            }
            else
            {
                StackPanelType.Visibility = Visibility.Collapsed;
                StackPanelCity.Visibility = Visibility.Collapsed;
                TextBoxPriceSelect.Visibility = Visibility.Collapsed;
                TextBoxRentSelect.Visibility = Visibility.Collapsed;
                StackPanelMarket.Visibility = Visibility.Collapsed;
                TextBoxSurfaceSelect.Visibility = Visibility.Collapsed;
                StackPanelTypeOfOven.Visibility = Visibility.Collapsed;
                TextBoxNumberofFloorsSelect.Visibility = Visibility.Collapsed;
                TextBoxHouseAreaSelect.Visibility = Visibility.Collapsed;
                StackPanelFlatStandards.Visibility = Visibility.Collapsed;
                TextBoxFloorNumberSelect.Visibility = Visibility.Collapsed;
                TextBoxRoomsSelect.Visibility = Visibility.Collapsed;
                StackPanelPlotTypes.Visibility = Visibility.Collapsed;
                ButtonDeleteProduct.Visibility = Visibility.Collapsed;
                ButtonAddProduct.Visibility = Visibility.Collapsed;
            }
            string typeOfSelectedRealEstate = realEstate.Type.ToString();
            switch (typeOfSelectedRealEstate)
            {
                case "Dom":
                    TextBlockFlatStandards.Visibility = Visibility.Collapsed;
                    TextBlockFlatStandardsSpecification.Visibility = Visibility.Collapsed;
                    StackPanelFlatStandards.Visibility = Visibility.Collapsed;
                    TextBlockFloorNumber.Visibility = Visibility.Collapsed;
                    TextBlockFloorNumberSpecification.Visibility = Visibility.Collapsed;
                    TextBoxFloorNumberSelect.Visibility = Visibility.Collapsed;
                    TextBlockRooms.Visibility = Visibility.Collapsed;
                    TextBlockRoomsSpecification.Visibility = Visibility.Collapsed;
                    TextBoxRoomsSelect.Visibility = Visibility.Collapsed;
                    TextBlockPlotTypes.Visibility = Visibility.Collapsed;
                    TextBlockPlotTypesSpecification.Visibility = Visibility.Collapsed;
                    StackPanelPlotTypes.Visibility = Visibility.Collapsed;
                    break;
                case "Mieszkanie":
                    TextBlockTypeOfOven.Visibility = Visibility.Collapsed;
                    TextBlockTypeOfOvenSpecification.Visibility = Visibility.Collapsed;
                    StackPanelTypeOfOven.Visibility = Visibility.Collapsed;
                    TextBlockNumberofFloors.Visibility = Visibility.Collapsed;
                    TextBlockNumberofFloorsSpecification.Visibility = Visibility.Collapsed;
                    TextBoxNumberofFloorsSelect.Visibility = Visibility.Collapsed;
                    TextBlockHouseArea.Visibility = Visibility.Collapsed;
                    TextBlockHouseAreaSpecification.Visibility = Visibility.Collapsed;
                    TextBoxHouseAreaSelect.Visibility = Visibility.Collapsed;
                    TextBlockPlotTypes.Visibility = Visibility.Collapsed;
                    TextBlockPlotTypesSpecification.Visibility = Visibility.Collapsed;
                    StackPanelPlotTypes.Visibility = Visibility.Collapsed;
                    //TextBlockDetails.SetValue(Grid.RowProperty, 6);
                    //TextBlockDetailsSpecification.SetValue(Grid.RowProperty, 6);
                    //TextBlockRooms.SetValue(Grid.RowProperty, 7);
                    //TextBlockRoomsSpecification.SetValue(Grid.RowProperty, 7);
                    break;
                case "Działka":
                    TextBlockTypeOfOven.Visibility = Visibility.Collapsed;
                    TextBlockTypeOfOvenSpecification.Visibility = Visibility.Collapsed;
                    StackPanelTypeOfOven.Visibility = Visibility.Collapsed;
                    TextBlockNumberofFloors.Visibility = Visibility.Collapsed;
                    TextBlockNumberofFloorsSpecification.Visibility = Visibility.Collapsed;
                    TextBoxNumberofFloorsSelect.Visibility = Visibility.Collapsed;
                    TextBlockHouseArea.Visibility = Visibility.Collapsed;
                    TextBlockHouseAreaSpecification.Visibility = Visibility.Collapsed;
                    TextBoxHouseAreaSelect.Visibility = Visibility.Collapsed;
                    TextBlockFlatStandards.Visibility = Visibility.Collapsed;
                    TextBlockFlatStandardsSpecification.Visibility = Visibility.Collapsed;
                    StackPanelFlatStandards.Visibility = Visibility.Collapsed;
                    TextBlockFloorNumber.Visibility = Visibility.Collapsed;
                    TextBlockFloorNumberSpecification.Visibility = Visibility.Collapsed;
                    TextBoxFloorNumberSelect.Visibility = Visibility.Collapsed;
                    TextBlockRooms.Visibility = Visibility.Collapsed;
                    TextBlockRoomsSpecification.Visibility = Visibility.Collapsed;
                    TextBoxRoomsSelect.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void ButtonBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
