using CMLApplication.Application.Interfaces;
using CMLApplication.Common.CustomExceptions;
using CMLApplication.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CMLApplication.Controllers
{
    [ApiController]
    [Route("authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private IAuthenticationService authenticationService;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthenticationService authentication
        )
        {
            _logger = logger;
            authenticationService = authentication;
        }

        [HttpPost]
        [Route("login-colaborador-ad")]
        public IActionResult LoginColaboradorAD([FromBody] Autenticacao autenticacao)
        {
            try
            {
                return Ok(authenticationService.LoginColaboradorAD(autenticacao));
            }
            catch (CustomDBException dbException)
            {
                _logger.LogError(dbException, "Erro em 'Repository'");
                return StatusCode(dbException.HTTPErrorCode, dbException.Message);
            }
            catch (CustomServiceException serviceException)
            {
                _logger.LogError(serviceException, "Erro em 'Service'");
                return StatusCode(serviceException.HTTPErrorCode, serviceException.Message);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Erro não tratado");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("login-colaborador")]
        public IActionResult LoginColaborador([FromBody] Autenticacao autenticacao)
        {
            try
            {
                return Ok(authenticationService.LoginColaborador(autenticacao));
            }
            catch (CustomDBException dbException)
            {
                _logger.LogError(dbException, "Erro em 'Repository'");
                return StatusCode(dbException.HTTPErrorCode, dbException.Message);
            }
            catch (CustomServiceException serviceException)
            {
                _logger.LogError(serviceException, "Erro em 'Service'");
                return StatusCode(serviceException.HTTPErrorCode, serviceException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
