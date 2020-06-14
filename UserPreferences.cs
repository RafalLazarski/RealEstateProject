using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    [Serializable]
    public class UserPreferences
    {
        public List<RealEstate.Types> FavouriteTypes { get; set; }
        public List<RealEstate.Cities> FavouriteCities { get; set; }
        public List<RealEstate.Markets> FavouriteMarkets { get; set; }

        public UserPreferences(List<RealEstate.Types> favouriteTypes, List<RealEstate.Cities> favouriteCities, List<RealEstate.Markets> favouriteMarkets)
        {
            this.FavouriteCities = favouriteCities;
            this.FavouriteMarkets = favouriteMarkets;
            this.FavouriteTypes = favouriteTypes;
        }
    }
}
