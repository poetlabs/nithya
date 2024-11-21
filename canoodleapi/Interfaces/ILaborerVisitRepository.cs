using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface ILaborerVisitRepository
    {
        Task<IEnumerable<LaborerVisits>> GetAllVisitsAsync();
        Task<LaborerVisits> GetVisitByIdAsync(int visitId);
        Task CreateVisitAsync(LaborerVisits visit);
        Task UpdateVisitAsync(LaborerVisits visit);
        Task DeleteVisitAsync(int visitId);
    }
}
