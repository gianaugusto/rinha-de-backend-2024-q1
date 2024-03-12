namespace rinha_2024_q1.Data;

using Microsoft.EntityFrameworkCore;

using rinha_2024_q1.Models;

public class RinhaDbContext : DbContext
{
    public RinhaDbContext(DbContextOptions<RinhaDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder
            .Entity<ClienteExtrato>()
            .Property(x => x.ClienteId)
            .HasColumnName("ClienteId");

        modelBuilder.Entity<ClienteExtrato>()
            .HasKey(i => i.ClienteId);

        modelBuilder
            .Entity<ClienteExtrato>()
            .HasMany(s => s.Transacaos)
            .WithOne(o => o.Extrato)
            .HasForeignKey(f => f.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

    }

    public DbSet<ClienteExtrato> ClientesExtrato => Set<ClienteExtrato>();

    public DbSet<Transacao> Transactions => Set<Transacao>();
}