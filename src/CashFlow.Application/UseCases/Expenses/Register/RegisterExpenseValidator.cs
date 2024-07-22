using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
    {
        public RegisterExpenseValidator() 
        {
            RuleFor(expense => expense.Title).NotEmpty().WithMessage("The tile is required");
            RuleFor(expense => expense.Amout).GreaterThan(0).WithMessage("The Amount must be grater than zero");
            RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Expenses cannot be in the future");
            RuleFor(expense =>  expense.PaymentType).IsInEnum().WithMessage("Payment Type is not valid");
        }

    }
}
