using Microsoft.EntityFrameworkCore;

namespace WorkshopApi.Models;

public class WorkshopContext : DbContext
{
    public WorkshopContext(DbContextOptions<WorkshopContext> options)
        : base(options)
    {
    }

    public DbSet<Collaborator> Collaborators { get; set; } = null!;
}