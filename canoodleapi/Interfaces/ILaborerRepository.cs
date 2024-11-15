using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface ILaborerRepository
    {
        Laborers SaveLaborers(Laborers laborer);
        bool GetlaborerbyUsername(string username, int qpin);
    }
}
