using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface IRouteRepository
    {        
        Routes SaveRoutes(Routes routes);
        List<Routes> GetAllRoutes();
        bool DeleteRoutes(int routeId);
        List<Shift> GetAllShift();
        Shift SaveShift(Shift shift);
        bool DeleteShift(int shiftID);



    }
}
