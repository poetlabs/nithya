using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface ILaborerRepository
    {
        Task<IEnumerable<Laborer>> GetAllLaborersAsync();
        Task<Laborer> GetLaborerByIdAsync(string laborerId);
        Task CreateLaborerAsync(Laborer laborer);
        Task UpdateLaborerAsync(Laborer laborer);
        Task DeleteLaborerAsync(string laborerId);
    }
}
