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

        private void RevendedorTabela(ModelBuilder construtorDeModelos)
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

        private void CompraTabela(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.Entity<Compra>(etd =>
            {
                etd.ToTable("Revendedores");
                etd.HasKey(c => c.Id).HasName("Id");
                etd.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                etd.Property(c => c.Codigo).HasColumnName("Codigo").HasMaxLength(100);
                etd.Property(c => c.Data).HasColumnName("Data");
                etd.Property(c => c.Valor).HasColumnName("Valor").HasMaxLength(30);
                etd.HasOne(c => c.Revendedor).WithMany(h => h.Compras).HasForeignKey(g => g.RevendedorId);

            });
        }

        protected override void OnModelCreating(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.ForSqlServerUseIdentityColumns();
            construtorDeModelos.HasDefaultSchema("Cashback");

            RevendedorTabela(construtorDeModelos);
        }
    }
}
