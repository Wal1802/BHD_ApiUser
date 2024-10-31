using BHD.Application.Repositories;
using BHD.Models.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BHD.Application.Validator
{
    public class UserValidator : AbstractValidator<User> 
    {
        private readonly IUserRepository _userRepository;

        private readonly string PasswordRegex;
        private readonly string EmailRegex;
        public UserValidator(
            IConfiguration config,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;

            PasswordRegex = config.GetSection("Validator:PasswordRegex").Value;
            EmailRegex = config.GetSection("Validator:EmailRegex").Value;
            RuleFor(user => user.Password).Matches(PasswordRegex)
                                          .WithMessage("Por favor, asegurate de que la contraseña cumpla con las condiciones minimas.")
                                          .WithName("Contraseña");

            RuleFor(user => user.Email).NotEmpty()
                                       .WithName("Correo Electrónico")
                                       .WithMessage("El correo electrónico es requerido.")
                                       .Matches(EmailRegex)
                                       .WithMessage("Por favor, asegurate de que el correo electrónico tenga el formato correcto.")
                                       .Must(BeUniqueEmail)
                                       .WithMessage("El correo ya está registrado");
            ;

        }

        private bool BeUniqueEmail(string email) => !_userRepository.EmailExists(email);
        

        public override ValidationResult Validate(ValidationContext<User> context)
        {
            var result = base.Validate(context);
            var formattedErrors = result.Errors
                .Select(error =>
                {
                    error.FormattedMessagePlaceholderValues.TryGetValue("PropertyName", out object propname);
                    if(propname is not string)
                        return new ValidationFailure(
                        error.PropertyName,
                        $"{error.PropertyName}: {error.ErrorMessage}",
                        error.AttemptedValue);
                    else
                        return new ValidationFailure(
                        (string)propname,
                        $"{(string)propname}: {error.ErrorMessage}",
                        error.AttemptedValue);
                })
                .ToList();

            return new ValidationResult(formattedErrors);
        }
    }
}

