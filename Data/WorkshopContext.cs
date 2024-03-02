using Microsoft.EntityFrameworkCore;

namespace WorkshopApi.Contexts;

public class WorkshopContext : DbContext
{
    public WorkshopContext(DbContextOptions<WorkshopContext> options)
        : base(options)
    {
    }

    public DbSet<Collaborator> Collaborators { get; set; } = null!;

    public DbSet<WorkshopApi.Models.Workshop> Workshop { get; set; } = default!;
}