using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;

namespace RealEstateProject
{
    public abstract class RealEstate
    {
        public string Type { get; set; }
        public double Price { get; set; }
        public double Surface { get; set; }
        public string City { get; set; }
        public double Rent { get; set; }
        public string Market { get; set; }
        public virtual int RealEstateID { get; set; }
        public static int RealEstateCount = 1;

        //działka
        public RealEstate(double price, double surface, Cities city)
        {
            this.Type = "Działka";
            this.Price = price;
            this.Surface = surface;

            switch (city)
            {
                case Cities.Białystok:
                    this.City = "Białystok";
                    break;
                case Cities.BuenosAires:
                    this.City = "Buenos Aires";
                    break;
                case Cities.Moskwa:
                    this.City = "Moskwa";
                    break;
            }
        }

        //dom
        public RealEstate(double rent, Markets market)
        {
            this.Type = "Dom";
            this.Rent = rent;

            switch (market)
            {
                case Markets.Primary:
                    this.Market = "Pierwotny";
                    break;
                case Markets.Secondary:
                    this.Market = "Wtórny";
                    break;
            }
        }

        //mieszkanie
        public RealEstate(double price, double surface, Cities city, double rent, Markets market)
        {
            this.Type = "Mieszkanie";
            this.Price = price;
            this.Surface = surface;
            this.Rent = rent;

            switch (market)
            {
                case Markets.Primary:
                    this.Market = "Pierwotny";
                    break;
                case Markets.Secondary:
                    this.Market = "Wtórny";
                    break;
            }

            switch (city)
            {
                case Cities.Białystok:
                    this.City = "Białystok";
                    break;
                case Cities.BuenosAires:
                    this.City = "Buenos Aires";
                    break;
                case Cities.Moskwa:
                    this.City = "Moskwa";
                    break;
            }
        }

        public enum Cities
        {
            Białystok,
            BuenosAires,
            Moskwa
        }

        public enum Markets
        {
            Primary,
            Secondary
        }
    }
}
