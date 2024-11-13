using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface IRouteRepository
    {
        Task<IEnumerable<Routes>> GetAllRoutesAsync();
        Task<Routes> GetRouteByIdAsync(string routeId);
        Task CreateRouteAsync(Routes route);
        Task UpdateRouteAsync(Routes route);
        Task DeleteRouteAsync(string routeId);
        
    }
}
