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
        public double Surface { get; set; }
        public string City { get; set; }
        public double Rent { get; set; }
        public string Market { get; set; }
        public virtual int RealEstateID { get; set; }
        public static int RealEstateCount = 0;
        public int OwnerID { get; set;}
        public bool SoldItem { get; set; }
        public string OwnerName { get; set; }

        //działka
        public RealEstate(double price, double surface, Cities city, Markets market)
        {
            this.OwnerID = 1;
            this.SoldItem = false;
            this.Type = Types.Działka.ToString();
            this.Price = price;
            this.Surface = surface;
            this.City = city.ToString();
            this.Market = market.ToString();
        }

        //dom
        public RealEstate(double rent)
        {
            this.OwnerID = 1;
            this.SoldItem = false;
            this.Type = Types.Dom.ToString();
            this.Rent = rent;
        }

        //mieszkanie
        public RealEstate(double price, double surface, Cities city, double rent, Markets market)
        {
            this.OwnerID = 1;
            this.SoldItem = false;
            this.Type = Types.Mieszkanie.ToString();
            this.Price = price;
            this.Surface = surface;
            this.Rent = rent;
            this.Market = market.ToString();
            this.City = city.ToString();
        }

        public enum Cities
        {
            Białystok,
            Buenos_Aires,
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
