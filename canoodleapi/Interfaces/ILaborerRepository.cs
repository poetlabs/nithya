using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface ILaborerRepository
    {
        Laborers SaveLaborers(Laborers laborer);
        LaborerLogin UserLogin(UserloginInput userlogin);
        List<Laborers> GetAllUsers();
        bool DeleteLaborers(int laborerId);
    }
}
