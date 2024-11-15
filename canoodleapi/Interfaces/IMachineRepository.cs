using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface IMachineRepository
    {
        
        Machines SaveMachines(Machines machines);
        List<Machines> GetAllMachines();
    }
}
