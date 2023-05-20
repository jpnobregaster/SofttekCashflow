using AutoMapper;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Softtek.Cashflow.Application.Entry.Command;
using Softtek.Cashflow.Application.Entry.Query;
using Softtek.Cashflow.Domain.Common.ViewModel;
using Softtek.Cashflow.Domain.ViewModel.Cashflow;

namespace Softtek.Cashflow.Api.Controllers
{
    /// <summary>
    /// Classe de controle para entrada de dados de lan�amentos
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CashflowController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CashflowCommand> _validator;
        private readonly ILogger<CashflowController> _logger;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        /// <param name="validator"></param>
        /// <param name="mapper"></param>
        public CashflowController(ILogger<CashflowController> logger, IMediator mediator, IValidator<CashflowCommand> validator,
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _validator = validator;
            _mapper = mapper;
        }
        /// <summary>
        /// M�todo para gera��o do rel�t�rio de balan�o di�rio consolidado
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "ConsolidatedDailyBalance")]
        [ProducesResponseType(typeof(UnknownErrorViewModel), 500)]
        [ProducesResponseType(typeof(IList<ConsolidatedBalanceViewModel>), 200)]
        public async Task<IActionResult> GetConsolidatedDailyBallance()
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de dados para gera��o do relat�rio de balanco consolidado di�rio");

                var consolidatedDailyBalance = await _mediator.Send(new ConsolidatedBalanceQuery());

                var result = _mapper.Map<IList<ConsolidatedBalanceViewModel>>(consolidatedDailyBalance);

                _logger.LogInformation("Iniciando consulta de dados para gera��o do relat�rio de balanco consolidado di�rio");

                return StatusCode(200, result);
            }
            catch (Exception)
            {
                _logger.LogDebug("Foi encontrado um erro n�o esperado");

                UnknownErrorViewModel errorDto = new()
                {
                    Error = "Erro desconhecido"
                };

                return StatusCode(500, errorDto);
            }
        }
        /// <summary>
        /// M�todo para lan�amento de entradas(Cr�dito/D�bito)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(Name = "Entring")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ValidateErrorViewModel), 400)]
        [ProducesResponseType(typeof(UnknownErrorViewModel), 500)]
        public async Task<IActionResult> EntryAsync(CashflowCommand command)
        {
            try
            {
                _logger.LogDebug("Iniciando entrada de lan�amentos");

                var validationResult = await _validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                    throw new ValidationException("", validationResult.Errors);

                await _mediator.Send(command);

                _logger.LogDebug("Valor adicionado com sucesso");

                return StatusCode(200);
            }

            catch (ValidationException exception)
            {
                string[] validationErrors = exception.Errors
                    .Select(e => e.ErrorMessage).ToArray();

                _logger.LogDebug($"Foi encontrados erros de valida��o de dados de entrada { string.Join(",", validationErrors) }");

                ValidateErrorViewModel errorDto = new()
                {
                    Errors = validationErrors
                };

                return StatusCode(400, errorDto);
            }
            catch (Exception)
            {
                _logger.LogDebug("Foi encontrado um erro n�o esperado");

                UnknownErrorViewModel errorDto = new()
                {
                    Error = "Erro desconhecido"
                };

                return StatusCode(500, errorDto);
            }
        }
    }
}