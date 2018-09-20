using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyCodes.eShopOnContainers.Services.LocationsAPI.Infrastructure.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error) : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
