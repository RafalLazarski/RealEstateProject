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

        public Plot(PlotTypes plotTypeID, double price, double surface, Cities city)
            : base(price, surface, city)
        {
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
