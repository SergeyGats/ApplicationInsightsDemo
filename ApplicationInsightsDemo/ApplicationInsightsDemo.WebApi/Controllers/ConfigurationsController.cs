using ApplicationInsightsDemo.Configuration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApplicationInsightsDemo.WebApi.Controllers;

public class ConfigurationsController : ApplicationControllerBase
{
    private readonly IOptionsMonitor<FirstFeatureSettings> _firstFeatureOptionMonitor;

    public ConfigurationsController(IOptionsMonitor<FirstFeatureSettings> firstFeatureOptionMonitor)
    {
        _firstFeatureOptionMonitor = firstFeatureOptionMonitor;
    }

    [HttpGet("FirstFeature")]
    public IActionResult Get()
    {
        var values = new List<string>
        {
            _firstFeatureOptionMonitor.CurrentValue.StartDate.ToString(),
            _firstFeatureOptionMonitor.CurrentValue.EndDate.ToString(),
            _firstFeatureOptionMonitor.CurrentValue.Message
        };

        return Ok(values);
    }
}