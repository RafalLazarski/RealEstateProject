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

        public Flat(int floorNumber, int roomsNumber, FlatStandards flatStandard, double price, double surface, Cities city, double rent, Markets market)
            : base(price, surface, city, rent, market)
        {
            ++RealEstate.RealEstateCount;
            this.FlatStandard = flatStandard.ToString();
            this.FloorNumber = floorNumber;
            this.RoomsNumber = roomsNumber;
            this.RealEstateID = RealEstate.RealEstateCount;
        }

        public enum FlatStandards
        {
            Komunalne,
            KlasyŚredniej,
            Apartament
        }

        public override string ToString()
        {
            return Convert.ToString(RealEstateID);
        }
    }
}
