using FluentValidation;
using HomeProjectAPI.Services.Model;

namespace HomeProjectAPI.Services.Validators
{
    public class UserCreationValidation : AbstractValidator<UserCreation>
    {
        public UserCreationValidation()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Lastname).NotEmpty();
        }
    }
}