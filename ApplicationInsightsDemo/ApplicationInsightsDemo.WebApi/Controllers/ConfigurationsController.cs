using ApplicationInsightsDemo.WebApi.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApplicationInsightsDemo.WebApi.Controllers;

public class ConfigurationsController : ApplicationControllerBase
{
    private readonly IOptionsMonitor<AppConfOptions> _optionsMonitor;

    public ConfigurationsController(IOptionsMonitor<AppConfOptions> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
    }

    [HttpGet]
    public List<string> GetAll()
    {
        return new List<string>
        {
            _optionsMonitor.CurrentValue.FirstConfValue,
            _optionsMonitor.CurrentValue.SecondConfValue
        };
    }
}