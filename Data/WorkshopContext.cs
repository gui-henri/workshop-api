using Microsoft.EntityFrameworkCore;
using WorkshopApi.Models;

namespace WorkshopApi.Models;

public class WorkshopContext : DbContext
{
    public WorkshopContext(DbContextOptions<WorkshopContext> options)
        : base(options)
    {
    }

    public DbSet<Collaborator> Collaborators { get; set; } = null!;

public DbSet<WorkshopApi.Models.Workshop> Workshop { get; set; } = default!;
}