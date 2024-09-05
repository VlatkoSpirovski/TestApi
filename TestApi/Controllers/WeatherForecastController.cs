using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public WeatherForecastController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecasts()
        {
            try
            {
                var forecasts = await _dbContext.WeatherForecasts.ToListAsync();
                if (forecasts == null || !forecasts.Any())
                {
                    return NotFound("No weather forecasts found.");
                }
                return Ok(forecasts);
            }
            catch (Exception ex)
            {
                // Log exception here if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving weather forecasts.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(int id)
        {
            try
            {
                var weatherForecast = await _dbContext.WeatherForecasts.FindAsync(id);
                if (weatherForecast == null)
                {
                    return NotFound($"Weather forecast with ID {id} not found.");
                }
                return Ok(weatherForecast);
            }
            catch (Exception ex)
            {
                // Log exception here if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the weather forecast.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWeatherForecast([FromBody] WeatherForecast weatherForecast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _dbContext.WeatherForecasts.Add(weatherForecast);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetWeatherForecast), new { id = weatherForecast.Id }, weatherForecast);
            }
            catch (Exception ex)
            {
                // Log exception here if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the weather forecast.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutWeatherForecast(int id, [FromBody] WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id)
            {
                return BadRequest("Weather forecast ID mismatch.");
            }

            _dbContext.Entry(weatherForecast).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
                {
                    return NotFound($"Weather forecast with ID {id} not found.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the weather forecast.");
                }
            }
            catch (Exception ex)
            {
                // Log exception here if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the weather forecast.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWeatherForecast(int id)
        {
            try
            {
                var weatherForecast = await _dbContext.WeatherForecasts.FindAsync(id);
                if (weatherForecast == null)
                {
                    return NotFound($"Weather forecast with ID {id} not found.");
                }

                _dbContext.WeatherForecasts.Remove(weatherForecast);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception here if necessary
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the weather forecast.");
            }
        }

        private bool WeatherForecastExists(int id)
        {
            return _dbContext.WeatherForecasts.Any(e => e.Id == id);
        }
    }
}
