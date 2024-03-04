using WorkshopApi.Dto;
using WorkshopApi.Entities;
using WorkshopApi.Interfaces;

namespace WorkshopApi.Services
{
    public class WorkshopService(IWorkshopRepository workshopRepository, ICollaboratorRepository collaboratorRepository)
    {
        public readonly IWorkshopRepository _repository = workshopRepository;
        public readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository;

        public async Task<Workshop?> GetWorkshop(Guid id)
        {
            return await _repository.GetWorkshop(id);  
        }

        public async Task<ICollection<Workshop>> GetWorkshops()
        {
            return await _repository.GetWorkshops();
        }

        public async Task<Workshop?> UpdateWorkshop(Guid id, WorkshopDTO workshop)
        {
            var newWorkshop = await GetWorkshop(id);
            if (newWorkshop == null) return null;

            newWorkshop.Update(workshop.Name, workshop.Description, workshop.Date);

            try
            {
                var updatedWorkshop = await _repository.UpdateWorkshop(newWorkshop);
                return updatedWorkshop;
            } catch
            {
                throw;
            }
        }

        public async Task<Workshop?> CreateWorkshop(WorkshopDTO workshop)
        {
            var newWorkshop = Workshop.FromDTO(workshop);
            if (newWorkshop == null)
            {
                return null;
            }

            try
            {
                return await _repository.CreateWorkshop(newWorkshop);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Workshop?> DeleteWorkshop(Guid id)
        {
            var workshop = await GetWorkshop(id);
            if (workshop == null) return null;
            try
            {
                return await _repository.DeleteWorkshop(workshop);
            } catch
            {
                throw;
            }
        } 

        public async Task<ICollection<Collaborator>> GetCollaborators(Guid workshopId)
        {
            return await _repository.GetCollaborators(workshopId);
        }

        public async Task<CollaboratorWorkshop?> AddCollaborator(Guid workshopId, Guid collaboratorId)
        {
            var workshop = await GetWorkshop(workshopId);
            if (workshop == null) return null;

            var collaborator = await _collaboratorRepository.GetCollaborator(collaboratorId);
            if (collaborator == null) return null;

            var collaboratorWorkshop = new CollaboratorWorkshop
            {
                WorkshopId = workshopId,
                Workshop = workshop,
                CollaboratorId = collaboratorId,
                Collaborator = collaborator
            };
            try
            {
                return await _repository.AddCollaborator(collaboratorWorkshop);
            } catch
            {
                throw;
            }
        }
    }
}
