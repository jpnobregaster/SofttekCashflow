using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Softtek.Cashflow.Application.Entry.Command;
using Softtek.Cashflow.Application.Entry.Query;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Softtek.Cashflow.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CashflowController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CashflowCommand> _validator;
        private readonly ILogger<CashflowController> _logger;

        public CashflowController(ILogger<CashflowController> logger, IMediator mediator, IValidator<CashflowCommand> validator)
        {
            _logger = logger;
            _mediator = mediator;
            _validator = validator;
        }

        [HttpGet(Name = "ConsolidatedDailyBalance")]
        public async Task<IActionResult> GetConsolidatedDailyBallance()
        {
            var query = new ConsolidatedBalanceQuery();

            var consolidatedDailyBalance = await _mediator.Send(query);

            return Ok(consolidatedDailyBalance);
        }

        [HttpPost(Name = "Entring")]
        public async Task<IActionResult> EntryAsync(CashflowCommand command)
        {
            _logger.LogInformation("Iniciando entrada de lançamentos");

            var validationResult = await _validator.ValidateAsync(command);

            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors.SelectMany(error => error.ErrorMessage).ToList());

            await _mediator.Send(command);

            return Ok("Entrada foi adicionada com sucesso");
        }
    }
}