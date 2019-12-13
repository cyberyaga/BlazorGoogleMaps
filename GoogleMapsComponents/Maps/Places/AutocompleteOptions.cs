using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneOf;

namespace GoogleMapsComponents.Maps.Places
{
    public class AutocompleteOptions
    {
        public OneOf<LatLngBounds, LatLngBoundsLiteral> Bounds { get; set; }
        public ComponentRestrictions ComponentRestrictions { get; set; }
        public string[] Fields { get; set; }
        public bool StrictBounds { get; set; }
        public string[] Types { get; set; }
    }
}
