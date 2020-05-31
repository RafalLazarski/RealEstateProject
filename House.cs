using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    public class House : RealEstate
    {
        public Plot HousePlot { get; set; }
        public int NumberOfFloors { get; set; }
        public double Area { get; set; }
        public string TypeOfOven { get; set; }


        public House(Plot housePlot, int numberOfFloors, double area, TypesOfOven typeOfOven, double rent, Markets market)
            : base(rent, market)
        {
            this.HousePlot = housePlot;
            this.NumberOfFloors = numberOfFloors;
            this.Area = area;

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
    }
}
