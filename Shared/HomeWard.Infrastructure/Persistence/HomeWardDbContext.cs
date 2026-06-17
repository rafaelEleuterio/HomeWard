using HomeWard.Domain.Entities;
using HomeWard.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HomeWard.Infrastructure.Persistence;

public sealed class HomeWardDbContext : DbContext
{
    public HomeWardDbContext(DbContextOptions<HomeWardDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // RoleLookup — tabela de lookup para o enum Role
        modelBuilder.Entity<RoleLookup>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(e => e.Id);

            entity.HasData(
                Enum.GetValues<Role>()
                    .Select(r => new RoleLookup
                    {
                        Id = (int)r,
                        Name = r.ToString()
                    })
            );
        });

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Role)
                  .HasConversion<int>()
                  .HasColumnName("RoleId");

            entity.HasOne<RoleLookup>()
                  .WithMany()
                  .HasForeignKey("RoleId")
                  .OnDelete(DeleteBehavior.Restrict);

            // Seed: usuário admin criado junto com o banco
            entity.HasData(new User
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), // Guid fixo para idempotência
                Username = "admin",
                FullName = "Administrator",
                FirstLastName = "User Admin",
                Email = "admin@admin",
                PasswordHash = "$2a$11$S7QIDk9XBsePW2GK8qz98.hZIb85Y8lNZCdpoOv8IgtffMfSp4wyG",
                Role = Role.Director,
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
        });

        // SessionStateLookup — seed com valores do enum
        modelBuilder.Entity<SessionStateLookup>(entity =>
        {
            entity.ToTable("SessionStates");
            entity.HasKey(e => e.Id);

            entity.HasData(
                Enum.GetValues<SessionState>()
                    .Select(s => new SessionStateLookup
                    {
                        Id = (int)s,
                        Name = s.ToString()
                    })
            );
        });

        // WorkSession
        modelBuilder.Entity<WorkSession>(entity =>
        {
            entity.ToTable("WorkSessions");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.State)
                  .HasConversion<int>()
                  .HasColumnName("StateId");

            entity.HasOne<SessionStateLookup>()
                  .WithMany()
                  .HasForeignKey("StateId")
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // SessionTransition
        modelBuilder.Entity<SessionTransition>(entity =>
        {
            entity.ToTable("SessionTransitions");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FromState)
                  .HasConversion<int>()
                  .HasColumnName("FromStateId");

            entity.Property(e => e.ToState)
                  .HasConversion<int>()
                  .HasColumnName("ToStateId");

            entity.HasOne<SessionStateLookup>()
                  .WithMany()
                  .HasForeignKey("FromStateId")
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<SessionStateLookup>()
                  .WithMany()
                  .HasForeignKey("ToStateId")
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SessionTransitionDocument>()
            .HasOne(x => x.SessionTransition)
            .WithMany(x => x.Documents)
            .HasForeignKey(x => x.SessionTransitionId);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<WorkSession> WorkSessions => Set<WorkSession>();
    public DbSet<SessionTransition> SessionTransitions => Set<SessionTransition>();
    public DbSet<SessionStateLookup> SessionStates => Set<SessionStateLookup>();
    public DbSet<SessionTransitionDocument> SessionTransitionDocuments => Set<SessionTransitionDocument>();

}
