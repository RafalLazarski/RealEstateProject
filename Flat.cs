using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    public class Flat : RealEstate
    {
        public int FloorNumber { get; set; }
        public int RoomsNumber { get; set; }
        public FlatStandards FlatStandard { get; set; }

        public Flat(int floorNumber, int roomsNumber, FlatStandards flatStandard, double price, double surface, Cities city, double rent, Markets market)
            : base(price, surface, city, rent, market)
        {
            this.FloorNumber = floorNumber;
            this.RoomsNumber = roomsNumber;
            this.FlatStandard = flatStandard;
        }

        public enum FlatStandards
        {
            CommunalFlat,
            AverageFlat,
            Apartment
        }
    }
}
