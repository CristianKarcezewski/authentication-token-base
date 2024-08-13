using CMLApplication.Common.CustomExceptions;
using CMLApplication.Models.Entities;
using CMLApplication.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CMLApplication.Repository.Implementation
{
    public class GrupoColaboradorPermissaoRepository : IBaseRepository
    {
        private readonly RepositoryDBContext _context;

        public GrupoColaboradorPermissaoRepository(RepositoryDBContext context)
        {
            _context = context;
        }

        public List<T> Filtrar<T>(T? entidade, int skip = 0, int take = 500, params string[] orderBy) where T : class
        {
            IQueryable<GrupoColaboradorPermissaoEntity> query = _context.GruposColaboradoresPermissoes.AsQueryable();

            if (entidade != null && entidade is GrupoColaboradorPermissaoEntity)
            {
                GrupoColaboradorPermissaoEntity filtro = entidade as GrupoColaboradorPermissaoEntity;

                if (filtro.Id != 0) { query = query.Where(gcp => gcp.Id == filtro.Id); }
                if (filtro.IdGrupoColaborador != 0) { query = query.Where(gcp => gcp.IdGrupoColaborador == filtro.IdGrupoColaborador); }
                if (filtro.IdPermissao != 0) { query = query.Where(gcp => gcp.IdPermissao == filtro.IdPermissao); }

                query = query.Include(gcp => gcp.GrupoColaborador);
                query = query.Include(gcp => gcp.Permissao);
            }

            if (orderBy.Count() > 0)
            {
                string orderByString = string.Join(", ", orderBy);
                query = query.OrderBy(orderByString);
            }
            else
            {
                query = query.OrderBy(gcp => gcp.Id);
            }

            query = query.Skip(skip).Take(take);
            return query.Cast<T>().ToList();
        }

        public T BuscarPorId<T>(int id) where T : class
        {
            if (id <= 0)
            {
                throw new CustomDBException("GrupoColaboradorPermissao não encontrado.", 404);
            }

            return _context.GruposColaboradoresPermissoes
                .Include(gcp => gcp.GrupoColaborador)
                .Include(gcp => gcp.Permissao)
                .FirstOrDefault(c => c.Id == id) as T;
        }

        public T Criar<T>(T entidade) where T : class
        {
            if (entidade == null)
            {
                throw new CustomDBException("Erro ao salvar novo GrupoColaboradorPermissao", 400);
            }

            if (entidade is GrupoColaboradorPermissaoEntity)
            {
                GrupoColaboradorPermissaoEntity grupoColaboradorPermissao = entidade as GrupoColaboradorPermissaoEntity;
                _context.GruposColaboradoresPermissoes.Add(grupoColaboradorPermissao);
                _context.SaveChanges();
                return entidade;
            }
            else
            {
                throw new CustomDBException("Erro ao salvar novo GrupoColaboradorPermissao", 400);
            }
        }

        public T Atualizar<T>(T entidade) where T : class
        {
            if (entidade == null)
            {
                throw new CustomDBException("Erro ao atualizar registro vazio", 400);
            }

            GrupoColaboradorPermissaoEntity novoRegistro = entidade as GrupoColaboradorPermissaoEntity;
            GrupoColaboradorPermissaoEntity registro = _context.GruposColaboradoresPermissoes.Find(novoRegistro.Id);

            if (registro == null)
            {
                throw new CustomDBException("GrupoColaboradorPermissao não encontrado para a atualização", 400);
            }

            if (novoRegistro.IdGrupoColaborador != 0) { registro.IdGrupoColaborador = novoRegistro.IdGrupoColaborador; }
            if (novoRegistro.IdPermissao != 0) { registro.IdPermissao = novoRegistro.IdPermissao; }

            _context.SaveChanges();
            return entidade;
        }

        public T Excluir<T>(int id) where T : class
        {
            if (id <= 0)
            {
                throw new CustomDBException("Código de chave primária invalido para excluir", 400);
            }

            GrupoColaboradorPermissaoEntity registro = _context.GruposColaboradoresPermissoes.Find(id);
            if (registro == null)
            {
                throw new CustomDBException("GrupoColaboradorPermissao não encontrado para excluir", 400);
            }
            else
            {
                _context.GruposColaboradoresPermissoes.Remove(registro);
                _context.SaveChanges();
            }

            return registro as T;
        }
    }
}
