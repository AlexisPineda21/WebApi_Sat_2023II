using Microsoft.AspNetCore.Mvc;
using WebAPI_Sat_2023II.git.DAL.Entities;
using WebAPI_Sat_2023II.git.Domain.Interfaces;

namespace WebAPI_Sat_2023II.git.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {

        private readonly ICountryService _countryService;    
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        //En un controlador los métodos cambian de nombre a acciones(ACTIONS), Si es una API se denominda EndPoint.
        //Todo EndPoint retorna un ActionResult, Significa que retorna el resultado de una acción (de un metodo).
        [HttpGet, ActionName("Get")]
        [Route("Get")] //Aquí concateno la URL inicial. 
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync(); //Llendo a la capa domain para traer la lista de paises.

            if(countries==null || !countries.Any()) //El método any significa si hay almenos un elemento. 
            {
                return NotFound();  //NotFound = 404 Http Status Code
            }
            return Ok(countries);   //Ok = 200 Http Status Code
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountryAsync(Country country)
        {
            try
            {
                var createdCountry = await _countryService.CreateCountryAsync(country);

                if (createdCountry == null) 
                {
                    return NotFound(); 
                }
                return Ok(createdCountry);  
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("duplicated"))
                {
                    return Conflict(String.Format("El país {0} ya existe.", country.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //Aquí concateno la URL inicial. 
        public async Task<ActionResult<Country>> GetCountryByIdAsync(Guid id)
        {
            if(id==null) return BadRequest("Id es requerido!");

            var country = await _countryService.GetCountryByIdAsync(id); 

            if (country == null) return NotFound();  //NotFound = 404 Http Status Code
            
            return Ok(country);   //Ok = 200 Http Status Code
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult> EditCountryAsync(Country country)
        {
            try
            {
                var editedCountry = await _countryService.EditCountryAsync(country);
                return Ok(editedCountry);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicated"))
                {
                    return Conflict(String.Format("El país {0} ya existe.", country.Name));
                }
                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult> DeleteCountryAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido");
            var deletedCountry = await _countryService.DeleteCountryAsync(id);
            if (deletedCountry == null) return NotFound("País no encontrado");  
            return Ok(deletedCountry);
        }
    }
}
