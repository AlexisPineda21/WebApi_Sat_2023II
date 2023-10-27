using Microsoft.EntityFrameworkCore;
using WebAPI_Sat_2023II.git.DAL;
using WebAPI_Sat_2023II.git.DAL.Entities;
using WebAPI_Sat_2023II.git.Domain.Interfaces;

namespace WebAPI_Sat_2023II.git.Domain.Services
{
    public class CountryService : ICountryService
    {
        public readonly DataBaseContext _context;
        public CountryService(DataBaseContext context)
        {
            _context= context;
        }


        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync(); //Traigo todos los datos de mi tabla countries.
        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
            try
            {
                country.Id= Guid.NewGuid(); //Se asigna automaticamente un ID a un registro
                country.CreatedDate= DateTime.Now; 

                _context.Countries.Add(country); //Aquí estoy creando el objeto Country en el contexto de mi Bd
                await _context.SaveChangesAsync(); //Yendo a la bd para hacer el INSERT en la tabla countries

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //Captura un mensaje cuando el país ya existe
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message); 
                //coallesense notation --> ?? (Validación de nulleables)
            }
        }
    }
}
