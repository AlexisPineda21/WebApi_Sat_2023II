using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
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

        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            //return await _context.Countries.FindAsync(id); //método propio de Db Context (DbSet)
            return await _context.Countries.FirstAsync(x => x.Id == id); //First Async es un método de EF CORE
            //return await _context.Countries.FirstOrDefaultAsync(x => x.Id == id); //FirstOrDefaultAsync es un método de EF CORE

        }

        public async Task<Country> EditCountryAsync(Country country)
        {
            try
            {
                country.ModifiedDate = DateTime.Now;

                _context.Countries.Update(country); //Método update sirve para actualizar un objeto
                await _context.SaveChangesAsync();

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //Captura un mensaje cuando el país ya existe
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
                //coallesense notation --> ?? (Validación de nulleables)
            }
        }

        public async Task<Country> DeleteCountryAsync(Guid id)
        {
            try
            {
                //Con el ID traigo desde el controller, el pais que luego voy a eliminar.
                //Lo guardo en la variable country.
                var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
                if (country == null) return null;

                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }


        }
    }
}
