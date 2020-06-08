using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    [Serializable]
    public class House : RealEstate
    {
        public Plot HousePlot { get; set; }
        public int NumberOfFloors { get; set; }
        public double Area { get; set; }
        public string TypeOfOven { get; set; }
        public override int RealEstateID { get => base.RealEstateID; set => base.RealEstateID = value; }


        public House(Plot housePlot, int numberOfFloors, double area, TypesOfOven typeOfOven, double rent)
            : base(rent)
        {
            this.RealEstateID = housePlot.RealEstateID;
            this.HousePlot = housePlot;
            this.NumberOfFloors = numberOfFloors;
            this.Area = area;
            this.Price = housePlot.Price;
            this.City = housePlot.City;
            this.Market = housePlot.Market;

            switch (typeOfOven)
            {
                case TypesOfOven.CoalFurnace:
                    this.TypeOfOven = "Piec węglowy";
                    break;
                case TypesOfOven.ElectricStove:
                    this.TypeOfOven = "Piec elektryczny";
                    break;
                case TypesOfOven.PelletStove:
                    this.TypeOfOven = "Piec na pellet";
                    break;
            }
        }


        public enum TypesOfOven
        {
            PelletStove,
            ElectricStove,
            CoalFurnace
        }

        public override string ToString()
        {
            return Convert.ToString(RealEstateID);
        }
    }
}
