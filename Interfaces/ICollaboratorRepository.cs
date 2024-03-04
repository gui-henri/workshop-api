using WorkshopApi.Dtos;
using WorkshopApi.Entities;

namespace WorkshopApi.Interfaces
{
    public interface ICollaboratorRepository
    {
        Task<ICollection<Collaborator>> GetCollaborators();
        Task<Collaborator?> GetCollaborator(Guid id);
        Task<Collaborator?> CreateCollaborator(Collaborator collaborator);
        Task<Collaborator?> UpdateCollaborator(Collaborator collaborator);
        Task<Collaborator?> DeleteCollaborator(Collaborator collaborator);
    }
}
