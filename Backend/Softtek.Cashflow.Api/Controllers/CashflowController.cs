using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Softtek.Cashflow.Application.Entry.Command;
using Softtek.Cashflow.Application.Entry.Query;
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
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        /// <param name="validator"></param>
        public CashflowController(ILogger<CashflowController> logger, IMediator mediator, IValidator<CashflowCommand> validator)
        {
            _logger = logger;
            _mediator = mediator;
            _validator = validator;
        }
        /// <summary>
        /// Método para resgatar consolidações diárias para relatório de balanço
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "ConsolidatedDailyBalance")]
        [ProducesResponseType(typeof(ConsolidatedBalanceViewModel), 200)]
        public async Task<IActionResult> GetConsolidatedDailyBallance()
        {
            _logger.LogInformation("Iniciando consulta de dados para geração do relatório de balanco consolidado diário");

            var query = new ConsolidatedBalanceQuery();

            var consolidatedDailyBalance = await _mediator.Send(query);

            _logger.LogInformation("Iniciando consulta de dados para geração do relatório de balanco consolidado diário");

            return Ok(consolidatedDailyBalance);
        }
        /// <summary>
        /// Método para entrada de lançamentos para o fluxo de caixa
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(Name = "Entring")]
        public async Task<IActionResult> EntryAsync(CashflowCommand command)
        {
            _logger.LogInformation("Iniciando entrada de lançamentos. Valor: {0}", command.Value);

            var validationResult = await _validator.ValidateAsync(command);

            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors.SelectMany(error => error.ErrorMessage).ToList());

            await _mediator.Send(command);

            _logger.LogInformation("Valor: {0} adicionado com sucesso", command.Value);

            return Ok();
        }
    }
}