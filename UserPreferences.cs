using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RealEstateProject.RealEstate;

namespace RealEstateProject
{
    [Serializable]
    public class UserPreferences
    {
        public List<Types> FavouriteTypes { get; set; }
        public List<Cities> FavouriteCities { get; set; }
        public List<Markets> FavouriteMarkets { get; set; }

        public UserPreferences(List<Types> favouriteTypes, List<Cities> favouriteCities, List<Markets> favouriteMarkets)
        {
            this.FavouriteCities = favouriteCities;
            this.FavouriteMarkets = favouriteMarkets;
            this.FavouriteTypes = favouriteTypes;
        }
    }
}
