using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    public class UserPreferences
    {
        public RealEstate.Types FavouriteType { get; set; }
        //public string FavouritePrice { get; set; }
        public RealEstate.Cities FavouriteCity { get; set; }
        public RealEstate.Markets FavouriteMarket { get; set; }
        //public string FavouriteSurface { get; set; }

        public UserPreferences(RealEstate.Types favouriteType, RealEstate.Cities favouriteCity, RealEstate.Markets favouriteMarket)
        {
            this.FavouriteCity = favouriteCity;
            this.FavouriteMarket = favouriteMarket;
            this.FavouriteType = favouriteType;
        }
    }
}
