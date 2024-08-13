using CMLApplication.Models;
using CMLApplication.Models.DTO;

namespace CMLApplication.Application.Interfaces
{
    public interface IAuthenticationService
    {
        string LoginColaboradorAD(Autenticacao autenticacao);
        string LoginColaborador(Autenticacao autenticacao);
    }
}
