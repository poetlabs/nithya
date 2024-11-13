using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface ILaborerVisitRepository
    {
        Task<IEnumerable<LaborerVisit>> GetAllVisitsAsync();
        Task<LaborerVisit> GetVisitByIdAsync(int visitId);
        Task CreateVisitAsync(LaborerVisit visit);
        Task UpdateVisitAsync(LaborerVisit visit);
        Task DeleteVisitAsync(int visitId);
    }
}
