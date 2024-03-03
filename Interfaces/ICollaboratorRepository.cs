using WorkshopApi.Dtos;
using WorkshopApi.Entities;

namespace WorkshopApi.Interfaces
{
    public interface ICollaboratorRepository
    {
        Task<ICollection<Collaborator>> GetCollaborators();
        Task<Collaborator?> GetCollaborator(Guid id);
        Task<Collaborator?> CreateCollaborator(CollaboratorDTO collaborator);
        Task<Collaborator?> UpdateCollaborator(Guid id, CollaboratorDTO collaborator);
        Task<Collaborator?> DeleteCollaborator(Guid id);
    }
}
