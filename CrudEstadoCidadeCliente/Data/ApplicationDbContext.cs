using CrudEstadoCidadeCliente.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudEstadoCidadeCliente.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Estado> Estados { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações das entidades
        modelBuilder.Entity<Estado>()
            .HasMany(e => e.Cidades)
            .WithOne(c => c.Estado)
            .HasForeignKey(c => c.EstadoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Cidade>()
            .HasMany(c => c.Clientes)
            .WithOne(cl => cl.Cidade)
            .HasForeignKey(cl => cl.CidadeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração para o campo Sexo
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Sexo)
            .HasConversion<string>()
            .HasMaxLength(1);
    }


}