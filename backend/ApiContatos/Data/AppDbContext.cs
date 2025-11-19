using ApiContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContatos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas { get; set; } = null!;
    public DbSet<Telefone> Telefones { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pessoa>(entidade =>
        {
            entidade.ToTable("Pessoas");
            entidade.HasKey(p => p.Id);

            entidade.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entidade.Property(p => p.Email)
                .HasMaxLength(150);
        });

        modelBuilder.Entity<Telefone>(entidade =>
        {
            entidade.ToTable("Telefones");
            entidade.HasKey(t => t.Id);

            entidade.Property(t => t.Numero)
                .IsRequired()
                .HasMaxLength(20);

            entidade.Property(t => t.Tipo)
                .HasMaxLength(30);

            entidade.HasOne(t => t.Pessoa)
                .WithMany(p => p.Telefones)
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
