using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace canoodleapi.Controllers
{// RoutesController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteRepository _routeRepository;

        public RoutesController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoutes()
        {
            var routes = await _routeRepository.GetAllRoutesAsync();
            return Ok(routes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteById(string id)
        {
            var route = await _routeRepository.GetRouteByIdAsync(id);
            return route == null ? NotFound() : Ok(route);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoute(Routes route)
        {
            await _routeRepository.CreateRouteAsync(route);
            return CreatedAtAction(nameof(GetRouteById), new { id = route.RouteId }, route);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(int id, Routes route)
        {
            if (id != route.RouteId) return BadRequest();
            await _routeRepository.UpdateRouteAsync(route);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(string id)
        {
            await _routeRepository.DeleteRouteAsync(id);
            return NoContent();
        }
    }

}
