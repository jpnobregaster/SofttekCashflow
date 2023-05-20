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
    /// Classe de controle para entrada de dados de lançamentos
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
        /// Método para geração do relátório de balanço diário consolidado
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "ConsolidatedDailyBalance")]
        [ProducesResponseType(typeof(UnknownErrorViewModel), 500)]
        [ProducesResponseType(typeof(IList<ConsolidatedBalanceViewModel>), 200)]
        public async Task<IActionResult> GetConsolidatedDailyBallance()
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de dados para geração do relatório de balanco consolidado diário");

                var consolidatedDailyBalance = await _mediator.Send(new ConsolidatedBalanceQuery());

                var result = _mapper.Map<IList<ConsolidatedBalanceViewModel>>(consolidatedDailyBalance);

                _logger.LogInformation("Iniciando consulta de dados para geração do relatório de balanco consolidado diário");

                return StatusCode(200, result);
            }
            catch (Exception)
            {
                _logger.LogDebug("Foi encontrado um erro não esperado");

                UnknownErrorViewModel errorDto = new()
                {
                    Error = "Erro desconhecido"
                };

                return StatusCode(500, errorDto);
            }
        }
        /// <summary>
        /// Método para lançamento de entradas(Crédito/Débito)
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
                _logger.LogDebug("Iniciando entrada de lançamentos");

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

                _logger.LogDebug($"Foi encontrados erros de validação de dados de entrada { string.Join(",", validationErrors) }");

                ValidateErrorViewModel errorDto = new()
                {
                    Errors = validationErrors
                };

                return StatusCode(400, errorDto);
            }
            catch (Exception)
            {
                _logger.LogDebug("Foi encontrado um erro não esperado");

                UnknownErrorViewModel errorDto = new()
                {
                    Error = "Erro desconhecido"
                };

                return StatusCode(500, errorDto);
            }
        }
    }
}