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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public List<RealEstate> RealEstatesListTest = new List<RealEstate>();
        public Test()
        {
            InitializeComponent();
            ImportRealEstatesListTest();
        }

        private void ImportRealEstatesListTest()
        {
            using (Stream stream = File.Open("RealEstatesList.txt", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                var realEstates = (List<RealEstate>)bin.Deserialize(stream);
                realEstates.ForEach(x => this.RealEstatesListTest.Add(x));
            }
            this.DataGridTest.ItemsSource = null;
            this.DataGridTest.ItemsSource = this.RealEstatesListTest;
        }

        private void EstateType_Click(object sender, RoutedEventArgs e)
        {
            //Specification specification = new Specification();
            //specification.Show();
        }

        //private void DataGridHyperlinkColumn_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
