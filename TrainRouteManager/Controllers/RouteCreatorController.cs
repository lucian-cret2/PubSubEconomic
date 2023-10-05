using Microsoft.AspNetCore.Mvc;
using TrainRouteManager.Domain;
using TrainRouteManager.Services;

namespace TrainRouteManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteCreatorController : ControllerBase
    {
        private readonly IRouteCreatorService _routeCreatorService;

        public RouteCreatorController(IRouteCreatorService routeCreatorService)
        {
            _routeCreatorService = routeCreatorService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainRoute trainRoute)
        {
            var result = await _routeCreatorService.CreateAsync(trainRoute.Name, trainRoute.InitialSeats);

            return Created("", result);
        }
    }
}