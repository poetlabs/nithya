using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface IRouteRepository
    {        
        Routes SaveRoutes(Routes routes);
        List<Routes> GetAllRoutes();


    }
}
