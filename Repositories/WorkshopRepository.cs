using Microsoft.EntityFrameworkCore;
using WorkshopApi.Contexts;
using WorkshopApi.Entities;
using WorkshopApi.Interfaces;

namespace WorkshopApi.Repositories
{
    public class WorkshopRepository(WorkshopContext context) : IWorkshopRepository
    {
        private readonly WorkshopContext _context = context;

        public async Task<CollaboratorWorkshop?> AddCollaborator(CollaboratorWorkshop collaboratorWorkshop)
        {
            _context.CollaboratorWorkshop.Add(collaboratorWorkshop);
            try
            {
                await _context.SaveChangesAsync();
                return collaboratorWorkshop;
            } catch
            {
                throw;
            }
        }

        public async Task<Workshop?> CreateWorkshop(Workshop Workshop)
        {
            _context.Workshop.Add(Workshop);
            try
            {
                await _context.SaveChangesAsync();
                return Workshop;
            } catch
            {
                throw;
            }
        }

        public async Task<Workshop?> DeleteWorkshop(Workshop workshop)
        {
            _context.Workshop.Remove(workshop);
            try
            {
                await _context.SaveChangesAsync();
                return workshop;
            } catch
            {
                throw;
            }
        }

        public async Task<ICollection<Collaborator>> GetCollaborators(Guid workshopId)
        {
            var collaborators = await _context.CollaboratorWorkshop.Where(c => c.WorkshopId == workshopId).Select(cw => cw.Collaborator).ToListAsync();
            if (collaborators == null)
            {
                return [];
            }

            return collaborators;
        }

        public async Task<Workshop?> GetWorkshop(Guid id)
        {
            return await _context.Workshop.FindAsync(id);
        }

        public async Task<ICollection<Workshop>> GetWorkshops()
        {
            try
            {
                return await _context.Workshop.ToListAsync();
            } 
            catch
            {
                throw;
            }
        }

        public async Task<Workshop?> UpdateWorkshop(Workshop Workshop)
        {
            var updatedWorkshop = _context.Workshop.Update(Workshop);
            try
            {
                await _context.SaveChangesAsync();
                return updatedWorkshop.Entity;
            } catch
            {
                throw;
            }
        }
    }
}
