using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    [Serializable]
    public class Plot : RealEstate
    {
        public string PlotTypeName { get; set; }
        public string RealEstateType { get; set; }
        public override int RealEstateID { get => base.RealEstateID; set => base.RealEstateID = value; }


        public Plot(PlotTypes plotType, double price, double surface, Cities city, Markets market)
            : base(price, surface, city, market)
        {
            ++RealEstate.RealEstateCount;
            this.RealEstateID = RealEstate.RealEstateCount;
            this.PlotTypeName = plotType.ToString();
        }

        public enum PlotTypes
        {
            Budowlana,
            Letniskowa,
            Leśna
        }

        public override string ToString()
        {
            return Convert.ToString(RealEstateID);
        }
    }
}
