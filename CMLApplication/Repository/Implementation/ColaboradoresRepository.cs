using CMLApplication.Common.CustomExceptions;
using CMLApplication.Models.Entities;
using CMLApplication.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CMLApplication.Repository.Implementation
{
    public class ColaboradoresRepository : IBaseRepository
    {
        private readonly RepositoryDBContext _context;

        public ColaboradoresRepository(RepositoryDBContext context)
        {
            _context = context;
        }

        public List<T> Filtrar<T>(T? entidade, int skip = 0, int take = 500, params string[] orderBy) where T : class
        {
            IQueryable<ColaboradorEntity> query = _context.Colaboradores.AsQueryable();
            query = query.Include(c => c.GrupoColaborador);
            query = query.Include(c => c.Chave);

            if (entidade != null && entidade is ColaboradorEntity)
            {
                ColaboradorEntity filtro = entidade as ColaboradorEntity;

                if (filtro.Id != 0) { query = query.Where(c => c.Id == filtro.Id); }
                if (filtro.IdGrupoColaborador != 0) { query = query.Where(c => c.IdGrupoColaborador == filtro.IdGrupoColaborador); }
                if (!string.IsNullOrWhiteSpace(filtro.NomeCompleto)) { query = query.Where(c => c.NomeCompleto.Equals(filtro.NomeCompleto)); }
                if (!string.IsNullOrWhiteSpace(filtro.Login)) { query = query.Where(c => c.Login.Equals(filtro.Login)); }
                if (!string.IsNullOrWhiteSpace(filtro.Email)) { query = query.Where(c => c.Email.Equals(filtro.Email)); }
                if (!string.IsNullOrWhiteSpace(filtro.Telefone)) { query = query.Where(c => c.Telefone.Equals(filtro.Telefone)); }
                query = query.Where(c => c.Ativo == filtro.Ativo);
            }

            if (orderBy.Count() > 0)
            {
                string orderByString = string.Join(", ", orderBy);
                query = query.OrderBy(orderByString);
            }
            else
            {
                query = query.OrderBy(c => c.Id);
            }

            query = query.Skip(skip).Take(take);
            return query.Cast<T>().ToList();
        }

        public T BuscarPorId<T>(int id) where T : class
        {
            if (id <= 0)
            {
                throw new CustomDBException("Colaborador não encontrado.", 404);
            }

            return _context.Colaboradores.Include(c => c.GrupoColaborador).FirstOrDefault(c => c.Id == id) as T;
        }

        public T Criar<T>(T entidade) where T : class
        {
            if (entidade == null)
            {
                throw new CustomDBException("Erro ao salvar novo colaborador", 400);
            }

            if (entidade is ColaboradorEntity)
            {
                ColaboradorEntity colaborador = entidade as ColaboradorEntity;
                _context.Colaboradores.Add(colaborador);
                _context.SaveChanges();
                return entidade;
            }
            else
            {
                throw new CustomDBException("Erro ao salvar novo colaborador", 400);
            }
        }

        public T Atualizar<T>(T entidade) where T : class
        {
            if (entidade == null)
            {
                throw new CustomDBException("Erro ao atualizar registro vazio", 400);
            }

            ColaboradorEntity novoRegistro = entidade as ColaboradorEntity;
            ColaboradorEntity registro = _context.Colaboradores.Find(novoRegistro.Id);

            if (registro == null)
            {
                throw new CustomDBException("Colaborador não encontrado para a atualização", 400);
            }

            if (novoRegistro.IdGrupoColaborador != 0) { registro.IdGrupoColaborador = novoRegistro.IdGrupoColaborador; }
            if (!string.IsNullOrWhiteSpace(novoRegistro.Login)) { registro.Login = novoRegistro.Login; }
            if (!string.IsNullOrWhiteSpace(novoRegistro.Email)) { registro.Email = novoRegistro.Email; }
            if (!string.IsNullOrWhiteSpace(novoRegistro.Telefone)) { registro.Telefone = novoRegistro.Telefone; }

            _context.SaveChanges();
            return entidade;
        }

        public T Excluir<T>(int id) where T : class
        {
            if (id <= 0)
            {
                throw new CustomDBException("Código de chave primária invalido para excluir", 400);
            }

            ColaboradorEntity registro = _context.Colaboradores.Find(id);
            if (registro == null)
            {
                throw new CustomDBException("Colaborador não encontrado para excluir", 400);
            }
            else
            {
                _context.Colaboradores.Remove(registro);
                _context.SaveChanges();
            }

            return registro as T;
        }
    }
}
