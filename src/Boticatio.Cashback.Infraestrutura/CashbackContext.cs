using Boticatio.Cashback.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Boticatio.Cashback.Infraestrutura
{
    public class CashbackContext : DbContext
    {
        public CashbackContext()
        { }

        public CashbackContext(DbContextOptions<CashbackContext> opcoes)
            : base(opcoes)
        { }

        public DbSet<Revendedor> Revendedores { get; set; }

        private void ConfiguraCliente(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.Entity<Revendedor>(etd =>
            {
                etd.ToTable("Revendedores");
                etd.HasKey(c => c.Id).HasName("Id");
                etd.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                etd.Property(c => c.Nome).HasColumnName("Nome").HasMaxLength(100);
                etd.Property(c => c.Email).HasColumnName("Email").HasMaxLength(30);
                etd.Property(c => c.Senha).HasColumnName("Senha").HasMaxLength(30);
                etd.Property(c => c.CPF).HasColumnName("CPF").HasMaxLength(30);
            });
        }

        protected override void OnModelCreating(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.ForSqlServerUseIdentityColumns();
            construtorDeModelos.HasDefaultSchema("Cashback");

            ConfiguraCliente(construtorDeModelos);
        }
    }
}
