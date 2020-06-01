using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    public class UserPreferences
    {
        public RealEstate.Types[] FavouriteType { get; set; }
        //public string FavouritePrice { get; set; }
        public RealEstate.Cities[] FavouriteCity { get; set; }
        public RealEstate.Markets[] FavouriteMarket { get; set; }
        //public string FavouriteSurface { get; set; }

        public UserPreferences(RealEstate.Types[] favouriteType, RealEstate.Cities[] favouriteCity, RealEstate.Markets[] favouriteMarket)
        {
            this.FavouriteCity[0] = favouriteCity[0];
            this.FavouriteCity[1] = favouriteCity[1];
            this.FavouriteCity[2] = favouriteCity[2];
            this.FavouriteMarket[0] = favouriteMarket[0];
            this.FavouriteMarket[1] = favouriteMarket[1];
            this.FavouriteType[0] = favouriteType[0];
            this.FavouriteType[1] = favouriteType[1];
            this.FavouriteType[2] = favouriteType[2];
        }
    }
}
