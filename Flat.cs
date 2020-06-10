using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RealEstateProject
{
    [Serializable]
    public class Flat : RealEstate
    {
        public int FloorNumber { get; set; }
        public int RoomsNumber { get; set; }
        public string FlatStandard { get; set; }
        public override int RealEstateID { get => base.RealEstateID; set => base.RealEstateID = value; }

        public Flat(int floorNumber, int roomsNumber, FlatStandards flatStandard, double price, float surface, Cities city, double rent, Markets market)
            : base(price, surface, city, rent, market)
        {
            ++RealEstate.RealEstateCount;
            this.FloorNumber = floorNumber;
            this.RoomsNumber = roomsNumber;
            this.RealEstateID = RealEstate.RealEstateCount;

            switch (flatStandard)
            {
                case FlatStandards.Apartment:
                    this.FlatStandard = "Apartament";
                    break;
                case FlatStandards.AverageFlat:
                    this.FlatStandard = "Mieszkanie klasy średniej";
                    break;
                case FlatStandards.CommunalFlat:
                    this.FlatStandard = "Mieszkanie komunalne";
                    break;
            }
        }

        public enum FlatStandards
        {
            CommunalFlat,
            AverageFlat,
            Apartment
        }

        public override string ToString()
        {
            return Convert.ToString(RealEstateID);
        }
    }
}
