using CMSASPNETCoreWebAPI.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CMSASPNETCoreWebAPI.DAL;

public class ModelDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<MemberImage> MemberImage { get; set; }
    public DbSet<Promotion> Promotions { get; set; }

    protected readonly IConfiguration Configuration;

    public ModelDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = Configuration.GetConnectionString("DbConnectionString");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemberImage>()
            .HasIndex(p => p.MemberId)
            .IsUnique();
    }
}