using GoogleApi;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using GoogleApi.Entities.Maps.Geocoding.Location.Request;
using GoogleApi.Entities.Maps.Geocoding.Place.Request;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodBot.Parsers
{
    public class Geocoding
    {
        private readonly string apiKey;

        public Geocoding(IConfiguration configuration)
        {
            apiKey = configuration["GoogleApiSecret"];
        }

        public async Task<GeocodeResponse> GetCoordinatesAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return new GeocodeResponse();
            }

            var request = new AddressGeocodeRequest
            {
                Key = apiKey,
                Address = address,
                Language = Language.Russian,
            };

            var result = await GoogleMaps.AddressGeocode.QueryAsync(request);
            return result;
        }
    }
}