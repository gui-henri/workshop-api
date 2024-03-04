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
        public async Task<Collaborator?> CreateCollaborator(Collaborator collaborator)
        {
            _context.Collaborators.Add(collaborator);

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

        public async Task<Collaborator?> DeleteCollaborator(Collaborator collaborator)
        {
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

        public async Task<Collaborator?> GetCollaborator(Guid id)
        {
            return await _context.Collaborators.FindAsync(id);
        }

        public async Task<ICollection<Collaborator>> GetCollaborators()
        {
            try
            {
                var collaborators = await _context.Collaborators.ToListAsync();
                if (collaborators == null)
                {
                    return [];
                }
                return collaborators;

            } catch
            {
                throw;
            }

        }

        public async Task<Collaborator?> UpdateCollaborator(Collaborator collaborator)
        {
            _context.Collaborators.Update(collaborator);
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
