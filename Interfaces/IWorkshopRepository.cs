using WorkshopApi.Entities;

namespace WorkshopApi.Interfaces
{
    public interface IWorkshopRepository
    {
        ICollection<Workshop> GetWorkshops();
        Workshop GetWorkshop(Guid id);
        Workshop CreateWorkshop(Workshop workshop);
        Workshop UpdateWorkshop(Guid id, Workshop workshop);
        bool WorkshopExists(Guid id);
        void DeleteWorkshop(Guid id);
    }
}
