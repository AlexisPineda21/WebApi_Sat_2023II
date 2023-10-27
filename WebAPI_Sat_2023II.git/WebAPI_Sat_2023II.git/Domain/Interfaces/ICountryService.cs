using WebAPI_Sat_2023II.git.DAL.Entities;

namespace WebAPI_Sat_2023II.git.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountriesAsync(); //Una firma de un método.

        Task<Country> CreateCountryAsync(Country country);

    }
}
