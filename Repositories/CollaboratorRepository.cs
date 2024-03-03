using Microsoft.EntityFrameworkCore;
using WorkshopApi.Contexts;
using WorkshopApi.Dtos;
using WorkshopApi.Entities;
using WorkshopApi.Interfaces;

namespace WorkshopApi.Repositories
{
    public class CollaboratorRepository(WorkshopContext context) : ICollaboratorRepository
    {
        private readonly WorkshopContext _context = context;

        public async Task<Collaborator?> GetCollaborator(Guid id)
        {
            return await _context.Collaborators.FindAsync(id);
        }

        public async Task<ICollection<Collaborator>> GetCollaborators()
        {
            try 
            {
                return await _context.Collaborators.ToListAsync();
            } catch
            {
                return [];
            }
        }

        public async Task<Collaborator?> UpdateCollaborator(Guid id, CollaboratorDTO collaborator)
        {
            var newCollaborator = await GetCollaborator(id);
            if (newCollaborator == null) return null;
            newCollaborator.Update(collaborator.Name);

            _context.Collaborators.Update(newCollaborator);
            try
            {
                await _context.SaveChangesAsync();
                return newCollaborator;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Collaborator?> CreateCollaborator(CollaboratorDTO collaborator)
        {

            var newCollaborator = Collaborator.FromDTO(collaborator);
            if (newCollaborator == null)
            {
                return null;   
            }

            _context.Collaborators.Add(newCollaborator);

            try
            {
                await _context.SaveChangesAsync();
                return newCollaborator;

            } catch
            {
                return null;
            }
        }

        public async Task<Collaborator?> DeleteCollaborator(Guid id)
        {
            var collaborator = await GetCollaborator(id);
            if (collaborator == null) return null;
            _context.Collaborators.Remove(collaborator);
            try
            {
                await _context.SaveChangesAsync();
                return collaborator;
            }
            catch
            {
                throw;
            }
        }


    }
}
