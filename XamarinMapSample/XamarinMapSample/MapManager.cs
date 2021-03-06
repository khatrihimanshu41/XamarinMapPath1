﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace XamarinMapSample
{
  public  class MapManager
    {
        private readonly Map _map;
        public MapManager(Map map)
        {
            this._map = map;
        }
        public async Task<IEnumerable<Position>> FindAddress(string addressQuery)
        {
            var geoCoder = new Geocoder();
            var locations = await geoCoder.GetPositionsForAddressAsync(addressQuery);
            return locations;
        }
        public void ClearAllPins()
        {
            _map.Pins.Clear();
        }
        public void AddLocationPins(IEnumerable<Position> positions)
        {
            Pin pin = null;
            foreach (var position in positions)
            {
                pin = new Pin
                {
                    Type = PinType.Generic,
                    Position = position,
                    Label = ""
                };
                _map.Pins.Add(pin);
            }
            if (null != pin)
            {
               FocusMapToPosition(pin.Position,5.0);
            }
        }
        public void FocusMapToPosition(Position position, double regionRadius)
        {
            var mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMiles(regionRadius));
            this._map.MoveToRegion(mapSpan);
        }
        public  void RemovePin(double a, double b)
        {
            var position = new Position(a, b); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = position,
                Label = "",
                // Address = Address
            };
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMeters(5000)));
            _map.Pins.Remove(pin);
            pin = null;
        }
        public void SearchPin()
        {
        }
    }
}
