using Microsoft.EntityFrameworkCore;
using WorkshopApi.Entities;

namespace WorkshopApi.Contexts;

public class WorkshopContext : DbContext
{
    public WorkshopContext(DbContextOptions<WorkshopContext> options)
        : base(options)
    {
    }   

    public DbSet<Collaborator> Collaborators { get; set; }
    public DbSet<Workshop> Workshop { get; set; }
    public DbSet<CollaboratorWorkshop> CollaboratorWorkshop { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CollaboratorWorkshop>(entity =>
        {
            entity.HasKey(e => new { e.CollaboratorId, e.WorkshopId });

            entity.HasOne(e => e.Workshop)
                .WithMany(e => e.CollaboratorWorkshops)
                .HasForeignKey(e => e.WorkshopId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(e => e.Collaborator)
                .WithMany(e => e.CollaboratorWorkshops)
                .HasForeignKey(e => e.CollaboratorId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}