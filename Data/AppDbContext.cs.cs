using checkpoint__10072025.Models;
using checkpoint__10072025.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace checkpoint__10072025.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets obrigatórios da prova
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Produto> Produtos => Set<Produto>();

        // Extras (tema Imobiliária)
        public DbSet<Imovel> Imoveis => Set<Imovel>();
        public DbSet<Contrato> Contratos => Set<Contrato>();
        public DbSet<Visita> Visitas => Set<Visita>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // Dica: no Oracle o schema padrão é o NOME DO USUÁRIO (ex.: RM98747).
            // Se quiser forçar:
            // mb.HasDefaultSchema("RM98747");

            // -------- Produto --------
            mb.Entity<Produto>()
              .HasIndex(p => p.SKU)
              .IsUnique();

            // exemplos de tipos que casam bem com Oracle
            mb.Entity<Produto>()
              .Property(p => p.Nome)
              .HasMaxLength(150);               // VARCHAR2(150)
            mb.Entity<Produto>()
              .Property(p => p.SKU)
              .HasMaxLength(80);
            mb.Entity<Produto>()
              .Property(p => p.Preco)
              .HasColumnType("NUMBER(12,2)");  // decimal

            // -------- Cliente --------
            mb.Entity<Cliente>()
              .Property(c => c.Nome)
              .HasMaxLength(120);
            mb.Entity<Cliente>()
              .Property(c => c.Email)
              .HasMaxLength(120);
            mb.Entity<Cliente>()
              .Property(c => c.Telefone)
              .HasMaxLength(20);

            // -------- Imóvel --------
            mb.Entity<Imovel>()
              .Property(i => i.Titulo)
              .HasMaxLength(150);
            mb.Entity<Imovel>()
              .Property(i => i.Endereco)
              .HasMaxLength(200);
            mb.Entity<Imovel>()
              .Property(i => i.Valor)
              .HasColumnType("NUMBER(12,2)");

            // -------- Contrato --------
            mb.Entity<Contrato>()
              .Property(c => c.Inicio)
              .HasColumnType("DATE");
            mb.Entity<Contrato>()
              .Property(c => c.Fim)
              .HasColumnType("DATE");

            // -------- Visita --------
            mb.Entity<Visita>()
              .Property(v => v.Inicio)
              .HasColumnType("DATE");
            mb.Entity<Visita>()
              .Property(v => v.Fim)
              .HasColumnType("DATE");
        }

        public override int SaveChanges()
        {
            ValidarRegras();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            ValidarRegras();
            return base.SaveChangesAsync(ct);
        }

        private void ValidarRegras()
        {
            // (1) Visitas sem sobreposição por imóvel
            var novasVisitas = ChangeTracker.Entries<Visita>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .ToList();

            foreach (var v in novasVisitas)
            {
                bool overlap = Visitas.Any(x =>
                       x.ImovelId == v.ImovelId &&
                       x.Id != v.Id &&
                       v.Inicio < x.Fim && x.Inicio < v.Fim);

                if (overlap)
                {
                    throw new InvalidOperationException("Já existe visita nesse intervalo para este imóvel.");
                }
            }

            // (2) Contrato ativo não pode ser editado (somente encerrado)
            var contratosEdit = ChangeTracker.Entries<Contrato>()
                .Where(e => e.State == EntityState.Modified);


            foreach (var entry in contratosEdit)
            {
                var originalAtivo = entry.OriginalValues.GetValue<bool>(nameof(Contrato.Ativo));
                var novoAtivo = entry.CurrentValues.GetValue<bool>(nameof(Contrato.Ativo));

                if (originalAtivo && novoAtivo)
                {
                    throw new InvalidOperationException(
                        "Contratos ativos não podem ser editados. Encerre o contrato antes de alterar os dados.");
                }
            }
        }
    }
}