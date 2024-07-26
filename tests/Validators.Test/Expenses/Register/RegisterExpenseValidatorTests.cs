using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requets;
using FluentAssertions;
namespace Validators.Test.Expenses.Register
{
    public class RegisterExpenseValidatorTests
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterExpenseValidator();

            var request = RequestRegisterExpenseJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();

        }

        [Fact]
        public void Error_Title_Empty()
        {
            var validator = new RegisterExpenseValidator();

            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Title = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.TITLE_IS_REQUIRED));
        }

        [Fact]
        public void Error_Date_Future()
        {
            var validator = new RegisterExpenseValidator();

            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_FOR_FUTURE));
        }


        [Fact]
        public void Error_Payment_Type_Invalid()
        {
            var validator = new RegisterExpenseValidator();

            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.PaymentType = (PaymentType)799;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        public void Error_Amount_Less_Zero(decimal amout)
        {
            var validator = new RegisterExpenseValidator();

            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Amount = amout;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.AMOUT_MUST_BE_GREATER_THAN_ZERO));
        }
    }

}
