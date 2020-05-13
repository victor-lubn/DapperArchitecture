using FluentValidation;
using RS.Domain.Models.Views;

namespace RS.Domain.Validators
{
    /// <summary>
    /// The UserValidator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{RS.Domain.Models.Views.UserModel}" />
    public class UserValidator : AbstractValidator<AppUserModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator"/> class.
        /// </summary>
        public UserValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}