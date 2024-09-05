using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models;

public class WeatherForecast
{
    
    public WeatherForecast(){}

    public WeatherForecast(DateTime date, int temperatureC, int temperatureF, string summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        TemperatureF = temperatureF;
        Summary = summary;
    }
    [Key]
    public int Id {get; set;}
    [Required]
    public DateTime Date {get; set;}
    [Range(-100, 150)]
    public int TemperatureC {get; set;}
    [Range(-100, 150)]
    public int TemperatureF {get; set;}
    [StringLength(100)]
    public string Summary {get; set;}

    public ICollection<Location> Locations { get; set; } = new List<Location>();

}