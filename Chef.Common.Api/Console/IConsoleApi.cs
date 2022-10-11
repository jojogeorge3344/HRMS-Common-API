using Chef.Common.Models;
using Refit;
using TimeZone = Chef.Common.Models.TimeZone;

namespace Chef.Common.Api
{
    public interface IConsoleApi
    {
        [Get("/Master/GetAllCountries")]
        Task<IEnumerable<CountryMaster>> GetMasterCountriesAsync();

        [Get("/Country/GetAll")]
        Task<IEnumerable<Country>> GetCountriesAsync();

        [Get("/TimeZone/GetAllTimeZone")]
        Task<IEnumerable<TimeZone>> GetTimeZonesAsync();

        [Get("/Master/GetAllCurrencies")]
        Task<IEnumerable<Currency>> GetCurrenciesAsync();

        [Get("/State/GetAllByCountry/{countryId}")]
        Task<IEnumerable<State>> GetStatesAsync(int countryId);
    }
}