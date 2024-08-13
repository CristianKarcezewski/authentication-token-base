using CMLApplication.Models;

namespace CMLApplication.Services.Interfaces
{
    public interface IColaboradorService
    {
        List<Colaborador> FiltrarColaborador(Colaborador colaborador);
        Colaborador BuscarPorId(int id);
        Colaborador Atualizar(Colaborador colaborador);
        Colaborador Excluir(Colaborador colaborador);
    }
}
