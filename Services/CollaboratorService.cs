using WorkshopApi.Dtos;
using WorkshopApi.Entities;
using WorkshopApi.Interfaces;

namespace WorkshopApi.Services
{
    public class CollaboratorService(ICollaboratorRepository repository)
    {
        private readonly ICollaboratorRepository _repository = repository;

        public async Task<Collaborator?> GetCollaborator(Guid id)
        {
            return await _repository.GetCollaborator(id);
        }

        public async Task<ICollection<Collaborator>> GetCollaborators()
        {
            return await _repository.GetCollaborators();
        }

        public async Task<Collaborator?> UpdateCollaborator(Guid id, CollaboratorDTO collaborator)
        {
            var newCollaborator = await GetCollaborator(id);

            if (newCollaborator == null) return null;

            newCollaborator.Update(collaborator.Name);

            var updatedCollaborator = await _repository.UpdateCollaborator(newCollaborator);
            if (updatedCollaborator == null)
            {
                return null;
            }

            return updatedCollaborator;
        }

        public async Task<Collaborator?> CreateCollaborator(CollaboratorDTO collaborator)
        {

            var newCollaborator = Collaborator.FromDTO(collaborator);
            if (newCollaborator == null)
            {
                return null;   
            }

            var createdCollaborator = await _repository.CreateCollaborator(newCollaborator);

            if (createdCollaborator == null)
            {
                return null;
            }

            return createdCollaborator;
        }

        public async Task<Collaborator?> DeleteCollaborator(Guid id)
        {
            var collaborator = await GetCollaborator(id);
            if (collaborator == null) return null;
            
            try
            {
                var deletedCollaborator = await _repository.DeleteCollaborator(collaborator);

                return deletedCollaborator;
            } catch
            {
                return null;
            }
            
        }

    }
}
