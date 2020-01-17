﻿using Boticatio.Cashback.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using Boticatio.Cashback.Utils;
using System.Linq;

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
        public DbSet<Compra> Compras { get; set; }

        private void RevendedorTabela(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.Entity<Revendedor>(etd =>
            {
                etd.ToTable("Revendedores");
                etd.HasKey(c => c.Id);
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
                etd.ToTable("Compras");
                etd.HasKey(c => c.Id);
                etd.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                etd.Property(c => c.Codigo).HasColumnName("Codigo").HasMaxLength(100);
                etd.Property(c => c.Data).HasColumnName("Data");
                etd.Property(c => c.Valor).HasColumnName("Valor").HasMaxLength(30);
                etd.HasOne(c => c.Revendedor).WithMany(h => h.Compras).HasForeignKey(g => g.Revendedor_Id);
                etd.HasOne(c => c.Status).WithMany().HasForeignKey(g => g.Status_Id);

            });
        }

        private void CompraStatusTabela(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.Entity<CompraStatus>(etd =>
            {
                etd.ToTable("CompraStatus");
                etd.HasKey(c => c.Id);
                etd.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                etd.Property(c => c.Descrição).HasColumnName("Descrição").HasMaxLength(100);

                var statusEnum = Enum.GetValues(typeof(CompraStatusEnum)).Cast<CompraStatusEnum>();

                foreach (var status in statusEnum)
                {

                    etd.HasData(new CompraStatus
                    {
                        Id = (int)status,
                        Descrição = status.Description()
                    });
                }

            });
        }

        protected override void OnModelCreating(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.ForSqlServerUseIdentityColumns();
            construtorDeModelos.HasDefaultSchema("Cashback");

            RevendedorTabela(construtorDeModelos);
            CompraTabela(construtorDeModelos);
            CompraStatusTabela(construtorDeModelos);
        }
    }
}
