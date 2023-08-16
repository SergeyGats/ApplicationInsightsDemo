using ApplicationInsightsDemo.WebApi.Constants;
using ApplicationInsightsDemo.WebApi.Dtos.Authentication;
using FluentValidation;

namespace ApplicationInsightsDemo.WebApi.FluentValidation.Validators.Authentication
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(ValidationConstants.PasswordMinimumLength)
                .MaximumLength(ValidationConstants.PasswordMaximumLength);
        }
    }
}