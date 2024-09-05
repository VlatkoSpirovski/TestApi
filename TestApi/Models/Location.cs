using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models;

public class Location
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string City { get; set; }
    
    [ForeignKey(nameof(WeatherForecast))]
    public int WeatherForecastId { get; set; }
    public WeatherForecast WeatherForecast { get; set; }
}