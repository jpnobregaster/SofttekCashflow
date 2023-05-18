using FluentValidation;

using MediatR;

namespace Softtek.Cashflow.Application.Entry.Command
{
    public class CashflowCommand : IRequest
    {
        public decimal Value { get; set; }
    }

    public class EntryCommandValidator : AbstractValidator<CashflowCommand>
    {
        public EntryCommandValidator()
        {
            RuleFor(x => x.Value).NotEmpty().WithMessage("Campo Valor deve conter um valor monetário. ex: 10.00");
            RuleFor(x => x.Value).NotEqual(0).WithMessage("Campo Valor deve ser diferente de 0.");
        }
    }
}
