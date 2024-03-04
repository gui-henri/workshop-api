using WorkshopApi.Entities;

namespace WorkshopApi.Interfaces
{
    public interface IWorkshopRepository
    {
        Task<ICollection<Workshop>> GetWorkshops();
        Task<Workshop?> GetWorkshop(Guid id);
        Task<Workshop?> CreateWorkshop(Workshop Workshop);
        Task<Workshop?> UpdateWorkshop(Workshop Workshop);
        Task<Workshop?> DeleteWorkshop(Workshop workshop);
        Task<ICollection<Collaborator>> GetCollaborators(Guid workshopId);
        Task<CollaboratorWorkshop?> AddCollaborator(CollaboratorWorkshop collaboratorWorkshop);
    }
}
