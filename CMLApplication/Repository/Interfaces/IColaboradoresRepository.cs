﻿namespace CMLApplication.Repository.Interfaces
{
    public interface IColaboradoresRepository
    {
        List<T> Filtrar<T>(T? entidade, int skip = 0, int take = 500, params string[] orderBy) where T : class;

        T BuscarPorId<T>(int id) where T : class;

        T Criar<T>(T entidade) where T : class;

        T Atualizar<T>(T entidade) where T : class;

        T Excluir<T>(int id) where T : class;
    }
}
