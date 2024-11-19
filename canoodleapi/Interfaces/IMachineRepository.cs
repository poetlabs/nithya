using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface IMachineRepository
    {
        
        Machines SaveMachines(Machines machines);
        List<Machines> GetAllMachines();
        bool DeleteMechine(int machineId);
    }
}
