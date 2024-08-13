using CMLApplication.Models;
using CMLApplication.Models.DTO;
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Options;

namespace CMLApplication.Requests
{
    public class ActivityDirectoryRequests : IActivityDirectoryRequests
    {
        private readonly AppConfig _appConfig;
        private readonly HttpClient _httpClient;

        public ActivityDirectoryRequests(IOptions<AppConfig> appConfig, HttpClient httpClient)
        {
            _appConfig = appConfig.Value;
            _httpClient = httpClient;
        }

        public bool AutenticarAD(Autenticacao autenticacao)
        {
            return new PrincipalContext(ContextType.Domain, _appConfig.ActivityDirectoryUrl).ValidateCredentials(autenticacao.Login, autenticacao.Password);
        }
    }
}
