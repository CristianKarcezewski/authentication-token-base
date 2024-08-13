using CMLApplication.Models;
using CMLApplication.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMLApplication.Repository.Implementation
{
    public class RepositoryDBContext : DbContext
    {
        public DbSet<ColaboradorEntity> Colaboradores { get; set; }
        public DbSet<GrupoColaboradorEntity> GruposColaboradores { get; set; }
        public DbSet<GrupoColaboradorPermissaoEntity> GruposColaboradoresPermissoes { get; set; }
        public DbSet<PermissaoEntity> Permissoes { get; set; }


        public RepositoryDBContext(DbContextOptions<RepositoryDBContext> options) : base(options)
        {
        }

        //Adiciona registros por default na criação da tabela
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissaoEntity>().HasData(
                new PermissaoEntity { Id = 1, Descricao = "Consultar" },
                new PermissaoEntity { Id = 2, Descricao = "Inserir" },
                new PermissaoEntity { Id = 3, Descricao = "Atualizar" },
                new PermissaoEntity { Id = 4, Descricao = "Excluir" }
            );

            modelBuilder.Entity<GrupoColaboradorEntity>().HasData(
                new GrupoColaboradorEntity { Id = 1, Nome = "Colaborador"},
                new GrupoColaboradorEntity { Id = 2, Nome = "Administrador" },
                new GrupoColaboradorEntity { Id = 3, Nome = "Auditor" }
            );

            modelBuilder.Entity<GrupoColaboradorPermissaoEntity>().HasData(
                new GrupoColaboradorPermissaoEntity { Id = 1, IdGrupoColaborador = 1, IdPermissao = 1 },
                new GrupoColaboradorPermissaoEntity { Id = 2, IdGrupoColaborador = 2, IdPermissao = 1 },
                new GrupoColaboradorPermissaoEntity { Id = 3, IdGrupoColaborador = 2, IdPermissao = 2 },
                new GrupoColaboradorPermissaoEntity { Id = 4, IdGrupoColaborador = 2, IdPermissao = 3 },
                new GrupoColaboradorPermissaoEntity { Id = 5, IdGrupoColaborador = 2, IdPermissao = 4 },
                new GrupoColaboradorPermissaoEntity { Id = 6, IdGrupoColaborador = 3, IdPermissao = 1 },
                new GrupoColaboradorPermissaoEntity { Id = 7, IdGrupoColaborador = 3, IdPermissao = 2 },
                new GrupoColaboradorPermissaoEntity { Id = 8, IdGrupoColaborador = 3, IdPermissao = 3 },
                new GrupoColaboradorPermissaoEntity { Id = 9, IdGrupoColaborador = 3, IdPermissao = 4 }
            );

            modelBuilder.Entity<ColaboradorEntity>().HasData(
                new ColaboradorEntity { Id = 1, IdGrupoColaborador = 2, NomeCompleto = "Diego dos Santos", Login = "str_dsantos", Email = "diego.santos@keyworks.com.br", Ativo = true, UltimaAtualizacao = DateTime.Now, DTHR = DateTime.Now }
            );
        }
    }
}
