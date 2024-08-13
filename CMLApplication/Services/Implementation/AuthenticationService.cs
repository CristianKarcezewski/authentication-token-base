using CMLApplication.Application.Interfaces;
using CMLApplication.Common.CustomExceptions;
using CMLApplication.Models;
using CMLApplication.Models.DTO;
using CMLApplication.Models.Entities;
using CMLApplication.Repository.Interfaces;
using CMLApplication.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CMLApplication.Application.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IColaboradoresRepository colaboradoresRepository;
        private readonly IGrupoColaboradorPermissaoRepository grupoColaboradorPermissaoRepository;
        private readonly IActivityDirectoryRequests activityDirectoryRequests;

        public AuthenticationService(
            IConfiguration configuration,
            IColaboradoresRepository colaboradoresRepository,
            IGrupoColaboradorPermissaoRepository grupoColaboradorPermissaoRepository,
            IActivityDirectoryRequests activityDirectoryRequests)
        {
            this._configuration = configuration;
            this.colaboradoresRepository = colaboradoresRepository;
            this.grupoColaboradorPermissaoRepository = grupoColaboradorPermissaoRepository;
            this.activityDirectoryRequests = activityDirectoryRequests;
        }

        public string LoginColaborador(Autenticacao autenticacao)
        {
            bool autorizado = this.activityDirectoryRequests.AutenticarAD(autenticacao);
            if (autorizado)
            {
                ColaboradorEntity filtroColaborador = new ColaboradorEntity { Login = autenticacao.Login, Ativo = true };
                ColaboradorEntity? colaboradorEntity = colaboradoresRepository.Filtrar(filtroColaborador).FirstOrDefault();
                if(colaboradorEntity == null) { throw new CustomServiceException("Colaborador não encontrado", 404); }

                GrupoColaboradorPermissaoEntity filtroGrupo = new GrupoColaboradorPermissaoEntity { IdGrupoColaborador = colaboradorEntity.IdGrupoColaborador };
                List<GrupoColaboradorPermissaoEntity>? gruposEntities = grupoColaboradorPermissaoRepository.Filtrar(filtroGrupo);
                if(gruposEntities == null) { throw new CustomServiceException("Erro ao carregar permissões do colaborador", 404); }

                Colaborador colaborador = new Colaborador(colaboradorEntity);
                gruposEntities.ForEach(grupo => {
                    if (grupo.IdPermissao != 0 && grupo.Permissao != null && !string.IsNullOrWhiteSpace(grupo.Permissao.Descricao))
                    {
                        colaborador.Permissoes.Add(
                            new Permissao { Id = grupo.IdPermissao, Descricao = grupo.Permissao.Descricao }
                        );
                    }
                });

                return GerarJwtTokenColaborador(colaborador);
            }
            else
            {
                throw new CustomServiceException("Não autorizado por AD", 401);
            }
        }

        private string GerarJwtTokenColaborador(Colaborador colaborador)
        {
            var permissoesJson = JsonSerializer.Serialize(colaborador.Permissoes.Select(p => p.Id).ToList());
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, colaborador.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", colaborador.Id.ToString()),
                new Claim("nome", colaborador.NomeCompleto),
                new Claim("login", colaborador.Login),
                new Claim("email", colaborador.Email),
                new Claim("permissoes", permissoesJson)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
