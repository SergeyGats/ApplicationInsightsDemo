using ApplicationInsightsDemo.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationInsightsDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationControllerBase : ControllerBase
    {
        private int? _userId;

        public int UserId
        {
            get
            {
                _userId ??= User.GetUserId();
                return _userId.Value;
            }
        }
    }
}