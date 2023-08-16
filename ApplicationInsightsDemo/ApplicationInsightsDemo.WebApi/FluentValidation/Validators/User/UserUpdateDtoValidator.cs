using ApplicationInsightsDemo.WebApi.Constants;
using ApplicationInsightsDemo.WebApi.Dtos.User;
using FluentValidation;

namespace ApplicationInsightsDemo.WebApi.FluentValidation.Validators.User
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.FirstName)
                .NotEmpty()
                .MaximumLength(ValidationConstants.FirstNameMaximumLength);

            RuleFor(u => u.LastName)
                .NotEmpty()
                .MaximumLength(ValidationConstants.LastNameMaximumLength);
        }
    }
}