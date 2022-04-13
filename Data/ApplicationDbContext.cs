using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetIdentityTest02.Data;

public class ApplicationDbContext : IdentityDbContext<
    IdentityUser<Guid>,
    IdentityRole<Guid>,
    Guid, IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityUser<Guid>>(b =>
        {
            b.ToTable("AspNetUsers");
            b.HasKey(u => u.Id);
            b.HasIndex(u => u.NormalizedUserName).IsUnique();
            b.HasIndex(u => u.NormalizedEmail).IsUnique();

            b.Property(u => u.Email).HasMaxLength(180);
            b.Property(u => u.NormalizedEmail).HasMaxLength(180);
            b.Property(u => u.UserName).HasMaxLength(180);
            b.Property(u => u.NormalizedUserName).HasMaxLength(180);
            b.Property(u => u.PhoneNumber).HasMaxLength(20);
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            b.HasMany<IdentityUserClaim<Guid>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            b.HasMany<IdentityUserLogin<Guid>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            b.HasMany<IdentityUserToken<Guid>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            b.HasMany<IdentityUserRole<Guid>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        });

        builder.Entity<IdentityUserClaim<Guid>>(b =>
        {
            b.ToTable("AspNetUserClaims");
            b.HasKey(uc => uc.Id);
            b.Property(u => u.ClaimType).HasMaxLength(255);
            b.Property(u => u.ClaimValue).HasMaxLength(255);
        });

        builder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.HasKey(l => new {l.LoginProvider, l.ProviderKey});
            b.Property(l => l.LoginProvider).HasMaxLength(128);
            b.Property(l => l.ProviderKey).HasMaxLength(128);
            b.ToTable("AspNetUserLogins");
            b.Property(u => u.ProviderDisplayName).HasMaxLength(255);
        });

        builder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.HasKey(t => new {t.UserId, t.LoginProvider, t.Name});
            b.Property(t => t.LoginProvider).HasMaxLength(120);
            b.Property(t => t.Name).HasMaxLength(180);
            b.ToTable("AspNetUserTokens");
        });

        builder.Entity<IdentityRole<Guid>>(b =>
        {
            b.HasKey(r => r.Id);
            b.HasIndex(r => r.NormalizedName).IsUnique();
            b.ToTable("AspNetRoles");
            b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            b.Property(u => u.Name).HasMaxLength(256);
            b.Property(u => u.NormalizedName).HasMaxLength(256);
            b.HasMany<IdentityUserRole<Guid>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
            b.HasMany<IdentityRoleClaim<Guid>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        });

        builder.Entity<IdentityRoleClaim<Guid>>(b =>
        {
            b.HasKey(rc => rc.Id);
            b.ToTable("AspNetRoleClaims");
            b.Property(u => u.ClaimType).HasMaxLength(255);
            b.Property(u => u.ClaimValue).HasMaxLength(255);
        });

        builder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.HasKey(r => new {r.UserId, r.RoleId});
            b.ToTable("AspNetUserRoles");
        });
    }
}