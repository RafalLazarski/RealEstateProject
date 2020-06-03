using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;

namespace RealEstateProject
{
    [Serializable]
    public abstract class RealEstate
    {
        public string Type { get; set; }
        public double Price { get; set; }
        public float Surface { get; set; }
        public string City { get; set; }
        public double Rent { get; set; }
        public string Market { get; set; }
        public virtual int RealEstateID { get; set; }
        public static int RealEstateCount = 1;

        //działka
        public RealEstate(double price, float surface, Cities city, Markets market)
        {
            this.Type = "Działka";
            this.Price = price;
            this.Surface = surface;
            this.City = city.ToString();
            this.Market = market.ToString();
        }

        //dom
        public RealEstate(double rent)
        {
            this.Type = "Dom";
            this.Rent = rent;
        }

        //mieszkanie
        public RealEstate(double price, float surface, Cities city, double rent, Markets market)
        {
            this.Type = "Mieszkanie";
            this.Price = price;
            this.Surface = surface;
            this.Rent = rent;
            this.Market = market.ToString();
            this.City = city.ToString();
        }

        public enum Cities
        {
            Białystok,
            BuenosAires,
            Moskwa
        }

        public enum Markets
        {
            Pierwotny,
            Wtórny
        }

        public enum Types
        {
            Działka,
            Dom,
            Mieszkanie
        }
    }
}
