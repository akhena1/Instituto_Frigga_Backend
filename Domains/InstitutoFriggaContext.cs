using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Instituto_Frigga_Backend.Domains
{
    public partial class InstitutoFriggaContext : DbContext
    {
        public InstitutoFriggaContext()
        {
        }

        public InstitutoFriggaContext(DbContextOptions<InstitutoFriggaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriaProduto> CategoriaProduto { get; set; }
        public virtual DbSet<CategoriaReceita> CategoriaReceita { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Receita> Receita { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=N-1S-DEV-06\\SQLEXPRESS; Database=InstitutoFrigga; User Id=sa; Password=132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaProduto>(entity =>
            {
                entity.Property(e => e.TipoProduto).IsUnicode(false);
            });

            modelBuilder.Entity<CategoriaReceita>(entity =>
            {
                entity.Property(e => e.TipoReceita).IsUnicode(false);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.Property(e => e.Bairro).IsUnicode(false);

                entity.Property(e => e.Cep)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cidade).IsUnicode(false);

                entity.Property(e => e.Complemento).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Numero).IsUnicode(false);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Endereco)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Endereco__Usuari__46E78A0C");
            });

            modelBuilder.Entity<Oferta>(entity =>
            {
                entity.Property(e => e.ImagemProduto).IsUnicode(false);

                entity.HasOne(d => d.Produto)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.ProdutoId)
                    .HasConstraintName("FK__Oferta__Produto___4AB81AF0");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Oferta__Usuario___49C3F6B7");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.Property(e => e.Tipo).IsUnicode(false);

                entity.HasOne(d => d.CategoriaProduto)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.CategoriaProdutoId)
                    .HasConstraintName("FK__Produto__Categor__403A8C7D");
            });

            modelBuilder.Entity<Receita>(entity =>
            {
                entity.Property(e => e.ImagemReceita).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.HasOne(d => d.CategoriaReceita)
                    .WithMany(p => p.Receita)
                    .HasForeignKey(d => d.CategoriaReceitaId)
                    .HasConstraintName("FK__Receita__Categor__4316F928");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Receita)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Receita__Usuario__440B1D61");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.Property(e => e.Tipo).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Biografia).IsUnicode(false);

                entity.Property(e => e.CnpjCpf).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);

                entity.Property(e => e.Telefone).IsUnicode(false);

                entity.HasOne(d => d.TipoUsuario)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.TipoUsuarioId)
                    .HasConstraintName("FK__Usuario__Tipo_us__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
