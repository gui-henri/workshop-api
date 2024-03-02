using Microsoft.EntityFrameworkCore;
using WorkshopApi.Entities;

namespace WorkshopApi.Contexts;

public class WorkshopContext : DbContext
{
    public WorkshopContext(DbContextOptions<WorkshopContext> options)
        : base(options)
    {
    }

    public DbSet<Collaborator> Collaborators { get; set; } = null!;

    public DbSet<Workshop> Workshop { get; set; } = default!;
}