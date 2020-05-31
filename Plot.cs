using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    public class Plot : RealEstate
    {
        public string PlotTypeName { get; set; }
        public string RealEstateType { get; set; }
        public override int RealEstateID { get => base.RealEstateID; set => base.RealEstateID = value; }

        public Plot(PlotTypes plotTypeID, double price, double surface, Cities city)
            : base(price, surface, city)
        {
            this.RealEstateID = RealEstate.RealEstateCount;
            RealEstate.RealEstateCount++;
            switch (plotTypeID)
            {
                case PlotTypes.BuildingPlot:
                    this.PlotTypeName = "Działka budowlana";
                    break;
                case PlotTypes.ForestPlot:
                    this.PlotTypeName = "Działka leśna";
                    break;
                case PlotTypes.SummerPlot:
                    this.PlotTypeName = "Działka letniskowa";
                    break;
            }
        }

        public enum PlotTypes
        {
            ForestPlot,
            SummerPlot,
            BuildingPlot
        }
    }
}
