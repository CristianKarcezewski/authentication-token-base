using CMLApplication.Models;
using CMLApplication.Common.CustomExceptions;
using CMLApplication.Models.Entities;
using CMLApplication.Repository.Interfaces;
using CMLApplication.Services.Interfaces;

namespace CMLApplication.Services.Implementation
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly IColaboradoresRepository colaboradoresRepository;

        public ColaboradorService(IColaboradoresRepository colaboradoresRepository)
        {
            this.colaboradoresRepository = colaboradoresRepository;
        }

        public List<Colaborador> FiltrarColaborador(Colaborador colaborador)
        {
            List<Colaborador> colaboradores = new List<Colaborador>();
            var entities = colaboradoresRepository.Filtrar(new ColaboradorEntity(colaborador));

            foreach (var item in entities)
            {
                colaboradores.Add(new Colaborador(item));
            }

            return colaboradores;
        }

        public Colaborador BuscarPorId(int id)
        {
            if (id <= 0) { throw new CustomServiceException("Colaborador inexistente.", 404); }
            var entity = colaboradoresRepository.BuscarPorId(id);
        }

        public Colaborador Atualizar(Colaborador colaborador)
        {
            throw new NotImplementedException();
        }

        public Colaborador Excluir(Colaborador colaborador)
        {
            throw new NotImplementedException();
        }
    }
}
